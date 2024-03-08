using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
  public Transform[] waypoints;  // Depicts the pathing of the animals.

  public Vector2 getWaypoint(int index) {
    return (Vector2)waypoints[index].position;
  }

  /**
   * Draw circular overlay for waypoints and connection lines toward waypoints.
   */
  private void OnDrawGizmos() {
    // For every waypoint.
    for (int i = 0; i < waypoints.Length; ++i) {
      // Circular overlay.
      Gizmos.color = Color.cyan;
      Gizmos.DrawSphere(waypoints[i].position, 0.2f);

      // Connection lines.
      if (i > 0) {
        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
      }
    }
  }
}
