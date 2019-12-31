using AutoBogus;
using PollChallenge.Domain.ValueObjects;
using PollChallenge.Domain.Commons;
using System.Collections.Generic;
using Xunit;
using PollChallenge.Domain.Entities;
using Bogus;
using System.Linq;

namespace PollChallenge.Domain.Tests.ValueObjects
{
    public class IgnorePropertiesTest
    {
        [Fact(DisplayName = "IgnoreProperties - instantiate class")]
        [Trait("Category", "ValueObjects")]
        public void IgnoreProperties_InstantiateClass()
        {
            // Arrange & Act
            var ignoreProperties = new IgnoreProperties(GetModel(), new List<string>());

            // Assert
            Assert.NotNull(ignoreProperties);
            Assert.NotNull(ignoreProperties.Model);
            Assert.Empty(ignoreProperties.JsonPropertyNames);
        }

        [Fact(DisplayName = "IgnoreProperties - instantiate list")]
        [Trait("Category", "ValueObjects")]
        public void IgnoreProperties_InstantiateList()
        {
            // Arrange
            var listIgnoreProperties = new ListIgnoreProperties();

            // Act
            listIgnoreProperties.AddProperty(GetModel(), "test");

            // Assert
            Assert.NotNull(listIgnoreProperties);
            Assert.NotEmpty(listIgnoreProperties);
            Assert.NotNull(listIgnoreProperties.ElementAt(0).Model);
            Assert.NotEmpty(listIgnoreProperties.ElementAt(0).JsonPropertyNames);
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
    }
}