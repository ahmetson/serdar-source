using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.UI;

namespace Ahmetson.Serdar {
	public class LevelResultsInstaller : MonoInstaller
	{

		[InjectOptional]
		public LevelResults optionalResults;

		public override void InstallBindings()
		{
			optionalResults = new LevelResults ();
			optionalResults.IsLevelSucceed = false;
			Container.BindInstance(optionalResults).WhenInjectedInto<LevelCompletitionSceneInformation>();
		}
	}
}
