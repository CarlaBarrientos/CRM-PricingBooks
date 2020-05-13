using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRM_PricingBooks.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //string var = HttpContext.Request.Headers["authorization"].ToString();
            // var = "mateo.lopez : Pass123";
            Console.WriteLine("This is the Auth Middleware");
            Console.WriteLine("Connecting with FB API (oAuth)"); // SSO, oAuth2.0/3.0
            // Consult to busines logic
            Console.WriteLine("waiting for FB credentials");
            // Consutl to DB if user exists or not (user/pass)
            Console.WriteLine("optional: store JWT token to our db (through businness logic)");
            // HTTP Header / Authorization: Basic base64(user:pass)
            Console.WriteLine("GRANTING ACCESS TO THE SYSTEM");
            // GRANT Acess
            //if(loginbusinesslogic.validateuser(var)){}
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
