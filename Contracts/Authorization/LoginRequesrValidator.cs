using System.Data;

namespace SurvayBucketsApi.Contracts.Authorization;

public class LoginRequesrValidator:AbstractValidator<LoginRequestDto>
{



    public LoginRequesrValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();


        RuleFor(x => x.Password)
            .NotEmpty()

;       
    }




}
