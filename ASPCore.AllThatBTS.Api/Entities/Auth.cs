using NPoco;
using System;

namespace ASPCore.AllThatBTS.Api.Entities
{
    [TableName("TB_AUTH")]
    public class TokenT
    {
        [Column("USER_NO")]
        public string UserNo { get; set; }
        [Column("ACCESS_TOKEN")]
        public string AccessToken { get; set; }
        [Column("REFRESH_TOKEN")]
        public string RefreshToken { get; set; }
        [Column("SCOPE")]
        public string Scope { get; set; }
        [Column("ACCESS_EXPIRE_DT")]
        public DateTime AccessTokenExpireDate { get; set; }
        [Column("REFRESH_EXPIRE_DT")]
        public DateTime RefreshTokenExpireDate { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDatetime { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDatetime { get; set; }

    }
}
