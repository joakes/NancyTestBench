using CAI.TestBench.Model;

namespace CAI.TestBench.Business.Contracts
{
    public interface ICaiSettingsRepository
    {
        CaiServiceSettings GetCaiServiceSettings(bool restoreToDefault);

        void UpdateServiceSettings(CaiServiceSettings settings);
    }
}
