using System.Reflection.Metadata.Ecma335;

namespace SurvayBucketsApi.Abstractions.Const;

public static class Permissions
{
    public static string Type { get; } = "permissions";

    public const string GetPoll = "poll:read"; 
    public const string AddPoll = "poll:add"; 
    public const string UpdatePoll = "poll:update"; 
    public const string DeletePoll = "poll:delete";


    public const string GetQuestion = "question:read";
    public const string AddQuestion = "question:add";
    public const string UpdateQuestion = "question:update";


    public const string GetUser = "user:read";
    public const string AddUser = "user:add";
    public const string UpdateUser = "user:update";


    public const string GetRole = "role:read";
    public const string AddRole = "role:add";
    public const string UpdateRole = "role:update";


    public const string Results = "result:read";


    public static IList<string?> GetAllPermissions() => 
        
        typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string ).ToList();



}
