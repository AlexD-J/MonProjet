CREATE TABLE [dbo].[Products] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (500)   NOT NULL,
    [Age] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

