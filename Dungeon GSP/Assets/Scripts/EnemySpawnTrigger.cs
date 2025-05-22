using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [Tooltip("The Key ID the player must possess for enemies to spawn when exiting this trigger. Leave empty if no specific key is required (but player must still have *a* key if 'KeyRequiredAtAll' is true).")]
    public string specificKeyIDRequired = ""; // Set in Inspector if a specific key is needed

    [Tooltip("If true, the player must have picked up *at least one key* (any key) for this trigger to work. If 'Specific Key ID Required' is set, that takes precedence.")]
    public bool keyRequiredAtAll = true; // Set to false if spawning is not key-dependent

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public bool enemiesSpawned = false;

    void OnTriggerExit(Collider other)
    {
        if (enemiesSpawned || !other.CompareTag("Player") || PlayerInventory.Instance == null)
        {
            return;
        }

        bool canSpawn = false;
        if (!keyRequiredAtAll) // If no key is ever needed for this trigger
        {
            canSpawn = true;
        }
        else if (!string.IsNullOrEmpty(specificKeyIDRequired)) // If a specific key is listed
        {
            if (PlayerInventory.Instance.HasKey(specificKeyIDRequired))
            {
                canSpawn = true;
                Debug.Log("Player has specific key '" + specificKeyIDRequired + "'. Spawning enemies.");
            }
            else
            {
                Debug.Log("Player does not have specific key '" + specificKeyIDRequired + "' for enemy spawn.");
            }
        }
        else if (keyRequiredAtAll && PlayerInventory.Instance.HasAnyKey()) // If any key is sufficient (and no specific one is listed)
        {
            canSpawn = true;
            Debug.Log("Player has at least one key. Spawning enemies.");
        }
        else
        {
            Debug.Log("Player does not have any key / the required key for enemy spawn.");
        }


        if (canSpawn)
        {
            SpawnEnemies();
            enemiesSpawned = true;
        }
    }

    void SpawnEnemies()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs or spawn points assigned to the EnemySpawnTrigger.");
            return;
        }

        Debug.Log("Spawning enemies from trigger: " + gameObject.name);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i < enemyPrefabs.Length)
            {
                Instantiate(enemyPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            }
            else if (enemyPrefabs.Length > 0) // Cycle through prefabs if more spawn points than prefabs
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[i].position, spawnPoints[i].rotation);
            }
        }
    }
}