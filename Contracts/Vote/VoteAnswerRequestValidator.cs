namespace SurvayBucketsApi.Contracts.Vote;

public class VoteAnswerRequestValidator : AbstractValidator<VoteAnswerRequest>
{

    public VoteAnswerRequestValidator()
    {

        RuleFor(v => v.QuestionId)
            .GreaterThan(0).WithMessage("Question ID must be greater than zero.");

        RuleFor(v => v.AnswerId)
            .GreaterThan(0).WithMessage("Question ID must be greater than zero.");
    }
}
