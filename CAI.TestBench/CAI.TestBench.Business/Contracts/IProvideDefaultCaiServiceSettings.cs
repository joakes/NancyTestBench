using CAI.TestBench.Model;

namespace CAI.TestBench.Business.Contracts
{
    public interface IProvideDefaultCaiServiceSettings
    {
        CaiServiceSettings GetDefaultCaiServiceSettings();
    }
}
