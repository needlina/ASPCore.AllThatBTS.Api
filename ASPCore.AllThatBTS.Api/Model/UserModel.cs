using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Model
{
    public class MakeUserM
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthType { get; set; }
        public string ConfirmYN { get; set; }
    }

    public class ReadUserM
    {
        public string UserNo { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string AuthType { get; set; }
        public string ConfirmYN { get; set; }
        public TokenM Token { get; set; }
        public DateTime CreateDatetime { get; set; }
        public DateTime UpdateDatetime { get; set; }
    }

    public class ModifyUserM
    {
        [Required]
        public string UserNo { get; set; }
        public string NickName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
