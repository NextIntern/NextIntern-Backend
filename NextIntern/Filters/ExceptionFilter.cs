using Microsoft.AspNetCore.Mvc.Filters;

namespace NextIntern.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
