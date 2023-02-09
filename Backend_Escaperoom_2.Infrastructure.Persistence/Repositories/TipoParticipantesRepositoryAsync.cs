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

    public class TipoParticipantesRepositoryAsync : GenericRepositoryAsync<TipoParticipante>, ITipoParticipantesRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public TipoParticipantesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
