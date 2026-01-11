using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Contracts.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestShape>
{

    public RegisterRequestValidator()
    {
        
        RuleFor(x=>x.Email)
            .NotEmpty()
            .EmailAddress()
            .Length(3, 100);


        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(RegexPattern.PasswordPattern);



        RuleFor(x =>  x.FirstName)
            .NotEmpty()
            .Length(3 , 50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 50);
    }
}
