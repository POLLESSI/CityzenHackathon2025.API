CREATE TABLE [dbo].[TrafficCondition]
(
	[Id] INT IDENTITY,
	[Latitude] DECIMAL(8, 2),
	[Longitude] DECIMAL(9, 3),
	[DateCondition] DATE,
	[CongestionLevel] NVARCHAR(32),
	[IncidentType] NVARCHAR(32),
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_TrafficCondition] PRIMARY KEY ([Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteTrafficCondition]
	ON [dbo].[TrafficCondition]
	INSTEAD OF DELETE
	AS
	BEGIN
		UPDATE TrafficCondition SET Active = 0
		WHERE Id = (SELECT Id FROM deleted)
	END
