using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
  public float speed = 3.0f;  // How fast the animal is.
  public int damage = 1;      // Amount of damage done when animal gets through.
  public int health = 1;      // How tanky the animal is.
  public int value;           // Amount of money to give when dead.

  private void getNextPosition() {
    // Vector2 nextPosition = ...
  }
}
