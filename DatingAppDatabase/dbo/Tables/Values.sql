CREATE TABLE [dbo].[Values] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Values] PRIMARY KEY CLUSTERED ([Id] ASC)
);

