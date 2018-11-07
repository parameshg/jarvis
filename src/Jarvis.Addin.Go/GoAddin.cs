using Autofac;
using Jarvis.Core;

namespace Jarvis.Addin.Go
{
    public sealed class GoAddin : IAddin
    {
        public void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<GoProvider>().As<IQueryProvider>().SingleInstance();
        }
    }
}