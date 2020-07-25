using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs {
    public class RegisterConfirmEmail {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
