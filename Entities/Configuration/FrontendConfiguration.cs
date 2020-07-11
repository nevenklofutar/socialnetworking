using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class FrontendConfiguration
    {
        public string BaseUrl { get; set; }
        public string AuthenticationControllerName { get; set; }
        public string ForgotPasswordActionName { get; set; }
    }
}
