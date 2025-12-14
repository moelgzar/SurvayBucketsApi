
namespace SurvayBucketsApi.Contracts.Validator;

public class CreatePollRequestValidator : AbstractValidator<CrearePollRequest>
{

    public CreatePollRequestValidator()
    {
        RuleFor(t=>t.Title).NotEmpty();
    }

}
