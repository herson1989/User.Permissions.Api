using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Permissions.Application.Permissions.GetAll
{
    public class GetAllPermissionsQuery : IRequest<IEnumerable<GetAllPermissionsQueryResponse>>
    {

    }
}
