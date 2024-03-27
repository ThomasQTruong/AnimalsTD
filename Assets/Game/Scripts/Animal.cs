using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
  public float speed = 3.0f;   // How fast the animal is.
  public Vector2 velocity;     //Connects to Tower math
  public int damage = 1;       // Amount of damage done when animal gets through.
  public int health = 1;       // How tanky the animal is.
  public int value;            // Amount of money to give when dead.
  private int _waypointIndex;  // Index of the waypoint it is going towards.


  private void Update() {
    Vector2 nextPosition = GetNextPosition();
    velocity = (transform.position * GameData.instance.track.GetWaypointPosition(_waypointIndex)) * speed;

    // Readjust rotation of the Sprite.
    Vector2 direction = (Vector2)gameObject.transform.position - nextPosition;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    if (direction.x < 0) {  // Going right, flip Y to make it upright.
      GetComponent<SpriteRenderer>().flipY = true;
    } else {  // Going left, unflip.
      GetComponent<SpriteRenderer>().flipY = false;
    }

    // Move animal to next position.
    transform.position = nextPosition;

    // Reached a waypoint, move to next waypoint.
    if (Vector2.Distance(transform.position, GameData.instance.track.GetWaypointPosition(_waypointIndex)) < 0.002f) {
      ++_waypointIndex;
      
      // Reached the end of the track..
      if (_waypointIndex >= GameData.instance.track.waypoints.Length) {
        Destroy(gameObject);
      }
    }
  }


  /**
   * Gets the distance away from the end.
   * 
   * @return float - the distance away from the end.
   */
  public float GetRemainingDistance() {
    return GameData.instance.track.GetCumulativeDist(_waypointIndex, transform.position);
  }


  /**
   * Calculates the next position to go to.
   *
   * @return Vector2 - the next position.
   */
  private Vector2 GetNextPosition() {
    Vector2 nextPosition = GameData.instance.track.GetWaypointPosition(_waypointIndex);
    Vector2 direction = nextPosition - (Vector2)transform.position;

    return (Vector2)transform.position + speed * Time.deltaTime * direction.normalized;
  }
}
