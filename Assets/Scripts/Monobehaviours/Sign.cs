using UnityEngine;
using Ahmetson.Serdar.Configuration;

namespace Ahmetson.Serdar.Monobehaviours {
	public class Sign : MonoBehaviour {

		public SIGN_TYPE SignType;
		[HideInInspector]
		public GameObject campus;
		[HideInInspector]
		public GameObject fightPlace;		// Place where warriors from Enemy and Ally sides are meeting to fight
	}
}