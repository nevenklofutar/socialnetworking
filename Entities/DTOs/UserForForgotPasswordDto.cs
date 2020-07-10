using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public class UserForForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
