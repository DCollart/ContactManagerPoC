ALTER TABLE Contacts ADD Street varchar(255)
ALTER TABLE Contacts ADD Number varchar(255)
ALTER TABLE Contacts ADD City varchar(255)
ALTER TABLE Contacts ADD ZipCode varchar(255)
ALTER TABLE Contacts ADD Country varchar(255)
GO

UPDATE Contacts SET Street = 'NoStreet', Number = 'NoNumber', City = 'NoCity', ZipCode = 'NoZipCode', Country = 'NoCountry'

ALTER TABLE Contacts ALTER COLUMN Street varchar(255) not null
ALTER TABLE Contacts ALTER COLUMN Number varchar(255) not null
ALTER TABLE Contacts ALTER COLUMN City varchar(255) not null
ALTER TABLE Contacts ALTER COLUMN ZipCode varchar(255) not null
ALTER TABLE Contacts ALTER COLUMN Country varchar(255) not null
