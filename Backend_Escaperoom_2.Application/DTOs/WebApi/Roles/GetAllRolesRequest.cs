using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Roles
{
    public class GetAllRolesRequest : IRequest<Response<IEnumerable<RolesResponse>>>
    {

    }
}
