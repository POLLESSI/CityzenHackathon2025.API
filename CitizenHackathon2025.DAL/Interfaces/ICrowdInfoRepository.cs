using CitizenHackathon2025.DAL.Entities;

namespace CitizenHackathon2025.DAL.Interfaces
{
    public interface ICrowdInfoRepository
    {
        Task<CrowdInfo?> SaveCrowdInfoAsync(CrowdInfo crowdInfo);
        Task<IEnumerable<CrowdInfo>> GetAllCrowdInfoAsync();
        Task<CrowdInfo?> GetCrowdInfoByIdAsync(int id);
        Task<bool> DeleteCrowdInfoAsync(int id);
    }
}
