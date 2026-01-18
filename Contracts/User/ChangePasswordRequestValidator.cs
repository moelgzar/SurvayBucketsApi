using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Contracts.User;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Currentpassword)
            .NotEmpty();
           

        RuleFor(x => x.Newpassword)
            .NotEmpty()
            .Matches(RegexPattern.PasswordPattern)
            .NotEqual(x => x.Currentpassword)
            .WithMessage("password the same as current password");
    }
}
