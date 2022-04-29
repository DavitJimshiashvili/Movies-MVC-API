using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Domain.Enums;
using Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel.Controllers
{
    public class UserController : Controller
    {

        private readonly IMovieService _movieService;
        private readonly IUserSevice _userService;

        public UserController(IMovieService movieService, IUserSevice userSevice)
        {
            _userService = userSevice;
            _movieService = movieService;
        }


        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PendingFilms()
        {
            var movies = await _movieService.GetAllPendingAsync();
            if (movies == null)
            {
                RedirectToAction("Index", "Home");
            }
            var list = movies.Adapt<List<MovieCardViewModel>>();

            return View(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            if (!await _movieService.Exists(id))
                return NotFound();
            var movie = await _movieService.GetNoTrackingAsync(id);

            movie.Status = Statuses.Published;
            var mappedObj = movie.Adapt<MovieServiceModel>();
            await _movieService.UpdateAsync(mappedObj);

            return RedirectToAction(nameof(PendingFilms));
        }
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0 || !await _movieService.Exists(id))
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _movieService.GetAsync(id);

        //    var mapped = movie.Adapt<MovieCardViewModel>();

        //    return View(mapped);
        //}

        //// POST: Movie/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _movieService.Exists(id))
                return NotFound();

            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(PendingFilms));
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> AuthenticatedUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users == null)
            {
                RedirectToAction("Index", "Home");
            }
            var list = users.Adapt<List<UserViewModel>>();

            return View(list);
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> UserTickets(string id)
        {

            if (!await _userService.Exists(id))
                return NotFound();

            var tickets = await _userService.GetUserTickets(id);

            var list = tickets.Adapt<List<TicketViewModel>>();

            return View(list);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("CancelTicket")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelTicket(string userId,int id)
        {
            if (!await _userService.Exists(userId) || !await _movieService.Exists(id))
                return NotFound();

            await _userService.CancelTicketAsync(userId,id);
            return RedirectToAction(nameof(UserTickets),new {id=userId});
        }
    }
}
