using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {
	public GUISkin skin;

	GameState gameState;

	// Use this for initialization
	void Start () {
		gameState = new GameState();

		PrintPrompt();
	}
	
	// Update is called once per frame
	void Update () {
		mainTextPosition.width = Screen.width-10;
		mainTextPosition.height = Screen.height-80;

		bool shouldUpdateGame = false;

		if (Input.GetKeyDown(KeyCode.A)) {
			PrintChosen("A");
			PrintLn("Fed Asterio!");

			gameState.asterio.Feed(0.5f);

			shouldUpdateGame = true;
		} else if (Input.GetKeyDown(KeyCode.Q)) {
			PrintChosen("Q");
			PrintLn("Fed self!");

			gameState.minotaur.Feed(0.5f);

			shouldUpdateGame = true;
		} else if (Input.GetKeyDown(KeyCode.W)) {
			PrintChosen("W");
			PrintLn("The Minotaur waits.");

			shouldUpdateGame = true;
		}

		if (shouldUpdateGame) {
			gameState.Update();

			string status = "";

			status += GetEaterStatus(gameState.asterio);
			status += GetEaterStatus(gameState.minotaur);
			PrintLn(status);
			PrintLn("{0} love={1}", gameState.asterio.name, gameState.asterio.love);

			PrintPrompt();
		}

	}

	void PrintChosen(string option) {
		ClearConsole();
		PrintPrompt();
		PrintLn(option);
	}

	void ClearConsole()
	{
		consoleText = "";
	}

	static string GetEaterStatus(Eater obj)
	{
		if (obj.dead) return obj.name+" is dead. :'-( ";
		if (obj.isStarving()) return obj.name+" is starving! ";
		if (obj.isHungry()) return obj.name+" is hungry. ";

		return string.Format("{0} food={1:0.00} ", obj.name, obj.food);

//		return "";
	}

	Rect mainTextPosition = new Rect(5, 5, 100, 100);
	string consoleText = "ASTERIO\n\n";
	void OnGUI() {
		GUI.skin = skin;
		GUI.enabled = false;
		GUI.TextArea(mainTextPosition, consoleText);

	}

	void PrintPrompt()
	{
		Print("\nA) feed asterio Q) feed self W) wait > ");
	}

	void PrintLn(string line, params object[] args)
	{
		Print(line+"\n", args);
	}

	void Print(string line, params object[] args)
	{
		consoleText += string.Format(line, args);
	}
}
