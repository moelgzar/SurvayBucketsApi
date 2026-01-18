namespace SurvayBucketsApi.Contracts.Authorization;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
            
    }
}
