using UnityEngine;
using System;


[Serializable]
public class AnimalSpawn {
  public GameObject animal;            // Animal prefab.
  public int startRound;               // The round that the animal starts spawning in.
  public float count = 5;              // Number of animals to spawn for the round.
  public float incrementPerRound = 3;  // Incremental value for count.
  public float spawnRate = 0.2f;       // Rate in which animal spawns one after another.
}
