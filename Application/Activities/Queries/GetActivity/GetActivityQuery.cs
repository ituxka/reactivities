using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Activities.Queries.GetActivity
{
    public class GetActivityQuery : IRequest<ActivityVm>
    {
        public Guid Id { get; set; }

        public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityVm>
        {
            private readonly IApplicationDbContext _context;

            public GetActivityQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ActivityVm> Handle(GetActivityQuery request, CancellationToken cancellationToken)
            {
                var vm = new ActivityVm
                {
                    Activity = await _context.Activities.FindAsync(request.Id)
                };

                return vm;
            }
        }
    }
}
