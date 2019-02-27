using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Developer_forum.Controllers.api
{
    public class Handling404Controller : ApiController
    {


      
        [HttpDelete, HttpGet, HttpHead, HttpOptions, HttpPost, HttpPatch, HttpPut]
        public string error404()
        {
            return "hi";
            // HttpRequestMessage req;//this contains route of rquest and data also
        }


    }





    /* intially when we provide url inside browser
     so the request will goes to below method 
     and try to find called controller 
     if controller is not available then we got 404 status code 
     using that code we redirect it to another custom controller
     
         */
    //for controller not found
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector

    {
        public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)

            : base(configuration)

        {
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;
            try
            { // first uri form browser gets here and tries to find requested controller
                //if fonud then call it , otherwise we got exception
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            { //required controlller doed not exists then we change requested route to our error controller and action 
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                    throw;
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "Handling404";
                routeValues["action"] = "error404";
              //  routeValues["id"] = "my id";// id is optional
                decriptor = base.SelectController(request);
            }
            return decriptor;
        }
    }


    //for action not found
    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "error404";
                IHttpController httpController = new Handling404Controller();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Handling404", httpController.GetType());
                decriptor = base.SelectAction(controllerContext);
            }
            return decriptor;
        }
    }
}
