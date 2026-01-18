namespace SurvayBucketsApi.Contracts.Roles;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{

    public RoleRequestValidator()
    {
        RuleFor(x => x.Name)

          .Length(3, 100)
          .NotEmpty();



        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty();


        RuleFor(x => x.Permissions)
        .Must(x => x.Distinct().Count() == x.Count)
        .When(x => x.Permissions != null);


    }
}
