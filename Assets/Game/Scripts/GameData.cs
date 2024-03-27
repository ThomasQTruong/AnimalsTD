using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The types of damage in the game.
public enum DamageType
{
  Standard,
  Explosive,
  Fire,
  Ice
}


public class GameData : MonoBehaviour
{
  public static GameData instance;  // Limits to one instance.
  public GameObject map;            // Current map of the game.
  public Track track;               // Track of the current map.


  /**
   * Initialize the instance.
   */
  public void Awake() {
    instance = this;
  }
}
