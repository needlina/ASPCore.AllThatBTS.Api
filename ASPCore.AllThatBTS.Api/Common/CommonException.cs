using ASPCore.AllThatBTS.Api.Enum;

namespace ASPCore.AllThatBTS.Api.Common
{
    /// <summary>
    /// 파라미터 오류 (유효성) : 901
    /// </summary>
    public class ValidationException : BaseException
    {
        public ValidationException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.ValidationError)
        {
            Code = base.Code + ControllerNo;
        }
    }

    /// <summary>
    /// 데이터 불일치 : 902
    /// </summary>
    public class IncorrectDataException : BaseException
    {
        public IncorrectDataException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.IncorrectData)
        {
            Code = base.Code + ControllerNo;
        }
    }

    /// <summary>
    /// 데이터 없을 경우 : 904
    /// </summary>
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.NotFound)
        {
            Code = base.Code + ControllerNo;
        }
    }

    /// <summary>
    /// 권한 오류 : 905
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.Unauthorized)
        {
            Code = base.Code + ControllerNo;
        }
    }

    /// <summary>
    /// 중복 오류 : 906
    /// </summary>
    public class DuplicatedException : BaseException
    {
        public DuplicatedException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.Duplicated)
        {
            Code = base.Code + ControllerNo;
        }
    }

    /// <summary>
    /// 기타 오류 : 907
    /// </summary>
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, string description, int ControllerNo) : base(message, description, StatusCodes.BadRequest)
        {
           Code = base.Code + ControllerNo;
        }
    }

}
