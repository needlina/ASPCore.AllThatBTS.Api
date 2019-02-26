using NPoco;
using System;

namespace ASPCore.AllThatBTS.Api.Entities
{
    [TableName("TB_USER")]
    public class UserT
    {
        [Column("USER_NO")]
        public string UserNo { get; set; }
        [Column("NICKNAME")]
        public string NickName { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("SECRET")]
        public string Password { get; set; }
        [Column("AUTH_TYPE")]
        public string AuthType { get; set; }
        [Column("CONFIRM_YN")]
        public string ConfirmYN { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDatetime { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDatetime { get; set; }
    }
}
