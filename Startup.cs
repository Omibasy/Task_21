using Microsoft.AspNetCore.Mvc;

namespace Task_20
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(TuningMvc);
        }

        public void Configure(IApplicationBuilder app) 
        { 
             app.UseStaticFiles();
             app.UseMvc(GetRoute);
        
        }

        private void GetRoute(IRouteBuilder route)
        {
            route.MapRoute("home", "{controller=home}/{action=Index}");
        }

        private void TuningMvc(MvcOptions switches) 
        {
            switches.EnableEndpointRouting = false;
        }
    }
}
