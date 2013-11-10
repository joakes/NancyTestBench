namespace CAI.TestBench.Web
{
    using Nancy;
    using Nancy.TinyIoc;
    using Raven.Client;
    using Raven.Client.Embedded;

    public class TestBenchBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Nancy.Session.CookieBasedSessions.Enable(pipelines);
            RegisterRavenDb(container);
        }

        private void RegisterRavenDb(TinyIoCContainer container)
        {
            IDocumentStore documentStore = new EmbeddableDocumentStore() { ConnectionStringName = "RavenDB" };
            documentStore.Initialize();
            container.Register<IDocumentStore>(documentStore);
        }
    }
}