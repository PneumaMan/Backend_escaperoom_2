using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Queries
{
    public class GetParticipanteByIdQuery : IRequestHandler<GetParticipanteByIdRequest, Response<ParticipanteResponse>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetParticipanteByIdQuery(IParticipantesRepositoryAsync participantesRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }
        public async Task<Response<ParticipanteResponse>> Handle(GetParticipanteByIdRequest request, CancellationToken cancellationToken)
        {
            var participante = await _participantesRepositoryAsync.GetByIdAsync(request.Id);
            if (participante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", this._languagesHelper.ParticipanteNoExiste));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return new Response<ParticipanteResponse>(this._mapper.Map<ParticipanteResponse>(participante));
        }
    }
}
