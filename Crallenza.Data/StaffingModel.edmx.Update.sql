-- Add [LiveEmail] column to table 'ConsultantTable'
ALTER TABLE [dbo].[ConsultantTable] ADD [LiveEmail] nvarchar(max)  NOT NULL;
GO

DELETE FROM [dbo].[ConsultantTable] 

INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Alexandre','Démoulin','alexandre.demoulin@cellenza.com','alexandre.demoulin@cellenza.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Alexandre','Plassais','alexandre.plassais@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Anne','Pedro','anne.pedro@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Arnaud','Hego','arnaud.hego@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Arnaud','Villenave','arnaud.villenave@cellenza.com','avillenave@hotmail.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Boris','Dhambahadour','boris.dhambahadour@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','David','Tran','david.tran@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Eric','Galiano','eric.galiano@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Fathi','Bellahcene','fathi.bellahcene@cellenza.com','fbellahc@hotmail.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Jason','De Oliveira','jason.deoliveira@cellenza.com','jason.oliveira@live.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Jonathan','Pamphile','jonathan.pamphile@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Kevin','Eliçagoyen','kevin.elicagoyen@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Michel','Perfetti','michel.perfetti@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Nicholas','Suter','nicholas.suter@cellenza.com','nicholas.suter@cellenza.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,1,'20130501','Pierre-Henri','Gache','pierre-henri.gache@cellenza.com','pierrehenri.gache@outlook.com')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Romain','Lassalle','romain.lassalle@cellenza.com','')
INSERT INTO [dbo].[ConsultantTable] (Actif,Admin,DateArrivee,Nom,Prenom,Email,LiveEmail) VALUES (1,0,'20130501','Simon','Gilquin','simon.gilquin@cellenza.com','')

SELECT * FROM [dbo].[ConsultantTable] 