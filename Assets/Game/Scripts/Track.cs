using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
  public Transform[] waypoints;

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
  }

  private void OnDrawGizmos() {
    for (int i = 0; i < waypoints.Length; ++i) {
      Gizmos.color = Color.cyan;
      Gizmos.DrawSphere(waypoints[i].position, 0.2f);
      if (i > 0) {
        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
      }
    }
  }
}
