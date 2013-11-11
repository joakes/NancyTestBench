using System;
using System.Linq;
using System.Reflection;
using CAI.TestBench.Business.Contracts;
using CAI.TestBench.Data;
using CAI.TestBench.Model;
using CAI.TestBench.Web.Modules;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Ninject;
using Ninject.Extensions.Conventions;

namespace CAI.TestBench.Web
{
    using Raven.Client;
    using Raven.Client.Embedded;

    public class TestBenchBootstrapper : NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Nancy.Session.CookieBasedSessions.Enable(pipelines);
            RegisterRavenDb(container);
        }

        private void RegisterRavenDb(IKernel container)
        {
            container.Bind<IDocumentStore>().ToMethod(ctx =>
            {
                IDocumentStore documentStore = new EmbeddableDocumentStore() { ConnectionStringName = "RavenDB" };
                return documentStore.Initialize();
            }).InSingletonScope();
        }

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            existingContainer.Bind(ctx => ctx.FromAssemblyContaining(typeof(HomeModule), typeof(CaiServiceSettings), typeof(ICaiSettingsRepository), typeof(CaiSettingsRepository))
                                             .Select(IsServiceType)
                                             .BindDefaultInterface()
                                             .Configure(binding => binding.InTransientScope()));
        }

        private bool IsServiceType(Type type)
        {
            return type.IsClass && type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
        }
    }
}