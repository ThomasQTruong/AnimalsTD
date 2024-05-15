using System.Collections;
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


/**
 * Tools for managing the game.
 */
public class GameManager : MonoBehaviour {
  public static GameManager instance;     // Limits to one instance.
  public GameObject gameOver;             // Game over screen object.
  public GameObject forfeitConfirmation;  // Forfeit confirmation UI.

  // Game stats.
  public int money;
  public int health;
  public int currentRound = 0;
  public int winRound;
  public int animalsLeft = 0;
  public float sellRatio = 0.8f;  // The ratio for the amount of money gained back from selling.

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
    // For every animal spawn for the round.
    foreach (AnimalSpawn spawn in RoundManager.instance.spawnList) {
      StartCoroutine(SpawnAnimal(spawn));
      yield return new WaitForSeconds(RoundManager.instance.spawnDelay);
    }

    // Wait until there are no animals left.
    while (animalsLeft > 0) {
      yield return null;
    }

    // No animals left, end round and give money.
    _roundInProgress = false;
    money += RoundManager.instance.moneyPerRound;
  }


  /**
   * Starts spawning for an animal.
   * 
   * @param spawn - the animal to spawn.
   */
  private IEnumerator SpawnAnimal(AnimalSpawn spawn) {
    // For every count.
    for (int i = 0; i < spawn.count; ++i) {
      // Create animal.
      Instantiate(spawn.animal, GameData.instance.track.GetWaypointPosition(0), Quaternion.identity);
      ++animalsLeft;

      yield return new WaitForSeconds(spawn.spawnRate);
    }
    // Finished spawning, increment for next round.
    spawn.count += spawn.incrementPerRound;
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
        // Increase round count and check if any new animal should be spawned.
        ++currentRound;
        RoundManager.instance.CheckForSpawn(currentRound);

        // Activate next round.
        _roundInProgress = true;
        StartCoroutine(SpawnAnimals());
      }
      yield return null;
    } while (_autoStartRound);
    _startCoroutineActive = false;
  }


  /**
   * Player lost the game (no health left).
   */
  public void LoseGame() {
    Time.timeScale = 0;
    gameOver.SetActive(true);
    GameOver.instance.UpdateRound();
  }


  /**
   * Restarts the game.
   */
  public void RestartGame() {
    // Reload current scene.
    GameData.instance.reloadType = ReloadType.Restart;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


  /**
   * Quits/forfeits the current game.
   */
  public void QuitGame() {
    // Reload current scene.
    GameData.instance.reloadType = ReloadType.Quit;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }


  /**
   * Upgrades the tower based on the type of upgrade it is.
   */
  public void UpgradeTower(Upgrade upgrade) {
    Tower tower = GameUIManager.instance.GetSelectedTower();
    if (tower == null) {
      return;
    }

    // Update tower's stats with type.
    switch (upgrade.upgradeType) {
      case UpgradeType.Damage:
        tower.damage += (int)(upgrade.value);
        break;
      case UpgradeType.Fire_Rate:
        tower.fireCooldown -= (upgrade.value * tower.fireCooldown);
        break;
      case UpgradeType.Range:
        tower.range += upgrade.value;
        tower.UpdateRadius();
        break;
      case UpgradeType.Pierce:
        tower.pierce += (int)(upgrade.value);
        break;
    }
  }
}
