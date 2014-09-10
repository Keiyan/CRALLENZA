CREATE TABLE [dbo].[ActiviteTable] (
    [ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [CompteRenduActiviteTableID] INT            NOT NULL,
    [Type]                       INT            NOT NULL,
    [Comment]                    NVARCHAR (MAX) NULL,
    [Afternoon]                  BIT            NOT NULL,
    [MissionTableID]             INT            NULL,
    [Day]                        INT            NOT NULL,
    CONSTRAINT [PK_ActiviteTable] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ActiviteCompteRenduActivite] FOREIGN KEY ([CompteRenduActiviteTableID]) REFERENCES [dbo].[CompteRenduActiviteTable] ([ID]),
    CONSTRAINT [FK_ActivityTableMissionTable] FOREIGN KEY ([MissionTableID]) REFERENCES [dbo].[MissionTable] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ActiviteCompteRenduActivite]
    ON [dbo].[ActiviteTable]([CompteRenduActiviteTableID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ActivityTableMissionTable]
    ON [dbo].[ActiviteTable]([MissionTableID] ASC);

