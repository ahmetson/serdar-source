namespace Ahmetson.Serdar {
	public class CurrentGameProgress  {
		public int CurrentLevel;
		public bool HardModeEnabled = false;
		public bool HardModeToggle = false;

		public CurrentGameProgress(int initialLevel) {
			CurrentLevel = initialLevel;

		}
	}
}