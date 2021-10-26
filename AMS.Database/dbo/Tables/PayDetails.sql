CREATE TABLE [dbo].[PayDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[PaySlipId] int not null,
	[Description] nvarchar(max),
	[Type] nvarchar(max),
	[Amount] decimal(18,3) not null,
	[Remarks] nvarchar(max),
	CONSTRAINT [FK_PayDetails_Pay] FOREIGN KEY (PaySlipId) REFERENCES [dbo].Pay([Id])
)
