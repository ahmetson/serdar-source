using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcsRx.Events;
using Ahmetson.Serdar.Events;
using Zenject;

namespace Ahmetson.Serdar.Monobehaviours {
	public class SignTrigger : MonoBehaviour {

		[Inject]
		public IEventSystem _eventSystem;


		void OnTriggerEnter(Collider collider) {
			Sign signData = collider.gameObject.GetComponent<Sign> ();
			_eventSystem.Publish<SignEvent> ( new SignEvent( signData, gameObject ) );
		}
	}
}
