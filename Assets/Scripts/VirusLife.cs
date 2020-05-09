using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusLife : MonoBehaviour
{

    private float virusLife = 30.0f;

    private float addFactor = 5.0f;

    private int infectedNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        infectedNum = GameObject.Find("Player").GetComponent<PlayerController>().infectedNum;
        virusLife = virusLife + addFactor * infectedNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > virusLife)
        {
            Destroy(gameObject);
        }
    }

}
