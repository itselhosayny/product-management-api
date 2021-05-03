namespace ProductManagement.Domain.Validation
{
    public class Result
    {
        public Status Status { get; set; }
        public string Error { get; set; }
        public static Result Success()
        {
            return new Result { Status = Status.Success };
        }
        public static Result Failure(string error)
        {
            return new Result
            {
                Error = error,
                Status = Status.Failure
            };
        }
    }

    public enum Status
    {
        Success,
        Failure
    }
}