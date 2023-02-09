using Backend_Escaperoom_2.Application.Enums;
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

    public class ParticipantesRepositoryAsync : GenericRepositoryAsync<Participante>, IParticipantesRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public ParticipantesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Participante> GetParticipanteByIdFullAsync(int id)
        {
            return await _dbContext.ParticipantesDbSet
                //.Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta).ThenInclude(x => x.Reto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<Participante> GetParticipanteByIdentFullAsync(string identificacion, Expression<Func<Participante, bool>> predicate)
        {
            return await _dbContext.ParticipantesDbSet.Where(predicate)
                //.Include(x => x.Encuestas).Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta).ThenInclude(x => x.Reto)
                .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        public async Task<IEnumerable<Participante>> GetAllParticipantesFullAsync()
        {
            return await _dbContext.ParticipantesDbSet
                //.Include(x => x.Encuestas).Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Participante>> GetAllParticipantesFullAsync(Expression<Func<Participante, bool>> predicate)
        {
            return await _dbContext.ParticipantesDbSet.Where(predicate)
                //.Include(x => x.Encuestas).Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Participante>> GetPagedReponseFullAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.ParticipantesDbSet.OrderBy(x => x.Id)
                //.Include(x => x.Encuestas).Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Participante>> GetPagedReponseFullAsync(int pageNumber, int pageSize, Expression<Func<Participante, bool>> predicate)
        {
            return await _dbContext.ParticipantesDbSet.Where(predicate).OrderBy(x => x.Id)                
                //.Include(x => x.Encuestas).Include(x => x.EscapeRoom).Include(x => x.RespuestasParticipantes).ThenInclude(x => x.Respuesta)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
