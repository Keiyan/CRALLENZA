CREATE TABLE [dbo].[ConsultantTable] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [Nom]               NVARCHAR (MAX) NOT NULL,
    [Prenom]            NVARCHAR (MAX) NOT NULL,
    [DateNaissance]     DATETIME       NULL,
    [NoSecu]            NVARCHAR (MAX) NULL,
    [Email]             NVARCHAR (MAX) NOT NULL,
    [DateArrivee]       DATETIME       NULL,
    [DateDepart]        DATETIME       NULL,
    [Image]             NVARCHAR (MAX) NULL,
    [Actif]             BIT            NOT NULL,
    [Admin]             BIT            NOT NULL,
    [Poste]             NVARCHAR (MAX) NULL,
    [AdresseRue]        NVARCHAR (MAX) NULL,
    [AdresseComplement] NVARCHAR (MAX) NULL,
    [AdresseCodePostal] NVARCHAR (MAX) NULL,
    [AdresseVille]      NVARCHAR (MAX) NULL,
    [LiveEmail]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ConsultantTable] PRIMARY KEY CLUSTERED ([ID] ASC)
);

