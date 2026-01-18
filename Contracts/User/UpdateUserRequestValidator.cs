using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Contracts.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
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

      


        RuleFor(x => x.Roles)
            .NotEmpty()
            .NotNull();


        RuleFor(x => x.Roles)
             .Must(x => x.Distinct().Count() == x.Count)
             .WithMessage("can not add dupplicae roles ")
             .When(x => x.Roles != null);

    }
}
