using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetKasa.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterProductRoutes();
        app.RegisterCartRoutes();
        app.RegisterPaymentRoutes();
    }
}
