using UnityEngine;

public class EnemyLootDropper : MonoBehaviour
{
    public GameObject treasurePrefab; // Assign your Treasure Prefab in the Inspector
    [Range(0f, 1f)]
    public float dropChance = 0.25f; // 25% chance

    private Damagable enemyDamagable; // Reference to the Damagable script (base of enemyBase)
    private bool hasDroppedLoot = false;

    void Start()
    {
        // Get the Damagable component on this same GameObject
        enemyDamagable = GetComponent<Damagable>();

        if (enemyDamagable == null)
        {
            Debug.LogError("EnemyLootDropper: Damagable component not found on " + gameObject.name +
                           ". This script needs to be on the same GameObject as a Damagable script (like enemyBase).");
            enabled = false; // Disable this script if no Damagable component
            return;
        }

        if (treasurePrefab == null)
        {
            Debug.LogWarning("EnemyLootDropper: Treasure Prefab not assigned on " + gameObject.name +
                             ". This enemy will not drop loot.");
        }
    }

    void Update()
    {
        // Check if the enemy is dead and loot hasn't been dropped yet
        if (!hasDroppedLoot && enemyDamagable != null && enemyDamagable.health <= 0)
        {
            TryDropLoot();
            hasDroppedLoot = true; // Ensure loot drop attempt only happens once
        }
    }

    void TryDropLoot()
    {
        if (treasurePrefab == null) return; // No prefab to drop

        if (Random.value <= dropChance) // Random.value is between 0.0 and 1.0
        {
            Debug.Log(gameObject.name + " is dropping treasure!");
            Instantiate(treasurePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log(gameObject.name + " did not drop treasure (chance failed).");
        }
    }
}