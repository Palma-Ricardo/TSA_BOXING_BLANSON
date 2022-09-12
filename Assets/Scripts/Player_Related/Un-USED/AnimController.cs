using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [Header("References")]
    public PlayerController PController;
    public CombatController CController;
    public Animator anim;

    private bool Moving;

    //Bool Names: Left_Step, Right_Step, Forward_Step, Back_Step, Right_Dash, Left_Dash

    // Start is called before the first frame update
    void Start()
    {
        PController = GetComponent<PlayerController>();
        CController = GetComponent<CombatController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks If Our Player Is Moving
        if(PController.moveDirection != new Vector3(0,0,0))
        {
            Moving = true;
        }

        if (Moving)
        {
            //Moving Left
            if (PController.moveDirection.x <= -1)
            {
                ResetMoveBool();
                anim.SetBool("Left", true);
            }
            else
            {
                anim.SetBool("Left", false);
            }

            //Moving Right
            if (PController.moveDirection.x >= 1)
            {
                ResetMoveBool();
                anim.SetBool("Right", true);
            }
            else
            {
                anim.SetBool("Right", false);
            }

            //Moving Forward
            if (PController.moveDirection.z >= 1)
            {
                ResetMoveBool();
                anim.SetBool("Forward", true);
            }
            else
            {
                anim.SetBool("Forward", false);
            }

            //Moving Back
            if (PController.moveDirection.z <= -1)
            {
                ResetMoveBool();
                anim.SetBool("Back", true);
            }
            else
            {
                anim.SetBool("Back", false);
            }
        }
        else
        {
            anim.SetBool("Idle", true);
        }
        
    }


    void ResetMoveBool()
    {
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
        anim.SetBool("Forward", false);
        anim.SetBool("Back", false);
    }
}
