using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : Damagable
{
    public int health = 10;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (other.GetComponent<enemytracking>().isattacking == true)
            {
                health--;
            }
        }
    }
}
