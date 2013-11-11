using System;
using System.Web.Configuration;
using CAI.TestBench.Business.Contracts;
using CAI.TestBench.Model;

namespace CAI.TestBench.Web.HtmlHelpers
{
    public class ProvideDefaultCaiServiceSettings : IProvideDefaultCaiServiceSettings
    {
        public CaiServiceSettings GetDefaultCaiServiceSettings()
        {
            var serviceId = WebConfigurationManager.AppSettings["ServiceId"];
            var username = WebConfigurationManager.AppSettings["Username"];
            var organisation = WebConfigurationManager.AppSettings["Organisation"];
            var branchNumber = WebConfigurationManager.AppSettings["BranchName"];

            return new CaiServiceSettings
            {
                AreDefault = true,
                BranchNumber = short.Parse(branchNumber),
                Organisation = organisation,
                ServiceId = int.Parse(serviceId),
                Username = username,
                LastUpdated = DateTime.Now
            };
        }
    }
}