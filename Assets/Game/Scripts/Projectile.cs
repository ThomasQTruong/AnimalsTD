using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  public ThornTower parent;
  public float lifetime = 5f;

  private void Start()
  {
    Destroy(gameObject, lifetime);
  }


  private void Update()
  {
    transform.position = transform.up * parent.speed * Time.deltaTime;
  }
}