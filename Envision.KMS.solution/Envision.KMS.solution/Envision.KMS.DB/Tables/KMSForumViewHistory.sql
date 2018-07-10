CREATE TABLE [dbo].[KMSForumViewHistory]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserName] NVARCHAR(100) NULL, 
    [Created] DATETIME NULL, 
    [ItemId] NVARCHAR(50) NULL, 
)
