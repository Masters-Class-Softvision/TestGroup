CREATE TABLE [dbo].[MenuItem] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]               VARCHAR (100)   NOT NULL,
    [Description]        VARCHAR (100)   NULL,
    [MenuItemCategoryId] INT             NOT NULL,
    [Price]              NUMERIC (19, 4) NOT NULL,
    [IsChefRecommended]  BIT             NOT NULL,
    [CookingTimeMinutes] INT             NOT NULL,
    [PrepTimeMinutes]    INT             NOT NULL,
    [ImageLink]          NVARCHAR (250)  NULL,
    [IsActive]           BIT             NOT NULL,
    CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);

