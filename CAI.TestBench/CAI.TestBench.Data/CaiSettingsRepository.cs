using System.Linq;

namespace CAI.TestBench.Data
{
    using System;
    using Business;
    using Model;
    using Raven.Client;

    public class CaiSettingsRepository : ICaiSettingsRepository 
    {
        private readonly IDocumentStore _documentStore;

        private CaiServiceSettings DefaultServiceSettings
        {
            get
            {
                return new CaiServiceSettings()
                {
                    BranchNumber = 992,
                    ServiceId = 602,
                    Username = "NET",
                    Organisation = "TEST_PNCS",
                    LastUpdated = DateTime.Now,
                    AreDefault = true
                };   
            }
        }

        public CaiSettingsRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public CaiServiceSettings GetCaiServiceSettings(bool restoreToDefault)
        {
            var defaultSettings = DefaultServiceSettings;

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
