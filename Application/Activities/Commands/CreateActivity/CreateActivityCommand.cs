using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }

        public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreateActivityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
            {
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    City = request.City,
                    Venue = request.Venue,
                };

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
