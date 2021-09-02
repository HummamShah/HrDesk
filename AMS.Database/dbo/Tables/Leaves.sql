CREATE TABLE [dbo].[Leaves]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int,
	[ApplyDate] datetime,
	[AccpRejDate] datetime,
	[LeaveFrom] datetime,
	[LeaveTo] datetime,
	[Status] int default(0),
	[Type] int,
	[Reason] nvarchar(max),
    CONSTRAINT [FK_Leaves_Agent] FOREIGN KEY (AgentId) REFERENCES [dbo].Agent([Id])
)
