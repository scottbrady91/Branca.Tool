namespace ScottBrady.Branca.Tool
{
    public class Result<T> where T : class
    {
        public static Result<T> Success(T value) => new Result<T> {IsSuccess = true, Error = null, Value = value};
        public static Result<T> Failure(string errorMessage) => new Result<T> {IsSuccess = false, Error = errorMessage, Value = null};

        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }
        public T Value { get; private set; }
    }
}