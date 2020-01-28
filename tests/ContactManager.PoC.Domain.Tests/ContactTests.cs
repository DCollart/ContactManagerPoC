using ContactManagerPoC.Domain.Contact;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using ContactManagerPoC.Domain;
using Xunit;

namespace ContactManager.PoC.Domain.Tests
{
    public class ContactTests
    {
        [Fact]
        public void NewContactShouldNotBeDeletedAndCouldBeDeleted()
        {
            // Act
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item).Item;

            // Assert
            contact.IsDeleted.Should().BeFalse();
            contact.CanDelete().Should().BeTrue();
        }

        [Fact]
        public void ContactShouldBeDeleted()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item).Item;

            // Act
            contact.Delete();

            // Assert
            contact.IsDeleted.Should().BeTrue();
        }


        [Fact]
        public void DeletedContactCouldNotBeDeleted()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item).Item;

            // Act
            contact.Delete();

            // Assert
            contact.CanDelete().Should().BeFalse();
        }

        [Fact]
        public void DeleteAnAlreadyDeletedContactShouldFailed()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item).Item;
            contact.Delete();

            // Act
            Action act = () => contact.Delete();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
