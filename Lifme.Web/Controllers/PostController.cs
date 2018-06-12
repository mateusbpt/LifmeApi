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
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public PostController(DatabaseContext context)
        {
            _postService = new PostService(context);
            _userService = new UserService(context);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody]PostModel postModel)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            await _postService.AddPost(postModel, user);

            return Ok(new { message = "Post adicionado com sucesso." });
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemovePost(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var post = await _postService.GetOneById(id);

            await _postService.RemovePost(post, user);
            return Ok(new { message = "Post removido com sucesso." });
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriendPosts()
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _postService.GetFriendPosts(user));
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserPosts(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            return Ok(await _postService.GetPostsById(id));
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> Like(int id)
        {
            var user = await _userService.GetOneByEmail(User.Identity.Name);
            var post = await _postService.GetOneById(id);

            await _postService.AddOrRemoveLike(user, post);
            return Ok();
        }

    }
}