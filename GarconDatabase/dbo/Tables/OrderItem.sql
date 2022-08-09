CREATE TABLE [dbo].[OrderItem] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderId]           INT NOT NULL,
    [OrderItemStatusId] INT NOT NULL,
    [MenuItemId]        INT NOT NULL,
    [Quantity]          INT NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItem_MenuItem] FOREIGN KEY ([MenuItemId]) REFERENCES [dbo].[MenuItem] ([Id]),
    CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id])
);

