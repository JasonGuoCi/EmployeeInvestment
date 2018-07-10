CREATE TABLE [dbo].[KMSForum]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ItemId] NVARCHAR(50) NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [CreatedBy] NVARCHAR(100) NULL, 
    [Created] DATETIME NOT NULL, 
    [CreatedUserName] NVARCHAR(100) NULL, 
    [Title] NVARCHAR(MAX) NULL 
)
