using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LifeCounter.Monitor.Models
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine razorViewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor contextAccessor;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = serviceProvider
            };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary =
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}