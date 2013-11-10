namespace CAI.TestBench.Web.Modules
{
    using Nancy;

    public class MemberControllerModule : NancyModule
    {
        public MemberControllerModule() : base("/member-controller")
        {
            Get["/accounts-with-balances"] = p => View["accounts-with-balances"];
        }
    }
}