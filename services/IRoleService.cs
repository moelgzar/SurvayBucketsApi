using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.Roles;

namespace SurvayBucketsApi.services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllRoles(bool? IncludeDisabled = false , CancellationToken cancellationToken = default);
    Task<Result<RoleDetalisResponse>> GetbyidAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<RoleDetalisResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatus(string id);



}
