using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Activities.Commands.DeleteActivity
{
    public class DeleteActivityCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteActivityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);

            if (activity is null)
                throw new NotFoundException(nameof(Activity), request.Id);

            _context.Activities.Remove(activity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
