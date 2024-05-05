using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * The types of damage in the game.
 */
public enum DamageType {
  Standard,
  Explosive,
  Fire,
  Ice
}


public class GameManager : MonoBehaviour {
  public static GameManager instance;     // Limits to one instance.
  public GameObject gameOver;             // Game over screen object.
  public GameObject forfeitConfirmation;  // Forfeit confirmation UI.

  // Game stats.
  public int health = 100;
  public int money = 1000;
  public RoundData[] rounds;
  public int currentRound = 0;
  public int animalsLeft = 0;

  private bool _autoStartRound = false;        // The toggle for auto starting rounds.
  private bool _startCoroutineActive = false;  // Whether the start coroutine is active or not.
  private bool _roundInProgress;               // Whether the round is in progress or not.


  /**
   * Initialize the instance.
   */
  public void Awake() {
    Time.timeScale = 1;
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
  }


  /**
   * Spawns the animals for the current round.
   * 
   * @return IEnumerator - a "hack" to delay the spawns.
   */
  private IEnumerator SpawnAnimals() {
    // Grab the animals for the current round.
    RoundData round = rounds[currentRound];

    // For every animal data for the round.
    foreach(AnimalData data in round.animals) {
      // For every data.
      for (int i = 0; i < data.count; ++i) {
        // Create animal.
        Instantiate(data.animal, GameData.instance.track.GetWaypointPosition(0), Quaternion.identity);
        ++animalsLeft;

        yield return new WaitForSeconds(data.spawnRate);
      }
      yield return new WaitForSeconds(round.spawnDelay);
    }

    // Wait until there are no animals left.
    while (animalsLeft > 0) {
      yield return null;
    }

    // No animals left, end round and give money.
    _roundInProgress = false;
    money += round.moneyGain;
  }


  /**
   * Button function to start the round.
   */
  public void StartButton() {
    // Prevent dupelicate coroutines.
    if (!_startCoroutineActive) {
      _startCoroutineActive = true;
      StartCoroutine(StartRound());
    }
  }


  /**
   * Toggles autoStartRound.
   */
  public void ToggleAutoStartRound() {
    _autoStartRound = !_autoStartRound;
    // User wants to autostart while round is in progress, put in loop if not looped already.
    if (_autoStartRound && _roundInProgress && !_startCoroutineActive) {
      _startCoroutineActive = true;
      StartCoroutine(StartRound());
    }
  }


  /**
   * Starts the next round if possible.
   * 
   * @return IEnumerator - to keep the function alive for the auto start feature..
   */
  public IEnumerator StartRound() {
    do {
      // Round is not in progress.
      if (!_roundInProgress) {
        // Activate next round.
        _roundInProgress = true;
        StartCoroutine(SpawnAnimals());
        ++currentRound;
      }
      yield return null;
    } while (_autoStartRound);
    _startCoroutineActive = false;
  }


  /**
   * Player lost the game (no health left).
   */
  public void EndGame() {
    Time.timeScale = 0;
    gameOver.SetActive(true);
    GameOver.instance.UpdateRound();
  }


  /**
   * Restarts the game.
   */
  public void RestartGame() {
    // Reload current scene.
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    GameData.instance.restart = true;
  }


  /**
   * The data of the round.
   */
  [System.Serializable]
  public class RoundData {
    public AnimalData[] animals;
    public float spawnDelay = 0.6f;
    public int moneyGain = 250;
  }


  /**
   * The data of the animal.
   */
  [System.Serializable]
  public class AnimalData {
    public Animal animal;
    public int count = 1;
    public float spawnRate = 0.5f;
  }


  /**
   * Toggles the visibility for the forfeit confirmation UI.
   * 
   * @param visible - whether the UI is shown or hidden.
   */
  public void SetForfeitUIActive(bool visible) {
    forfeitConfirmation.SetActive(visible);
  }


  /**
   * Sets the difficulty of a game to a given value.
   * 
   * @param value - the value that represents the difficulty: easy(0), medium(1), hard(2).
   */
  public void SetDifficulty(int value) {
    GameData.instance.difficulty = value;
  }
}
