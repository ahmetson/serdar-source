using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EcsRx.Systems.Custom;
using EcsRx.Groups;
using EcsRx.Entities;
using UniRx;
using UniRx.Triggers;
using EcsRx.Unity.Components;
using Ahmetson.Serdar.Events;

/// <summary>
/// This System runs on Full Tutorial and Level systems
/// </summary>
using EcsRx.Events;
using Ahmetson.Serdar.Configuration;


namespace Ahmetson.Serdar.Systems.ApplicationSystem {
	public class SignSystem : EventReactionSystem<SignEvent> {

		public IGroup TargetGroup { get { return new Group ( typeof ( SerdarsTribeComponent ), typeof ( ViewComponent ) ); } }

		IEventSystem _eventSystem;

		public SignSystem ( IEventSystem eventSystem) : base ( eventSystem) {
			_eventSystem = eventSystem;
		}

		public override void EventTriggered ( SignEvent eventData ) {
			switch ( eventData.sign.SignType ) {
			case SIGN_TYPE.LEVEL_COMPLETITION:
				//SerdarApplication.sceneSwitcher.OpenLevelCompletition();
				break;
			case SIGN_TYPE.CAMP:
				Debug.Log ("The " + eventData.onSign + " on the " + eventData.sign.SignType);
				LevelStateEvent ls = new LevelStateEvent ();
				ls.campus = eventData.sign.campus;
				ls.tribe = eventData.onSign;
				ls.fightZone = eventData.sign.fightPlace;
				ls.state = LEVEL_STATE.FIGHT;
				_eventSystem.Publish<LevelStateEvent> ( ls );
				break;

			}
		}

	}
}