CREATE TABLE [dbo].[Order] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderDate]     DATETIME2 (7)   NOT NULL,
    [ServiceCharge] NUMERIC (19, 4) NOT NULL,
    [InclusiveTax]  NUMERIC (19, 4) NOT NULL,
    [OrderStatusId]   INT             NOT NULL,
    [BillId]        INT             NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Bill] FOREIGN KEY ([BillId]) REFERENCES [dbo].[Bill] ([Id])
);

