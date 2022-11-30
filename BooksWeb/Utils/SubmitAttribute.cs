using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksWeb.Utils
{
    public class SubmitAttribute: ActionFilterAttribute
    {
        public string ViewName { get; set; } = null;
        public string RedirectAction { get; set; } = "Index";

        public string RedirectUrl { get; set; } = null;

        public string ModelName { get; set; } = "model";

        //before you action executes
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 400;
                var controller = context.Controller as Controller;
                context.Result = new ViewResult()
                {
                    ViewName = ViewName ?? context.RouteData.Values["Action"]?.ToString(),
                    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(controller.ViewData)
                    {
                        Model = context.ActionArguments[ModelName]
                    }
                };


            }

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.ModelState.IsValid)
            {
                if (RedirectUrl != null)
                    context.Result = new RedirectResult(RedirectUrl);
                else
                    context.Result = new RedirectToActionResult(RedirectAction, null, null);
            }
            
        }
    }
}
