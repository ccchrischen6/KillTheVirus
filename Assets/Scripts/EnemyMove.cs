using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float moveSpeed = 0.01f;

    private Rigidbody enemyRb;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = ((pos(player) - pos()).normalized)*moveSpeed;
        transform.position = new Vector3(pos().x + moveDirection.x, pos().y, pos().z + moveDirection.z);
        
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




}
