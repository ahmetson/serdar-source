using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Ahmetson.Serdar.Enums;
using Zenject;
using EcsRx.Events;
using Ahmetson.Serdar.Events;
using System.IO;
using EcsRx.Systems;

namespace Ahmetson.Serdar {

	public class SystemInstaller : MonoInstaller{

        public ISystem[] Systems;

        [Inject]
        IEventSystem EventSystem;
        private IDisposable _subscription;

        private IntReactiveProperty _currentGameProgress = new IntReactiveProperty();

        public override void InstallBindings()
        {
            //Container.Bind<ISceneInformation>().FromComponentInHierarchy();
            //Container.Bind<FightingSystem>().FromComponentInHierarchy ();
        }

    }
}
