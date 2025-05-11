using CitizenHackathon2025.DAL.Entities;
using CityzenHackathon2025.API.DTOs;
using CityzenHackathon2025.Shared.DTOs;

namespace CityzenHackathon2025.API.Tools
{
    public static class Mappers
    {
#nullable disable
        public static CrowdInfoDTO MapToCrowdInfoDTO(this CrowdInfo crowdInfo)
        {
            return new CrowdInfoDTO
            {
                LocationName = crowdInfo.LocationName,
                Latitude = crowdInfo.Latitude,
                Longitude = crowdInfo.Longitude,
                CrowdLevel = crowdInfo.CrowdLevel
            };
        }

        public static CrowdInfo MapToCrowdInfo(this CrowdInfoDTO crowdInfoDTO)
        {
            return new CrowdInfo
            {
                LocationName = crowdInfoDTO.LocationName,
                Latitude = crowdInfoDTO.Latitude,
                Longitude = crowdInfoDTO.Longitude,
                CrowdLevel = crowdInfoDTO.CrowdLevel,
                Timestamp = DateTime.UtcNow
            };
        }
        public static EventDTO MapToEventDTO(this Event eventEntity)
        {
            return new EventDTO
            {
                Name = eventEntity.Name,
                Latitude = eventEntity.Latitude,
                Longitude = eventEntity.Longitude,
                DateEvent = eventEntity.DateEvent,
                ExpectedCrowd = eventEntity.ExpectedCrowd,
                IsOutdoor = eventEntity.IsOutdoor
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

        // DTO → Entity (dans Tools/Mappers.cs)
        public static WeatherForecast MapToWeatherForecast(this WeatherForecastDTO dto)
        {
            return new WeatherForecast
            {
                DateWeather = dto.DateWeather,
                TemperatureC = dto.TemperatureC,
                Summary = dto.Summary,
                RainfallMm = dto.RainfallMm,
                Humidity = dto.Humidity,
                WindSpeedKmh = dto.WindSpeedKmh,
                Active = true
            };
        }

        // Entity → API-DTO
        public static WeatherForecastDTO MapToWeatherForecastDTO(this WeatherForecast entity)
        {
            return new WeatherForecastDTO
            {
                DateWeather = entity.DateWeather,
                TemperatureC = entity.TemperatureC,
                Summary = entity.Summary,
                RainfallMm = entity.RainfallMm,
                Humidity = entity.Humidity,
                WindSpeedKmh = entity.WindSpeedKmh
            };
        }
    }
}
