using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Commands
{
    public class CreateParticipanteCommand : IRequestHandler<CreateParticipanteResquest, Response<int>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public CreateParticipanteCommand(IParticipantesRepositoryAsync participantesRepositoryAsync, IMapper mapper,
            LanguagesHelper languagesHelper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _mapper = mapper;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(CreateParticipanteResquest request, CancellationToken cancellationToken)
        {
            var res = await _participantesRepositoryAsync.AddAsync(this._mapper.Map<Participante>(request));
            return new Response<int>() { IsSuccess = true, Data = res.Id, Message = this._languagesHelper.SeHaGuardado };
        }
    }
}
