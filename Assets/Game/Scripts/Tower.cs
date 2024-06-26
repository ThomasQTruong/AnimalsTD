using UnityEngine;


/**
 * Tower AI, data, and settings.
 */
public class Tower : MonoBehaviour {
  public new string name;
  public float fireCooldown = 1;
  public float range = 1;

  public int damage = 1;
  public float speed = 11;
  public int pierce = 1;
  public bool AOE = false;
  public DamageType[] damageTypes;  // The types of damage the tower does.
  public Upgrade[] upgrades;        // The possible upgrades the tower has.
  public int maxUpgrades;           // Amount of total upgrades allowed for the tower.
  public int upgradeCount = 0;      // Current amount of upgrades.

  public GameObject radiusDisplay;
  public Projectile projectile;
  public GameObject mesh;
  private float _nextFire;
  public int price = 20;  // How much the tower costs.
  private int _value;     // Total value of the tower (price + upgrades).


  private void Awake() {
    _value = price;
  }


  private void Update() {
    ShootAtAnimalsInRange();
  }


  /**
   * Shoots at the animals if they are in range.
   */
  private void ShootAtAnimalsInRange() {
    // Spam shoot prevention.
    if (Time.time > _nextFire) {
      Animal animal = GetClosestToEnd();
      // Animal still exists.
      if (animal != null) {
        _nextFire = Time.time + fireCooldown;
        Shoot(animal);
      }
    }
  }


  /**
   * Enables selection visualization if selected.
   * 
   * @param isSelected - if the tower is selected or not.
   */
  public void Selected(bool isSelected) {
    radiusDisplay.SetActive(isSelected);
    UpdateRadius();
  }


  /**
   * Updates the radius size.
   */
  public void UpdateRadius() {
    radiusDisplay.transform.localScale = new Vector2(range, range);
  }


  /**
   * Retrieves all the animals in the range of the tower.
   * 
   * @return Animal[] - the list of animals in range.
   */
  private Animal[] GetAllAnimalsInRange() {
    // Gets all overlapping Collider2Ds.
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
    Animal[] animals = new Animal[colliders.Length];

    // No animals near.
    if (colliders.Length == 0) {
      return animals;
    }

    // For every collider.
    for (int i = 0; i < colliders.Length; ++i) {
      // Get the animal that has the collider.
      Animal animal = colliders[i].gameObject.GetComponent<Animal>();
      // Animal exists, add to list.
      if (animal != null) {
        animals[i] = animal;
      }
    }

    return animals;
  }


  /**
   * uses AllInRange to get the specific animal to shoot
   */
  private Animal GetClosestToEnd() {
    Animal[] animals = GetAllAnimalsInRange();
    Animal closest = null;
    float closestDistance = float.MaxValue;

    // For every animal in range.
    foreach (Animal animal in animals)
    {
      // Animal does not exist anymore.
      if (animal == null) {
        continue;
      }

      // Found animal that is closer, update variable.
      float distance = animal.GetRemainingDistance();
      if (distance < closestDistance) {
        closestDistance = distance;
        closest = animal;
      }
    }

    return closest;
  }


  /**
   * Shoots a projectile towards animal.
   * 
   * @param target - the animal to shoot at.
   */
  private void Shoot(Animal target) {
    float dist = Vector2.Distance(transform.position, target.transform.position);
    Vector2 prediction = (Vector2)target.transform.position - (target.velocity * dist / (speed * 2));
    transform.up = prediction - (Vector2)transform.position;
    Projectile p = Instantiate(projectile, transform.position, transform.rotation);
    p.parent = this;
  }


  /**
   * Retrives the value of the tower.
   * 
   * @return int - the value of the tower.
   */
  public int GetValue() {
    return _value;
  }


  /**
   * Changes the value by a given amount.
   * 
   * @param amount - the amount to change the value by.
   */
  public void ChangeValue(int amount) {
    _value += amount;
  }


  /**
   * Draws the radius of the tower (only for editor, not user).
   */
  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
