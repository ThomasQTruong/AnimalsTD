using UnityEngine;
using TMPro;


/**
 * Tools for the Game Over UI.
 */
public class GameOver : MonoBehaviour {
  public static GameOver instance;

  public TMP_Text roundText;


  /**
   * Limits object to one instance.
   */
  private void Awake() {
    instance = this;
  }


  public void UpdateRound() {
    roundText.text = "Round: " + GameManager.instance.currentRound;
  }
}
