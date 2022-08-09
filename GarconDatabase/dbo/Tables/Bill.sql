CREATE TABLE [dbo].[Bill] (
    [Id]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TotalPrice] NUMERIC (19, 4) NOT NULL,
    [BillDate]   DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED ([Id] ASC)
);

