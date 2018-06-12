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
    public class GroupService
    {
        private readonly DatabaseContext _context;

        public GroupService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Group> GetOneById(int id, User user)
        {
            var group = await _context.Groups
                .Include(x => x.Administrator)
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group == null)
            {
                throw new NotFoundException("Grupo não existe");
            }

            if (!group.Users.Any(x => x.UserId == user.Id))
            {
                throw new BadRequestException("Você não pertence a esse grupo.");
            }

            return group;
        }

        public async Task<GroupProfileModel> GetProfileById(int id, User user)
        {
            var group = await GetOneById(id, user);

            if (group == null)
            {
                throw new NotFoundException("Grupo não existe.");
            }

            if (!group.Users.Any(x => x.UserId == user.Id))
            {
                throw new BadRequestException("Você não está nesse grupo.");
            }

            var tournaments = await _context.Tournaments
                .Include(x => x.Group)
                .Include(x => x.Winner)
                .Where(x => group.Id == x.Group.Id)
                .ToListAsync();

            return GroupProfileModel.ConvertToSimpleUserModel(group, tournaments); 
        }

        public async Task AddGroup(GroupAddModel groupAddModel, User user)
        {
            groupAddModel.Validate();
            var group = groupAddModel.ConvertToGroup(user);
            await _context.Groups.AddAsync(group);
            await _context.GroupUsers.AddAsync(new GroupUser { Group = group, User = user });
            await _context.SaveChangesAsync();
        }

        public async Task AddUserInGroup(User user, Group group, User add)
        {
            if (group == null)
            {
                throw new NotFoundException("Grupo não existe.");
            }

            if (!user.Friends.Any(x => x.FriendId == add.Id))
            {
                throw new BadRequestException("Não pode adicionar ao grupo quem não é seu amigo.");
            }

            if (group.Administrator.Id != user.Id)
            {
                throw new BadRequestException("Você não é o administrador do grupo, não pode adicionar usuários ao grupo.");
            }

            if (group.Users.Any(x => x.UserId == add.Id))
            {
                throw new BadRequestException("Esse Usuário já está no grupo.");
            }

            await _context.GroupUsers.AddAsync(new GroupUser { User = add, Group = group });
            await _context.SaveChangesAsync();
        }

        public async Task GetOutGroup(User user, Group group)
        {
            if (group == null)
            {
                throw new NotFoundException("Grupo não existe.");
            }

            if (group.Administrator.Id != user.Id)
            {
                throw new BadRequestException("Você não é o administrador do grupo, não pode sair do grupo.");
            }

            if (!group.Users.Any(x => x.UserId == user.Id))
            {
                throw new BadRequestException("Você não está nesse grupo.");
            }

            var groupUser = await _context.GroupUsers.FirstOrDefaultAsync(x => x.GroupId == group.Id && x.UserId == user.Id);

            _context.GroupUsers.Remove(groupUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SimpleGroupModel>> GetGroupsByUser(User user)
        {
            var groups = await _context.GroupUsers
                .Include(x => x.Group)
                .ThenInclude(x => x.Administrator)
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.Group)
                .ToListAsync();

            return groups.Select(x => SimpleGroupModel.ConvertToSimpleGroupModel(x)).ToList();
        }

        public async Task AddTournament(TournamentAddModel tournamentAddModel, Group group, User user)
        {
            tournamentAddModel.Validate();
            ValidateTournament(group, user);       
            await _context.Tournaments.AddAsync(tournamentAddModel.ConvertToTournament(group));
            await _context.SaveChangesAsync();
        }

        public async Task AddWinnerTournament(TournamentWinnerModel tournamentWinnerModel, Tournament tournament, Group group, User user)
        {
            tournamentWinnerModel.Validate();
            var winner = await _context.Users.FirstOrDefaultAsync(x => x.Id == tournamentWinnerModel.IdWinner);
            ValidateAddWinner(tournament, group, user, winner);
            _context.Entry(tournament).CurrentValues.SetValues(tournamentWinnerModel.UpdatedTournament(winner, tournament));
            await _context.SaveChangesAsync();

        }

        public async Task RemoveTournament(Tournament tournament, Group group, User user)
        {
            if (tournament == null)
            {
                throw new NotFoundException("Torneio não existe");
            }

            if (tournament.Group.Id != group.Id)
            {
                throw new BadRequestException("Torneio não pertence ao grupo.");
            }

            ValidateTournament(group, user);

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<Tournament> GetTournamentById(int id)
        {
            return await _context.Tournaments
                .Include(x => x.Winner)
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private void ValidateTournament(Group group, User user)
        {
            if (group == null)
            {
                throw new NotFoundException("Grupo não existe.");
            }

            if (group.Administrator.Id != user.Id)
            {
                throw new BadRequestException("Você não é o administrador do grupo, não pode adicionar/remover um torneio.");
            }

            if (group.Users.Count <= 1)
            {
                throw new BadRequestException("O grupo só possui um usuário, não é possível criar/remover um torneio.");
            }
        }

        private void ValidateAddWinner(Tournament tournament, Group group, User user, User winner)
        {
            if (tournament == null || group == null)
            {
                throw new NotFoundException("Grupo ou torneio não existe.");
            }

            if (tournament.Group.Id != group.Id)
            {
                throw new BadRequestException("Torneio não pertence ao grupo.");
            }

            if (tournament.Winner != null)
            {
                throw new BadRequestException("Torneio já possui um vencedor.");
            }

            if (!group.Users.Any(x => x.User.Id == winner.Id))
            {
                throw new BadRequestException("Não pode adicionar um vencendor que não pertence ao grupo.");
            }

            if (group.Administrator.Id != user.Id)
            {
                throw new BadRequestException("Você não é o administrador do grupo, não pode adicionar o vencedor do torneio.");
            }

        }
    }
}
