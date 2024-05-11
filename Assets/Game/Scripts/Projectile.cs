using UnityEngine;


/**
 * Projectile collision, motion, and data.
 */
public class Projectile : MonoBehaviour {
  public Tower parent;           // The tower that throws this projectile.
  public float lifetime = 5.0f;  // The amount of seconds it is alive for.

  private int _pierce;


  private void Start() {
    // Kill projectile when its lifetime expires.
    Destroy(gameObject, lifetime);
    _pierce = parent.pierce;
  }


  private void Update() {
    Motion();
  }


  /**
   * The movement of the projectile.
   */
  private void Motion() {
    transform.position += parent.speed * Time.deltaTime * transform.up;
  }


  /**
   * On collision, deal damage to animal.
   * 
   * @param collision - the Collider2D of the animal.
   */
  private void OnTriggerEnter2D(Collider2D collision) {
    // Get the animal that collided with projectile.
    Animal animal = collision.GetComponent<Animal>();

    // Animal and projectile exists.
    if (animal != null && gameObject != null) {
      // Only damage if the projectile can still pierce.
      if (_pierce > 0) {
        animal.TakeDamage(parent.damage, parent.damageTypes);
      }
      // Remove a pierce value and check if it should be destroyed.
      --_pierce;
      if (_pierce <= 0) {
        Destroy(gameObject);
      }
    }
  }
}
