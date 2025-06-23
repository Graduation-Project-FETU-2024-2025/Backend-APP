using AutoMapper;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models.Order_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(a => a.ClinicName , o => o.MapFrom(a => a.Clinic.Name))
                .ForMember(a => a.UserImage , o => o.MapFrom(a => a.User.Picture));

            CreateMap<Prescription, PrescriptionDTO>()
                .ForMember(p => p.PrescriptionProductDTOs, o => o.MapFrom(s => s.PrescriptionProducts));
            CreateMap<PrescriptionProduct, PrescriptionProductDTO>();

            CreateMap<Clinic, ClinicDTO>()
                .ForMember(d => d.AppointmentDates, o => o.MapFrom(s => s.AppointmentDates))
                .ForMember(d => d.ClinicPhones, o  => o.MapFrom(s => s.ClinicPhones));
            CreateMap<AppointmentDates, AppointmentDateDTO>();
            CreateMap<ClinicPhone, ClinicPhonesDTO>();

            CreateMap<WorkingPeriodInClinic, WorkingPeriodInClinicDTO>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Ar_BranchName, o => o.MapFrom(s => s.Branch.AR_BranchName))
                .ForMember(d => d.En_BranchName, o => o.MapFrom(s => s.Branch.EN_BranchName));

            CreateMap<OrderItem, OrderItemToReturnDTO>();
        }
    }
}
