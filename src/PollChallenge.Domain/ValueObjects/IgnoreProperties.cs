using System.Collections.Generic;

namespace PollChallenge.Domain.ValueObjects
{
    public class IgnoreProperties
    {
        public IgnoreProperties(object model, IEnumerable<string> jsonPropertyNames)
        {
            Model = model;
            JsonPropertyNames = jsonPropertyNames;
        }

        public object Model { get; }
        public IEnumerable<string> JsonPropertyNames { get; }
    }

    public class ListIgnoreProperties : List<IgnoreProperties>
    {
        public void AddProperty(object model, params string[] jsonPropertyNames)
        {
            Add(new IgnoreProperties(model, jsonPropertyNames));
        }
    }
}