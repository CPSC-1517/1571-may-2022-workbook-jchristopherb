using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WestWindSystem.DAL;
using WestWindSystem.BLL;

#endregion

namespace WestWindSystem
{
    public static class BackEndExtensions
    {
        // add a parameter IServicesCollection - which is within the system they have special class thats gonna hold every class
        public static void WWBackendDepedencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //  we will register all the services that will be used by the system (context setup)
            //  and will be provided by the system (BLL Services)
            //  options contents the connection string information
            services.AddDbContext<WestWindContext>(options);

            //  we will register all the services class that will be used by the system (context setup) - BLL classes
            //  we use the AddTransient<T> method where T is your BLL class name
            //  the AddTransient is called a factory and the => means "do the following.."

            //   for EACH BLL class copy the codes below and change the service name
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
            {
                //  get the DbConnection class that was registered above in AddDbContext
                var context = serviceProvider.GetService<WestWindContext>();

                //  create an instance of the service class (BuildVersionServices) supplying the context reference to the service class

                return new BuildVersionServices(context);
            });
            
            services.AddTransient<RegionServices>((serviceProvider) =>
            {
                //  get the DbConnection class that was registered above in AddDbContext
                var context = serviceProvider.GetService<WestWindContext>();

                //  create an instance of the service class (BuildVersionServices) supplying the context reference to the service class

                return new RegionServices(context);
            });

            services.AddTransient<TerritoryServices>((serviceProvider) =>
            {
                //  get the DbConnection class that was registered above in AddDbContext
                var context = serviceProvider.GetService<WestWindContext>();

                //  create an instance of the service class (BuildVersionServices) supplying the context reference to the service class

                return new TerritoryServices(context);
            });

        }
    }
}
