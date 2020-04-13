using Ahmetson.Serdar.Enums;

namespace Ahmetson.Serdar.Events {
	public class SceneSwitchEvent  {
        public SCENE_TYPE SceneType;
        public string SceneName;
        public string SceneNameSpace;

		public SceneSwitchEvent ( SCENE_TYPE sceneType, string sceneName, string sceneNameSpace ) {
			SceneType = sceneType;
			SceneName = sceneName;
            SceneNameSpace = sceneNameSpace;
		}
	}
}