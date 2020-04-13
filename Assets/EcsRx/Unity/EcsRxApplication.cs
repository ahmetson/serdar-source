using System.Collections.Generic;
using System.Linq;
using EcsRx.Extensions;
using UnityEngine;
using EcsRx.Pools;
using EcsRx.Systems;
using EcsRx.Systems.Executor;
using EcsRx.Unity.Plugins;
using EcsRx.Unity.Systems;
using Zenject;
using Ahmetson.Serdar;
using EcsRx.Events;

namespace EcsRx.Unity
{
    public abstract class EcsRxApplication : MonoBehaviour
    {
		//[Inject]
		//public ApplicationLevelInformation Information { get; set; }
		public List<ISystem> SceneSystems = new List<ISystem> (); 	// Added by MEDET.
        private string lastSceneNamespace;
        private string applicationNamespace;

        [Inject]
        public ISystemExecutor SystemExecutor { get; private set; }

        [Inject]
        public IPoolManager PoolManager { get; private set; }

        protected List<IEcsRxPlugin> Plugins { get; private set; }
        protected DiContainer Container { get; private set; }

		[Inject]
		public IEventSystem eventSystem;

        [Inject]
        private void Init(DiContainer container)
        {
            Plugins = new List<IEcsRxPlugin>();
            Container = container;
            ApplicationStarting();
            RegisterAllPluginDependencies();
            SetupAllPluginSystems();
            ApplicationStarted();
        }

        protected virtual void ApplicationStarting() { }
        protected abstract void ApplicationStarted();

        protected virtual void RegisterAllPluginDependencies()
        { Plugins.ForEachRun(x => x.SetupDependencies(Container)); }

        protected virtual void SetupAllPluginSystems()
        {
            Plugins.SelectMany(x => x.GetSystemForRegistration(Container))
                .ForEachRun(x => SystemExecutor.AddSystem(x));
        }

        protected void RegisterPlugin(IEcsRxPlugin plugin)
        { Plugins.Add(plugin); }
        
        protected virtual void RegisterAllBoundSystems()
        {
            var allSystems = Container.ResolveAll<ISystem>();
            Debug.Log("Signed up " + allSystems.Count);
            var orderedSystems = allSystems
                .OrderByDescending(x => x is ViewResolverSystem)
                .ThenByDescending(x => x is ISetupSystem);
            orderedSystems.ForEachRun(SystemExecutor.AddSystem);
        }

        protected virtual void RegisterBoundSystem<T>() where T : ISystem
        {
            var system = Container.Resolve<T>();
            SystemExecutor.AddSystem(system);
        }

		///---------------------------------------------------------------------------------------------------
		/// Methods below are Added for Ahmetson project
		/// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Unregisters the scene systems.
		/// Delete's Scene systems from SystemExecutor.
		/// Clears the List of Scene Systems
		/// </summary>
		public void UnregisterSceneSystems() {
			if (null == SceneSystems || 0 == SceneSystems.Count) {
				return;
			}
            Debug.Log(SceneSystems.Count + " systems are there");
			foreach (ISystem SceneSystem in SceneSystems) {
				SystemExecutor.RemoveSystem(SceneSystem);
			}
		}

		/// <summary>
		/// Gets the application systems list.
		/// Return the list of systems that should run on all scenes
		/// </summary>
		/// <returns>The application systems list.</returns>
		public List<ISystem> GetApplicationSystems() {
			return GetSystems(applicationNamespace);
		}

		/// <summary>
		/// Registers the application systems.
		/// 
		/// </summary>
		public void RegisterApplicationSystems(string nameSpace) {
            applicationNamespace = nameSpace;
			List<ISystem> applicationSystems = GetApplicationSystems();

			foreach (ISystem applicationSystem in applicationSystems) {
				SystemExecutor.AddSystem(applicationSystem);
			}
		}

		/// Get List of systems that are run only for this specific system
		/// Set them at the @SceneSystems list
		public void GetSceneSystems(string nameSpace) {
			SceneSystems = GetSystems (nameSpace);
		}

		private List<ISystem> GetSystems(string nameSpace) {
			List<ISystem> systems = new List<ISystem> ();
			var allSystems = Container.ResolveAll<ISystem>();

			var orderedSystems = allSystems
				.OrderByDescending(x => x is ViewResolverSystem)
				.ThenByDescending(x => x is ISetupSystem);
			foreach (ISystem system in orderedSystems) {
				var systemType = system.GetType();
				if (systemType.Namespace == nameSpace) {
					//Debug.Log ("Namespaces are matched");
					systems.Add (system);
				} else {
					//Debug.Log (systemType.Namespace + " != "+nameSpace);
				}
			}
			return systems;
		}


		/// <summary>
		/// Register Scene Systems
		/// Adds Scene Systems to the System Executor of EcsRX framework
		/// </summary>
		public void RegisterSceneSystems(string nameSpace) {
            
			// Register New Systems that are specific for this scene
			GetSceneSystems(nameSpace);

			if (null == SceneSystems || 0 == SceneSystems.Count) {
				return;
			}

			foreach (ISystem SceneSystem in SceneSystems) {
				SystemExecutor.AddSystem(SceneSystem);
			}
		}
    }
}
