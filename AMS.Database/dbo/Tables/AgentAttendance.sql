﻿CREATE TABLE [dbo].[AgentAttendance]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int not null,
	[IsPresent] bit not null default(0),
	[IsLate] bit not null default(0),
	[StartDateTime] datetime,
	[EndDate] datetime,
	[StartDate] datetime,
	[EndDateTime] datetime,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	[IsExcused] BIT NOT NULL DEFAULT 0, 
    [Status] BIT NOT NULL DEFAULT 0, 
    [Remarks] NVARCHAR(MAX) NULL, 
	[IsAttendanceMarked] bit default(1) NOT NULL,
	[IsAbsent] bit default(1) NOT NULL,
	[Latitude] float ,
	[Longitude] float ,
	[Type] int not null default(0),
    Constraint [FK_AgentAttendace_Agent] foreign key (AgentId) References [dbo].Agent ([Id]),
	
	
)
