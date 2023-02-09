using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Infrastructure.Persistence.Contexts;
using Backend_Escaperoom_2.Infrastructure.Persistence.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Repositories
{

    public class EscapeRoomsRepositoryAsync : GenericRepositoryAsync<EscapeRoom>, IEscapeRoomsRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public EscapeRoomsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EscapeRoom> GetEscapeRoomByIdFullAsync(int id)
        {
            return await _dbContext.EscapeRoomsDbSet
                .Include(x => x.Estaciones).ThenInclude(x => x.Retos).ThenInclude(x => x.Respuestas)
                .Include(x => x.Participantes).Include(x => x.Equipos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<EscapeRoom> GetEscapeRoomByIdAsync(int id)
        {
            return await _dbContext.EscapeRoomsDbSet
                .Include(x => x.Estaciones).ThenInclude(x => x.Retos).ThenInclude(x => x.Respuestas)
                //.Include(x => x.Participantes).Include(x => x.Equipos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<EscapeRoom>> GetAllFullEscapeRooms()
        {
            return await _dbContext.EscapeRoomsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Estaciones).ThenInclude(x => x.Retos).ThenInclude(x => x.Respuestas)
                .Include(x => x.Participantes).Include(x => x.Equipos)
                .AsNoTracking().ToListAsync();        
        }

        public async Task<IEnumerable<EscapeRoom>> GetPagedReponseFullAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.EscapeRoomsDbSet.OrderByDescending(x => x.Id)
                .Include(x => x.Estaciones).ThenInclude(x => x.Retos).ThenInclude(x => x.Respuestas)
                .Include(x => x.Participantes).Include(x => x.Equipos)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EscapeRoom>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<EscapeRoom, bool>> predicate)
        {
            return await _dbContext.EscapeRoomsDbSet.Where(predicate).OrderByDescending(x => x.Id)
                .Include(x => x.Estaciones).ThenInclude(x => x.Retos).ThenInclude(x => x.Respuestas)
                .Include(x => x.Participantes).Include(x => x.Equipos)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
