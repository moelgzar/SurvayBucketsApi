using Microsoft.AspNetCore.Identity;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Contracts.Roles;
using SurvayBucketsApi.Contracts.User;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context , IRoleService roleService) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IRoleService _roleService = roleService;

    public async Task<Result> ChangePassword(string userid, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userid);

        var result = await _userManager.ChangePasswordAsync(user!, request.Currentpassword, request.Newpassword);

        if (result.Succeeded)

            return Result.Success();

        var error = result.Errors.First();

        return Result.Fail(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result<UserProfileResponse>> GetProfile(string userid)
    {

        var user = await _userManager.Users.Where(x => x.Id == userid)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();

        return Result.Success(user);
    }

    public async Task<Result> UpdateProfile(string userid, [FromBody] UpdateProfileRequest request)
    {
        var user = await _userManager.FindByIdAsync(userid);

        user = request.Adapt(user);

        await _userManager.UpdateAsync(user!);
        return Result.Success(user);
    }

    public async Task<IEnumerable<UserResponse>> GetAllUser(CancellationToken cancellationToken = default) => await

       (from u in _context.Users
        join ur in _context.UserRoles
         on u.Id equals ur.UserId
        join r in _context.Roles
         on ur.RoleId equals r.Id into roles
        where !roles.Any(x => x.Name == DefaultRole.Member)
        select new
        {
            u.Id,
            u.FirstName,
            u.LastName,
            u.Email,
            u.IsDisabled,
            Roles = roles.Select(x => x.Name!).ToList()

        }
        ).GroupBy(u => new { u.Id, u.FirstName, u.LastName, u.Email, u.IsDisabled })
        .Select(u => new UserResponse(

            u.Key.Id,
            u.Key.FirstName,
            u.Key.LastName,
            u.Key.Email,
            u.Key.IsDisabled,
            u.SelectMany(r => r.Roles)

            )).ToListAsync(cancellationToken);



    public async Task<Result<UserResponse>> Getbyid(string id)
    {

        if (await _userManager.FindByIdAsync(id) is not { } user)
            return Result.Fail<UserResponse>(UserError.UserNotFound);


        var userroles = await _userManager.GetRolesAsync(user);


        var response = (user, userroles).Adapt<UserResponse>();

        return Result.Success(response);


    }

    public async Task<Result<UserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var IsExists = _userManager.Users.Any(x => x.Email == request.Email);

        if (IsExists)
            return Result.Fail<UserResponse>(UserError.UserEmailExits);

        var allowroles = await _roleService.GetAllRoles(cancellationToken:  cancellationToken);

        

        if(request.Roles.Except(allowroles.Select(x=>x.Name)).Any())
            return Result.Fail<UserResponse>(UserError.NotAllowedRoles);


        var user = request.Adapt<ApplicationUser>();


        var result = await _userManager.CreateAsync(user, request.Password);

        if(result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user , request.Roles);

            var response = (user, request.Roles).Adapt<UserResponse>();

            return Result.Success(response);

        }

        var error = result.Errors.FirstOrDefault();

        return Result.Fail<UserResponse>(new Error(error.Code , error.Description , StatusCodes.Status400BadRequest));


    }


    public async Task<Result> UpdateUserAsync(string id , UpdateUserRequest request, CancellationToken cancellationToken)
    {

        var EmailIsExists = await  _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id , cancellationToken);

        if (EmailIsExists)
            return Result.Fail(UserError.DuplicatEmail);

        var allowroles = await _roleService.GetAllRoles(cancellationToken: cancellationToken);

        if (await _userManager.FindByIdAsync(id) is not { } user)
            return Result.Fail(UserError.UserNotFound);

        user = request.Adapt(user);


        if (request.Roles.Except(allowroles.Select(x => x.Name)).Any())
            return Result.Fail<UserResponse>(UserError.NotAllowedRoles);


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
           await _context.UserRoles
                .Where(x=>x.UserId == id)
                .ExecuteDeleteAsync(cancellationToken);

            await _userManager.AddToRolesAsync(user , request.Roles);

            return Result.Success();

        }

        var error = result.Errors.First();

        return Result.Fail(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


    }




    public async Task<Result> ToggleAsync(string id,   CancellationToken cancellationToken)
    {


        if (await _userManager.FindByIdAsync(id) is not { } user)
            return Result.Fail(UserError.UserNotFound);

        user.IsDisabled =!user.IsDisabled;

        var result =  await _userManager.UpdateAsync(user);

        
        if (result.Succeeded) 
            return Result.Success();
        var error = result.Errors.First();

        return Result.Fail(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


    }

    public async Task<Result> UnLock(string id, CancellationToken cancellationToken)
    {


        if (await _userManager.FindByIdAsync(id) is not { } user)
            return Result.Fail(UserError.UserNotFound);



        var result = await _userManager.SetLockoutEndDateAsync(user , null);


        if (result.Succeeded)
            return Result.Success();
        var error = result.Errors.First();

        return Result.Fail(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


    }

}

