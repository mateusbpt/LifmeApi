using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lifme.Domain.Model;
using Lifme.Repository.Context;
using Lifme.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lifme.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    public class UserController : Controller
    {

        private readonly UserService _userService;
        private readonly FeedbackService _feedbackService;

        public UserController(DatabaseContext context)
        {
            _userService = new UserService(context);
            _feedbackService = new FeedbackService(context);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _userService.GetAllUsers(user));
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditProfile([FromBody]UserUpdateModel userUpdateModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            await _userService.UpdateUser(userUpdateModel, user);

            return Ok(new { message = "Dados alterados com sucesso" });
        }

        [HttpPost("invite/{id}")]
        public async Task<IActionResult> SendInvite(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var invite = await _userService.GetOneById(id);

            await _userService.SendInvite(user, invite);

            return Ok(new { message = "Convite enviado com sucesso" });
        }

        [HttpPost("invite/{id}/accept")]
        public async Task<IActionResult> AcceptInvite(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var invite = await _userService.GetOneById(id);

            await _userService.AcceptInvite(user, invite);

            return Ok(new { message = "Convite aceito com sucesso" });
        }

        [HttpDelete("invite/{id}/reject")]
        public async Task<IActionResult> RejectInvite(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var invite = await _userService.GetOneById(id);

            await _userService.RejectInvite(user, invite);

            return Ok(new { message = "Convite rejeitado com sucesso" });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _userService.GetProfileByEmail(User.Identity.Name, user));
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _userService.GetProfileById(id, user));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsersPendingsAndFriends()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _userService.GetFriendsAndPendings(user));
        }

        [HttpGet("feedback")]
        public async Task<IActionResult> GetFeedback()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _feedbackService.GetFeedbackUser(user));
        }

        [HttpDelete("friends/{id}/remove")]
        public async Task<IActionResult> RemoveFriend(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var friend = await _userService.GetOneById(id);

            await _userService.RemoveFriend(user, friend);

            return Ok(new { message = "Usuário removido com sucesso." });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterModel userRegisterModel)
        {
            await _userService.AddUser(userRegisterModel);
            return Ok(new { message = "Usuário cadastrado com sucesso" });
        }


    }
}