using System.Linq;
using CAI.TestBench.Business.Contracts;

namespace CAI.TestBench.Data
{
    using System;
    using Model;
    using Raven.Client;

    public class CaiSettingsRepository : ICaiSettingsRepository 
    {
        private readonly IDocumentStore _documentStore;
        private readonly IProvideDefaultCaiServiceSettings _defaultCaiServiceSettings;

        public CaiSettingsRepository(IDocumentStore documentStore, IProvideDefaultCaiServiceSettings defaultCaiServiceSettings)
        {
            _documentStore = documentStore;
            _defaultCaiServiceSettings = defaultCaiServiceSettings;
        }

        public CaiServiceSettings GetCaiServiceSettings(bool restoreToDefault)
        {
            var defaultSettings = _defaultCaiServiceSettings.GetDefaultCaiServiceSettings();

            using (var session = _documentStore.OpenSession())
            {
                var currentSettings = session.Query<CaiServiceSettings>().FirstOrDefault();
                
                if (currentSettings == null)
                {
                    session.Store(defaultSettings);
                    session.SaveChanges();
                    return defaultSettings;
                }

                if (!restoreToDefault) 
                    return currentSettings;
                
                if (currentSettings.Equals(defaultSettings))
                    return currentSettings;

                session.Delete(currentSettings);
                session.Store(defaultSettings);
                session.SaveChanges();
                return defaultSettings;
            }
        }

        public void UpdateServiceSettings(CaiServiceSettings settings)
        {
            using (var session = _documentStore.OpenSession())
            {
                var currentSettings = session.Query<CaiServiceSettings>().First();

                if (currentSettings.Equals(settings))
                    return;

                currentSettings.BranchNumber = settings.BranchNumber;
                currentSettings.LastUpdated = DateTime.Now;
                currentSettings.Organisation = settings.Organisation;
                currentSettings.ServiceId = settings.ServiceId;
                currentSettings.Username = settings.Username;
                currentSettings.AreDefault = false;

                session.SaveChanges();
            }
        }
    }
}
