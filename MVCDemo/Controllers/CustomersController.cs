using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;
using MVCDemo.Models.EFCoreDb;
using MVCDemo.ViewModel;

namespace MVCDemo.Controllers
{
    public class CustomersController : Controller
    {
        private MovieRateContext _MovieRateContext;

        public CustomersController(MovieRateContext movieRateContext)
        {
            _MovieRateContext = movieRateContext;
        }
        protected override void Dispose(bool disposing)
        {
            _MovieRateContext.Dispose();

        }

        public IActionResult Details(int Id)
        {
            var customer = _MovieRateContext.Customers.Include(m => m.MembershipType).SingleOrDefault(s => s.Id == Id);
            if (customer == null)
            {
                return StatusCode(404);
            }
            return View(customer);
        }

        public IActionResult Index()
        {
            // var customers = _MovieRateContext.Customers.ToList();
            var customers = _MovieRateContext.Customers.Include(c => c.MembershipType).ToList();       // early loading
            return View(customers);
        }

        [HttpGet]
        public IActionResult New()
        {
            var membershipTypes = _MovieRateContext.MemberShipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            //return View(viewModel);
            return View("CustomerForm",viewModel);  // beacause we changed our View name from 'New' to 'CustomerForm'
        }

        // (Customer customer => works  )         (Customer model => does not work )
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _MovieRateContext.MemberShipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _MovieRateContext.Customers.Add(customer);
            }
            else
            {
                var customerFromDb = _MovieRateContext.Customers.Single(c => c.Id == customer.Id);
                TryUpdateModelAsync(customerFromDb);    // mallicious user can modify RequestData and add additional key,value pair in FormData

                customerFromDb.Name = customer.Name;
                customerFromDb.MembershipTypeId = customer.MembershipTypeId;
                customerFromDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerFromDb.BirthDate = customer.BirthDate;

                //Mapper.Map(customer, customerFromDb);
            }

            _MovieRateContext.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Edit(int id)
        {
            var customer = _MovieRateContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return StatusCode(404);
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _MovieRateContext.MemberShipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}