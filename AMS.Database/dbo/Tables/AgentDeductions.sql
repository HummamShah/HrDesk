CREATE TABLE [dbo].[AgentDeductions]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int not null,
	[DeductionId] int not null,
	[AgentName] nvarchar(max),
	[DeductionName] nvarchar(max),
	Constraint [FK_AgentDeductions_Deduction] foreign key ([DeductionId]) References [dbo].[Deductions] ([Id]),
	Constraint [FK_AgentDeductions_Agent] foreign key ([AgentId]) References [dbo].[Agent] ([Id])
)
