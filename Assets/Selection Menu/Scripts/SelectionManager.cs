using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public enum Difficulty {
  Easy,  // Default value: 0.
  Medium,
  Hard
};


public class SelectionManager : MonoBehaviour {
  public static SelectionManager instance;
  
  public GameObject difficultySelection;  // The difficulty selection object.
  public GameObject mapDisplayer;         // The object that displays the map's image.
  public TMP_Text mapName;                // The object that displays the map's name.

  public GameObject game;                 // The game object.
  public GameObject mapSelector;          // The map selector UI object.

  // Difficulty Selection Info
  private readonly string[,] _difficultyInfo = {{"Easy", "1000", "100"},  // Easy
                                                {"Medium", "800", "65"},  // Medium
                                                {"Hard", "600", "30"}};   // Hard
  public TMP_Text difficultyDisplay;
  public TMP_Text money;
  public TMP_Text health;


  public void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }

    if (GameData.instance.reloadType == ReloadType.Quit) {
      // Is a quit, start at map selection.
      MainMenuManager.instance.DisplayMainMenu(false);
      DisplayMapSelection(true);
    } else if (GameData.instance.reloadType == ReloadType.Restart) {
      // Is a restart, start at game.
      PlayGame();
    }
  }


  /**
   * Updates the map information.
   */
  public void UpdateMap(GameObject mapButton) {
    mapDisplayer.GetComponent<Image>().sprite = mapButton.GetComponent<Image>().sprite;
    mapName.text = mapButton.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
    GameData.instance.mapName = mapName.text;
  }


  /**
   * Displays the difficulty selection UI.
   * 
   * @param visible - whether the UI should be visible or not.
   */
  public void DisplayDifficultySelection(bool visible) {
    difficultySelection.SetActive(visible);
    
    if (visible) {
      GameManager.instance.SetDifficulty((int)Difficulty.Easy);
      UpdateDifficultyInfo();
    }
  }


  /**
   * Updates the difficulty information.
   */
  public void UpdateDifficultyInfo() {
    difficultyDisplay.text = _difficultyInfo[GameData.instance.difficulty, 0];
    money.text = "$" + _difficultyInfo[GameData.instance.difficulty, 1];
    health.text = _difficultyInfo[GameData.instance.difficulty, 2];
  }


  /**
   * Sets up the game.
   */
  public void PlayGame() {
    MainMenuManager.instance.DisplayMainMenu(false);
    DisplayMapSelection(false);
    game.SetActive(true);

    foreach (GameObject m in GameData.instance.maps) {
      if (m.name == GameData.instance.mapName) {
        GameObject cloned = Instantiate(m, new Vector3(0, 0, 10), Quaternion.identity);
        cloned.transform.SetParent(GameObject.Find("Game").transform, true);
        cloned.name = "Map";
        GameData.instance.SetMap();
        break;
      }
    }

    GameManager.instance.money = Int32.Parse(_difficultyInfo[GameData.instance.difficulty, 1]);
    GameManager.instance.health = Int32.Parse(_difficultyInfo[GameData.instance.difficulty, 2]);
  }


  /**
   * Sets the visibility of the map selection UI.
   * 
   * @param visible - whether the UI is visible or not.
   */
  public void DisplayMapSelection(bool visible) {
    mapSelector.SetActive(visible);

    // Also close the difficulty selection.
    if (!visible) {
      difficultySelection.SetActive(false);
    }
  }
}
