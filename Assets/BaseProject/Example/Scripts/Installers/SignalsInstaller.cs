using BaseProject.Scripts.Core.EventBus;
using Zenject;

namespace BaseProject.Example.Scripts.Installers
{
    public class SignalsInstaller : MonoInstaller<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            //SignalBusInstaller.Install(Container);
            //DeclareSignals();
        }

        protected virtual void DeclareSignals()
        {
        }
    }
}