using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public Transform orientation; // orientation of camera

    //////////////////////////////// - Walk/Sprint Variables
    public float normalSpeed = 15;
    public float sprintSpeed = 30;
    public float stamina = 50;
    public float speed;
    float topSpeed = 500;
    bool moving;
    bool sprinting;
    bool isJumping;
    ////////////////////////////////
    
    //////////////////////////////// - Cam Movement Variables
    public float sensitivityX = 10;
    public float sensitivityY = 10;
    float xRotation;
    float yRotation;
    ///////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock and hide cursor
        Cursor.visible = false;
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {   // should be more fluid, aka movement should be relative to the last key pressed to avoid locking the player if they press multiple keys.
        // if I press A and hold then press D I should move right and vise versa. Same for W and S.


        float xInput = Input.GetAxisRaw("Mouse X") * sensitivityX * Time.deltaTime;
        float yInput = Input.GetAxisRaw("Mouse Y") * sensitivityY * Time.deltaTime;

        //Debug.Log(xInput + " " + yInput);

        yRotation += xInput;
        xRotation -= yInput;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);


        moving = false;
        sprinting = false;
        {
            ///////////////////////////////////////////////////////////////////////////// - Get Keyboard input for directional movement
            //if (Input.GetKey(KeyCode.W))
            //{
            //    transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            //    moving = true;
            //}
            //else if (Input.GetKey(KeyCode.S))
            //{
            //    transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            //    moving = true;
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    player.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            //    moving = true;
            //}
            //else if (Input.GetKey(KeyCode.D))
            //{
            //    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            //    moving = true;
            //}
            //if(Input.GetKey(KeyCode.Space))
            //{
            //    rb.AddForce(0f, 10f, 0f);
            //    moving = true;
            //}
        }

        if (Input.GetKey(KeyCode.W) && isJumping == false)
        {
            Debug.Log("Move attempt");
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            capspeed();
            moving = true;
        }
        if (Input.GetKey(KeyCode.S) && isJumping == false)
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            capspeed();
            moving = true;
        }
        if (Input.GetKey(KeyCode.D) && isJumping == false)
        {
            rb.AddForce(transform.right * speed * Time.deltaTime, ForceMode.VelocityChange);
            capspeed();
            moving = true;
        }
        if (Input.GetKey(KeyCode.A) && isJumping == false)
        {
            rb.AddForce(-transform.right * speed * Time.deltaTime, ForceMode.VelocityChange);
            capspeed();
            moving = true;
        }

        //////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////// - Sprint and stamina system
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 15 && moving)
        {
            speed = sprintSpeed; // set speed to sprinting and take off 20 stamina per second.
            stamina -= 20 * Time.deltaTime;
            sprinting = true;
        }

        else if (stamina <= 50)
        {
            speed = normalSpeed;
            if (!sprinting) // only regen stamina when player not sprinting
            {
                stamina += 2.5f * Time.deltaTime;
                if(stamina > 50) // cap stamina at 50
                {
                    stamina = 50;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////
    }


    void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }

    void capspeed()
    {
        Debug.Log("Capping Speed");
        Vector2 tmpXZ = new Vector2(rb.velocity.x, rb.velocity.z);
        if (tmpXZ.magnitude > topSpeed)
        {
            Debug.Log("top speed breached");
            tmpXZ = tmpXZ.normalized * topSpeed;
            rb.velocity = new Vector3(tmpXZ.x, rb.velocity.y, tmpXZ.y);
        }
    }
}
