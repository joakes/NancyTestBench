namespace CAI.TestBench.Business
{
    using Model;

    public interface ICaiSettingsRepository
    {
        CaiServiceSettings GetCaiServiceSettings(bool restoreToDefault);

        void UpdateServiceSettings(CaiServiceSettings settings);
    }
}
