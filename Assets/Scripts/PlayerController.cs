using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float moveSpeed = 0.03f;
    public float jumpForce = 3.0f;
    public float gravityModifier;

    private float zBound = 25.0f;
    private float xBound = 23.0f;
    private float yBound = 0.7f;

    private bool isOnGround = true;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        Debug.Log(pos().y);
        //constrain();
        //gravity();



    }

    //Moves the player based on arrow key input
    private void movePlayer()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            float yInput = jumpForce;
            isOnGround = false;
            playerRb.AddForce(Vector3.up * yInput, ForceMode.Impulse);
        }
        
        transform.position = new Vector3(pos().x + moveSpeed * xInput,
            pos().y, pos().z + moveSpeed * zInput);
        
        xInput = zInput = 0;
        //playerRb.AddForce(Vector3.forward * speed * verticalInput);
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //make sure player moves inside the border
    private void constrain()
    {
        //if (Mathf.Abs(pos().z) >= zBound)
        //{
        //    transform.position = pos().z > 0 ? new Vector3(pos().x, pos().y, zBound) : new Vector3(pos().x, pos().y, -zBound);
        //}

        //if(Mathf.Abs(pos().x) >= zBound)
        //{
        //    transform.position = pos().x > 0 ? new Vector3(xBound, pos().y, pos().z) : new Vector3(-xBound, pos().y, pos().z);
        //}

        if (Mathf.Abs(pos().y) < yBound)
        {
            transform.position = new Vector3(pos().x, yBound, pos().z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
        }

        //if (other.CompareTag("Enemy"))
        //{
        //    Destroy(other.gameObject);
        //}

        //if (other.CompareTag("Wall"))
        //{
        //    transform.position = pos();
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    //get the current position of gameObject
    private Vector3 pos()
    {
        return transform.position;
    }

}
