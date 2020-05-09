using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Game global settings
    private Rigidbody playerRb;
    public bool isGameOver = false;


    //player health
    public int HP;


    //virus properties
    public int virusAttackValue = 10;


    //player basic motion settings
    public float moveSpeed = 0.03f;
    public float normSpeed = 0.03f;
    public float slowSpeed = 0.025f;
    public float jumpForce = 3.0f;
    public float gravityModifier;
    private float zBound = 25.0f;
    private float xBound = 23.0f;
    private float yBound = 0.7f;
    private bool isOnGround = true;


    //settgins for player when attacked
    public int slowSpeedTime = 5;


    //powerups
    private bool stayHomeActive = false;
    private bool firstAidActive = false;
    private bool cityHeroActive = false;
    private bool wearMaskActive = false;
    private bool washHandsActive = false;
    public List<bool> powerups = new List<bool>();
    private int powerupValidTime = 3;

    private int firstAidAddHP = 20;
    private int invincibleTime = 5;
    private bool isInvincible = false;
    private bool isWashHandsActive = false;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        moveSpeed = normSpeed;

        initializePowerups();
        Physics.gravity *= gravityModifier;
        HP = 100;

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        if (HP <= 0 && !isInvincible) isGameOver = true;
        Debug.Log("isGameOver " + isGameOver);
        //constrain();




    }

    //Moves the player based on arrow key input
    private void movePlayer()
    {
        basicControl();


    }

    //make sure player moves inside the border
    private void constrain()
    {
        if (Mathf.Abs(pos().y) < yBound)
        {
            transform.position = new Vector3(pos().x, yBound, pos().z);
        }

    }

    //listener for powerups
    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "StayHome":
                powerActivator(0);
                break;

            case "FirstAid":
                powerActivator(1);
                firstAid();
                break;

            case "CityHero":
                powerActivator(2);
                break;

            case "WearMask":
                powerActivator(3);
                wearMask();
                break;

            case "WashHands":
                powerActivator(4);
                washHandsActive = true;
                break;

        }

        Destroy(other.gameObject);
    }

    //remain invincible for invincibleTime
    private void wearMask()
    {
        isInvincible = true;
        StartCoroutine(invincible());
    }

    IEnumerator invincible()
    {
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;

    }

    //add firstAidAddHP to HP
    private void firstAid()
    {
        HP += firstAidAddHP;
        HP = HP > 100 ? 100 : HP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            if (!washHandsActive)
            {
                HP -= virusAttackValue;
                if (HP < 80)
                {
                    moveSpeed = slowSpeed;
                    StartCoroutine(speedDown());

                }
            }
            washHandsActive = false;


        }
    }

    //get the current position of gameObject
    private Vector3 pos()
    {
        return transform.position;
    }

    //change froze the powerup
    private void powerActivator(int idx)
    {
        powerups[idx] = true;
        StartCoroutine(powerupDeactive(idx));

    }

    //add powerup froze delay
    IEnumerator powerupDeactive(int idx)
    {
        yield return new WaitForSeconds(powerupValidTime);
        powerups[idx] = false;
    }

    private void initializePowerups()
    {
        powerups.Add(stayHomeActive);
        powerups.Add(firstAidActive);
        powerups.Add(cityHeroActive);
        powerups.Add(wearMaskActive);
        powerups.Add(washHandsActive);

    }

    //Player's motion control
    private void basicControl()
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
    }


    IEnumerator speedDown()
    {
        yield return new WaitForSeconds(slowSpeedTime);
        moveSpeed = normSpeed;
    }





}
