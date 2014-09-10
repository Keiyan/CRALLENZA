
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/22/2013 18:32:45
-- Generated from EDMX file: C:\Users\Jonathan\Desktop\DEV\Main\Staffing\Cellenza.Service\Data\StaffingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
--USE [STAFFING];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ConsultantTableMissionTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MissionTable] DROP CONSTRAINT [FK_ConsultantTableMissionTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ActiviteCompteRenduActivite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActiviteTable] DROP CONSTRAINT [FK_ActiviteCompteRenduActivite];
GO
IF OBJECT_ID(N'[dbo].[FK_CompteRenduActiviteConsultantTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompteRenduActiviteTable] DROP CONSTRAINT [FK_CompteRenduActiviteConsultantTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityTableMissionTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActiviteTable] DROP CONSTRAINT [FK_ActivityTableMissionTable];
GO
IF OBJECT_ID(N'[dbo].[FK_VacanceTableConsultantTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VacanceTable] DROP CONSTRAINT [FK_VacanceTableConsultantTable];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientTableMissionTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MissionTable] DROP CONSTRAINT [FK_ClientTableMissionTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ConsultantTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsultantTable];
GO
IF OBJECT_ID(N'[dbo].[MissionTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MissionTable];
GO
IF OBJECT_ID(N'[dbo].[ActiviteTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActiviteTable];
GO
IF OBJECT_ID(N'[dbo].[CompteRenduActiviteTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompteRenduActiviteTable];
GO
IF OBJECT_ID(N'[dbo].[VacanceTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VacanceTable];
GO
IF OBJECT_ID(N'[dbo].[ClientTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientTable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ConsultantTable'
CREATE TABLE [dbo].[ConsultantTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [Prenom] nvarchar(max)  NOT NULL,
    [DateNaissance] datetime  NULL,
    [NoSecu] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DateArrivee] datetime  NULL,
    [DateDepart] datetime  NULL,
    [Image] nvarchar(max)  NULL,
    [Actif] bit  NOT NULL,
    [Admin] bit  NOT NULL,
    [Poste] nvarchar(max)  NULL,
    [AdresseRue] nvarchar(max)  NULL,
    [AdresseComplement] nvarchar(max)  NULL,
    [AdresseCodePostal] nvarchar(max)  NULL,
    [AdresseVille] nvarchar(max)  NULL
);
GO

-- Creating table 'MissionTable'
CREATE TABLE [dbo].[MissionTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Intitule] nvarchar(max)  NOT NULL,
    [ConsultantTableID] int  NOT NULL,
    [DateDebut] datetime  NULL,
    [DateFin] datetime  NULL,
    [ReferenceFacturation] nvarchar(max)  NULL,
    [DateFacturation] datetime  NULL,
    [Facturation] int  NULL,
    [Image] nvarchar(max)  NULL,
    [Actif] bit  NOT NULL,
    [InterlocuteurNom] nvarchar(max)  NULL,
    [InterlocuteurTelephone] nvarchar(max)  NULL,
    [InterlocuteurEmail] nvarchar(max)  NULL,
    [ClientTableID] int  NOT NULL
);
GO

-- Creating table 'ActiviteTable'
CREATE TABLE [dbo].[ActiviteTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CompteRenduActiviteTableID] int  NOT NULL,
    [Type] int  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [Afternoon] bit  NOT NULL,
    [MissionTableID] int  NULL,
    [Day] int  NOT NULL
);
GO

-- Creating table 'CompteRenduActiviteTable'
CREATE TABLE [dbo].[CompteRenduActiviteTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ConsultantTableID] int  NOT NULL,
    [Year] int  NOT NULL,
    [Month] int  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'VacanceTable'
CREATE TABLE [dbo].[VacanceTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ConsultantTableID] int  NOT NULL,
    [Type] int  NOT NULL,
    [DateDebut] datetime  NOT NULL,
    [DateFin] datetime  NOT NULL,
    [Motif] nvarchar(max)  NULL,
    [Commentaire] nvarchar(max)  NULL,
    [Status] int  NOT NULL,
    [Actif] bit  NOT NULL
);
GO

-- Creating table 'ClientTable'
CREATE TABLE [dbo].[ClientTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RaisonSociale] nvarchar(max)  NULL,
    [CodeClient] nvarchar(max)  NULL,
    [Image] nvarchar(max)  NULL,
    [Actif] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ConsultantTable'
ALTER TABLE [dbo].[ConsultantTable]
ADD CONSTRAINT [PK_ConsultantTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MissionTable'
ALTER TABLE [dbo].[MissionTable]
ADD CONSTRAINT [PK_MissionTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActiviteTable'
ALTER TABLE [dbo].[ActiviteTable]
ADD CONSTRAINT [PK_ActiviteTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CompteRenduActiviteTable'
ALTER TABLE [dbo].[CompteRenduActiviteTable]
ADD CONSTRAINT [PK_CompteRenduActiviteTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'VacanceTable'
ALTER TABLE [dbo].[VacanceTable]
ADD CONSTRAINT [PK_VacanceTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ClientTable'
ALTER TABLE [dbo].[ClientTable]
ADD CONSTRAINT [PK_ClientTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ConsultantTableID] in table 'MissionTable'
ALTER TABLE [dbo].[MissionTable]
ADD CONSTRAINT [FK_ConsultantTableMissionTable]
    FOREIGN KEY ([ConsultantTableID])
    REFERENCES [dbo].[ConsultantTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsultantTableMissionTable'
CREATE INDEX [IX_FK_ConsultantTableMissionTable]
ON [dbo].[MissionTable]
    ([ConsultantTableID]);
GO

-- Creating foreign key on [CompteRenduActiviteTableID] in table 'ActiviteTable'
ALTER TABLE [dbo].[ActiviteTable]
ADD CONSTRAINT [FK_ActiviteCompteRenduActivite]
    FOREIGN KEY ([CompteRenduActiviteTableID])
    REFERENCES [dbo].[CompteRenduActiviteTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActiviteCompteRenduActivite'
CREATE INDEX [IX_FK_ActiviteCompteRenduActivite]
ON [dbo].[ActiviteTable]
    ([CompteRenduActiviteTableID]);
GO

-- Creating foreign key on [ConsultantTableID] in table 'CompteRenduActiviteTable'
ALTER TABLE [dbo].[CompteRenduActiviteTable]
ADD CONSTRAINT [FK_CompteRenduActiviteConsultantTable]
    FOREIGN KEY ([ConsultantTableID])
    REFERENCES [dbo].[ConsultantTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompteRenduActiviteConsultantTable'
CREATE INDEX [IX_FK_CompteRenduActiviteConsultantTable]
ON [dbo].[CompteRenduActiviteTable]
    ([ConsultantTableID]);
GO

-- Creating foreign key on [MissionTableID] in table 'ActiviteTable'
ALTER TABLE [dbo].[ActiviteTable]
ADD CONSTRAINT [FK_ActivityTableMissionTable]
    FOREIGN KEY ([MissionTableID])
    REFERENCES [dbo].[MissionTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityTableMissionTable'
CREATE INDEX [IX_FK_ActivityTableMissionTable]
ON [dbo].[ActiviteTable]
    ([MissionTableID]);
GO

-- Creating foreign key on [ConsultantTableID] in table 'VacanceTable'
ALTER TABLE [dbo].[VacanceTable]
ADD CONSTRAINT [FK_VacanceTableConsultantTable]
    FOREIGN KEY ([ConsultantTableID])
    REFERENCES [dbo].[ConsultantTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VacanceTableConsultantTable'
CREATE INDEX [IX_FK_VacanceTableConsultantTable]
ON [dbo].[VacanceTable]
    ([ConsultantTableID]);
GO

-- Creating foreign key on [ClientTableID] in table 'MissionTable'
ALTER TABLE [dbo].[MissionTable]
ADD CONSTRAINT [FK_ClientTableMissionTable]
    FOREIGN KEY ([ClientTableID])
    REFERENCES [dbo].[ClientTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientTableMissionTable'
CREATE INDEX [IX_FK_ClientTableMissionTable]
ON [dbo].[MissionTable]
    ([ClientTableID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------