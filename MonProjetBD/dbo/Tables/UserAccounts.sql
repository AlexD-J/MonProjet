CREATE TABLE [dbo].[UserAccounts] (
    [Id]        UNIQUEIDENTIFIER           NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

