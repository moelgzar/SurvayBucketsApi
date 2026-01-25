namespace SurvayBucketsApi.Contracts.User;

public class UpdateProfileResponseValidator : AbstractValidator<UpdateProfileRequest>
{

    public UpdateProfileResponseValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
    }


}




