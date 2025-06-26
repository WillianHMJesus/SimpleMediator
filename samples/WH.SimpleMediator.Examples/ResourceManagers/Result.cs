namespace WH.SimpleMediator.Examples.ResourceManagers;

public record Result
{
    public bool Success => !Errors.Any();
    public object? Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();

    public static Result CreateResponseWithData(object? data = null)
    {
        return new Result { Data = data };
    }

    public static Result CreateResponseWithErrors(IEnumerable<string> errors)
    {
        return new Result { Errors = errors };
    }
}
