CREATE TABLE [dbo].[AgentTaxes]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int not null,
	[TaxId] int not null,
	[AgentName] nvarchar(max),
	[TaxName] nvarchar(max),
	Constraint [FK_AgentTaxes_Tax] foreign key ([TaxId]) References [dbo].[Tax] ([Id]),
	Constraint [FK_AgentTaxes_Agent] foreign key ([AgentId]) References [dbo].[Agent] ([Id])
)
