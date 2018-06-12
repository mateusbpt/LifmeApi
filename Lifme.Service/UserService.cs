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
    public class UserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetOneById(int id)
        {
            return await _context.Users
                .Include(x => x.Pendings)
                .ThenInclude(x => x.Pending)
                .Include(x => x.Friends)
                .ThenInclude(x => x.Friend)
                .FirstOrDefaultAsync(x => id.Equals(x.Id));
        }

        public async Task<User> GetOneByEmail(string email)
        {
            return await _context.Users
                .Include(x => x.Pendings)
                .ThenInclude(x => x.Pending)
                .Include(x => x.Friends)
                .ThenInclude(x => x.Friend)
                .FirstOrDefaultAsync(x => String.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddUser(UserRegisterModel userRegisterModel)
        {
            userRegisterModel.Validation();

            if (await VerifyEmailExists(userRegisterModel.Email))
            {
                throw new BadRequestException("Já existe um usuário com esse e-mail");
            }

            await _context.Users.AddAsync(userRegisterModel.ConvertToUser());
            await _context.SaveChangesAsync();
        }

        public async Task<ProfileModel> GetProfileById(int id, User loggedUser)
        {
            var user = await _context.Users
                .Include(x => x.Pendings)
                .ThenInclude(x => x.Pending)
                .Include(x => x.Friends)
                .ThenInclude(x => x.Friend)
                .FirstOrDefaultAsync(x => x.Id == id);

            return await GetProfile(user, loggedUser);
        }

        public async Task<ProfileModel> GetProfileByEmail(string email, User loggedUser)
        {
            var user = await _context.Users
                .Include(x => x.Pendings)
                .ThenInclude(x => x.Pending)
                .Include(x => x.Friends)
                .ThenInclude(x => x.Friend)
                .FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return await GetProfile(user, loggedUser);
        }

        public async Task<FriendsAndPendingsModel> GetFriendsAndPendings(User user)
        {
            var pendings = await _context.UserPendings
                                 .Include(x => x.User)
                                 .Include(x => x.Pending)
                                 .Where(x => x.User.Id == user.Id)
                                 .Select(x => x.Pending)
                                 .ToListAsync();

            var friends = await _context.UserFriends
                                 .Include(x => x.User)
                                 .Include(x => x.Friend)
                                 .Where(x => x.User.Id == user.Id)
                                 .Select(x => x.Friend)
                                 .ToListAsync();

            return FriendsAndPendingsModel.ConvertToFriendsAndPendingsModel(pendings, friends);

        }

        public async Task UpdateUser(UserUpdateModel userUpdateModel, User user)
        {
            if (user == null)
            {
                throw new NotFoundException("Usuário editado não encontrado.");
            }

            userUpdateModel.Validation();
            _context.Entry(user).CurrentValues.SetValues(userUpdateModel.UpdatedUser(user));
            await _context.SaveChangesAsync();
        }

        public async Task SendInvite(User user, User invite)
        {
            await InviteValidation(user, invite);

            var invited = new UserPending { User = invite, Pending = user };
            await _context.UserPendings.AddAsync(invited);
            await _context.SaveChangesAsync();
        }

        public async Task RejectInvite(User user, User reject)
        {
            if (reject == null)
            {
                throw new NotFoundException("Usuário não existe.");
            }

            var rejected = await _context.UserPendings
                .Include(x => x.User)
                .Include(x => x.Pending)
                .FirstOrDefaultAsync(x => CompareIds(user.Id, x.User.Id) && CompareIds(reject.Id, x.Pending.Id));

            if (rejected == null)
            {
                throw new NotFoundException("Não há convites relacionado ao usuário informado");
            }

            _context.UserPendings.Remove(rejected);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptInvite(User user, User accept)
        {
            if (accept == null)
            {
                throw new NotFoundException("Usuário não existe.");
            }

            var accepted = await _context.UserPendings
                .Include(x => x.User)
                .Include(x => x.Pending)
                .FirstOrDefaultAsync(x => CompareIds(user.Id, x.User.Id) && CompareIds(accept.Id, x.Pending.Id));

            if (accepted == null)
            {
                throw new NotFoundException("Não há convites relacionado ao usuário informado.");
            }

            _context.UserPendings.Remove(accepted);

            var userFriendship = new UserFriend { User = user, Friend = accept };
            var acceptFriendship = new UserFriend { User = accept, Friend = user };

            await _context.UserFriends.AddAsync(userFriendship);
            await _context.UserFriends.AddAsync(acceptFriendship);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriend(User user, User friend)
        {
            var userFriendship = await _context.UserFriends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .FirstOrDefaultAsync(x => CompareIds(user.Id, x.User.Id) && CompareIds(friend.Id, x.Friend.Id));

            var friendFriendship = await _context.UserFriends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .FirstOrDefaultAsync(x => CompareIds(friend.Id, x.User.Id) && CompareIds(user.Id, x.Friend.Id));

            if (userFriendship == null || friendFriendship == null)
            {
                throw new NotFoundException("Amizade com o usuário não encontrada");
            }

            _context.Remove(userFriendship);
            _context.Remove(friendFriendship);

            await _context.SaveChangesAsync();
        }

        public async Task<List<UsersModel>> GetAllUsers(User user)
        {
            var users = await _context.Users
                .Include(x => x.Pendings)
                .ThenInclude(x => x.Pending)
                .Include(x => x.Friends)
                .ThenInclude(x => x.Friend)
                .ToListAsync();

            return users.Select(x => UsersModel.ConvertToUsersModel(x, user)).ToList();
        }

        public async Task<bool> VerifyEmailExists(string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> VerifyPendingExists(User user, User pending)
        {
            // Compara se o usuário está na lista de pendentes ou se ele recebeu um convite do usuário
            return await _context.UserPendings
                .AnyAsync(x =>
                (CompareIds(x.User.Id, user.Id) && CompareIds(x.Pending.Id, pending.Id)) ||
                (CompareIds(x.User.Id, pending.Id) && CompareIds(x.Pending.Id, user.Id)));
        }

        public async Task<bool> VerifyFriendExists(User user, User friend)
        {
            // Nesse caso, só uma verificação já basta, se o usuário já possui o amigo
            return await _context.UserFriends
                .AnyAsync(x => (CompareIds(x.User.Id, user.Id) && CompareIds(x.Friend.Id, friend.Id)));
        }

        public async Task<List<SimpleUserModel>> GetFriends(User user)
        {
            return await _context.UserFriends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .Where(x => x.User.Id == user.Id)
                .Select(x => SimpleUserModel.ConvertToSimpleUserModel(x.Friend))
                .ToListAsync();
        }

        private bool CompareIds(int id1, int id2)
        {
            return id1 == id2;
        }

        private async Task<ProfileModel> GetProfile(User user, User loggedUser)
        {
            if (user == null)
            {
                throw new BadRequestException("Usuário com id informado não existente");
            }

            var groups = await _context.GroupUsers
                .Include(x => x.User)
                .Include(x => x.Group)
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.Group)
                .ToListAsync();

            return ProfileModel.ConvertToProfileModel(user, loggedUser, groups);
        }

        private async Task InviteValidation(User user, User invite)
        {
            if (invite == null)
            {
                throw new NotFoundException("Usuário que o convite foi enviado não existe.");
            }

            if (user.Id == invite.Id)
            {
                throw new BadRequestException("Usuário não pode enviar convite a si mesmo.");
            }

            if (await VerifyPendingExists(user, invite))
            {
                throw new BadRequestException("Você já enviou solicitação de amizade a esse usuário ou já recebeu uma solicitação do mesmo.");
            }
            else if (await VerifyFriendExists(user, invite))
            {
                throw new BadRequestException("O usuário já é seu amigo.");
            }
        }

    }
}
