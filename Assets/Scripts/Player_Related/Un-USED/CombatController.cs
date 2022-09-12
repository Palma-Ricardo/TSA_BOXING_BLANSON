using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float MaxStamina = 100;
    public float Stamina;

    [Header("Stamina Bools")]
    public bool CanAttack;
    public bool CanDash;
    [Header("Stamina Drain")]
    public int Drain = 10;

    [Header("References")]
    [Tooltip("Be Sure To Assign Obect Here!")]
    public GameObject Player;
    public Animator PAnim;

    [Header("Combo System")]
    public int numOClicks = 0;
    private float lastClickedTime = 0;
    private float comboDelay = .9f;
    private int comboLen = 3;


    [Header("Advanced Settings")]
    public int LSpeed = 2;
    public int RSpeed = 1;
    private bool Regening = false;

    public bool P1;




    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject; //Sets Itself To Player
        PAnim = Player.GetComponent<Animator>();
        Stamina = MaxStamina; //Sets Initial Stamina to max Stamina
        CanAttack = true; //Sets Defautl Boolean To On Start
        CanDash = true;  //Sets Defaut Boolean To True On Start

        if (gameObject.CompareTag("P1"))
        {
            P1 = true;
        }
        Debug.Log(hinput.gamepad[0].fullName);
    }


    // Update is called once per frame
    void Update()
    {
        CheckStamina();
        CheckAttack();
        Stamina = Mathf.Clamp(Stamina, 0, 100);


    }


    void CheckStamina()
    {
        if (Stamina < 100)
        {
            if (Stamina < 10)
            {
                CanAttack = false;
                CanDash = false;
            }
            else
            {
                CanAttack = true;
                CanDash = true;
            }

            StartCoroutine(Regen(Drain/2, 1.5f));

        }
    }

    void CheckAttack()
    {
        if (Time.time - lastClickedTime > comboDelay) //If We Waited too Long Between Attacks
        {
            numOClicks = 0;
        }

        if (Stamina > 10) //Checks If We Have Enough Stamina To Attack 
        {
            if (Input.GetMouseButtonDown(0) && P1 )
            {
                lastClickedTime = Time.time; //Sets Last Time We Clicked To Current Game time (Since we subtract current gametime to check for delay
                numOClicks++; //Increases times clicked
                //Now Lets Attack Based On Number Of Clicks
                if(numOClicks == 1)
                {
                    Stamina -= Drain;
                    PAnim.SetBool("R_Punch",true);
                }
                
                numOClicks = Mathf.Clamp(numOClicks, 0, comboLen); //Ensures we never go over the our max combo limit
            }
            else if (hinput.anyGamepad.A.justPressed && !P1 || hinput.anyGamepad.rightTrigger.justPressed && !P1)
            {
                Debug.Log("GamePad Punch");
                lastClickedTime = Time.time; //Sets Last Time We Clicked To Current Game time (Since we subtract current gametime to check for delay
                numOClicks++; //Increases times clicked
                //Now Lets Attack Based On Number Of Clicks
                if (numOClicks == 1)
                {
                    Stamina -= Drain;
                    PAnim.SetBool("R_Punch",true);
                }
                numOClicks = Mathf.Clamp(numOClicks, 0, comboLen); //Ensures we never go over the our max combo limit
            }
            
        }
    }

    //Checks For Num Of Clicks And Resets Animations Occourdingy (This is public because it will be called from the animations,
    //During our animation it will fire these events to check if 2+ times or havent clicked at all
    //The below then either continues onto a new animation or ends all current attacking animations
    public void Return1()
    {
        if(numOClicks >= 2)
        {
            if(Stamina > 10)
            {
                Stamina -= Drain;
                PAnim.SetBool("L_Punch",true);
            }
           
        }
        else
        {
            PAnim.SetBool("R_Punch",false);
            numOClicks = 0;
        }
    }

    public void Return2()
    {
        if (numOClicks >= 3)
        {
            if(Stamina > 10)
            {
                Stamina -= Drain;
                PAnim.SetBool("H_Punch",true);
            }
            
        }
        else
        {
            PAnim.SetBool("R_Punch",false);
            PAnim.SetBool("L_Punch",false);
            numOClicks = 0;
        }
    }

    public void Return3()
    {
        PAnim.SetBool("R_Punch",false);
        PAnim.SetBool("L_Punch",false);
        PAnim.SetBool("H_Punch",false);
        numOClicks = 0;
    }



    //Resets Boolean
    IEnumerator ResetBool(string b)
    {
        yield return new WaitForSeconds(.1f);
        //Debug.Log(b.ToString());
        PAnim.SetBool(b, false);
        PAnim.speed = 1;


    }

    IEnumerator Regen(float Amount, float interval) //Lets Make It So We Can Spam This Courotine
    {
        if (!Regening && Stamina < MaxStamina)
        {
            Regening = true;
            yield return new WaitForSeconds(interval);
            Stamina += Amount;
            Regening = false;
        }
        else
        {
            //Do Nothing Were Already Regening'
            //May change this later
        }
        
    }
}
