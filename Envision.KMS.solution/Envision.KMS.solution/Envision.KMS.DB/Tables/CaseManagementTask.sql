CREATE TABLE [dbo].[CaseManagementTask]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [CaseId] NVARCHAR(250) NULL, 
    [CaseTitle] NVARCHAR(500) NULL, 
    [AssignedToAccount] NVARCHAR(250) NULL, 
    [AssignedToDisplayName] NVARCHAR(250) NULL, 
    [Status] NVARCHAR(50) NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [Result] NVARCHAR(500) NULL, 
    [Created] DATETIME NOT NULL, 
    [Modified] DATETIME NOT NULL, 
    [CaseType] NVARCHAR(500) NULL, 
    [WebID] NVARCHAR(250) NULL, 
    [CreatedBy] NVARCHAR(250) NULL, 
    [CreatedByAccount] NVARCHAR(250) NULL, 
    [AssignedToEmail] NVARCHAR(250) NULL
)
