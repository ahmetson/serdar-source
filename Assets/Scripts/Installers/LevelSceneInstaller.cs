using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.UI;
using SWS;
using Ahmetson.Serdar.Systems.LevelSystem;
using EcsRx.Systems;

namespace Ahmetson.Serdar {
	public class LevelSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			//Container.Bind<splineMove>().FromComponentInHierarchy();
			Container.Bind<FightingSystem>().AsSingle();
		}
	}
}
