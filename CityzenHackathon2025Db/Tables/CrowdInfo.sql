CREATE TABLE [dbo].[CrowdInfo]
(
	[Id] INT IDENTITY,
	[LocationName] NVARCHAR(64) NOT NULL,
	[Latitude] DECIMAL(8, 6) NOT NULL,
	[Longitude] DECIMAL(9, 6) NOT NULL,
	[CrowdLevel] INT NOT NULL,
	[Timestamp] DATETIME NOT NULL,
	[Active] BIT DEFAULT 1,

	CONSTRAINT [PK_CrowdInfo] PRIMARY KEY ([Id]),
)

GO

CREATE TRIGGER [dbo].[OnDeleteCrowdInfo]
	ON [dbo].[CrowdInfo]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE CrowdInfo SET Active = 0
		WHERE Id IN (SELECT Id FROM deleted)
	END