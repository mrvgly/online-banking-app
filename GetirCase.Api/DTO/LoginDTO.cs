using System;
using System.ComponentModel.DataAnnotations;

namespace GetirCase.Api.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
