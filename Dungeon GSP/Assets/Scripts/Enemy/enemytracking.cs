using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemytracking : MonoBehaviour
{
    public Transform playertransform;
    private NavMeshAgent nma;
    public bool isattacking;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform != null)
        {
            nma.SetDestination(playertransform.position);
        }

        
        //transform.position = Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
    }
}
