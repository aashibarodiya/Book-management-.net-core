using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksWeb.Utils
{
    public class NullModelIs404Attribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var vr = context.Result as ViewResult;
            if (vr !=null )
            {
                if(vr.Model==null)
                {
                    context.HttpContext.Response.StatusCode = 404;
                    vr.ViewName = "NotFound"; //Got to this View
                }

            }
        }
    }
}
