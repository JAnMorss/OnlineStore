namespace OnlineStoreAPI.Shared.Kernel.ErrorHandling
{
    public class Result
    {
        public Error Error { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;


        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException("A successful result must not have an error.");
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException("A failed result must have an error.");
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() 
            => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) 
            => new(value, true, Error.None);

        public static Result Failure(Error error) 
            => new(false, error);

        public static Result<TValue> Failure<TValue>(Error error) 
            => new(default, false, error);

        public static Result<TValue> Create<TValue>(TValue? value) 
            => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
