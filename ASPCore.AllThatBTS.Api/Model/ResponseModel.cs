using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Model
{
    public class Response<T> : CommonResponse
    {
        public T Result { get; set; }
    }

    public class Response : CommonResponse
    {
    }

    public class ListResponse<T> : CommonResponse
    {
        public List<T> ListResult { get; set; }
        public long CurrentPage { get; set; } = 0;
        public long TotalPages { get; set; } = 0;
        public long TotalItems { get; set; } = 0;
        public long ItemsPerPage { get; set; } = 0;
    }

    public class CommonResponse
    {
        public string Message { get; set; }
        public string ErrMsg { get; set; }
        public string Status { get; set; }
    }

}
