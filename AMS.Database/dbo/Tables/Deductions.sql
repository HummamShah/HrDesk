CREATE TABLE [dbo].[Deductions]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[Type] nvarchar(max),
	[Order] int,
	[Amount] decimal(18,3) not null,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime
)

