using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : Damagable
{
    // Original damage indication variables
    private Renderer[] renderers;
    private float duration;
<<<<<<< HEAD

    private List<Color> originalColors = new List<Color>();
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    private List<Color> originalColors = new List<Color>();

    // Treasure system variables
    [Header("Treasure Settings")]
    [SerializeField] private GameObject treasurePrefab;
    [SerializeField][Range(0f, 1f)] private float dropChance = 0.25f;
    [SerializeField] private float pickupRadius = 1.5f;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    // Treasure tracking
    private static GameObject currentTreasure;
    private static Vector3 treasurePosition;
    private static bool treasureActive = false;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> HK
    // Start is called before the first frame update
    void Start()
    {
        // Original material caching
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                originalColors.Add(mat.color);
            }
        }
    }

    // Original damage method with flashing effect
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        duration = Time.time + 0.2f;
        StartCoroutine(damageIndication());
    }

    // Damage indication coroutine (flashing red)
    IEnumerator damageIndication()
    {
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        // Store all original colors for every material
        List<Color> originalColors = new List<Color>();

=======
        // Flash red
>>>>>>> Stashed changes
=======
        // Flash red
>>>>>>> Stashed changes
=======
        // Flash red
>>>>>>> Stashed changes
>>>>>>> HK
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                originalColors.Add(mat.color);
>>>>>>> HK
                mat.color = new Color(1f,0f,0f,1f);
=======
                mat.color = new Color(1f, 0f, 0f, 1f);
>>>>>>> Stashed changes
=======
                mat.color = new Color(1f, 0f, 0f, 1f);
>>>>>>> Stashed changes
=======
                mat.color = new Color(1f, 0f, 0f, 1f);
>>>>>>> Stashed changes
            }
        }

        yield return new WaitForSeconds(0.2f);

        // Restore original colors
        int colorIndex = 0;
        foreach (Renderer r in renderers)
        {
            Debug.Log("Returning to original colours.");
            foreach (Material mat in r.materials)
            {
                mat.color = originalColors[colorIndex];
                colorIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Original death check
        if (health <= 0)
        {
            Death();
        }

        // Treasure pickup check (runs continuously)
        if (treasureActive)
        {
            CheckForPickup();
        }
    }

    // Death method with treasure drop
    private void Death()
    {
        // Roll for treasure drop
        if (Random.value <= dropChance)
        {
            DropTreasure();
        }

        // Destroy the enemy
        Destroy(gameObject);
    }

    // Creates treasure at death position
    private void DropTreasure()
    {
        if (treasurePrefab != null)
        {
            currentTreasure = Instantiate(treasurePrefab, transform.position, Quaternion.identity);
            treasurePosition = transform.position;
            treasureActive = true;
            Debug.Log("Treasure dropped at " + treasurePosition);
        }
    }

    // Handles treasure pickup logic
    private void CheckForPickup()
    {
        if (currentTreasure == null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, treasurePosition);

        // Show pickup prompt when in range
        if (distance <= pickupRadius)
        {
            Debug.Log("Press E to pick up treasure (Distance: " + distance + ")");

            if (Input.GetKeyDown(pickupKey))
            {
                PickUpTreasure();
            }
        }
    }

    // Executes when treasure is collected
    private void PickUpTreasure()
    {
        Debug.Log("Treasure collected!");

        // Add your reward logic here (score, items, etc.)

        // Clean up treasure
        Destroy(currentTreasure);
        currentTreasure = null;
        treasureActive = false;
    }

    // Attack method for derived classes
    public virtual void Attack()
    {
        // To be implemented in child classes
    }
}