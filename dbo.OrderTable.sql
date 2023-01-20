CREATE TABLE [dbo].[OrderTable]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ClientId] INT FOREIGN KEY REFERENCES LoginTable(Id) NOT NULL,
    [Brand]                 NVARCHAR (50) NOT NULL,
    [Model]                 NVARCHAR (50) NOT NULL,
    [Fix]                   BIT           DEFAULT ((0)) NOT NULL,
    [Review]                BIT           DEFAULT ((0)) NOT NULL,
    [Assembly]              BIT           DEFAULT ((0)) NOT NULL,
    [TechnicalConsultation] BIT           DEFAULT ((0)) NOT NULL,
    [Training]              BIT           DEFAULT ((0)) NOT NULL,
    [OrderingParts]         BIT           DEFAULT ((0)) NOT NULL,
    [NrVIN]                 NVARCHAR (50) NOT NULL,
    [ProductionYear]        INT NOT NULL,
    [RegistrationNumber]    NVARCHAR (50) NOT NULL,
    [EngineCapacity]        INT NOT NULL,
    [OrderState]            INT NOT NULL,
)