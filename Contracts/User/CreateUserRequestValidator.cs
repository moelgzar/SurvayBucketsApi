using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Contracts.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{

    public CreateUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(x => x.LastName)
         .NotEmpty()
         .Length(3, 100);


        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPattern.PasswordPattern)
            .WithMessage("the password not allowed ");


        RuleFor(x => x.Roles)
            .NotEmpty()
            .NotNull();


        RuleFor(x => x.Roles)
             .Must(x => x.Distinct().Count() == x.Count)
             .WithMessage("can not add dupplicae roles ")
             .When(x=>x.Roles != null);

            
    }
}
