using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesController : MonoBehaviour
{
    public GameObject LeftGlove;
    public GameObject RightGlove;

    // Start is called before the first frame update
    void Start()
    {
        LeftCollisionFalse();
        RightCollisionFalse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     //Disables Collision Not Gloves
    public void LeftCollisionTrue()
    {
        Debug.Log("ENABLE L");
        LeftGlove.GetComponent<Collider>().enabled = true; //Setting Collide To Trigger Will Have A Similar Effect To Deavtivating It Since We Have To Code In OnTriggerEnter For These
    }

    public void RightCollisionTrue()
    {
        Debug.Log("ENABLE R");
        RightGlove.GetComponent<Collider>().enabled = true;
    }

    public void LeftCollisionFalse()
    {
        LeftGlove.GetComponent<Collider>().enabled = false;
    }

    public void RightCollisionFalse()
    {
        RightGlove.GetComponent<Collider>().enabled = false;
    }


    //Current We Use Non-Trigger Collision which means we collide with any object with collision and pass through those with a trigger, and i check for collision events, and triggers can check if i collide with them but if i set gloves to triggers when there not used
    //it wont fire off my collision events and since i have no trigger events it should throretically do nothing

}
