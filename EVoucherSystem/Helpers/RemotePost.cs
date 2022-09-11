namespace EVoucherSystem.Helpers
{
    public class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();
        public string Url = "";
        public string Method = "post";
        public string FormName = "form1";

        private readonly HttpContext httpContext; 

        public RemotePost(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }
        public void Post()
        { 
            httpContext.Response.Clear();
            httpContext.Response.WriteAsync("").Wait();
            httpContext.Response.WriteAsync(string.Format("", FormName));
            httpContext.Response.WriteAsync(string.Format("", FormName, Method, Url));
            for (int i = 0; i < Inputs.Keys.Count; i++)
            {
                httpContext.Response.WriteAsync(string.Format("", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            }
            httpContext.Response.WriteAsync("");
            httpContext.Response.WriteAsync("");
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
        }
    }
}
