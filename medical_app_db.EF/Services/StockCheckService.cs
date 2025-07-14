using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.EF.Services
{
    public class StockCheckService : BackgroundService
    {
        private readonly IServiceProvider _services;
        public StockCheckService(IServiceProvider services)
        {
            _services = services;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MedicalDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var accountBranches = await context.AccountBranches
                    .Include(m => m.Account)
                    .ToListAsync(stoppingToken);
                var lowStock = await context.BranchProducts.Include(pb => pb.SystemProduct).Where(m => m.stock < 5).GroupBy(m => m.BranchId)
                    .ToListAsync();
                if (lowStock.Any())
                {
                    var branchIds = lowStock.Select(m => m.Key).Distinct().ToList();
                    foreach (var branchId in branchIds)
                    {
                        var branchName = await context.Branches
                            .Where(m => m.Id == branchId)
                            .Select(m => m.EN_BranchName)
                            .FirstOrDefaultAsync(stoppingToken);
                        string body = GenerateLowStockEmailBody(branchName, lowStock.FirstOrDefault(g => g.Key == branchId)?.ToList() ?? []);
                        var accountEmails = accountBranches.Where(m => m.BranchId == branchId).Select(m => m.Account.Email).Distinct().ToList();
                        foreach (var item in accountEmails)
                        {
                            try
                            {
                                await emailService.SendEmailAsync(item, "Low Stock Alert", body, true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed to send email to {item}: {ex.Message}");
                            }
                        }
                    }
                }
                Console.WriteLine("Running stock check...");
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // run once a day
            }
        }
        private string GenerateLowStockEmailBody(string? brancName , List<BranchProduct> products)
        {
            var sb = new StringBuilder();
            sb.AppendLine("⚠️ The following medicines are low on stock:<br><ul>");
            sb.AppendLine($"<h4>Branch: {brancName}</h4><ul>");
            foreach (var item in products)
            {
                sb.AppendLine($"<li>{item.SystemProduct.EN_Name} - Quantity: {item.stock}</li>");
            }
            
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
