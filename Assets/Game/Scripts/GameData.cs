using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
  public static GameData instance;  // Limits to one instance.
  
  public GameObject map;            // Current map of the game.
  public Track track;               // Track of the current map.

  public int difficulty = (int)Difficulty.Easy;  // The difficulty of the game.

  public GameObject[] maps;  // Holds all of the maps of the game.

  public string mapName;        // Name of the current map.
  public bool restart = false;  // Whether it is a restart or not.


  /**
   * Initialize the instance.
   */
  public void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(this);
    } else {
      Destroy(gameObject);
    }
  }


  /**
   * Obtains the map and track gameobjects.
   */
  public void SetMap() {
    map = GameObject.Find("Game/Map");
    track = map.transform.Find("Track").Find("TrackPoints").GetComponent<Track>();
  }
}
