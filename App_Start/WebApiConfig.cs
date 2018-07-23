﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Solution.Services.WebServices.Ext
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{codigo}",
                defaults: new { codigo = RouteParameter.Optional }
            );
        }
    }
}
