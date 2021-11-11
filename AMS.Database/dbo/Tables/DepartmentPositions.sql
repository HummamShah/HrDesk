CREATE TABLE [dbo].[DepartmentPositions]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[DeptId] int not null,
	[DeptName] nvarchar(max),
	[PositionName] nvarchar(max),
	[PositionOrder] int,
	[JobDescription] nvarchar(max),
	[CreatedBy] nvarchar(max),
	[CreatedAt] datetime,

	Constraint [FK_DepartmentPosition_Department] foreign key ([DeptId]) References [dbo].[Department] ([Id])
)