using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float zBound = 6.0f;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        constrain();
        
    }

    //Moves the player based on arrow key input
    private void movePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //make sure player moves inside the border
    private void constrain()
    {
        if (Mathf.Abs(transform.position.z) >= zBound)
        {
            transform.position = transform.position.z > 0 ? new Vector3(transform.position.x, transform.position.y, zBound) : new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }
}
