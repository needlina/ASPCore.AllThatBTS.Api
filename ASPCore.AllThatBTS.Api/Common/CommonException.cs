using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Common
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, string description) : base(message, description, (int)HttpStatusCode.NotFound)
        {
        }
    }

    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, string description) : base(message, description, (int)HttpStatusCode.Unauthorized)
        {
        }
    }

    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, string description) : base(message, description, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
