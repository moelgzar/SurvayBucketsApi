using Microsoft.AspNetCore.Identity;
using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Contracts.Roles;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;

namespace SurvayBucketsApi.services;

public class RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context) : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<RoleResponse>> GetAllRoles(bool IncludeDisabled = false, CancellationToken cancellationToken = default)
    {

        return await _roleManager.Roles.Where(x => !x.IsDefault || IncludeDisabled)
            .ProjectToType<RoleResponse>()
            .ToListAsync(cancellationToken);

    }


    public async Task<Result<RoleDetalisResponse>> GetbyidAsync(string id, CancellationToken cancellationToken)
    {

        var role = await _roleManager.FindByIdAsync(id);

        if (role is null)
            return Result.Fail<RoleDetalisResponse>(UserError.invalidCode);

        var permissions = await _roleManager.GetClaimsAsync(role);

        var response = new RoleDetalisResponse(role.Id, role.Name!, role.IsDeleted, permissions.Select(x => x.Value));

        return Result.Success(response);

    }
    public async Task<Result<RoleDetalisResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken)
    {
        var IsExist = await _roleManager.RoleExistsAsync(request.Name);


        if (IsExist)
            return Result.Fail<RoleDetalisResponse>(RoleError.DuplicatRole);

        var AllowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(AllowedPermissions).Any())
            return Result.Fail<RoleDetalisResponse>(RoleError.NotAllowedPermission);


        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString()

        };

        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            var permissions = request.Permissions.Select(x => new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = Permissions.Type,
                ClaimValue = x

            });
            await _context.AddRangeAsync(permissions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new RoleDetalisResponse(role.Id, role.Name!, role.IsDeleted, request.Permissions);

            return Result.Success(response);

        }


        var error = result.Errors.First();

        return Result.Fail<RoleDetalisResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


        //var result = await _roleManager.AddClaimAsync(request.Name, request.Permissions);

    }
    public async Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken)
    {

        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.Fail<RoleDetalisResponse>(RoleError.RoleNotFound);


        var IsExists = await _roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);
        if (IsExists)
            return Result.Fail<RoleDetalisResponse>(RoleError.invalidName);

        var CurrentPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(CurrentPermissions).Any())
            return Result.Fail<RoleDetalisResponse>(RoleError.NotAllowedPermission);


        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            var currentpermissions = await _context.RoleClaims
                .Where(x => x.RoleId == id && x.ClaimType == Permissions.Type)
                .Select(x => x.ClaimValue)
                .ToListAsync(cancellationToken);


            var newpermission = request.Permissions.Except(currentpermissions).Select(x => new IdentityRoleClaim<string>

            {
                ClaimType = Permissions.Type,
                ClaimValue = x,
                RoleId = role.Id
            }

                );


            var removedpermissions = currentpermissions.Except(request.Permissions);


            await _context.RoleClaims
                .Where(x => x.RoleId == id && removedpermissions.Contains(x.ClaimValue))
                .ExecuteDeleteAsync(cancellationToken);

            await _context.AddRangeAsync(newpermission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            return Result.Success();

        }


        var error = result.Errors.First();

        return Result.Fail<RoleDetalisResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


        //var result = await _roleManager.AddClaimAsync(request.Name, request.Permissions);

    }




    public async Task<Result> ToggleStatus(string id)
    {
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.Fail<RoleDetalisResponse>(RoleError.RoleNotFound);

        role.IsDeleted = !role.IsDeleted;

        await _roleManager.UpdateAsync(role);
        return Result.Success();

    }


}
