using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lifme.Domain.Entity;
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
    public class ChallengeController : Controller
    {
        private readonly ChallengeService _challengeService;
        private readonly UserService _userService;

        public ChallengeController(DatabaseContext databaseContext)
        {
            _userService = new UserService(databaseContext);
            _challengeService = new ChallengeService(databaseContext);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);

            return Ok(await _challengeService.GetChallengeProfile(user));
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptChallenge(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var userChallenge = await _challengeService.GetUserChallengeById(user, id);

            await _challengeService.AcceptChallenge(user, userChallenge);

            return Ok(new { message = "Desafio aceito com sucesso." });
        }

        [HttpDelete("{id}/reject")]
        public async Task<IActionResult> RejectChallenge(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var userChallenge = await _challengeService.GetUserChallengeById(user, id);

            await _challengeService.RejectChallenge(user, userChallenge);

            return Ok(new { message = "Desafio rejeitado com sucesso." });
        }

        [HttpPost("{id}/finalize")]
        public async Task<IActionResult> FinalizeChallenge(int id, [FromBody]CompleteChallengeModel completeChallengeModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var userChallenge = await _challengeService.GetUserChallengeById(user, id);

            await _challengeService.CompleteChallenge(completeChallengeModel, user, userChallenge);

            return Ok(new { message = "Desafio finalizado com sucesso." });
        }
    }
}