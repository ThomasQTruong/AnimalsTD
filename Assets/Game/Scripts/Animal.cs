using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
  public float speed = 3.0f;      // How fast the animal is.
  public Vector2 velocity;        // The current velocity of the animal.
  public int damage = 1;          // Amount of damage done when animal gets through.
  public int health = 1;          // How tanky the animal is.
  public int value;               // Amount of money to give when dead.
  public DamageType resistances;  // The type of damage the animal is resistant against.

  private int _waypointIndex;  // Index of the waypoint it is going towards.


  /**
   * Gives motion to the animal and flips image based on X-direction of movement.
   */
  private void Update() {
    Vector2 nextPosition = GetNextPosition();
    velocity = (transform.position * GameData.instance.track.GetWaypointPosition(_waypointIndex)).normalized * speed;

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
      
      // Reached the end of the track.
      if (_waypointIndex >= GameData.instance.track.waypoints.Length) {
        Destroy(gameObject);
        GameData.instance.health -= damage;
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


  /**
   * Kills the animal by projectile.
   */
  private void Die() {
    Destroy(gameObject);
    GameData.instance.money += value;
  }


  /**
   * Makes the animal take a certain amount of damage.
   * 
   * @param damage - the amount of damage to take.
   * @param damageTypes - the type(s) of the damage.
   */
  public void TakeDamage(int damage, DamageType[] damageTypes) {
    health -= damage;
    
    // Animal died.
    if (health <= 0) {
      Die();
    }
  }
}
