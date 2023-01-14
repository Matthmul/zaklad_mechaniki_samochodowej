CREATE TABLE [dbo].[LoginTable]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [username] NVARCHAR(50) NULL,
    [password] NVARCHAR(50) NULL,
    [isAdmin] BIT NOT NULL DEFAULT 0
)