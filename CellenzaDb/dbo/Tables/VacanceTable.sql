CREATE TABLE [dbo].[VacanceTable] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [ConsultantTableID] INT            NOT NULL,
    [Type]              INT            NOT NULL,
    [DateDebut]         DATETIME       NOT NULL,
    [DateFin]           DATETIME       NOT NULL,
    [Motif]             NVARCHAR (MAX) NULL,
    [Commentaire]       NVARCHAR (MAX) NULL,
    [Status]            INT            NOT NULL,
    [Actif]             BIT            NOT NULL,
    CONSTRAINT [PK_VacanceTable] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_VacanceTableConsultantTable] FOREIGN KEY ([ConsultantTableID]) REFERENCES [dbo].[ConsultantTable] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_VacanceTableConsultantTable]
    ON [dbo].[VacanceTable]([ConsultantTableID] ASC);

