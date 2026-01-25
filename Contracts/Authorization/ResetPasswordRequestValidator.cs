using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Contracts.Authorization;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPattern.PasswordPattern)
            .WithMessage("not matched password");

        RuleFor(x => x.Code)
            .NotEmpty();
    }
}
