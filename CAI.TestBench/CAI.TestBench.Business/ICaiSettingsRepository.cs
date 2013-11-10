namespace CAI.TestBench.Business
{
    using Model;

    public interface ICaiSettingsRepository
    {
        CaiServiceSettings GetDefaultServiceSettings();

        void UpdateServiceSettings(CaiServiceSettings settings);
    }
}
