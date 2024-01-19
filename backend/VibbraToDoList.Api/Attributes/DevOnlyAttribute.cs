using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VibbraToDoList.Api.Attributes
{
    public class DevOnlyAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new DevOnlyAttributeImp(serviceProvider.GetRequiredService<IWebHostEnvironment>());
        }

        private class DevOnlyAttributeImp : Attribute, IAuthorizationFilter
        {
            private readonly IWebHostEnvironment _environment;

            public DevOnlyAttributeImp(IWebHostEnvironment environment)
            {
                _environment = environment;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (_environment.IsDevelopment()) return;
                context.Result = new NotFoundResult();
            }
        }

    }
}
