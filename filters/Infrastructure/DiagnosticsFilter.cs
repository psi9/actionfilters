using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filters.Infrastructure
{
    public class DiagnosticsFilter : IAsyncResultFilter
    {
        private IFilterDiagnostics diagnostics;
        public DiagnosticsFilter(IFilterDiagnostics diags)
        {
            diagnostics = diags;
        }
        public async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
        {
            await next();
            foreach (string message in diagnostics?.Messages)
            {
                byte[] bytes = Encoding.ASCII.GetBytes($"<div>{message}</div>");
                await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
