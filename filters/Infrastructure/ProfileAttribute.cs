using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filters.Infrastructure
{
    public class ProfileAttribute : ActionFilterAttribute
    {
        private Stopwatch timer;
        private double actionTime;

        public override async Task OnActionExecutionAsync(        ActionExecutingContext context,        ActionExecutionDelegate next)
        {
            timer = Stopwatch.StartNew();
            await next();
            await Task.Delay(1000);
            actionTime = timer.Elapsed.TotalMilliseconds;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context,ResultExecutionDelegate next)
        {
            await next();
            timer.Stop();
            string result = "<div>Action time: "
            + $"{actionTime} ms</div><div>Total time: "
            + $"{timer.Elapsed.TotalMilliseconds} ms</div>";
            byte[] bytes = Encoding.ASCII.GetBytes(result);
            await context.HttpContext.Response.Body.WriteAsync(bytes,
            0, bytes.Length);
        }
    }
}
