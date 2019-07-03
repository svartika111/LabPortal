using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlinePortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Studentside",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Retreivestudents",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "RetreiveData", action = "Students", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "FileUploading",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "FileUpload", action = "Upload", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "LoginStudents",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Login", action = "LoginStudent", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "AfterLoginStudents",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Login", action = "AfterLogin", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "ProfessorMaintenance",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "StudentRecord", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "AdminMaintenance",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "AdminRecord", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "ProfessorLogin",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "ProfLogin", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
          name: "AdminLogin",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
      );
            routes.MapRoute(
          name: "AfterAdminLogin",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "AfterAdminLogin", action = "Index", id = UrlParameter.Optional }
      );

        }
    }
}
