namespace SurvayBucketsApi.Contracts.Question;

public class QuestionRequestValidator: AbstractValidator<QuestionRequest>
{
    public QuestionRequestValidator()
    {




        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required")
            .Length(3, 1000);
           

        RuleFor(x => x.Answers)
          .NotNull();

        RuleFor(x => x.Answers)
            .Must(x=>x.Count > 1)
            .WithMessage("At least two answers are required")
            .When(x=>x.Answers != null);

        RuleFor(x => x.Answers)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("you can not add duplicated answers value !!  ")
            .When(x=>x.Answers != null);
            


    }

}
