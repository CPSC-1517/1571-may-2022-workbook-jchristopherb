﻿using System;
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
    public class SupplierServices
    {

        #region setup the context connection variable and class constructor
        //variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal SupplierServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }

        #endregion

        #region Service: Queries
        
        //looking up a record on a table via the primary key value
        public List<Supplier> Supplier_List()
        {
            return _context.Suppliers
                            .OrderBy(x => x.CompanyName)
                            .ToList();

        }
        #endregion

    }
}
