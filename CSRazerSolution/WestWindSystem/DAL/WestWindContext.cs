using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.DAL
{
    //let the access type for this class as internal
    //  "internal" means that this class can only be accessed by code in the same assembly
    // it adds a layer of security to your program.

    // DbContext is a class that is used to interact with the database.
    //  it is a wrapper around the database connection.
    // we inherit the required classes from the DbContext class.

    // Add NuGet Package: Microsoft.EnittyFrameworkCore.SqlServer

    internal class WestWindContext : DbContext
    {
        // the constructor will pass the context connection to the DbContext parent for use in setting up the database connection
        public WestWindContext(DbContextOptions<WestWindContext> options) : base(options)
        {
        }

        //setup properties in this class that can be referenced by others code within your class library
        //the properties represent a collection of instances of the entity retrieved from or sent to the database
        //  one property per entity in Entities

        public DbSet<BuildVersion> BuildVersions { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Territory> Territories { get; set; }
    }


}
