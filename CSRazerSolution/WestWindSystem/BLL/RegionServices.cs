#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion


namespace WestWindSystem.BLL
{
    public class RegionServices
    {
        #region set up the context connection variable and class constructor
        // variable to hold an instance of context class
        private readonly WestWindContext _context;

        // constructor to create an instance of the registered context class
        internal RegionServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }

        #endregion

        #region Service: Queries
        //  query: look for something/data
        //  obtain all the data within the sql entity to display on the webpage
        //  this mean a collection (List<T>) will be generated and returned by the following query (method)
        //  the query will be called outside the class library project therefore the method class level MUST be public
        
        public List<Region> Region_List()
        {
            /*//  var datatype is resolved during execution
            //  return the retrieved database data to whatever called this method
            //  NOTE: the data collection MUST be returned as a List<T>
            //  .ToList() will convert the collection into a List<T>
            
            var info = _context.Regions;
            return info.ToList();*/

            //EXAMPLE CODING #2
            /*
             *  The colection datatype is "strongly" typed at creation time
             *  Strongly typed data is resolved at compile time
             *  The datatype from a query will be either IEnumerable<T> or IQueryable<T>
             *
             *
                IEnumerable<Region> info = _context.Regions;
                return info.ToList();
             *
             */

            //EXAMPLE CODING #3
            /*
             * 
             * return the data converted all within one statement
             *   
             */
            return _context.Regions.ToList();
        }

        //  Looking up a record on a table via the primary key value
        
        public Region Region_GetByID(int id)
        {
            //  EXAMPLE #1 using the extension method .Find(pkeyvalue)
            //  return _context.Regions.Find(id);

            //EXAMPLE CODING #2 USES THE LINQ METHOD SYNTAX
            //  LINQ: Language INtegrated Query (More in DMIT2018)
            //  what we are using is Linq to Entities
            //  by default expects 0, 1 or more records to be returned
            //  in our case, since we are looking up by the pkey
            //  we expect only 1 record (or none) to be returned
            //  add an additional extension which specifies the number of expected records to be returned: .FirstOrDefault()
            /**
             * FIRST the first record found that matches the filter (where)
             *     OR
             * Default the default for an object (null) if no match found.
             */
            return _context.Regions
                .Where(info => info.RegionID == id)
                .FirstOrDefault();
        }
        #endregion

    }
}
