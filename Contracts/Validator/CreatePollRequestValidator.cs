
namespace SurvayBucketsApi.Contracts.Validator;

public class CreatePollRequestValidator : AbstractValidator<CrearePollRequest>
{

    public CreatePollRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .Length(1, 200);

        RuleFor(s=>s.Summray)
            .NotEmpty()
            .Length (1, 1500);

        RuleFor(s => s.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(e => e.EndsAt)
            .NotEmpty();
        RuleFor(e => e)
            .Must(HasValidDate)
            .WithName(nameof(CrearePollRequest.EndsAt))
            .WithMessage("the End date ({PropertyName}) must greater than start date. ");
            

    }


    private bool HasValidDate(CrearePollRequest request)
    {
        return request.EndsAt >= request.StartsAt;
    }
}
