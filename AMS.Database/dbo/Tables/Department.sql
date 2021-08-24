CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Description] nvarchar(max),
	[CreatedBy] nvarchar(max),
	[CreatedAt] datetime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] datetime,
	[Date] datetime
)
