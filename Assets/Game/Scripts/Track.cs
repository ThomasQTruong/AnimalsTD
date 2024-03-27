using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
  public Transform[] waypoints;  // Depicts the pathing of the animals.


  /**
   * Draw circular overlay for waypoints and connection lines toward waypoints.
   */
  private void OnDrawGizmos() {
    // For every waypoint.
    for (int i = 0; i < waypoints.Length; ++i) {
      // Circular overlay.
      Gizmos.color = Color.cyan;
      Gizmos.DrawSphere(waypoints[i].position, 0.15f);

      // Connection lines.
      if (i > 0) {
        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
      }
    }
  }


  /**
   * Retrieve a waypoint's position.
   * 
   * @param index - the waypoint to get.
   */
  public Vector2 GetWaypointPosition(int index) {
    return waypoints[index].position;
  }


  /**
   * Gets the distance away from the end at the current position.
   * 
   * @param index - the current waypoint's index.
   * @param pos - the current position of the object.
   */
  public float GetCumulativeDist(int index, Vector2 pos) {
    // Current distance away from the target waypoint.
    float distance = Vector2.Distance(pos, waypoints[index].position);

    // Sum the distance to every other waypoint.
    for (int i = index; i < waypoints.Length - 1; ++i) {
      distance += Vector2.Distance(waypoints[i].position, waypoints[i + 1].position);
    }

    return distance;
  }
}
