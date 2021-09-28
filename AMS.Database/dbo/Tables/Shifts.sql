CREATE TABLE [dbo].[Shifts]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar(max),
	[StartTime] DateTime,
	[EndTime] DateTime
)
