using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public interface ISaveProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISaveProgress : ISaveProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}