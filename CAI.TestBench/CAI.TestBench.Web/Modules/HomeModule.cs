using CAI.TestBench.Business.Contracts;

namespace CAI.TestBench.Web.Modules
{
    using System.Linq;
    using Business;
    using Model;
    using Nancy;
    using Nancy.ModelBinding;

    public class HomeModule : NancyModule
    {
        private readonly ICaiSettingsRepository _caiSettingsRepository;

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
                var defaultSettings = _caiSettingsRepository.GetCaiServiceSettings(true);
                return View["settings", defaultSettings];
            }

            var settings = _caiSettingsRepository.GetCaiServiceSettings(false);
            return View["settings", settings];
        }

        private dynamic UpdateSettings(dynamic @params)
        {
            try
            {
                var settings = this.BindAndValidate<CaiServiceSettings>();
                if (ModelValidationResult.IsValid)
                {
                    _caiSettingsRepository.UpdateServiceSettings(settings);
                    return Response.AsRedirect("/settings");
                }

                return View["settings", settings];
            }
            catch (ModelBindingException ex)
            {
                var invalidProperties = ex.PropertyBindingExceptions.Select(x => x.PropertyName).ToArray();
                var settings = this.BindAndValidate<CaiServiceSettings>(invalidProperties);
                return View["/settings", settings];
            }
        }
    }
}