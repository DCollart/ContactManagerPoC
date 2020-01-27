CREATE TABLE Contacts 
(
	Id integer identity(1,1) primary key,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	IsDeleted bit not null
)
