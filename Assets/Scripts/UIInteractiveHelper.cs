using Ahmetson.Serdar.Configuration;


namespace Ahmetson.Serdar.Helpers {
	public class UIInteractiveHelper {
		public BUTTON_ACTION GetActionForButton(string buttonName) {
			BUTTON_ACTION action;
			switch (buttonName) {
			case "Continue Button":
				action = BUTTON_ACTION.CONTINUE;
				break;
			case "Play Button":
				action = BUTTON_ACTION.NEW_GAME;
				break;
			case "New Game Button":
				action = BUTTON_ACTION.NEW_GAME;
				break;
			case "Info Button":
				action = BUTTON_ACTION.INFO;
				break;
			case "Music Toggle Button":
				action = BUTTON_ACTION.MUSIC_TOGGLE;
				break;
			case "Sound Toggle Button":
				action = BUTTON_ACTION.SOUND_TOGGLE;
				break;
			case "Hard Mode Toggle Button":
				action = BUTTON_ACTION.HARD_MODE_TOGGLE;
				break;
			case "Hard Mode Purchase Button":
				action = BUTTON_ACTION.HARD_MODE_PURCHASE;
				break;
			case "Skip Scene Button":
				action = BUTTON_ACTION.SKIP_SCENE;
				break;
			case "Restart Button":
				action = BUTTON_ACTION.RESTART;
				break;
			case "Upgrade 1 Button":							// First Skill
				action = BUTTON_ACTION.SPEND_XP;			
				break;			
			case "Upgrade 2 Button":							// Second Skill
				action = BUTTON_ACTION.SPEND_XP;
				break;			
			case "Upgrade 3 Button":							// First Skill
				action = BUTTON_ACTION.SPEND_XP;
				break;			
			case "Upgrade 4 Button":							// First Skill
				action = BUTTON_ACTION.SPEND_XP;
				break;
			case "Trophies Button":
				action = BUTTON_ACTION.TROPHIES_WINDOW;
				break;
			case "Letters Button":
				action = BUTTON_ACTION.LETTERS_WINDOW;
				break;
			case "Home Button":
				action = BUTTON_ACTION.HOME;
				break;
			default:
				action = BUTTON_ACTION.QUIT;
				break;
			}
			return action;
		}

	}
}
