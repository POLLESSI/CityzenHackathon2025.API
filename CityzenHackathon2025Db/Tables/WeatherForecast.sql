CREATE TABLE [dbo].[WeatherForecast]
(
	[Id] INT IDENTITY,
	[DateWeather] DATETIME ,
	[TemperatureC] INT,
	[TemperatureF] INT,
	[Summary] NVARCHAR,
	[RainfallNm] FLOAT,
	[Humidity] INT,
	[WindSpeedKmh] FLOAT,
	[Active] BIT DEFAULT 1

	CONSTRAINT [PK_WeatherForecast] PRIMARY KEY ([Id])
)

GO

CREATE TRIGGER [dbo].[OnDeleteWeatherForecast]
    ON [dbo].[WeatherForecast]
	INSTEAD OF DELETE
	AS 
	BEGIN
		UPDATE WeatherForecast SET Active = 0
		WHERE Id IN (SELECT Id FROM deleted)
	END
