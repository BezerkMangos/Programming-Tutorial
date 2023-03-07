using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    Collectable gear;

    private void Awake()
    {
        gear = new Collectable("Gear",1,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gear.UpdateScore();
            Destroy(gear);
        }

    }








}


