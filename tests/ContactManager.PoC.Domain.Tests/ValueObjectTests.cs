using ContactManagerPoC.Domain.Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContactManager.PoC.Domain.Tests
{
    public class ValueObjectTests
    {
        class FakeValueObject : ValueObject
        {
            public int A { get; set; }
            public string B { get; set; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return A;
                yield return B;
            }
        }

        class FakeValueObject2 : ValueObject
        {
            protected override IEnumerable<object> GetEqualityComponents()
            {
                return Enumerable.Empty<object>();
            }
        }

        [Fact]
        public void SimilarValueObjectsShouldEquals()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 5, B = "Hello" };

            // Act
            var areEquals = valueObject1 == valueObject2;

            // Assert
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void NotSimilarValueObjectsShouldNotEquals()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 6, B = "World" };

            // Act
            var areEquals = valueObject1 == valueObject2;

            // Assert
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void SimilarValueObjectsShouldHaveSameHashCode()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 5, B = "Hello" };

            // Act
            var sameHashCode = valueObject1.GetHashCode() == valueObject2.GetHashCode();

            // Assert
            sameHashCode.Should().BeTrue();
        }

        [Fact]
        public void SimilarValueObjectsShouldNotHaveSameHashCode()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 6, B = "World" };

            // Act
            var sameHashCode = valueObject1.GetHashCode() == valueObject2.GetHashCode();

            // Assert
            sameHashCode.Should().BeFalse();
        }

        [Fact]
        public void NullObjectShouldNeverBeSimilarToNotNullObject()
        {
            // Arrange
            var valueObject = new FakeValueObject2();

            // Act
            var areSimilar = valueObject == (FakeValueObject2)null;

            // Assert
            areSimilar.Should().BeFalse();
        }
    }
}
