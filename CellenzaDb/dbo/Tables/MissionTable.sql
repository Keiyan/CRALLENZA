CREATE TABLE [dbo].[MissionTable] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [Intitule]               NVARCHAR (MAX) NOT NULL,
    [ConsultantTableID]      INT            NOT NULL,
    [DateDebut]              DATETIME       NULL,
    [DateFin]                DATETIME       NULL,
    [ReferenceFacturation]   NVARCHAR (MAX) NULL,
    [DateFacturation]        DATETIME       NULL,
    [Facturation]            INT            NULL,
    [Image]                  NVARCHAR (MAX) NULL,
    [Actif]                  BIT            NOT NULL,
    [InterlocuteurNom]       NVARCHAR (MAX) NULL,
    [InterlocuteurTelephone] NVARCHAR (MAX) NULL,
    [InterlocuteurEmail]     NVARCHAR (MAX) NULL,
    [ClientTableID]          INT            NOT NULL,
    CONSTRAINT [PK_MissionTable] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ClientTableMissionTable] FOREIGN KEY ([ClientTableID]) REFERENCES [dbo].[ClientTable] ([ID]),
    CONSTRAINT [FK_ConsultantTableMissionTable] FOREIGN KEY ([ConsultantTableID]) REFERENCES [dbo].[ConsultantTable] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ConsultantTableMissionTable]
    ON [dbo].[MissionTable]([ConsultantTableID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ClientTableMissionTable]
    ON [dbo].[MissionTable]([ClientTableID] ASC);

