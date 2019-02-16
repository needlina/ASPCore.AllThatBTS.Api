using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ASPCore.AllThatBTS.Api.Filter
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Console.WriteLine("Bye bye");
        }
    }
}
