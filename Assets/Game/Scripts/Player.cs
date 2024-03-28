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
    while(isPlacing) {
      Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = 0;

      // Is a left click.
      if (Input.GetMouseButtonDown(0)) {
        // Has enough money.
        if (GameData.instance.money >= currentPlaceBuffer.price) {
          // Place tower.
          PlaceTower(mousePos);

          // Reduces money and ends the placement process.
          GameData.instance.money -= currentPlaceBuffer.price;
          currentPlaceBuffer = null;
          isPlacing = false;
        }
      }

      yield return null;
    }
  }
}
