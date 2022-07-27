using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking; //for EntityEntry<T>
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


        public List<Product> Product_GetByCategory(int categoryid)
        {
            return _context.Products
                            .Where(x => x.CategoryID == categoryid)
                            .ToList();

        }

        public Product Product_GetById(int productid)
        {
            return _context.Products
                            .Where(x => x.ProductID == productid)  //filter
                            .FirstOrDefault(); //if found return the first instance else null

        }
        #endregion

        #region Add,Update,Delete
        //Adding a record to your database MAY require you to
        //  verifiy the data does not already exist on the database
        //This is referred to as Business Logic (Business Rules)
        //One way this can be done is using a Linq query and a given set
        //  of veridication fields
        //Example: for this product insertion we will verify that
        //          there is no product record on the database with
        //          the same product name and quantity per unit from
        //          the same supplier. If so, throw an Exception

        //other key points
        //  you MUST know whether the table has an identity pkey or not
        //  if the table pkey is NOT an identity then you MUST ensure
        //      that the incoming instance of the entity HAS a pkey value
        //  if the table pkey is an identity then the database WILL generate
        //      the pkey value and make it assessable to your AFTER the
        //      data has been commited.

        //for our Add, we will return the new identity pkey to the webapp
        //this is optional
        public int Product_AddProduct(Product item)
        {
            //is item present
            if (item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            //this is an OPTIONAL sample of validation of incoming data
            //  called a Business Rule
            Product exists = _context.Products
                            .Where(x => x.ProductName.Equals(item.ProductName)
                                     && x.QuantityPerUnit.Equals(item.QuantityPerUnit)
                                     && x.SupplierID == item.SupplierID)
                            .FirstOrDefault();

            if (exists != null)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit}" +
                    $" from the selected supplier is already on file.");
            }

            //at this point: assume good data

            //two steps in adding your add: Staging and Commit
            //stage the data in local memory to be submitted to the database for storage
            // a) what is the DbSet
            // b) the action
            // c) indicate the data involved

            //IMPORTANT: the data is in LOCAL MEMORY; it has NOT!!!!!!! yet been sent to
            //              the database
            //           THEREFORE: at this time, there is NO!!!!!!! primary key value
            //                      (except for the system default (numerics => 0))
            _context.Products.Add(item);

            //commit the LOCAL data to the database

            //IF there are validation annotations on your Entity defintion
            //  they will be executed during the SaveChanges()
            //SO, if you violate a validation annotation, then your data DOES NOT
            //  go to the database, the system throws an Exception to the violated
            //  annotation.
            _context.SaveChanges();

            //AFTER the commit, your new identity pkey value will NOW be available to you
            return item.ProductID;
        }

        public int Product_UpdateProduct(Product item)
        {
            //for an update, you MUST have the pkey value on your instance
            //if not, updating will not work.

            //check that the incoming item has an instance
            if (item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            //you MAY wish to check if the pkey value exists on the database table
            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);
            //if(!_context.Products.Any(x => x.ProductID == item.ProductID))
            if(!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit}" +
                    $" from the selected supplier is not on file.");
            }

            //Can include any additional Business Rule validation testing

            //stage the update
            EntityEntry<Product> updating = _context.Entry(item);
            //flag the entity for its action
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //commit
            //during the commit, the value returned from SaveChanges will be the
            //  number of records altered
            return _context.SaveChanges();

        }

        public int Product_DeleteProduct(Product item)
        {
            //for an delete, you MUST have the pkey value on your instance
            //if not, updating will not work.

            //check that the incoming item has an instance
            if (item == null)
            {
                throw new ArgumentNullException("Product data is missing");
            }

            //you MAY wish to check if the pkey value exists on the database table
            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);
            //if(!_context.Products.Any(x => x.ProductID == item.ProductID))
            if (!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit}" +
                    $" from the selected supplier is not on file.");
            }

            //Can include any additional Business Rule validation testing

            //there are two types of deleting: physical and logically
            //whether you have a physical or logically delete is determind WHEN
            //  the database was designed.

            //physical delete
            //you physical remove a record from the database
            //REMEMBER: you CANNOT delete a parent record IF there are child records
            //              (pkey => fkey)
            //          if there are child records, sql will throw and error


            //stage the delete
            //EntityEntry<Product> deleting = _context.Entry(item);
            //flag the entity for its action
            //deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            //logical delete
            //you logically flag a record on the database by setting a specific field
            //we have a discontinued flag
            //DO NOT rely on the user to set the flag
            //DO IT FOR the user within your method
            item.Discontinued=true;
            
            //for a logical delete your are ACTUALLY doing an update
            //stage the update
            EntityEntry<Product> updating = _context.Entry(item);
            //flag the entity for its action
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //commit
            //during the commit, the value returned from SaveChanges will be the
            //  number of records altered
            return _context.SaveChanges();

        }

        #endregion
    }
}
