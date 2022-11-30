namespace BooksWeb.Utils
{
    public static  class MiddlewareExtensions
    {
        public static WebApplication UseExceptionHandler<T>(this WebApplication app,int statusCode, 
            bool showExceptionDetails=true, Func<T,object> responseBuilder=null) where T:Exception
        {

            app.Use(async (context, next) =>
            {

                try
                {
                    await next(context);
                }
                catch(Exception ex)
                {
                    if(ex is T)
                    {
                        context.Response.StatusCode = statusCode;
                        if(responseBuilder!=null)
                        {
                            var response = responseBuilder((T)ex);

                            await context.Response.WriteAsJsonAsync(response);

                        }
                        else if(showExceptionDetails)
                        {
                            await context.Response.WriteAsJsonAsync(new {Status=statusCode,Message=ex.Message});
                        }
                    }
                }

            });


            return app;
        }
    }
}
