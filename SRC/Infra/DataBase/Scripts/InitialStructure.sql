CREATE DATABASE [BankReconciliation];

CREATE TABLE [dbo].[Transaction](
	Id int identity primary key,
	Type VARCHAR(255) NOT NULL,
	DatePosted DATETIME NOT NULL,
	Amount DECIMAL(19,5) NOT NULL,
	Memo VARCHAR(255) NOT NULL
);
