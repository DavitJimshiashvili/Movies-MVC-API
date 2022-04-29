using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using Movies.ITAcademy.Ge.Infrastructure.Extensions;
using Movies.ITAcademy.Ge.Models;
using Movies.ITAcademy.Ge.Models.ViewModels;
using Movies.ITAcademy.Ge.Services.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> SignInManager;
        private readonly UserManager<User> UserManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMvcUserService _mvcUserService;

        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<UserController> logger,
            IMvcUserService mVCUserService)
        {
            _logger = logger;
            _mvcUserService = mVCUserService;
            SignInManager = signInManager;
            UserManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (SignInManager.IsSignedIn(User))
                await ManageTickets();
            var result = await _mvcUserService.GetAvialableFilmsAsync();

            var mapped = result.Adapt<List<MovieCardViewModel>>();

            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {

            ViewBag.tktStatus = HttpContext.Session.GetString(Id.ToString());
            var result = await _mvcUserService.GetFilmDetails(Id);

            var mapped = result.Adapt<MovieDetailsViewModel>();

            return View(mapped);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTicket(int MovieId)
        {

            string userId;
            if (SignInManager.IsSignedIn(User))
            {
                userId = GetUserID();
                await _mvcUserService.BookTicketAsync(userId, MovieId);

            }
            else
            {
                if (HttpContext.Session.GetSessionData<HashSet<int>>("booked") != null)
                {
                    var bookedMovies = HttpContext.Session.GetSessionData<HashSet<int>>("booked");
                    bookedMovies.Add(MovieId);
                    HttpContext.Session.SetSessionData("booked", bookedMovies);
                }
                else
                {
                    HttpContext.Session.SetSessionData("booked", new HashSet<int> { MovieId });

                }
                HttpContext.Session.SetString(MovieId.ToString(), TKTStatuses.Booked);
            }
            return RedirectToAction("Details", new { Id = MovieId });

            //  <input name="userId", type="hidden"value="@userId"> details 101//

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket(/*string userId,*/ int movieId)
        {
            string userId;
            if (SignInManager.IsSignedIn(User))
            {
                userId = GetUserID();
                await _mvcUserService.BuyTicketAsync(userId, movieId);
            }
            else
            {
                if (HttpContext.Session.GetSessionData<HashSet<int>>("purchased") != null)
                {
                    var purchasedMovies = HttpContext.Session.GetSessionData<HashSet<int>>("purchased");
                    purchasedMovies.Add(movieId);
                    HttpContext.Session.SetSessionData("purchased", purchasedMovies);
                }
                else
                {
                    HttpContext.Session.SetSessionData("purchased", new HashSet<int> { movieId });

                }
                HttpContext.Session.SetString(movieId.ToString(), TKTStatuses.Purchased);
            }

            return RedirectToAction("Details", new { Id = movieId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelTicket( int movieId)
        {
            if (SignInManager.IsSignedIn(User))
            {
                await _mvcUserService.CancelTicketAsync(GetUserID(), movieId);
            }
            else
            {
                if (HttpContext.Session.GetSessionData<HashSet<int>>("booked") != null)
                {
                    var bookedMovies = HttpContext.Session.GetSessionData<HashSet<int>>("booked");
                    bookedMovies.Remove(movieId);
                    HttpContext.Session.SetSessionData("booked", bookedMovies);
                }
                HttpContext.Session.SetString(movieId.ToString(),TKTStatuses.Canceled);
            }

            return RedirectToAction("Details", new { Id = movieId });

        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task ManageTickets()
        {

            var userId = GetUserID();
            var bookedMovies = HttpContext.Session.GetSessionData<HashSet<int>>("booked");
            var purchasedMovies = HttpContext.Session.GetSessionData<HashSet<int>>("purchased");
            var existingBookedMovies = await _mvcUserService.GetMovieIds(userId,TKTStatuses.Booked);
            var existingPurchasedMovies = await _mvcUserService.GetMovieIds(userId,TKTStatuses.Purchased);
            if (purchasedMovies != null)
            {
                if (existingPurchasedMovies.Count>0)
                    purchasedMovies.RemoveWhere(x => existingPurchasedMovies.Contains(x));

                foreach (var movieId in purchasedMovies)
                {
                    await _mvcUserService.BuyTicketAsync(userId, movieId);
                }

                if (bookedMovies != null)
                {
                    bookedMovies.RemoveWhere(x => purchasedMovies.Contains(x));
                }
                HttpContext.Session.SetSessionData("purchased", null);

            }
            if (bookedMovies != null)
            {
                if (existingBookedMovies.Count>0)
                    bookedMovies.RemoveWhere(x => existingBookedMovies.Contains(x));

                foreach (var movieId in bookedMovies)
                {
                    await _mvcUserService.BookTicketAsync(userId, movieId);
                }
                HttpContext.Session.SetSessionData("booked", null);
            }
            //HttpContext.Session.Clear();

        }
        private string GetUserID()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }

}
