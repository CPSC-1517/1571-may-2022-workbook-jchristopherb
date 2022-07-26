using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking; //  for EntityEntrty>
using WestWindSystem.Entities;
#endregion


namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup the context connection variable and class constructor
        //variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal ProductServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }

        #endregion

        #region Service: Queries
        
        //looking up a record on a table via the primary key value
        public List<Product> Product_GetByCategory(int categoryid)
        {
            return _context.Products
                            .Where(x => x.CategoryID == categoryid)
                            .ToList();

        }
        #endregion

        #region Add, Update, Delete
        //  Adding a record to your database MAY require you to verify the
        //      data does not already exist on the datatbase
        //  This is referred to as Business Logic (Business Rules)
        //  Only way this can be done is using a Linq query and a given set of verification fields
        //  Ecample: for this product insertion we will verify that there is no product record on the database with the same product name and quantity per unit from the same supplier. If so, throw an Exception

        //  other key points
        //  you MUST know whether the table has an identity pkey or not
        //  if the table pkey is NOT an identity then you MUST ensure that the incoming instance of the entity HAS a pkey value
        //  if the table pkey is an identity then the database WILL generate the pkey value and make it assessable to your AFTER the data has been commited.

        //  for our Add, we will return the new idnetity pkey to the webapp
        //  this is optional

        public int Product_AddProduct(Product Item)
        {
            // is item present
            if (Item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }
            
            Product exists = _context.Products
                            .Where(x => x.ProductName == Item.ProductName && x.QuantityPerUnit == Item.QuantityPerUnit && x.SupplierID == Item.SupplierID)
                            .FirstOrDefault();
            
            if (exists != null)
            {
                throw new Exception($"{Item.ProductName} with a size of {Item.QuantityPerUnit} already exists");
            }

            //at this point: assume good data
            
            //  two steps in adding your ADD: Staging and Commit
            //  stage the data in local memory to be submitted to the databse for storage
            //  a) whta is the DbSet
            //  b) the action
            //  c) indicate the data involved

            //  IMPORTANT: the data is in LOCAL MEMORY; it has NOT!! yet been sent to the database
            //  THEREFORE: at this time, there is NOOO!! primary key value (except for the system default (numerics => 0)
            _context.Products.Add(Item);
            
            //  commit the LOCAL data to the database
            _context.SaveChanges();
            
            //  return the new identity pkey value to the webapp
            return Item.ProductID;
        }

        public int Product_UpdateProduct(Product Item)
        {
            // for an update, you MUST have the pkey value on your instance
            // if not, updating will not work

            // check that the incoming item has an instance
            
            if (Item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            // you MAY wish to check if the pkey value exists on the database table
            bool exists = _context.Products.Any(x => x.ProductID == Item.ProductID);
            //  if (!_context.Products.Any(x => x.ProductID == Item.ProductID))
            if (!exists)
            {
                throw new Exception($"{Item.ProductName} with a size of {Item.QuantityPerUnit} not found");
            }

            // can include any additional Business Rule Validation testing

            // stage the update
            EntityEntry<Product> entry = _context.Entry(Item);
            //  flag the entry for its action
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //  commit
            //  during the commit, the database will be updated with the new data
            return _context.SaveChanges();
        }
        #endregion


    }
}
