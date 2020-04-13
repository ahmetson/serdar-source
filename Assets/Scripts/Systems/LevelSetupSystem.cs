using EcsRx.Systems;
using EcsRx.Systems.Custom;
using EcsRx.Groups.Accessors;
using UnityEngine;
using EcsRx.Groups;
using Ahmetson.Serdar.Events;
using EcsRx.Entities;
using Ahmetson.Serdar.Configuration;
using EcsRx.Events;
using System.Collections.Generic;
using SWS;
using Zenject;
using EcsRx.Pools;
using EcsRx.Unity.Components;
using Ahmetson.Serdar.Monobehaviours;

namespace Ahmetson.Serdar.Systems.LevelSystem {
	public class LevelSetupSystem : IManualSystem {

		public IGroup TargetGroup { get { return new EmptyGroup();} }

		[Inject]
		IPoolManager poolManager;

		public  void StartSystem ( IGroupAccessor @group ) {
			//
			IPool poolCampus = poolManager.GetPool ( "Level Campuses" );
			if ( null == poolCampus ) {
				Debug.LogError ( "Pool for campus containing is missing!" );
			}

			IPool poolSigns = poolManager.GetPool ( "Level Signs" );
			if (null == poolSigns ) {
				Debug.LogError ( "Pool for signs containing is missing!" );
			}

			List<Entity> foundEntities = new List<Entity>();
			foreach ( Entity signEntity in poolSigns.Entities ) {
				Sign sign = signEntity.GetComponent<ViewComponent> ().View.GetComponent<Sign> ();

				if ( SIGN_TYPE.CAMP != sign.SignType ) {
					continue;
				}
				Vector3 signPosition = sign.gameObject.transform.position;

				float minDistance = -1f;
				Entity foundEntity = null;
				foreach ( Entity campusEntity in poolCampus.Entities ) {
					Vector3 campusPosition = campusEntity.GetComponent<ViewComponent> ().View.transform.position;

					float distance = Vector3.Distance ( signPosition, campusPosition );

					if ( -1f == minDistance || minDistance > distance ) {
						minDistance = distance;
						foundEntity = campusEntity;
					}
				}

				if (null == foundEntity) {
					break;				// There are no campuses in the Level
				}

				// Signs cannot attach to the same Campus. So track the binded campuses
				if ( foundEntities.IndexOf ( foundEntity ) < 0 ) {
					foundEntities.Add ( foundEntity );
				}

				sign.campus = foundEntity.GetComponent<ViewComponent> ().View;
				Debug.Log ( "The Sign: "+sign.gameObject.name+" has assigned campus: "+sign.campus.name );
				minDistance = -1f;
				foundEntity = null;
			}

		}
		public void StopSystem ( IGroupAccessor group ) {}

	}

}
