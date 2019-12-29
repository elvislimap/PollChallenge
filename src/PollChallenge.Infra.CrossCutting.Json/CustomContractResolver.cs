using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PollChallenge.Infra.CrossCutting.Json
{
    public sealed class CustomContractResolver : DefaultContractResolver, ICustomContractResolver
    {
        private readonly Dictionary<Type, HashSet<string>> _ignores;

        public CustomContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
            _ignores = new Dictionary<Type, HashSet<string>>();
        }


        public object GetObjectIgnoringProperties(object model, params string[] jsonPropertyNames)
        {
            if (model == null)
                return null;

            AddIgnore(model.GetType(), jsonPropertyNames);

            return JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    ContractResolver = this,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        public object GetObjectIgnoringProperties(ListIgnoreProperties ignoreProperties)
        {
            if (ignoreProperties.Any(i => i.Model == null))
                return null;

            foreach (var ignorePropertie in ignoreProperties)
                AddIgnore(ignorePropertie.Model.GetType(), ignorePropertie.JsonPropertyNames.ToArray());

            return JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(ignoreProperties.ElementAt(0).Model, new JsonSerializerSettings
                {
                    ContractResolver = this,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = i => false;
                property.Ignored = true;
            }

            return property;
        }

        private bool IsIgnored(Type type, string jsonPropertyName)
        {
            if (!_ignores.ContainsKey(type))
                return false;

            return _ignores[type].Contains(jsonPropertyName);
        }

        private void AddIgnore(Type type, params string[] jsonPropertyNames)
        {
            if (!_ignores.ContainsKey(type))
                _ignores[type] = new HashSet<string>();

            foreach (var prop in jsonPropertyNames)
                _ignores[type].Add(prop);
        }
    }
}