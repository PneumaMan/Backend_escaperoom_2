using Backend_Escaperoom_2.Application.DTOs;
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

namespace Backend_Escaperoom_2.Application.Features.WebApi.Parameters.Queries
{
    public class GetAllTiposRetosQuery : IRequestHandler<GetAllTiposRetosRequest, Response<IEnumerable<EnumResponse>>>
    {
        public GetAllTiposRetosQuery()
        {

        }

        public async Task<Response<IEnumerable<EnumResponse>>> Handle(GetAllTiposRetosRequest request, CancellationToken cancellationToken)
        {
            var tiposIdent = Enum.GetValues(typeof(TiposRetos)).Cast<TiposRetos>()
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
