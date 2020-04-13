using System.Collections;

using UnityEngine;

namespace Ahmetson.Serdar {
	public interface ISceneInformation {
		string NameSpace { get; set; }
		int NextSceneId { get; set; }
	}
}
