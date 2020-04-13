using EcsRx.Systems;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;
using EcsRx.Pools;
using UnityEngine.UI;
using Zenject;
using Ahmetson.Serdar.Helpers;
using EcsRx.Entities;
using Ahmetson.Serdar.Component;
using Assets.EcsRx.Unity.Extensions;
using System.Linq;
using EcsRx.Unity;

namespace Ahmetson.Serdar.Systems.MainMenuSystem {
	public class MainMenuSystem : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup(); } }

        [HideInInspector]
        public Button[] buttons;

        [Inject]
        EcsRxApplication SerdarApplication;


        public void StartSystem(IGroupAccessor group) {
            //SerdarApplication.sceneSwitcher.SwitchToNextScene (SerdarApplication.Global.currentSceneInformation.NextSceneId);
            Debug.Log("Main Menu has been initialized");

            this.ActivateButtons();
            
        }

		public void StopSystem(IGroupAccessor group) {
			Debug.Log ("Scene Stop");
		}

        private void ActivateButtons()
        {
            buttons = Object.FindObjectsOfType<Button>();
            IPool pool = SerdarApplication.PoolManager.GetPool();
            UIInteractiveHelper helper = new UIInteractiveHelper();
            foreach (Button button in buttons)
            {
                IEntity entity = pool.CreateEntity();
                entity.AddComponent<UIInteractiveComponent>();
                UIInteractiveComponent interactiveComponent = entity.GetComponent<UIInteractiveComponent>();
                interactiveComponent.Action = helper.GetActionForButton(button.gameObject.name);
                button.gameObject.LinkEntity(entity, pool);
            }

            var hardModeToggleButton = buttons.Where(x => x.gameObject.name.Contains("Hard Mode Toggle Button")).Select(x => x.gameObject);
            var hardModePurchaseButton = buttons.Where(x => x.gameObject.name.Contains("Hard Mode Purchase Button")).Select(x => x.gameObject);
        }
    }

}
