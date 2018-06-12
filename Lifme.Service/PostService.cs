using Lifme.Domain.Entity;
using Lifme.Domain.Entity.Relation;
using Lifme.Domain.Exceptions;
using Lifme.Domain.Model;
using Lifme.Domain.Model.ReturnModel;
using Lifme.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifme.Service
{
    public class PostService
    {
        private readonly DatabaseContext _context;

        public PostService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Post> GetOneById(int id)
        {
            var post = await _context.Posts
                .Include(x => x.User)
                .Include(x => x.Likes)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                throw new NotFoundException("Não foi encotrado um post com esse id.");
            }

            return post;
        }

        public async Task AddPost(PostModel postModel, User user)
        {
            postModel.Validate();
            await _context.Posts.AddAsync(postModel.ConvertToPost(user));
            await _context.SaveChangesAsync();
        }

        public async Task<List<SimplePostModel>> GetPostsById(int id)
        {
            var posts = await _context.Posts
                .Include(x => x.User)
                .Include(x => x.Likes)
                .ThenInclude(x => x.User)
                .Where(x => x.User.Id == id)
                .ToListAsync();

            return posts.Select(x => SimplePostModel.ConvertToSimplePostModel(x)).ToList();

        }

        public async Task<List<SimplePostModel>> GetFriendPosts(User user)
        {
            var friendsIds = await _context.UserFriends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.FriendId)
                .ToListAsync();

            var friendPosts = await _context.Posts
                .Include(x => x.User)
                .Include(x => x.Likes)
                .ThenInclude(x => x.User)
                .Where(x => friendsIds.Contains(x.User.Id))
                .ToListAsync();

            return friendPosts.Select(x => SimplePostModel.ConvertToSimplePostModel(x)).ToList();

        }

        public async Task AddOrRemoveLike(User user, Post post)
        {
            var userLike = await _context.PostLikes
                .Include(x => x.User)
                .Include(x => x.Post)
                .FirstOrDefaultAsync(x => x.User.Id == user.Id && post.Id == x.Post.Id);

            if (userLike != null)
            {
                _context.PostLikes
                    .Remove(userLike);
            }
            else
            {
               await _context.PostLikes
                    .AddAsync(new PostLike { User = user, Post = post });
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemovePost(Post post, User user)
        {
            if (post == null)
            {
                throw new NotFoundException("Não foi encotrado um post com esse id.");
            }
            if (post.User.Id != user.Id)
            {
                throw new BadRequestException("Não é possível remover um post que não é seu.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

    }
}
