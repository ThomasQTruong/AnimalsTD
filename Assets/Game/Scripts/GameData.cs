using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The types of damage in the game.
public enum DamageType {
  Standard,
  Explosive,
  Fire,
  Ice
}


public class GameData : MonoBehaviour {
  public static GameData instance;  // Limits to one instance.
  public GameObject map;            // Current map of the game.
  public Track track;               // Track of the current map.

  // Game stats.
  public int health = 100;
  public int money = 1000;
  public RoundData[] rounds;
  public int currentRound = 0;
  public int animalsLeft = 0;

  private bool roundInProgress;


  /**
   * Initialize the instance.
   */
  public void Awake() {
    instance = this;
  }


  /**
   * Spawns the animals for the current round.
   * 
   * @return IEnumerator - a "hack" to delay the spawns.
   */
  private IEnumerator spawnAnimals() {
    // Grab the animals for the current round.
    RoundData round = rounds[currentRound];

    // For every animal data for the round.
    foreach(AnimalData data in round.animals) {
      // For every data.
      for (int i = 0; i < data.count; ++i) {
        // Create animal.
        Animal animal = Instantiate(data.animal, track.GetWaypointPosition(0), Quaternion.identity);
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
    roundInProgress = false;
    money += round.moneyGain;
  }


  /**
   * Starts the next round if possible.
   */
  public void NextRound() {
    // Round is still in progress or there is still an animal left.
    if (roundInProgress || animalsLeft > 0) {
      return;
    }

    // Activate next round.
    roundInProgress = true;
    StartCoroutine(spawnAnimals());
    ++currentRound;
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
}
