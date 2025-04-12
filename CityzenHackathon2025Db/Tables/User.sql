CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY,
	[Email] NVARCHAR(64),
	[Pwd] BINARY(64),
	[Active] BIT DEFAULT 1


	CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
)

--GO

--CREATE TRIGGER [dbo].[OnDeleteUser]
--	ON [dbo].[User]
--	INSTEAD OF DELETE
--	AS
--	BEGIN
--		UPDATE User SET Active = 0
--		WHERE Id = (SELECT Id FROM deleted)
--	END