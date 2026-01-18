using SurvayBucketsApi.Abstractions;
using SurvayBucketsApi.Contracts.User;

namespace SurvayBucketsApi.services;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfile(string userid);
    Task<Result> UpdateProfile(string userid , UpdateProfileRequest request );
    Task<Result> ChangePassword(string userid, ChangePasswordRequest request);
    Task<IEnumerable<UserResponse>> GetAllUser(CancellationToken cancellationToken = default);
    Task<Result<UserResponse>> Getbyid(string id);
    Task<Result<UserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateUserAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleAsync(string id, CancellationToken cancellationToken);
    Task<Result> UnLock(string id, CancellationToken cancellationToken);





}
