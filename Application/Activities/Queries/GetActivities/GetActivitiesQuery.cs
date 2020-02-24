using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities.Queries.GetActivities
{
    public class GetActivitiesQuery : IRequest<ActivitiesVm>
    {
        public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, ActivitiesVm>
        {
            private readonly IApplicationDbContext _context;

            public GetActivitiesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ActivitiesVm> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
            {
                var vm = new ActivitiesVm
                {
                    Activities = await _context.Activities.ToListAsync(cancellationToken)
                };

                return vm;
            }
        }
    }
}
