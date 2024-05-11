using UnityEngine;
using System;


[Serializable]
public class AnimalSpawn {
  public GameObject animal;      // Animal prefab.
  public int startRound;         // The round that the animal starts spawning in.
  public int count;              // Number of animals to spawn for the round.
  public int incrementPerRound;  // Incremental value for count.
  public float spawnRate;        // Rate in which animal spawns one after another.
}
