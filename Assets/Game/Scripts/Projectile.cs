using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {
  public Tower parent;           // The tower that throws this projectile.
  public float lifetime = 5.0f;  // The amount of seconds it is alive for.

  private int pierce;


  /**
   * Kill projectile when its lifetime expires.
   */
  private void Start() {
    Destroy(gameObject, lifetime);
    pierce = parent.pierce;
  }


  /**
   * Gives motion to the projectile.
   */
  private void Update() {
    transform.position += transform.up * parent.speed * Time.deltaTime;
  }


  /**
   * On collision, deal damage to animal.
   * 
   * @param collision - the Collider2D of the animal.
   */
  private void OnTriggerEnter2D(Collider2D collision) {
    // Get the animal that collided with projectile.
    Animal animal = collision.GetComponent<Animal>();

    // Animal exists.
    if (animal != null) {
      if (pierce <= 0) {
        Destroy(gameObject);
        return;
      }
      --pierce;
      animal.TakeDamage(parent.damage, parent.damageTypes);
    }
  }
}
