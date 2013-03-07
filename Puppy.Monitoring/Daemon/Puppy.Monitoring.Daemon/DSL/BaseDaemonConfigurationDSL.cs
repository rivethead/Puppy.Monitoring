using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Puppy.Monitoring.Adapters;

namespace Puppy.Monitoring.Daemon.DSL
{
    public abstract class BaseDaemonConfigurationDSL
    {
        public delegate void ActionDelegate();

        protected ActionDelegate registration;

        [Meta]
        public static Expression configure_publisher(Expression expression)
        {
            return expression;
        }

        public abstract void Prepare();

//        public void with(Expression expression)
        public void with(IPipelineAdapter expression)
        {
            var i = 0;
        }

        public void Execute()
        {
            registration();
        }

        public void ConfigurePublisher(ActionDelegate action)
        {
            this.registration = action;
        }
    }
}