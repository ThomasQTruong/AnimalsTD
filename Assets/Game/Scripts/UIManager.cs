using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text healthText;
    public TMP_Text roundText;

    public GameObject towerMenu;
    public GameObject upgradeMenu;


    private Tower selectedTower;

    public void sellSelectedTower(){
        GameData.instance.money += selectedTower.price / 2;
        Destroy(selectedTower.gameObject);
        deselectTower();
    }

    //After selecting a tower, menu to buy turret disappears and upgrade menu appears
    public void selectTower(Tower t){
        selectedTower = t;
        towerMenu.SetActive(false);
        upgradeMenu.SetActive(true);
    }

    //Clicking off of a tower will make upgrade menu disappear and tower menu reappear
    public void deselectTower(){
        towerMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }

    //Fusion menu for combinable towers to be implemented in the future if time allows i
    /*
    public void fuseTower(Tower t1, Tower t2)
    {
        if(t1 + t2 == fusionTowerID)
        {
            t1 = fusionTowerID;
            // or something like that
        }

    }
    or something similar
    */

    public void updateTextUi(){
        healthText.text = "Health: " + GameData.instance.health;
        moneyText.text = "Money: " + GameData.instance.money;
        roundText.text = "Round: " + GameData.instance.currentRound;
    }

    // Update is called once per frame
    void Update(){
        updateTextUi();
    }

    //Sometimes might display instance doesn't exist in this context
    private void Awake(){
        instance = this;
    }
}
