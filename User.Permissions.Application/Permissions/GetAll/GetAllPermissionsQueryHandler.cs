using MediatR;
using Microsoft.Extensions.Logging;
using User.Permissions.Application.Mappers;
using User.Permissions.Application.Permissions.Create;
using User.Permissions.Domain.Entities;
using User.Permissions.Domain.Interfaces.UseCases;

namespace User.Permissions.Application.Permissions.GetAll
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<GetAllPermissionsQueryResponse>>
    {
        private readonly IGetAllPermissionsUseCase _getAllPermissionsUseCase;
        private readonly ILogger<GetAllPermissionsQueryHandler> _logger;

        public GetAllPermissionsQueryHandler(IGetAllPermissionsUseCase getAllPermissionsUseCase, ILogger<GetAllPermissionsQueryHandler> logger)
        {
            _getAllPermissionsUseCase = getAllPermissionsUseCase;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllPermissionsQueryResponse>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Permission> permissions =  await _getAllPermissionsUseCase.Execute();

            return PermissionResponseMapper.Map(permissions);
        }
    }
}
