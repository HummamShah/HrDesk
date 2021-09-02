CREATE TABLE [dbo].[Pay]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[AgentId] int,
	[Month] nvarchar(max),
	[Year] int,
	[PaySlipUrl] nvarchar(max),
	[GeneratedOn] datetime,
	[Salary] int,
	CONSTRAINT [FK_Pay_Agent] FOREIGN KEY (AgentId) REFERENCES [dbo].Agent([Id])
)
