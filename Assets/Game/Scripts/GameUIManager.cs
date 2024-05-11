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
    GameManager.instance.money += (int)(_selectedTower.price * 0.8);
    DeselectTower();
  }


  /**
   * Selects a tower; menu to buy turret disappears and upgrade menu appears.
   * 
   * @param tower - the tower that is selected.
   */
  public void SelectTower(Tower tower) {
    _selectedTower = tower;
    towerMenu.SetActive(false);
    upgradeMenu.SetActive(true);
  }


  /**
   * Unselects a tower; makes upgrade menu disappear and tower menu reappear.
   */
  public void DeselectTower() {
    towerMenu.SetActive(true);
    upgradeMenu.SetActive(false);
  }


  /**
   * Updates the Health, Money, and Round info for the UI.
   */
  public void UpdateInfoUI() {
    healthText.text = "Health: " + GameManager.instance.health;
    moneyText.text = "Money: " + GameManager.instance.money;
    roundText.text = "Round: " + GameManager.instance.currentRound;
  }


  /**
   * Updates the Info UI every frame.
   */
  void Update() {
    UpdateInfoUI();
  }


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
