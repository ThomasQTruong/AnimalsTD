using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
  public float speed = 3.0f;  // How fast the animal is.
  public int damage = 1;      // Amount of damage done when animal gets through.
  public int health = 1;      // How tanky the animal is.
  public int value;           // Amount of money to give when dead.
  private int index;


  private void Update() {
    transform.position = (Vector2)getNextPosition();
    
    if (Vector2.Distance((Vector2)transform.position, GameData.instance.track.getWaypointPosition(index)) < 0.2f) {
      ++index;
      if (index >= GameData.instance.track.waypoints.Length) {
        Destroy(gameObject);
      }
    }
  }


  private Vector2 getNextPosition() {
    Vector2 nextPosition = GameData.instance.track.getWaypointPosition(index);
    Vector2 direction = nextPosition - (Vector2)transform.position;

    return (Vector2)transform.position + speed * Time.deltaTime * direction.normalized;
  }
}
