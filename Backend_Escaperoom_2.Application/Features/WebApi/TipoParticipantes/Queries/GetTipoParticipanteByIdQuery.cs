using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries
{
    public class GetTipoParticipanteByIdQuery : IRequestHandler<GetTipoParticipanteByIdRequest, Response<TipoParticipanteResponse>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetTipoParticipanteByIdQuery(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }
        public async Task<Response<TipoParticipanteResponse>> Handle(GetTipoParticipanteByIdRequest request, CancellationToken cancellationToken)
        {
            var tipoParticipante = await _tipoParticipantesRepositoryAsync.GetByIdAsync(request.Id);
            if (tipoParticipante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Tipo participante' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return new Response<TipoParticipanteResponse>(this._mapper.Map<TipoParticipanteResponse>(tipoParticipante));
        }
    }
}
