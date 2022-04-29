using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Services.Abstractions;
using MovieManagement.Services.Models;
using Movies.ItAcademy.API.Models.DTOs;
using Movies.ItAcademy.API.Models.Requests.Account;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ItAcademy.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterRequest request)
        {
            var result = await _service.RegisterAsync(request.Adapt<UserServiceModel>());

            return Ok(result);
        }

        [Route("LogIn")]
        [HttpPost]
        public async Task<IActionResult> LogIn(AccountLogInRequest request)
        {
            var token = await _service.AuthenticateAsync(request.UserName, request.Password);

            return Ok(token);
        }

        [Route("Films")]
        [HttpGet]
        public async Task<IActionResult> GetAvialableFilms()
        {
            var result = await _service.GetAvialableFilmsAsync();

            return Ok(result.Adapt<List<MovieDTO>>());
        }


        [Authorize]
        [Route("GetUserFilms")]
        [HttpGet]
        public async Task<IActionResult> GetUserFilms()
        {
            var result = await _service.GetUserFilms(GetUserID());

            return Ok(result.Adapt<List<MovieDTO>>());
        }

        [Authorize]
        [Route("BookTicket")]
        [HttpPost]
        public async Task<IActionResult> BookTicket(int movieId)
        {

            var result = await _service.BookTicketAsync(GetUserID(), movieId);

            return Ok(result);

        }

        [Authorize]
        [Route("BuyTicket")]
        [HttpPut]
        public async Task<IActionResult> BuyTicket(int movieId)
        {
            var result = await _service.BuyTicketAsync(GetUserID(),movieId);
            return Ok(result);

        }

        [Authorize]
        [Route("CancelTicket")]
        [HttpDelete]
        public async Task<IActionResult> CancelTicket(int movieId)
        {
            var result = await _service.CancelTicketAsync(GetUserID(), movieId);
            return Ok(result);

        }

        private string GetUserID()
        {
            string? userId;

            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    return userId;
                }
            }
            throw new NullReferenceException("user not found");


        }

    }
}
