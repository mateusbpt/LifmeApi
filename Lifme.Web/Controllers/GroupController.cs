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
    public class GroupController : Controller
    {
        private readonly UserService _userService;
        private readonly GroupService _groupService;

        public GroupController(DatabaseContext context)
        {
            _userService = new UserService(context);
            _groupService = new GroupService(context);
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GroupProfile(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);

            return Ok(await _groupService.GetProfileById(id, user));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroup([FromBody]GroupAddModel groupAddModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            await _groupService.AddGroup(groupAddModel, user);

            return Ok(new { message = "Grupo criado com sucesso." });
        }

        [HttpPost("{id}/user/add/{idUser}")]
        public async Task<IActionResult> AddUserInGroup(int id, int idUser)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var other = await _userService.GetOneById(idUser);
            var group = await _groupService.GetOneById(id, user);

            await _groupService.AddUserInGroup(user, group, other);

            return Ok(new { message = "Usuário adicionado com sucesso." });
        }

        [HttpDelete("{id}/user/out")]
        public async Task<IActionResult> GetOutGroup(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var group = await _groupService.GetOneById(id, user);
            await _groupService.GetOutGroup(user, group);

            return Ok(new { message = "Você saiu do grupo com sucesso." });
        }

        [HttpGet("user/all")]
        public async Task<IActionResult> GetGroups()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _groupService.GetGroupsByUser(user));
        }

        [HttpPost("{id}/tournament/add")]
        public async Task<IActionResult> AddTournament(int id, [FromBody]TournamentAddModel tournamentAddModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var group = await _groupService.GetOneById(id, user);

            await _groupService.AddTournament(tournamentAddModel, group, user);

            return Ok(new { message = "Torneio incluído com sucesso." });
        }

        [HttpDelete("{id}/tournament/remove/{idTournament}")]
        public async Task<IActionResult> RemoveTournament(int id, int idTournament)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var group = await _groupService.GetOneById(id, user);
            var tournament = await _groupService.GetTournamentById(idTournament);

            await _groupService.RemoveTournament(tournament, group, user);

            return Ok(new { message = "Torneio removido com sucesso." });
        }

        [HttpPost("{id}/tournament/{idTournament}/winner")]
        public async Task<IActionResult> AddTournament(int id, int idTournament, [FromBody]TournamentWinnerModel tournamentWinnerModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var tournament = await _groupService.GetTournamentById(idTournament);
            var group = await _groupService.GetOneById(id, user);

            await _groupService.AddWinnerTournament(tournamentWinnerModel, tournament, group, user);

            return Ok(new { message = "Vencedor incluído com sucesso." });
        }

    }
}