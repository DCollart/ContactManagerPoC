﻿using ContactManagerPoC.Domain.Contact;
using FluentAssertions;
using System;
using ContactManagerPoC.Domain;
using Xunit;

namespace ContactManager.PoC.Domain.Tests
{
    public class ContactTests
    {
        private readonly Address _address;

        public ContactTests()
        {
            _address = Address.Create("Street", "Number", "City", "ZipCode", "Country").Item;
        }

        [Fact]
        public void NewContactShouldNotBeDeletedAndCouldBeDeleted()
        {
            // Act
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item, _address);

            // Assert
            contact.IsDeleted.Should().BeFalse();
            contact.CanDelete().Should().BeTrue();
        }

        [Fact]
        public void ContactShouldBeDeleted()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item, _address);

            // Act
            contact.Delete();

            // Assert
            contact.IsDeleted.Should().BeTrue();
        }


        [Fact]
        public void DeletedContactCouldNotBeDeleted()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item, _address);

            // Act
            contact.Delete();

            // Assert
            contact.CanDelete().Should().BeFalse();
        }

        [Fact]
        public void DeleteAnAlreadyDeletedContactShouldFailed()
        {
            // Arrange
            var contact = Contact.Create(Name.Create("Isaac").Item, Name.Create("Asimov").Item, _address);
            contact.Delete();

            // Act
            Action act = () => contact.Delete();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
