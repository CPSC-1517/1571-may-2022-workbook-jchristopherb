using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;       //this is where the services were coded
using WestWindSystem.Entities;  //this is where the entity definition is coded
using WebApp.Helpers;
#endregion


namespace WebApp.Pages.Samples
{
    public class CRUDProductModel : PageModel
    {
        #region Private service fields & class constructor
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;


        public CRUDProductModel(CategoryServices categoryservices,
                                        ProductServices productservices,
                                        SupplierServices supplierServices)
        {
            _categoryServices = categoryservices;
            _productServices = productservices;
            _supplierServices=supplierServices;
        }
        #endregion

        [TempData]
        public string Feedback { get; set; }
        public bool HasFeedback { get { return !string.IsNullOrWhiteSpace(Feedback); } }
        [TempData]
        public string ErrorMsg { get; set; }
        public bool HasError { get { return !string.IsNullOrWhiteSpace(ErrorMsg); } }

        [BindProperty(SupportsGet = true)]
        public int? productid { get; set; }

        [BindProperty]
        public Product ProductInfo { get; set; }

        public List<Supplier> SupplierList { get; set; }

        public List<Category> CategoryList { get; set; }

        public void OnGet()
        {
            PopulateSupportLists();
            //the OnGet executes the first time the page is generated
            //the OnGet executes in reponse to a requested issued when using
            //      RedirectToPage()
            //test to see if the routing parameter has 
            //  a) a value
            //  b) the value is valid ( > 0), pkey value

            //if a product was select on the Query page, then it's pkey value
            //  will have been passed to this CRUD page
            //if a pkey value exists then look up the current data off the 
            //  database for that pkey value

            //.Value is used because the productid is a nullable integer

            if(productid.HasValue && productid.Value > 0)
            {
                ProductInfo = _productServices.Product_GetById(productid.Value);
            }
        }

        public void PopulateSupportLists()
        {
            SupplierList = _supplierServices.Supplier_List();
            CategoryList = _categoryServices.Category_List();
        }


        public IActionResult OnPostClear()
        {
            Feedback = "User clear";
            ErrorMsg = "";
            productid = (int?)null;
            ModelState.Clear();
            return RedirectToPage(new { productid = productid });
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Samples/CategoryProducts");
        }

        public IActionResult OnPostNew()
        {
            try
            {
                //  REMEMBER: [BindProperty] is a two-way movement of data
                //  it will fill
                //  call the appropriate service method
                int newproductid = _productServices.Product_AddProduct(ProductInfo);

                //  always want to give feedback to your user
                Feedback = $"Product {ProductInfo.ProductName} added";

                return RedirectToPage(new { productid = newproductid });

            }
            catch (ArgumentNullException ex)
            {
                //  use the drill down to find the REAL ERROR
                ErrorMsg = GetInnerException(ex).Message;

                //  reload ANY list that are being used on your from
                //  
                PopulateSupportLists();

                //  stay on the "same" page
                //  the idea is not to "leave" the current request page (data)
                //  this is required because the method is using IActionResult
                //  IActionResult expects a return action RedirectToPage or Page
                //  RedirectToPage, finishes this request and issues a Get request
                return Page();
            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
        }
        
        public IActionResult OnPostUpdate()
        {
            try
            {
                //  call the appropriate service method
                //  the update service method returns the nmber of rows affected
                int rowsAffected = _productServices.Product_UpdateProduct(ProductInfo);

                //  always want to give feedback to your user
                //  with an update three results are possible
                //  a) a record was found and changed
                //  b) the action was aborted (handled by the catch)
                //  c) a record having the given pkey value ws NOT found and therefore no change to the database
                if (rowsAffected > 0)
                {
                    Feedback = $"Product {ProductInfo.ProductName} updated on the system";
                }
                else
                {
                    Feedback = $"Product {ProductInfo.ProductName} not found on the system";
                }

                return RedirectToPage(new { productid = productid });

            }
            catch (ArgumentNullException ex)
            {
                ErrorMsg = GetInnerException(ex).Message; 
                PopulateSupportLists();
                return Page();
            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
        }
        
        public IActionResult OnPostDelete()
        {
            try
            {
                //  call the appropriate service method
                //  the update service method returns the nmber of rows affected
                int rowsAffected = _productServices.Product_DeleteProduct(ProductInfo);

                //  always want to give feedback to your user
                //  with an update three results are possible
                //  a) a record was found and deleted
                //  b) the action was aborted (handled by the catch)
                //  c) a record having the given pkey value ws NOT found and therefore no change to the database
                if (rowsAffected > 0)
                {
                    Feedback = $"Product {ProductInfo.ProductName} has been updated to be discontinued on the system";
                }
                else
                {
                    Feedback = $"Product {ProductInfo.ProductName} no longer exists on the system";
                }

                return RedirectToPage(new { productid = productid }); // logical delete
                //  return RedirectToPage(new { productid = (int?) null }); // physical delete

            }
            catch (ArgumentNullException ex)
            {
                ErrorMsg = GetInnerException(ex).Message; 
                PopulateSupportLists();
                return Page();
            }
            catch (ArgumentException ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMsg = GetInnerException(ex).Message;
                PopulateSupportLists();
                return Page();
            }
        }


        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
