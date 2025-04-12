CREATE TABLE [dbo].[GptInteractions]
(
	[Id] INT IDENTITY,
	[Prompt] NVARCHAR(MAX),
	[Response] NVARCHAR(MAX),
	[CreatedAt] DATETIME DEFAULT GETDATE(),
	[Active] BIT DEFAULT 1,

	CONSTRAINT [PK_GptInteractions] PRIMARY KEY ([Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteGptInteractions]
	ON [dbo].[GptInteractions]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE GptInteractions SET Active = 0
		WHERE Id = (SELECT Id FROM deleted)
	END
