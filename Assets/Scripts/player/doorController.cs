using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public LayerMask doorLayer;
    private Animator animator;
    private BoxCollider2D doorCollider;
    public GameObject doorText;
    public float distance;
    public bool hitDoor;

    private Door door;


    void Update()
    {
        checkDoor();
    }

    private void checkDoor()
    {
        RaycastHit2D hitDoorR = Physics2D.Raycast(transform.position, Vector2.right, distance, doorLayer);
        Debug.DrawRay(transform.position, Vector2.right * distance, Color.red);

        RaycastHit2D hitDoorL = Physics2D.Raycast(transform.position, Vector2.left, distance, doorLayer);
        Debug.DrawRay(transform.position, Vector2.left * distance, Color.blue);

        if (hitDoorR)
        {
            hitDoor = true;

            door = hitDoorR.collider.gameObject.GetComponent<Door>();
            doorText.active = true;

            if (hitDoor && Input.GetKeyDown(KeyCode.E))
            {
                door.openDoor();
            }
        }
        else if (hitDoorL)
        {
            hitDoor = true;

            door = hitDoorL.collider.gameObject.GetComponent<Door>();
            doorText.active = true;

            if (hitDoor && Input.GetKeyDown(KeyCode.E))
            {
                door.openDoor();
            }
        }
        else
        {
            hitDoor = false;
            doorText.active = false;
        }
    }
}
