using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	//Variables for the buttons and also the menus
	public Button newGameButton;

	public Button loadGameButton;

	public Button optionsButton;
	
	public Button exitGameButton;

	public string newGameSceneName;

	public GameObject loadGameMenu;

	public GameObject loadOptionsMenu;

	// Trying to hardcode the buttons to work
	// public Void Awake() {
	// 	newGameButton.onClick.AddListener(NewGame);
	// 	loadGameButton.onClick.AddListener(OpenLoadGameMenu);
	// 	optionsButton.onClick.AddListener(OptionsMenu);
	// 	exitGameButton.onClick.AddListener(ExitGame);
	// }


	//New game with the scene name
	public void NewGame() {
		SceneManager.LoadScene(newGameSceneName);

	}

	//Loads up previous games
	public void OpenLoadGameMenu () {
		loadGameMenu.SetActive(true);
	}
	//Options menu
	public void OptionsMenu() {
		loadOptionsMenu.SetActive(true);

	}
	//Exits the Game/ Application
	public void ExitGame() {
		Application.Quit();
	}

}
