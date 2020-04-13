using System.Collections;
using UnityEngine;
using EcsRx.Systems;
using EcsRx.Groups;
using EcsRx.Entities;
using UniRx;

namespace Ahmetson.Serdar.Systems.ApplicationSystem {
	//public class GameProgressUpdateSystem : IReactToEntitySystem {

	//	public IGroup TargetGroup { get { return new EmptyGroup (); } }

	//	public IObservable<IEntity> ReactToEntity(IEntity entity) {
	//		return SerdarApplication.Global.currentGameProgress.DistinctUntilChanged ().Select( x => entity);
	//	}

	//	public void Execute(IEntity entity) {
	//		Debug.Log ("Data on Game Progress was Changed");
	//		SerdarApplication.Global.SaveGameProgress ();
	//	}
	//}

}