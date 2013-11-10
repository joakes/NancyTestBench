namespace CAI.TestBench.Data
{
    using System;
    using Business;
    using Model;
    using Raven.Client;

    public class CaiSettingsRepository : ICaiSettingsRepository 
    {
        private readonly IDocumentStore _documentStore;

        public CaiSettingsRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public CaiServiceSettings GetDefaultServiceSettings()
        {
            throw new NotImplementedException();
        }

        public void UpdateServiceSettings(CaiServiceSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
