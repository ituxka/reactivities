using FluentValidation;

namespace Application.Activities.Commands.CreateActivity
{
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.Title).NotEmpty();
            RuleFor(v => v.Description).NotEmpty();
            RuleFor(v => v.Category).NotEmpty();
            RuleFor(v => v.Date).NotEmpty();
            RuleFor(v => v.City).NotEmpty();
            RuleFor(v => v.Venue).NotEmpty();
        }
    }
}
