namespace Assignment.CustomMiddleware
{
    /// <summary>
    /// This is to implement the custom middleware for Content Security Policy (CSP)
    /// </summary>
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentSecurityPolicyMiddleware(RequestDelegate next) //constructor
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Add the CSP header to the response
            //if required we can add more options here.
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self';");

            await _next(context);
        }
    }
}
