CREATE TABLE [dbo].[Articles] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ProductId]    UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (500)   NULL,
    [Title]       NVARCHAR (100)   NOT NULL,
    [Url] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Articles_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Articles_ProductId]
    ON [dbo].[Articles]([ProductId] ASC);

