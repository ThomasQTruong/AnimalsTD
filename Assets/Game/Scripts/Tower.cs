using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
  public string name;
  public float fireRate = 1;
  public float radius = 1;

  public int damage = 1;
  public float speed = 11;
  public int pierce = 1;
  public bool AOE = false;
  public DamageType[] damageTypes;

  public int cost = 20;
  public GameObject radiusDisplay;
  public Projectile projectile;
  public GameObject mesh;
  private float nextFire;


  private void Update() {
    // Spam shoot prevention.
    if (Time.time > nextFire) {
      Animal animal = GetClosestToEnd();
      // Animal still exists.
      if (animal != null) {
        nextFire = Time.time + fireRate;
        Shoot(animal);
      }
    }
  }


  /**
   * Retrieves all the animals in the range of the tower.
   * 
   * @return Animal[] - the list of animals in range.
   */
  private Animal[] GetAllAnimalsInRange() {
    // Gets all overlapping Collider2Ds.
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    Animal[] animals = new Animal[colliders.Length];

    // No animals near.
    if (colliders.Length == 0) {
      return animals;
    }

    // For every collider.
    for (int i = 0; i < colliders.Length; ++i) {
      // Get the animal that has the collider.
      Animal animal = colliders[i].gameObject.GetComponent<Animal>();
      // Animal exists, add to list.
      if (animal != null) {
        animals[i] = animal;
      }
    }

    return animals;
  }


  /**
   * uses AllInRange to get the specific animal to shoot
   */
  private Animal GetClosestToEnd() {
    Animal[] animals = GetAllAnimalsInRange();
    Animal closest = null;
    float closestDistance = float.MaxValue;

    // For every animal in range.
    foreach (Animal animal in animals)
    {
      // Animal does not exist anymore.
      if (animal == null) {
        continue;
      }

      // Found animal that is closer, update variable.
      float distance = animal.GetRemainingDistance();
      if (distance < closestDistance) {
        closestDistance = distance;
        closest = animal;
      }
    }

    return closest;
  }


  /**
   * Shoots a projectile towards animal.
   * 
   * @param target - the animal to shoot at.
   */
  private void Shoot(Animal target) {
    float dist = Vector2.Distance(transform.position, target.transform.position);
    Vector2 prediction = (Vector2)target.transform.position - (target.velocity * dist / speed) * 2;
    transform.up = prediction - (Vector2)transform.position;
    Projectile p = Instantiate(projectile, transform.position, transform.rotation);
    p.parent = this;
  }


  /**
   * Draws the radius of the tower.
   */
  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
