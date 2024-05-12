using System;


public enum UpgradeType {
  Damage,
  Fire_Rate,
  Range,
  Pierce
};


[Serializable]
public class Upgrade {
  public int MAX_LEVEL = 3;

  public UpgradeType upgradeType;
  public float value;
  public int price;
  public float priceScaling = 1;

  private int _level = 0;


  /**
   * Retrieves the level.
   */
  public int GetLevel() {
    return _level;
  }

  
  /**
   * Increases the level and increases the price based on the scaling.
   */
  public void UpgradeLevel() {
    // Already maxed level, do nothing.
    if (_level == MAX_LEVEL) {
      return;
    }

    ++_level;
    price = (int)(price * priceScaling);
  }
}
