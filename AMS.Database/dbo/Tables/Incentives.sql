CREATE TABLE [dbo].[Incentives]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Type] nvarchar(max),
	[Amount] decimal(18,3) not null,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime
)

