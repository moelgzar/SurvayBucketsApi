namespace SurvayBucketsApi.Contracts.Vote;

public class VoteRequestValidator : AbstractValidator<VoteRequest>
{

    public VoteRequestValidator()
    {
        RuleFor(v => v.Answers)
            .NotEmpty().WithMessage("Poll ID cannot be empty.");
        RuleForEach(v => v.Answers)
            .SetInheritanceValidator(v =>
            {
                v.Add(new VoteAnswerRequestValidator());
            });
    }
}
