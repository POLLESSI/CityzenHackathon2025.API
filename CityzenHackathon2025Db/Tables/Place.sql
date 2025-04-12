CREATE TABLE [dbo].[Place]
(
	[Id] INT IDENTITY,
	[Name] NVARCHAR (64),
	[Type] NVARCHAR (32),
	[Indoor] BIT,
	[Latitude] DECIMAL(8, 6),
	[Longitude] DECIMAL(9, 6),
	[Capacity] INT,
	[Tag] NVARCHAR(16),
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_Place] PRIMARY KEY ([Id])
)

GO

CREATE TRIGGER [dbo].[OnDeletePlace]
	ON [dbo].[Place]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE Place SET Active = 0
		WHERE Id = (SELECT Id FROM deleted)
	END