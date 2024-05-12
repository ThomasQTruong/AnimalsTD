using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpgradeUIManager : MonoBehaviour {
  public static UpgradeUIManager instance;

  public TMP_Text towerName;
  public TMP_Text upgradesCount;
  public TMP_Text sellValue;
  public GameObject panel;
  public GameObject upgradePrefab;


  /**
   * Limits object to one instance.
   */
  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
  }


  /**
   * Removes all of the upgrade buttons from the panel.
   */
  public void ClearPanel() {
    for (int i = panel.transform.childCount - 1; i > 0; --i) {
      Destroy(panel.transform.GetChild(i).gameObject);
    }
  }


  /**
   * Shows the possible upgrades for a selected tower.
   * 
   * @param tower - the selected tower to show upgrades for.
   */
  public void ShowUpgrades(Tower tower) {
    // Clears the panel before displaying upgrades.
    ClearPanel();

    foreach (Upgrade upgrade in tower.upgrades) {
      GameObject prefab = Instantiate(upgradePrefab, panel.transform);
      prefab.GetComponent<UpgradeButton>().SetUpButton(upgrade);
    }
  }


  /**
   * Update menu with the tower's name.
   * 
   * @param tower - the tower with the name to display.
   */
  public void UpdateName(Tower tower) {
    towerName.text = tower.name;
  }


  /**
   * Update menu with upgrade count.
   * 
   * @param tower - the tower to display the upgrade count for.
   */
  public void UpdateCount(Tower tower) {
    upgradesCount.text = tower.upgradeCount + "/" + tower.maxUpgrades;
  }


  /**
   * Updates the sell value for the tower.
   * 
   * @param tower - the tower to update sell value with.
   */
  public void UpdateSellValue(Tower tower) {
    sellValue.text = "$" + (int)(tower.GetValue() * GameManager.instance.sellRatio);
  }


  /**
   * Updates the upgrade menu for a tower.
   * 
   * @param tower - the tower to display information about.
   */
  public void UpdateMenu(Tower tower) {
    ShowUpgrades(tower);
    UpdateName(tower);
    UpdateCount(tower);
    UpdateSellValue(tower);
  }
}
