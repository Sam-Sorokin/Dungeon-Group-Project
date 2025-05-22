using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public Image healthFill;
    PlayerHealth playerHealth;
    public int maxHealth;
    public float health;
    // Update is called once per frame

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        maxHealth = playerHealth.health;
    }
    void Update()
    {
        float healthRatio = (float)playerHealth.health / playerHealth.maxHealth;
        healthFill.fillAmount = healthRatio;
    }
}
