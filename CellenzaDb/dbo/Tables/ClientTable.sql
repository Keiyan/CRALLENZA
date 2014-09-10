CREATE TABLE [dbo].[ClientTable] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [RaisonSociale] NVARCHAR (MAX) NULL,
    [CodeClient]    NVARCHAR (MAX) NULL,
    [Image]         NVARCHAR (MAX) NULL,
    [Actif]         BIT            NOT NULL,
    CONSTRAINT [PK_ClientTable] PRIMARY KEY CLUSTERED ([ID] ASC)
);

