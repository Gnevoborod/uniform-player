using Microsoft.AspNetCore.Http;

namespace uniform_player.Infrastructure.Middleware
{
    public class RequestIdentityMiddleware
    {
        private readonly RequestDelegate next;
        public RequestIdentityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public void Invoke(HttpContext context) 
        {
            string requestId = context.TraceIdentifier;
            context.Response.Headers.Add("RequestId", requestId);
            next.Invoke(context);
        }
    }
}
