using UnityEngine;
using TMPro;


public class UpgradeButton : MonoBehaviour {
  public Upgrade upgrade;
  public GameObject levelDisplay;
  public GameObject levelIncrement;
  public TMP_Text nameDisplay;
  public TMP_Text priceDisplay;


  /**
   * Sets up the button to display the name, value gained, and current level.
   */
  public void SetUpButton(Upgrade upgrade) {
    // Store upgrade.
    this.upgrade = upgrade;

    // Set text.
    nameDisplay.text = upgrade.upgradeType.ToString().Replace("_", " ") + " (+" + upgrade.value + ")";

    // Set up the level displayed.
    for (int i = 0; i < upgrade.GetLevel(); ++i) {
      Instantiate(levelIncrement, levelDisplay.transform);
    }

    // Set price.
    priceDisplay.text = "$" + upgrade.price;
  }


  /**
   * Updates the price displayed.
   */
  public void UpdatePrice() {
    if (upgrade.GetLevel() == upgrade.MAX_LEVEL) {
      priceDisplay.text = "MAX";
    } else {
      priceDisplay.text = "$" + upgrade.price;
    }
  }


  /**
   * Increases the level for the upgrade if player can afford it.
   */
  public void UpgradeLevel() {
    // Cannot afford.
    if (GameManager.instance.money < upgrade.price) {
      return;
    }
    // Cannot level any further.
    if (upgrade.GetLevel() == upgrade.MAX_LEVEL) {
      return;
    }
    // Cannot be upgraded anymore.
    if (!GameUIManager.instance.CanBeUpgraded()) {
      return;
    }

    // Can afford and upgrade; proceed with upgrading.
    GameManager.instance.money -= upgrade.price;
    upgrade.UpgradeLevel();
    GameUIManager.instance.UpgradedTower(upgrade);
    UpdatePrice();
    GameManager.instance.UpgradeTower(upgrade);
    Instantiate(levelIncrement, levelDisplay.transform);
  }
}