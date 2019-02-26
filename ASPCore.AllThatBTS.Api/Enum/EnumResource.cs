namespace ASPCore.AllThatBTS.Api.Enum
{
    /// <summary>
    /// 검색 조건 Type
    /// </summary>
    public class SearchType
    {
        public const string All = "";
        public const string Subject = "SUBJECT";
        public const string Contents = "CONTENTS";
    }

    /// <summary>
    /// Error Code List (정리중)
    /// </summary>
    public class HttpStatusCodes
    {
        public const int NotFound = 404;
        public const int UnAuthorized = 401;
        public const int TokenError = 402;
        public const int BadRequest = 403;
    }
}
