using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float moveSpeed = 0.03f;
    public float normMoveSpeed = 0.03f;
    public float slowMoveSpeed = 0.001f;

    private Rigidbody enemyRb;
    private GameObject player;

    private int moveSlowDuration = 7;

    //powerups = new bool[5] { stayHomeActive, firstAidActive, cityHeroActive,
    //                          wearMaskActive, washHandsActive };
    private List<bool> powerups;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        powerups = playerController.powerups;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDirection = ((pos(player) - pos()).normalized) * moveSpeed;
        transform.position = new Vector3(pos().x + moveDirection.x, pos().y, pos().z + moveDirection.z);
        powerupsListener();

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

        if (powerups != null)
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                if (powerups[i])
                {
                    startEffect(i);
                }

            }
        }
    }

    //handle the effect if the powerup is activated
    private void startEffect(int i)
    {
        switch (i)
        {
            //stayHome
            case 0:
                staryHome();
                break;

            //firstAid
            case 1:
                break;

            //cityHero
            case 2:
                break;

            //wearMask
            case 3:
                break;

            //washHands
            case 4:
                break;
        }

    }



    private void staryHome()
    {
        //slow down the enemies' moving speed
        moveSpeed = slowMoveSpeed;
        StartCoroutine(moveSlow());
    }


    //revert the moving speed after delay
    IEnumerator moveSlow()
    {
        yield return new WaitForSeconds(moveSlowDuration);
        //revert enemies's moving speed
        moveSpeed = normMoveSpeed;
    }

}
