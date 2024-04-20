using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
  public static UIManager instance;
  public GameObject towerMenu;
  public GameObject upgradeMenu;

  // Info UI texts.
  public TMP_Text moneyText;
  public TMP_Text healthText;
  public TMP_Text roundText;

  private Tower selectedTower;

  /**public GameObject scrollBar;

  scrollBarList = scrollBar.getComponent<ScrollBar>()
  **/
  /**
   * Sells the tower that is selected.
   */
  public void SellSelectedTower() {
    GameData.instance.money += selectedTower.price / 2;
    Destroy(selectedTower.gameObject);
    DeselectTower();
  }


  /**
   * Selects a tower; menu to buy turret disappears and upgrade menu appears.
   * 
   * @param tower - the tower that is selected.
   */
  public void SelectTower(Tower tower){
    selectedTower = tower;
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
    healthText.text = "Health: " + GameData.instance.health;
    moneyText.text = "Money: " + GameData.instance.money;
    roundText.text = "Round: " + GameData.instance.currentRound;
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
    instance = this;
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
