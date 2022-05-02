using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.Enums.UserEnums;
using Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Movies.ITAcademy.Ge.ControlPanel.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //GET: Movie
        [Authorize(Roles =Roles.Moderator)]
        public async Task<IActionResult> Index(int? page)
        {
         
            var movies = await _movieService.GetAllAsync();
            if (movies==null)
            {
                RedirectToAction("Index", "Home");
            }
            var mappedMovies = movies.Adapt<List<MovieCardViewModel>>();
            var pageNumber = page ?? 1;
            var OnePageOfMovies = mappedMovies.ToPagedList(pageNumber, 5);
            
            return View(OnePageOfMovies);
        }

        // GET: Movie/Details/5
        [Authorize(Roles = Roles.Moderator)]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 ||  !await _movieService.Exists(id))
                return NotFound();

            var movie = await _movieService.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            var mappedObj = movie.Adapt<MovieCardViewModel>();

            return View(mappedObj);
        }

        // GET: Movie/Create
        [Authorize(Roles = Roles.Moderator)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = Roles.Moderator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateViewModel movie)
        {
            if (ModelState.IsValid)
            {
                movie.Status = Statuses.Uploaded;
                var mappedOBJ = movie.Adapt<MovieServiceModel>();
                await _movieService.CreateAsync(mappedOBJ);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        //GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!await _movieService.Exists(id))
            {
                return NotFound();
            }
            var movie = await _movieService.GetAsync(id);
            var mapped = movie.Adapt<MovieUpdateModel>();
            return View(mapped);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = Roles.Moderator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieUpdateModel movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movieToUpdate = movie.Adapt<MovieServiceModel>();
                    movieToUpdate.Status = Statuses.Uploaded;
                    await _movieService.UpdateAsync(movieToUpdate);
                }
                catch (Exception ex)
                {
                    if (!await _movieService.Exists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        //// GET: Movie/Delete/5
        [Authorize(Roles = Roles.Moderator)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || ! await _movieService.Exists(id))
            {
                return NotFound();
            }

            var movie = await _movieService.GetAsync(id);

            var mapped=movie.Adapt<MovieCardViewModel>();

            return View(mapped);
        }

        //// POST: Movie/Delete/5
        [Authorize(Roles = Roles.Moderator)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _movieService.Exists(id))
                return NotFound();
            
            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
