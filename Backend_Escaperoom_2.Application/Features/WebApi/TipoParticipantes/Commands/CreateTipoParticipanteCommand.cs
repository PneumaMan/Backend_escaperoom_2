using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Commands
{
    public class CreateTipoParticipanteCommand : IRequestHandler<CreateTipoParticipanteResquest, Response<int>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;
        private List<ValidationFailureResponse> _errors;

        public CreateTipoParticipanteCommand(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync, IMapper mapper,
            LanguagesHelper languagesHelper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _mapper = mapper;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(CreateTipoParticipanteResquest request, CancellationToken cancellationToken)
        {
            var tipo = this._mapper.Map<TipoParticipante>(request);
            tipo.Estado = true;

            tipo = await _tipoParticipantesRepositoryAsync.AddAsync(tipo);
            return new Response<int>() { IsSuccess = true, Data = tipo.Id, Message = this._languagesHelper.SeHaGuardado };
        }
    }
}
