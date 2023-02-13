using AutoMapper;
using MediatR;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Roles;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Roles.Queries
{
    public class GetAllRolesQuery : IRequestHandler<GetAllRolesRequest, Response<IEnumerable<RolesResponse>>>
    {
        private readonly IRolesRepositoryAsync _rolesRepositoriesAsync;
        private readonly IMapper _mapper;

        public GetAllRolesQuery(IRolesRepositoryAsync rolesRepositoriesAsync, IMapper mapper)
        {
            _rolesRepositoriesAsync = rolesRepositoriesAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<RolesResponse>>> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            var res = await _rolesRepositoriesAsync.GetAllAsync();
            return new Response<IEnumerable<RolesResponse>>(this._mapper.Map<IEnumerable<RolesResponse>>(res));
        }
    }
}
