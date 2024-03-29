using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  private Tower currentPlaceBuffer;
  private Camera cam;
  private bool isPlacing;

  private Tower selectedTower;


  private void Awake() {
    cam = Camera.main;
  }

  private void Update() {
    TrackClicks();
  }


  /**
   * Tracks the user's clicks for selection/deselection.
   */
  private void TrackClicks() {
    // User left clicked.
    if (Input.GetMouseButton(0)) {
      // Get mouse position.
      Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = -30;
      RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

      // Clicked on a collider.
      if (hit.collider != null) {
        Tower tower = hit.collider.gameObject.GetComponent<Tower>();
        // Collider is a tower and exists, select it.
        if (tower != null) {
          SelectTower(tower);
          return;
        }

        // Not a tower or shop menu.
        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Tower") &&
            hit.collider.gameObject.layer != LayerMask.NameToLayer("UI")) {
          UnselectTower();
        }
      }
    }
  }


  /**
   * User unselected the tower.
   */
  private void UnselectTower() {
    if (selectedTower != null) {
      UIManager.instance.DeselectTower();
      selectedTower.Selected(false);
      selectedTower = null;
    }
  }


  /**
   * User clicked on tower: select it.
   * 
   * @param tower - the tower that was clicked.
   */
  private void SelectTower(Tower tower) {
    selectedTower = tower;
    UIManager.instance.SelectTower(tower);
    selectedTower.Selected(true);
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
      // Get mouse position.
      Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = 0;
      RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

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
        
        // Unable to afford tower or out of bounds, stop placement.
        if (GameData.instance.money < currentPlaceBuffer.price
            || hit.collider.gameObject.layer != LayerMask.NameToLayer("Map")) {
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
