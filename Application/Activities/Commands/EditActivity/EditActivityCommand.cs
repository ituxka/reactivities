using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Activities.Commands.EditActivity
{
    public class EditActivityCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime? Date { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }

        public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand>
        {
            private readonly IApplicationDbContext _context;

            public EditActivityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EditActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity is null)
                    throw new NotFoundException(nameof(Activity), request.Id);

                activity.Title = request.Title ?? activity.Title;
                activity.Description = request.Description ?? activity.Description;
                activity.Category = request.Category ?? activity.Category;
                activity.Date = request.Date ?? activity.Date;
                activity.City = request.City ?? activity.City;
                activity.Venue = request.Venue ?? activity.Venue;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
