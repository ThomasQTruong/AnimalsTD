using UnityEngine;


/**
 * Tools for managing the main menu UI.
 */
public class MainMenuManager : MonoBehaviour {
  public static MainMenuManager instance;
  public GameObject mainMenu;


  public void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this);
    }
  }


  /**
   * Sets the visibility of the main menu UI.
   * 
   * @param visible - whether the UI is visible or not.
   */
  public void DisplayMainMenu(bool visible) {
    mainMenu.SetActive(visible);
  }
}
