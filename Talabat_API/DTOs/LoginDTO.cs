﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
