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

  public int cost = 20;
  public GameObject radiusDisplay;
  public Projectile projectile;
  public GameObject mesh;
  private float nextFire;


  private void Update() {
    if (Time.time > nextFire) {
      Animal a = getClosest();
      if (a != null) {
        nextFire = Time.time + fireRate;
        shoot(a);
      }
    }
  }


  /**
   * helper function that returns all animals in range of tower
   */
  private Animal[] getAllInRange()
  {
    Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius);
    Animal[] animals = new Animal[col.Length];
    int i = 0;
    if (col.Length == 0)
      return animals;
    foreach (var c in col)
    {
      Animal a = c.gameObject.GetComponent<Animal>();
      if (a != null)
        animals[i] = a;
      i++;
    }
    return animals;
  }


  /**
   * uses AllInRange to get the specific animal to shoot
   */
  private Animal getClosest()
  {
    Animal[] animals = getAllInRange();
    Animal closest = null;
    float distance = float.MaxValue;
    foreach (Animal a in animals)
    {
      if (a == null) continue;
      float d = a.GetRemainingDistance();
      if (d < distance)
      {
        distance = d;
        closest = a;
      }
    }
    return closest;
  }


  /**
   * shooting function
   */
  private void shoot(Animal a)
  {
    float dist = Vector2.Distance(transform.position, a.transform.position);
    Vector2 prediction = (Vector2)a.transform.position - (a.velocity * dist / speed) * 2;
    transform.up = prediction - (Vector2)transform.position;
    Projectile p = Instantiate(projectile, transform.position, transform.rotation);
    p.parent = this;
  }
}
