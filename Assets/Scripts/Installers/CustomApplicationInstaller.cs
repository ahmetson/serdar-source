using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using Ahmetson.Serdar.Systems.ApplicationSystem;
using UnityEngine.EventSystems;
using EcsRx.Unity;

namespace Ahmetson.Serdar {
	public class CustomApplicationInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<EventSystem> ().FromComponentInNewPrefabResource ("EventSystem").AsSingle();
			Container.Bind<GameProgress> ().To<GameProgress> ().AsSingle();
            Container.Bind<EcsRxApplication>().FromComponentInHierarchy().AsSingle();
		}
	}
}
