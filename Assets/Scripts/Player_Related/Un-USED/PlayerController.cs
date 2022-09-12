using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Controller Settings")]
    CharacterController characterController;
    public float movementSpeed = 0.5f;
    public Vector3 moveDirection = Vector3.zero;

    [Header("DoubleTap Settings")]
    public float TimeFrame = .5f;
    public int tappedTimes = 0;
    public int dashDrain = 25;
    private Animator anim;

    [Header("References")]
    public GameObject Stats;
    public float Stamina;

    public bool P1;


    


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        Stats = gameObject;
        if (gameObject.CompareTag("P1"))
        {
            P1 = true;
        }
    }

    void Update()
    {

        if (P1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = transform.TransformDirection(Vector3.left * movementSpeed);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = transform.TransformDirection(Vector3.right * movementSpeed);

            }
            else if (Input.GetKey(KeyCode.W))
            {
                moveDirection = transform.TransformDirection(Vector3.forward * movementSpeed);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = transform.TransformDirection(Vector3.back * movementSpeed);

            }
            else
            {
                moveDirection = transform.TransformDirection(Vector3.forward * 0); //Stops movement If Not Moving
            }

            //Gravity
            moveDirection.y -= 10f * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);

            //Dash
            Stamina = Stats.GetComponent<CombatController>().Stamina;

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Stamina > 25)
                {
                    //Dash System
                    if (TimeFrame > 0 && tappedTimes == 1)
                    {
                        //Double Tapped
                        Debug.Log("Dashed!");
                        StartCoroutine(Dash("DashL"));
                        Stats.GetComponent<CombatController>().Stamina -= dashDrain;
                        StartCoroutine(ResetDash("DashL"));
                    }
                    else
                    {
                        TimeFrame = 0.5f;
                        tappedTimes++;
                    }


                }

            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Stamina > 25)
                {
                    //Dash System
                    if (TimeFrame > 0 && tappedTimes == 1)
                    {
                        //Double Tapped
                        Debug.Log("Dashed!");
                        StartCoroutine(Dash("DashR"));
                        Stats.GetComponent<CombatController>().Stamina -= dashDrain;
                        StartCoroutine(ResetDash("DashR"));
                    }
                    else
                    {
                        TimeFrame = 0.5f;
                        tappedTimes++;
                    }

                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////Player 2 (Hard Coded P2 COntrols Because Its A Simple game, I could use cases or somethign else but this works)
        else
        {
            if (hinput.anyGamepad.leftStick.left)
            {
                moveDirection = transform.TransformDirection(Vector3.left * movementSpeed);

            }
            else if (hinput.anyGamepad.leftStick.right)
            {
                moveDirection = transform.TransformDirection(Vector3.right * movementSpeed);

            }
            else if (hinput.anyGamepad.leftStick.up)
            {
                moveDirection = transform.TransformDirection(Vector3.forward * movementSpeed);

            }
            else if (hinput.anyGamepad.leftStick.down)
            {
                moveDirection = transform.TransformDirection(Vector3.back * movementSpeed);

            }
            else
            {
                moveDirection = transform.TransformDirection(Vector3.forward * 0); //Stops movement If Not Moving
            }

            //Gravity
            moveDirection.y -= 100f * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);

            //Dash
            Stamina = Stats.GetComponent<CombatController>().Stamina;

            if (hinput.anyGamepad.B.justPressed)
            {
                if (Stamina > 25)
                {
                    //Dash System
                    if (TimeFrame > 0 && tappedTimes == 1)
                    {
                        //Double Tapped
                        Debug.Log("Dashed!");
                        StartCoroutine(Dash("Left_Dash"));
                        Stats.GetComponent<CombatController>().Stamina -= dashDrain;
                        StartCoroutine(ResetDash("Left_Dash"));
                    }
                    else
                    {
                        TimeFrame = 0.5f;
                        tappedTimes++;
                    }


                }

            }


            if (hinput.anyGamepad.B.justPressed)
            {
                if (Stamina > 25)
                {
                    //Dash System
                    if (TimeFrame > 0 && tappedTimes == 1)
                    {
                        //Double Tapped
                        Debug.Log("Dashed!");
                        StartCoroutine(Dash("Right_Dash"));
                        Stats.GetComponent<CombatController>().Stamina -= dashDrain;
                        StartCoroutine(ResetDash("Right_Dash"));
                    }
                    else
                    {
                        TimeFrame = 0.5f;
                        tappedTimes++;
                    }

                }
            }
        }
            


            //Reset Times Pressed
            if (TimeFrame > 0)
            {
                TimeFrame -= 1 * Time.deltaTime;
            }
            else
            {
                tappedTimes = 0;
            }

             /////////////////End of Update Function//////////////////
        
        
        
    
    }

    IEnumerator Dash(string Direction)
    {
        //Play Dash Animation
        anim.SetTrigger(Direction);
        //Double movement speed
        movementSpeed *= 4;
        //Wait .5 Secods 
        yield return new WaitForSeconds(.5f);
        //Reset Speed
        movementSpeed /= 4;
    }

    IEnumerator ResetDash(string Direction)
    {
        //Wait A Few MiliSeconds
        yield return new WaitForSeconds(.25f);
        //Reset BOolean
        //anim.SetBool(Direction, false);
    }

}