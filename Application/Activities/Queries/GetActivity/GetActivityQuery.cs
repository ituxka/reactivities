using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain;
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
                var vm = new ActivityVm();

                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity is null)
                    throw new NotFoundException(nameof(Activity), request.Id);

                vm.Activity = activity;
                return vm;
            }
        }
    }
}
