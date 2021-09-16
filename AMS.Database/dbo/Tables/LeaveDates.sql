CREATE TABLE [dbo].[LeaveDates]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[LeaveId] int not null,
	[Dates] DateTime,
    CONSTRAINT [FK_LeaveDates_Leaves] FOREIGN KEY ([LeaveId]) REFERENCES [dbo].Leaves ([Id])
)
