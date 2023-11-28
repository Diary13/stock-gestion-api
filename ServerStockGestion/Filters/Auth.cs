using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ServerStockGestion.Filters
{
    public class Auth : Attribute, IActionFilter
    {
        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var headers=HttpContext.Current.Request.Headers;
            var auth=headers["Authorization"];
            if (auth == null || auth!="1")
                throw new UnauthorizedAccessException("Vous n'êtes pas autorisé à accéder à cette ressource!");
            return continuation();
        }
    }
}