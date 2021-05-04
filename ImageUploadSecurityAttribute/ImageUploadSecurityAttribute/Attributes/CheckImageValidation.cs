using ImageUploadSecurityAttribute.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ImageUploadSecurityAttribute.Attributes
{
    public class CheckImageValidation: Attribute, IAsyncActionFilter
    {
        public string FileParam { get; set; }

        public bool AllowMultiple => throw new NotImplementedException();

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var exec = new ImageFilterAttributeExecuter();
            exec.Execute(context.ActionArguments.ToList(), FileParam);
            var result = await next();
        }
    }
}
