using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;
using MVCDemo.Models.EFCoreDb;
using MVCDemo.ViewModel;

namespace MVCDemo.Controllers
{
    public class MoviesController : Controller
    {
        public MovieRateContext _MovieRateContext;
        public MoviesController(MovieRateContext movieRateContext)
        {
            _MovieRateContext = movieRateContext;
        }

        protected override void Dispose(bool disposing)
        {
            _MovieRateContext.Dispose();
        }

        public IActionResult Random()
        {
            var movie = new Movie() { Name ="Shera"};
            // ------- this is what happens when we return Model via View method
            //ViewResult viewResult = new ViewResult();
            //viewResult.ViewData.Model = movie;

            //-- ViewData practice
            //ViewData["Movie"] = movie;
            //return View();

            //return View(movie);

            // --- ViewModel Demo
            var customers = new List<Customer>();
            customers.Add(new Customer {Id =1, Name ="Cust1" });
            customers.Add(new Customer { Id = 2, Name = "Cust2" });

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public IActionResult Index(int? pageIndex, string sortBy)
        {
            //if (!pageIndex.HasValue)
            //{
            //    pageIndex = 1;
            //}
            //if (string.IsNullOrWhiteSpace(sortBy))
            //{
            //    sortBy = "Name";
            //}
            //return Content(String.Format("PageIndex = {0} and sort by = {1}", pageIndex, sortBy));

            var movies = _MovieRateContext.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        [HttpGet]
        public ViewResult New()
        {
            var genres = _MovieRateContext.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            //return Content(id.ToString());
            var movie = _MovieRateContext.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return StatusCode(404);
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _MovieRateContext.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _MovieRateContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return StatusCode(404);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _MovieRateContext.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _MovieRateContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _MovieRateContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _MovieRateContext.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [HttpGet]
        [Route("movies/released/{year}/{month:regex(\\d{{2}})}")]
        // use '{{' in place of '{' and '}}' in place of '}' when regular expression specified
        public IActionResult ByReleaseDate(int year, int month) {
            return Content(year +"/"+ month);
        }
    }
}