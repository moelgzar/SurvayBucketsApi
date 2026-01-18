namespace SurvayBucketsApi.Abstractions;

public class Result
{
    public Result(bool isSuccess , Error error)
    {
        if((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;

    }
    public bool IsSuccess { get;}
    public bool IsFailed => !IsSuccess;
    public Error? Error { get; }

    public static Result Success() => new (true , Error.None);
    public static Result Fail(Error error) => new (false , error);
    public static Result<TValue> Success<TValue>(TValue value) => new( value , true, Error.None);
    public static Result<Tvalue> Fail<Tvalue>(Error error) => new( default!, false, error);

}


public class  Result<Tvalue> : Result
{
    private readonly Tvalue _value;

    public 
        Result(Tvalue value , bool isSuccess, Error error) : base(isSuccess , error)
    {
        _value = value;
    }
    
    public Tvalue Value => IsSuccess ? _value : throw new InvalidOperationException("failure result can not have value ");



}
