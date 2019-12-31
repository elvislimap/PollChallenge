using AutoBogus;
using PollChallenge.Domain.Interfaces.CrossCutting.Json;
using PollChallenge.Domain.Commons;
using Xunit;
using System.Collections.Generic;
using PollChallenge.Domain.Entities;
using PollChallenge.Domain.ValueObjects;
using Bogus;

namespace PollChallenge.Infra.CrossCutting.Json.Tests
{
    public class CustomContractResolverTest
    {
        private readonly ICustomContractResolver _customContractResolver;

        public CustomContractResolverTest()
        {
            _customContractResolver = new CustomContractResolver();
        }


        [Fact(DisplayName = "CustomContractResolver - get ignoring properties first")]
        [Trait("Category", "CrossCutting")]
        public void CustomContractResolver_GetObjectIgnoringPropertiesFirst()
        {
            // Arrange & Act
            var modelResult = _customContractResolver
                .GetObjectIgnoringProperties(GetModel(), GetArrayStringRandom());

            // Assert
            Assert.NotNull(modelResult);
        }

        [Fact(DisplayName = "CustomContractResolver - get ignoring properties second")]
        [Trait("Category", "CrossCutting")]
        public void CustomContractResolver_GetObjectIgnoringPropertiesSecond()
        {
            // Arrange
            var listIgnoreProperties = new ListIgnoreProperties();
            listIgnoreProperties.AddProperty(GetModel(), GetArrayStringRandom());

            // Act
            var modelResult = _customContractResolver.GetObjectIgnoringProperties(listIgnoreProperties);

            // Assert
            Assert.NotNull(modelResult);
        }


        private static object GetModel()
        {
            var faker = AutoFaker.Create(b => b.WithLocale(Constants.LanguageBogus));
            var listEntities = new List<object> {
                faker.Generate<Poll>(),
                faker.Generate<PollOption>(),
                faker.Generate<Vote>()
            };

            return new Faker<object>(Constants.LanguageBogus)
                .CustomInstantiator(f => f.PickRandom(listEntities))
                .Generate();
        }

        private static string[] GetArrayStringRandom()
        {
            return AutoFaker.Create(b => b.WithLocale(Constants.LanguageBogus))
                .Generate<string[]>();
        }
    }
}