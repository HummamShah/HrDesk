CREATE TABLE [dbo].[Documents]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int,
	[UploadedBy] int,
	[UploadedOn] datetime,
	[DocumentUrl] nvarchar(max),
	[Title] nvarchar(max),
	[SubTitle] nvarchar(max),
    CONSTRAINT [FK_Documents_AgentId] FOREIGN KEY (AgentId) REFERENCES [dbo].Agent([Id]),
    CONSTRAINT [FK_Documents_UploadedBy] FOREIGN KEY (UploadedBy) REFERENCES [dbo].Agent([Id])
)

