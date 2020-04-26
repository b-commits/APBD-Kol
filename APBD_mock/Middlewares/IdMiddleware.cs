using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_mock.Middlewares
{
    public class IdMiddleware
    {
        private readonly RequestDelegate _next;
        public IdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            context.Response.Headers.Add("Student: ", "s19677");
            await _next.Invoke(context);
        }



    }
}
