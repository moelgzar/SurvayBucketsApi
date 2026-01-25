namespace SurvayBucketsApi.Contracts.Polls;

public class PollRequestValidator : AbstractValidator<PollRequest>
{

    public PollRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .Length(1, 200);

        RuleFor(s => s.Summray)
            .NotEmpty()
            .Length(1, 1500);

        RuleFor(s => s.StartsAt)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(e => e.EndsAt)
            .NotEmpty();
        RuleFor(e => e)
            .Must(HasValidDate)
            .WithName(nameof(PollRequest.EndsAt))
            .WithMessage("the End date ({PropertyName}) must greater than start date. ");


    }


    private bool HasValidDate(PollRequest request)
    {
        return request.EndsAt >= request.StartsAt;
    }
}
