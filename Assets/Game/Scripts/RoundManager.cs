using System.Collections.Generic;
using UnityEngine;


public class RoundManager : MonoBehaviour {
  public static RoundManager instance;

  public int moneyPerRound;  // Amount of money gained per round.
  public float spawnDelay;   // Delay for spawning the next animal.

  public List<AnimalSpawn> animalsList;  // List of every animal possible.
  public List<AnimalSpawn> spawnList;    // List of animals to spawn for the round.


  /**
   * Limits to one instance.
   */
  public void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this);
    }
  }


  /**
   * Checks and adds to spawn list if an animal should be spawned yet.
   * 
   * round - the current round.
   */
  public void CheckForSpawn(int round) {
    // No animals left to check.
    if (animalsList.Count == 0) {
      return;
    }
    // Not the correct round.
    if (animalsList[0].startRound != round) {
      return;
    }

    // Is correct round: move animal to spawn list.
    spawnList.Add(animalsList[0]);
    instance.animalsList.RemoveAt(0);
  }
}
