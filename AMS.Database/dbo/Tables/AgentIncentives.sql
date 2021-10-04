CREATE TABLE [dbo].[AgentIncentives]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int not null,
	[IncentiveId] int not null,
	[AgentName] nvarchar(max),
	[IncentiveName] nvarchar(max),
	Constraint [FK_AgentIncentives_Incentive] foreign key ([IncentiveId]) References [dbo].[Incentives] ([Id]),
	Constraint [FK_AgentIncentives_Agent] foreign key ([AgentId]) References [dbo].[Agent] ([Id])
)

