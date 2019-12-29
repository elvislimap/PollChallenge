using PollChallenge.Domain.ValueObjects;

namespace PollChallenge.Domain.Interfaces.CrossCutting.Json
{
    public interface ICustomContractResolver
    {
        object GetObjectIgnoringProperties(object model, params string[] jsonPropertyNames);
        object GetObjectIgnoringProperties(ListIgnoreProperties ignoreProperties);
    }
}