CREATE TABLE [dbo].[Pay]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int,
	[AgentName] nvarchar(max),
	[Month] nvarchar(max),
	[MonthEnum] nvarchar(max),
	[Year] int,
	[PaySlipUrl] nvarchar(max),
	[GeneratedOn] datetime,
	[GeneratedBy] nvarchar(max),
	[BasicSalary] decimal,
	[SalaryPerDay] decimal,
	[FinalSalary] decimal,
	[TotalTaxDeduction] decimal,
	[TotalDeductionsDeduction] decimal,
	[TotalIncentiveAddition] decimal,
	[Basic] DECIMAL(18, 3) NULL, 
    [HouseRent] DECIMAL(18, 3) NULL,
    [Utilities] DECIMAL(18, 3) NULL,
    CONSTRAINT [FK_Pay_Agent] FOREIGN KEY (AgentId) REFERENCES [dbo].Agent([Id])
)
