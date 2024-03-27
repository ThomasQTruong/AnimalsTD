using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  public Tower parent;           // The tower that throws this projectile.
  public float lifetime = 5.0f;  // The amount of seconds it is alive for.


  private void Start() {
    // Kill projectile when its lifetime expires.
    Destroy(gameObject, lifetime);
  }


  // Gives motion to the projectile.
  private void Update() {
    transform.position = transform.up * parent.speed * Time.deltaTime;
  }
}