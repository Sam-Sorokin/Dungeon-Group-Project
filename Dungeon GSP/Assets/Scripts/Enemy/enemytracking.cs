using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemytracking : MonoBehaviour
{
    public Animator animator;
    public Transform playertransform;
    private NavMeshAgent nma;
    public bool attacking = false;
    public bool iSwalking = false;
    public float speed;
    private float originalSpeed;
    public float distanceFromPlayer;

    [Header("Tracking Distances")]
    public float patrolDistance = 5f;
    public float trackDistance = 3.2f;

    private void OnDrawGizmosSelected() // drawing the attack ranges to visualise the distance in the scene view
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolDistance);
        Gizmos.DrawWireSphere(transform.position, trackDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        nma = GetComponent<NavMeshAgent>();
        originalSpeed = speed;
    }

    void HandleMovement()
    {
        distanceFromPlayer = Vector3.Distance(playertransform.position, transform.position);
        Vector3 directionToPlayer = (playertransform.position - transform.position).normalized;


        iSwalking = false;
        if(distanceFromPlayer <= patrolDistance && distanceFromPlayer >= trackDistance)
        {
            //Debug.Log("TrackingPlayer");
            nma.SetDestination(playertransform.position);
            iSwalking = true;
        }
        else if(distanceFromPlayer <= trackDistance)
        {
            nma.SetDestination(transform.position);
            // nav mesh agent stops tracking so we need to manually adjust rotation.
            // Rotate to look at the player
            Vector3 lookDirection = playertransform.position - transform.position;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // smoothenned rotation
            }
            iSwalking = false;
        }
        animator.SetBool("Walking", iSwalking);
    }

    // Public call
    public void SlowMovement(float _modifier, float _duration)
    {
        StartCoroutine(SlowSpeed(_modifier, _duration));
    }

    // Modify the speed of the enemy, for Ice
    // Modifier should be between 0 and 1 to
    // change speed by a percentage
    IEnumerator SlowSpeed(float _modifier, float _duration)
    {
        animator.SetFloat("SpeedMultiplier", _modifier);
        speed = speed * _modifier;
        yield return new WaitForSeconds(_duration);
        speed = originalSpeed;
        animator.SetFloat("SpeedMultiplier", 1);
    }



    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
