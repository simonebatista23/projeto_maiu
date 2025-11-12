using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginUserAdmin.Services
{
    class SettingsService
    {
        private const string DefaultBaseUrl = "http://localhost:5000";
        private const string DefaultLoginAdmin = "/api/Users/login";

        public string BaseUrl { get; set; } = DefaultBaseUrl;
        public string LoginAdmin { get; set; } = DefaultLoginAdmin;
    }
}
