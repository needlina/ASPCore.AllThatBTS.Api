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
    public class StatusCodes
    {
        // 유효성 체크 오류
        public const int ValidationError = 901;
        // 올바르지 않은 데이터
        public const int IncorrectData = 902;
        // 데이터 없음
        public const int NotFound = 904;
        // 중복 데이터
        public const int Duplicated = 906;
        // 권한 오류
        public const int Unauthorized = 905;
        // 기타 오류
        public const int BadRequest = 907;
    }

    /// <summary>
    /// 컨트롤러 아이디 Error 구분 용도
    /// </summary>
    public class LayerID
    {
        public const int AuthController = 11000;
        public const int AuthService = 12000;
        public const int AuthRepository = 13000;

        public const int BoardController = 21000;
        public const int BoardService = 22000;
        public const int BoardRepository = 23000;

        public const int UserController = 31000;
        public const int UserService = 32000;
        public const int UserRepository = 33000;

        public const int CrawlerController = 41000;
        public const int CrawlerService = 42000;
        public const int CrawlerRepository = 43000;
    }
}
