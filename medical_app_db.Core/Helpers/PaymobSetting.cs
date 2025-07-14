using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Helpers
{
    public class PaymobSetting
    {
        public string? ApiKey { get; set; }
        public string? IntegrationId { get; set; }
        public string? IFrameId { get; set; }
        public string? HmacSecret { get; set; }

    }
}
