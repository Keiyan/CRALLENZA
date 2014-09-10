CREATE TABLE [dbo].[CompteRenduActiviteTable] (
    [ID]                INT IDENTITY (1, 1) NOT NULL,
    [ConsultantTableID] INT NOT NULL,
    [Year]              INT NOT NULL,
    [Month]             INT NOT NULL,
    [Status]            INT NOT NULL,
    CONSTRAINT [PK_CompteRenduActiviteTable] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CompteRenduActiviteConsultantTable] FOREIGN KEY ([ConsultantTableID]) REFERENCES [dbo].[ConsultantTable] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CompteRenduActiviteConsultantTable]
    ON [dbo].[CompteRenduActiviteTable]([ConsultantTableID] ASC);

