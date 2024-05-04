using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour {
  public GameObject difficultySelection;  // The difficulty selection object.
  public GameObject mapDisplayer;         // The object that displays the map's image.
  public TMP_Text mapName;              // The object that displays the map's name.


  public void DisplayMap(GameObject mapButton) {
    mapDisplayer.GetComponent<Image>().sprite = mapButton.GetComponent<Image>().sprite;
    mapName.text = mapButton.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
  }


  public void SetActiveDifficultySelection(bool visible) {
    difficultySelection.SetActive(visible);
  }
}
