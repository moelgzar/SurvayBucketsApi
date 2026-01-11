namespace SurvayBucketsApi.Contracts.Authorization;

public class ResendConfirmEmailValidator : AbstractValidator<ResendConfirmEmail>
{

    public ResendConfirmEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
