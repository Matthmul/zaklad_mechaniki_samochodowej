CREATE TABLE [dbo].[LoginTable] (
    [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Username] NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [PhoneNumber] NVARCHAR (9) NULL,
    [EmialAddress] NVARCHAR (50) NULL,
    [IsAdmin] BIT DEFAULT ((0)) NOT NULL
);

