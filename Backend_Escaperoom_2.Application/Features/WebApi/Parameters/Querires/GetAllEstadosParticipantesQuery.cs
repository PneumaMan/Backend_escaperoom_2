﻿using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Parameters;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Extensions;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Parameters.Querires
{
    public class GetAllEstadosParticipantesQuery : IRequestHandler<GetAllEstadosParticipantesRequest, Response<IEnumerable<EnumResponse>>>
    {
        public GetAllEstadosParticipantesQuery()
        {

        }

        public async Task<Response<IEnumerable<EnumResponse>>> Handle(GetAllEstadosParticipantesRequest request, CancellationToken cancellationToken)
        {
            var tiposIdent = Enum.GetValues(typeof(EstadosParticipantes)).Cast<EstadosParticipantes>()
                .Select(x => new EnumResponse
                {
                    Id = (int)x,
                    Abreviatura = x.ToString(),
                    Descripcion = x.GetEnumDescription()
                }).ToList();

            return new Response<IEnumerable<EnumResponse>>(tiposIdent);
        }
    }
}
