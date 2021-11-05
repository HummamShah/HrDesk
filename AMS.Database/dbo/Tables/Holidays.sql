CREATE TABLE [dbo].[Holidays]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Date] DateTime NOT NULL,
	[Remarks] nvarchar(max),
	[AddedBy] nvarchar(max),
	[AddedAt] DateTime 
)
