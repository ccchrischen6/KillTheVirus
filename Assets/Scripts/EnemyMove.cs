using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 0.03f;
    public float normMoveSpeed = 0.03f;
    public float slowMoveSpeed = 0.001f;



    public float startMoveDistance = 11;
    private Vector3 randomDirectionBase;
    private Vector3 randomDirection;

    private Rigidbody enemyRb;
    private GameObject player;

    private int moveSlowDuration = 7;

    //powerups = new bool[5] { stayHomeActive, firstAidActive, cityHeroActive,
    //                          wearMaskActive, washHandsActive };
    private List<bool> powerups;

    private bool stayHomeActive = false;
    private bool cityHeroActive = false;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        powerups = playerController.powerups;
        randomDirectionBase = getRandomDirectionBase();
    }

    // Update is called once per frame
    void Update()
    {

        virusMove();
        powerupsListener();


    }

    private void virusMove()
    {
        //move towards player if close enough
        if (Vector3.Distance(pos(player), pos()) < startMoveDistance)
        {
            Vector3 moveDirection = ((pos(player) - pos()).normalized) * moveSpeed;
            transform.position = new Vector3(pos().x + moveDirection.x, pos().y, pos().z + moveDirection.z);
        }

        //randomly move
        else
        {
            randomDirection = randomDirectionBase * moveSpeed;
            // Debug.Log("Random: " + randomDirectionBase.x + " " + randomDirectionBase.z);
            transform.position = new Vector3(pos().x + randomDirection.x, pos().y, pos().z + randomDirection.z);
        }

    }

    private Vector3 getRandomDirectionBase()
    {
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-10, 10),
                                            randomDirectionBase.y,
                                            UnityEngine.Random.Range(-10, 10)).normalized;
        return direction;
    }

    //get the current position of gameObject
    private Vector3 pos(GameObject gameObject)
    {
        return gameObject.transform.position;
    }

    private Vector3 pos()
    {
        return transform.position;
    }

    //check which powerup is activated
    private void powerupsListener()
    {
        //handle the stayHome powerup
        stayHomeHandler();
        cityHeroHandler();

    }

    private void stayHomeHandler()
    {
        stayHomeActive = powerups[0];
        if (stayHomeActive)
        {
            moveSpeed = slowMoveSpeed;
        }
        else moveSpeed = normMoveSpeed;
    }

    private void cityHeroHandler()
    {
        cityHeroActive = powerups[2];
        if (cityHeroActive)
        {
            if (Vector3.Distance(pos(player), pos()) < 5)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // randomDirection = new Vector3(-randomDirection.x * UnityEngine.Random.Range(1, 10), randomDirection.y, -randomDirection.z * UnityEngine.Random.Range(1, 10)).normalized * moveSpeed;
            randomDirectionBase = getRandomDirectionBase();
        }
    }

    private bool isAlive(int virusLife)
    {
        return virusLife > 0;
    }

}
