Feature: ContactManagement

Scenario: Create contact and change name
       Given I have created a contact named Isaac Asimov
       When I change that name for Stephen King
       Then the new name should be Stephen King

Scenario: Create contact and change address
       Given I have created a contact living in 30 - Arsene Luping street - Thief City - 12345 - United kingdom
       When I change it for 35 - Sherlock Holmes street - Detective Town - 54321 - USA
       Then the new address should be 35 - Sherlock Holmes street - Detective Town - 54321 - USA

Scenario: Create contact and delete it
       Given I have created a contact
       When I delete it
       Then it should be marked as deleted