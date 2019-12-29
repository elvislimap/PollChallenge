namespace PollChallenge.Domain.ValueObjects
{
    public class ResultOk : BaseResult
    {
        public ResultOk(object data)
        {
            Success = true;
            Data = data;
        }

        public object Data { get; }
    }
}