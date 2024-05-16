using UnityEngine;
using TMPro;


/**
 * Tools for managing the game interfaces UI (i.e. shop).
 */
public class GameUIManager : MonoBehaviour {
  public static GameUIManager instance;
  public GameObject towerMenu;
  public GameObject upgradeMenu;

  // Info UI texts.
  public TMP_Text moneyText;
  public TMP_Text healthText;
  public TMP_Text roundText;

  private Tower _selectedTower;

  // Game Over/Win UI texts.
  public TMP_Text gameOverRound;
  public TMP_Text winRound;
 

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
   * Updates the Info UI every frame.
   */
  void Update() {
    UpdateInfoUI();
  }


  /**
   * Sells the tower that is selected.
   */
  public void SellSelectedTower() {
    // Tower doesn't exist anymore!
    if (_selectedTower == null) {
      return;
    }
    // Tower exists, sell.
    Destroy(_selectedTower.gameObject);
    GameManager.instance.money += (int)(_selectedTower.GetValue() * GameManager.instance.sellRatio);
    DeselectTower();
  }


  /**
   * Selects a tower; hides shop menu and shows upgrade menu.
   * 
   * @param tower - the tower that is selected.
   */
  public void SelectTower(Tower tower) {
    if (_selectedTower == tower) {
      return;
    }

    _selectedTower = tower;
    towerMenu.SetActive(false);
    UpgradeUIManager.instance.UpdateMenu(tower);
    upgradeMenu.SetActive(true);
  }


  /**
   * Unselects a tower; makes upgrade menu disappear and tower menu reappear.
   */
  public void DeselectTower() {
    _selectedTower = null;
    towerMenu.SetActive(true);
    upgradeMenu.SetActive(false);
  }


  /**
   * Updates the Health, Money, and Round info for the UI.
   */
  public void UpdateInfoUI() {
    healthText.text = "Health: " + GameManager.instance.health;
    moneyText.text = "Money: " + GameManager.instance.money;

    // Not over the winning round (not in free play): display out of winRound.
    if (GameManager.instance.currentRound <= GameManager.instance.winRound) {
      roundText.text = "Round: " + GameManager.instance.currentRound + "/" + GameManager.instance.winRound;
    } else {  // Is freeplay, just display round number.
      roundText.text = "Round: " + GameManager.instance.currentRound;
    }
  }


  /**
   * Changes the selected tower's value by an amount.
   * 
   * @param amount - the amount to change the tower's value by.
   */
  public void ChangeTowerValue(int amount) {
    // Tower doesn't exist!
    if (_selectedTower == null) {
      return;
    }

    // Tower exists, change value.
    _selectedTower.ChangeValue(amount);
  }


  /**
   * Increases the selected tower's upgrade count.
   */
  public void IncreaseUpgradeCount() {
    ++_selectedTower.upgradeCount;
  }


  /**
   * Checks if selected tower can be upgraded any further.
   * 
   * @return bool - whether the tower can still be upgraded or not.
   */
  public bool CanBeUpgraded() {
    // Cannot upgrade non-existent towers.
    if (_selectedTower == null) {
      return false;
    }
    return _selectedTower.upgradeCount < _selectedTower.maxUpgrades;
  }


  /**
   * Applies the value changes when a tower is upgraded.
   * 
   * @param price - the price of the applied upgrade.
   */
  public void UpgradedTower(Upgrade upgrade) {
    if (_selectedTower == null) {
      return;
    }

    ChangeTowerValue(upgrade.price);
    IncreaseUpgradeCount();
    UpgradeUIManager.instance.UpdateCount(_selectedTower);
    UpgradeUIManager.instance.UpdateSellValue(_selectedTower);
  }


  /**
   * Retrieves the selected tower.
   * 
   * @return Tower - the current selected tower.
   */
  public Tower GetSelectedTower() {
    return _selectedTower;
  }


  /**
   * Updates the round text for the game over UI.
   */
  public void GameOverUpdateRound() {
    gameOverRound.text = "Round: " + GameManager.instance.currentRound;
  }


  /**
   * Updates the round text for the win UI.
   */
  public void WinUpdateRound() {
    winRound.text = "Round: " + GameManager.instance.currentRound + "/" + GameManager.instance.winRound;
  }


  // Fusion menu for combinable towers to be implemented in the future if time allows it.
  /*
  public void fuseTower(Tower tower1, Tower tower2) {
    if(tower1 + tower2 == fusionTowerID) {
      tower1 = fusionTowerID;
      // or something like that
    }
  }
  or something similar
  */
}
