using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;

    //////////////////////////////// - Walk/Sprint Variables
    public float normalSpeed = 15;
    public float sprintSpeed = 30;
    public float stamina = 50;
    float speed;
    bool moving;
    bool sprinting;
    ////////////////////////////////
    
    //////////////////////////////// - Cam Movement Variables
    public float sensitivityX = 10;
    public float sensitivityY = 10;
    float xInput;
    float yInput;
    ///////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock and hide cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {   // should be more fluid, aka movement should be relative to the last key pressed to avoid locking the player if they press multiple keys.
        // if I press A and hold then press D I should move right and vise versa. Same for W and S.


        xInput = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        yInput = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        player.transform.rotation *= Quaternion.Euler(xInput, 0, yInput);


        moving = false;
        sprinting = false;

        ///////////////////////////////////////////////////////////////////////////// - Get Keyboard input for directional movement
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            moving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            moving = true;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0f, 10f, 0f);
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
        Debug.Log("Stamina: " + stamina);
        ////////////////////////////////////////////////////////////////////////////
    }
}
