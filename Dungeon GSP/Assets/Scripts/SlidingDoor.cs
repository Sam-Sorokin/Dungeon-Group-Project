using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    [Tooltip("The Key ID required to open this door. Leave empty if no key is required.")]
    public string requiredKeyID = "DefaultKey"; // Set this in Inspector for each door

    public float openHeight = 3.0f;
    public float openSpeed = 2.0f;
    public KeyCode openKey = KeyCode.E;
    public float interactionDistance = 2.0f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isOperating = false;
    private Transform playerTransform;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = new Vector3(transform.position.x, transform.position.y + openHeight, transform.position.z);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found for door! Make sure your player GameObject is tagged 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform == null || PlayerInventory.Instance == null)
        {
            if (PlayerInventory.Instance == null) Debug.LogWarning("SlidingDoor: PlayerInventory.Instance is not available.");
            return;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(openKey) && !isOperating)
            {
                // Check if player has the specific key (or if no key is required)
                if (PlayerInventory.Instance.HasKey(requiredKeyID) && !isOpen)
                {
                    Debug.Log("Player has key '" + requiredKeyID + "' or no key required. Opening door.");
                    StartCoroutine(OperateDoor(true));
                }
                else if (!PlayerInventory.Instance.HasKey(requiredKeyID))
                {
                    Debug.Log("Player does not have the required key: " + requiredKeyID);
                    // Optional: Play a "locked" sound or show a message
                }
            }
        }
    }

    IEnumerator OperateDoor(bool open)
    {
        isOperating = true;
        Vector3 targetPosition = open ? openPosition : closedPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * openSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition;
        isOpen = open;
        isOperating = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}