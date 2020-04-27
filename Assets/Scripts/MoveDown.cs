using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 5;

    private float zDestory;
    private Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        zDestory = -GameObject.Find("Ground").GetComponent<BoxCollider>().size.z / 2 * 5;
        Debug.Log(zDestory);
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);
        if(transform.position.z < zDestory)
        {
            Destroy(gameObject);
        }
    }
}
