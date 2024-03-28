using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  private Tower currentPlaceBuffer;
  private Camera cam;
  private bool isPlacing;


  private void Awake() {
    cam = Camera.main;
  }


  /**
   * Starts the placement process for the selected tower.
   * 
   * @param tower - the tower being placed.
   */
  public void StartPlacement(Tower tower) {
    // Already placing! Ignore.
    if (isPlacing) {
      return;
    }

    // Sets the buffer and switches boolean for placement.
    currentPlaceBuffer = tower;
    isPlacing = true;

    // Starts the placement process.
    StartCoroutine(Place());
  }


  /**
   * Places the tower at a specified position.
   * 
   * @param position - the position to place the tower at.
   */
  private void PlaceTower(Vector3 position) {
    // Create tower and prevent from being placed under the background.
    position.z = -5;
    Instantiate(currentPlaceBuffer.gameObject, position, Quaternion.identity);
  }


  /**
   * Placement process: while the user is currently trying to place a tower.
   */
  IEnumerator Place() {
    // Create a visualization for the user to see while placing.
    GameObject ghost = Instantiate(currentPlaceBuffer.mesh, Vector3.zero, Quaternion.identity);
    GameObject radius = Instantiate(currentPlaceBuffer.radiusDisplay, Vector3.zero, Quaternion.identity);
    radius.transform.localScale = new Vector2(currentPlaceBuffer.radius * 2, currentPlaceBuffer.radius * 2);

    while (isPlacing) {
      Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = 0;

      // Makes visualizations follow the mouse.
      ghost.transform.position = mousePos;
      radius.transform.position = mousePos;

      // Is a left click.
      if (Input.GetMouseButtonDown(0)) {
        // Toggle placement flag.
        isPlacing = false;

        // Remove visualization after placing.
        Destroy(ghost);
        Destroy(radius);
        
        // Unable to afford tower, stop placement.
        if (GameData.instance.money < currentPlaceBuffer.price) {
          currentPlaceBuffer = null;
          break;
        }

        // Has money, place tower and remove money.
        PlaceTower(mousePos);
        GameData.instance.money -= currentPlaceBuffer.price;
        currentPlaceBuffer = null;
      }

      yield return null;
    }
  }
}
