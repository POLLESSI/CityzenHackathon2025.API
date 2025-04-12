using CitizenHackathon2025.DAL.Entities;
using CityzenHackathon2025.API.DTOs;

namespace CityzenHackathon2025.API.Tools
{
    public static class Mappers
    {
#nullable disable
        public static EventDTO MapToEventDTO(this Event eventEntity)
        {
            return new EventDTO
            {
                Name = eventEntity.Name,
                Latitude = eventEntity.Latitude,
                Longitude = eventEntity.Longitude,
                DateEvent = eventEntity.DateEvent,
                ExpectedCrowd = eventEntity.ExpectedCrowd
            };
        }
        public static PlaceDTO MapToPlaceDTO(this Place place)
        {
            return new PlaceDTO
            {
                Name = place.Name,
                Type = place.Type,
                Indoor = place.Indoor,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Coordonates = place.Coordonates,
                Capacity = place.Capacity,
                Tags = place.Tags
            };
        }
        public static SuggestionDTO MapToSuggestionDTO(this Suggestion suggestion)
        {
            return new SuggestionDTO
            {
                UserId = suggestion.UserId,
                DateSuggestion = suggestion.DateSuggestion,
                OriginalPlace = suggestion.OriginalPlace,
                SuggestedAlternatives = suggestion.SuggestedAlternatives,
                Reason = suggestion.Reason
            };
        }
        public static TrafficConditionDTO MapToTrafficConditionDTO(this TrafficCondition trafficCondition)
        {
            return new TrafficConditionDTO
            {
                Latitude = trafficCondition.Latitude,
                Longitude = trafficCondition.Longitude,
                DateCondition = trafficCondition.DateCondition,
                CongestionLevel = trafficCondition.CongestionLevel,
                IncidentType = trafficCondition.IncidentType
            };
        }
        public static WeatherForecastDTO MapToWeatherForecastDTO(this WeatherForecast weatherForecast)
        {
            return new WeatherForecastDTO
            {
                //DateWeather = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary,
                //RainfallMm = weatherForecast.RainfallMm,
                //Humidity = weatherForecast.Humidity,
                WindSpeedKmh = weatherForecast.WindSpeedKmh
            };
        }
    }
}
