using Ahmetson.Serdar.Monobehaviours;
using UnityEngine;


namespace Ahmetson.Serdar.Events {
	public class SignEvent  {
		public Sign sign;
		public GameObject onSign;	// Object on Sign

		public SignEvent() {

		}

		public SignEvent ( Sign signData, GameObject gameObject ) {
			sign = signData;
			onSign = gameObject;
		}

		public SignEvent ( Sign signData ) {
			sign = signData;
		}
	}
}