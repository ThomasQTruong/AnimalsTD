using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
  public static GameData instance;
  public GameObject map;
  public Track track;


  /**
   * Only allows one instance.
   */
  public void Awake() {
    instance = this;
  }
}
