USE [MovieDB]
GO

INSERT INTO [dbo].[MovieCredit]
           ([Id]
           ,[Name])
     VALUES
           (NEWID(), 'Director'), 
		   (NEWID(), 'Actor')
GO


