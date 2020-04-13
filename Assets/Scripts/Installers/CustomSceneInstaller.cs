using Zenject;
using EcsRx.Pools;

namespace Ahmetson.Serdar {
	public class CustomSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<ISceneInformation>().FromComponentInHierarchy();
            Container.Bind<PoolManager>().AsSingle();
			//Container.Bind<FightingSystem>().FromComponentInHierarchy ();
		}
	}
}
