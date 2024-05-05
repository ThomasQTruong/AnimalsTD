using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
  private readonly string[,] _difficultyInfo = {{"Easy", "1000", "1"},      // Easy
                                                {"Medium", "900", "1.25"},  // Medium
                                                {"Hard", "800", "1.5"}};    // Hard
  public TMP_Text difficultyDisplay;
  public TMP_Text startingCash;
  public TMP_Text priceMultiplier;


  public void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }

    if (GameData.instance.restart) {
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
    startingCash.text = "$" + _difficultyInfo[GameData.instance.difficulty, 1];
    priceMultiplier.text = _difficultyInfo[GameData.instance.difficulty, 2] + "x";
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
