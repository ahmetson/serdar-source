using Ahmetson.Serdar.Monobehaviours;
using Ahmetson.Serdar.Configuration;
using System.Collections.Generic;
using UnityEngine;


namespace Ahmetson.Serdar.Events {
	public class LevelStateEvent  {
		public LEVEL_STATE state;
		public List<RoadContinuation> roadsToContinue;
		public Road road;
		public GameObject campus;
		public GameObject tribe;
		public GameObject fightZone;
	}
}