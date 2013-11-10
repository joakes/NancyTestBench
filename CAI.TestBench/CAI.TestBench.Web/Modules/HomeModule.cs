namespace CAI.TestBench.Web.Modules
{
    using System;
    using System.Linq;
    using Business;
    using Model;
    using Nancy;
    using Nancy.ModelBinding;
    using Raven.Client;

    public class HomeModule : NancyModule
    {
        private readonly ICaiSettingsRepository _caiSettingsRepository;
        private const string SettingsItemName = "Settings";

        private readonly CaiServiceSettings _defaultCaiServiceSettings = new CaiServiceSettings()
        {
            BranchNumber = 992,
            ServiceId = 602,
            Username = "NET",
            Organisation = "TEST_PNCS",
            LastUpdated = DateTime.Now
        };

        public HomeModule(ICaiSettingsRepository caiSettingsRepository)
        {
            _caiSettingsRepository = caiSettingsRepository;

            Get["/"] = p => View["home"];
            Get["/about"] = p => View["about"];
            Get["/cai-status"] = p => View["cai-status"];
            Get["/settings/{restore?}"] = GetSettings;
            Post["/settings"] = UpdateSettings;
        }

        private dynamic GetSettings(dynamic @params)
        {
            if (Request.Query["restore"])
            {
                // retrieve from data store
                return View["settings", _defaultCaiServiceSettings];
            }

            if (Session[SettingsItemName] != null)
            {
                var settings = Session[SettingsItemName] as CaiServiceSettings;
                Session.Delete(SettingsItemName);
                return View["settings", settings];
            }

            // TODO retrieve from data store
            return View["settings", _defaultCaiServiceSettings];
        }

        private dynamic UpdateSettings(dynamic @params)
        {
            try
            {
                var settings = this.BindAndValidate<CaiServiceSettings>();
                settings.LastUpdated = DateTime.Now;
                if (ModelValidationResult.IsValid)
                {
                    // TODO persist to data store
                    Session[SettingsItemName] = settings;
                    return Response.AsRedirect("/settings");   
                }

                return View["settings", settings];
            }
            catch (ModelBindingException ex)
            {
                var invalidProperties = ex.PropertyBindingExceptions.Select(x => x.PropertyName).ToArray();
                var settings = this.BindAndValidate<CaiServiceSettings>(invalidProperties);
                settings.LastUpdated = DateTime.Now;
                return View["/settings", settings];
            }
        }
    }
}