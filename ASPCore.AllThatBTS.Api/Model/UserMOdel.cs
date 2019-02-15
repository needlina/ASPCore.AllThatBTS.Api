using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Model
{
    public class MakeUserM
    {
        public string NickName { get; set; }
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
        public DateTime CreateDatetime { get; set; }
        public DateTime UpdateDatetime { get; set; }
    }

    public class ModifyUserM
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
