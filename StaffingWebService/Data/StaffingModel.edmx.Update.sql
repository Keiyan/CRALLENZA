-- Add [LiveEmail] column to table 'ConsultantTable'
ALTER TABLE [dbo].[ConsultantTable] ADD [LiveEmail] nvarchar(max)  NOT NULL;
GO

DELETE FROM [dbo].[ConsultantTable] 

INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Jonathan','Pamphile','jonathan.pamphile@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Boris','Dhambahadour','boris.dhambahadour@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','David','Tran','david.tran@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Jason','De Oliveira','jason.deoliveira@cellenza.com','jason.oliveira@live.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Georges','Damien','georges.damien@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Mick','Philippon','mick.philippon@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Olivier','Delmotte','olivier.delmotte@cellenza.com','')

SELECT * FROM [dbo].[ConsultantTable] 