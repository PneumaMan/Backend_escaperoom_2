using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Parameters
{
    public class GetAllTiposRetosRequest : IRequest<Response<IEnumerable<EnumResponse>>>
    {

    }
}
