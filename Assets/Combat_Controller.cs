using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public enum ComboState
{ 
    NONE,
    ATTACK_1,
    ATTACK_2,
    ATTACK_3
}


public class Combat_Controller : MonoBehaviour
{
    //Player identification
    public GameObject Character;
    public bool p2;

    // Used for hit detection
    public GameObject leftHandAttackPoint, rightHandAttackPoint;

    // Used for animations
    private Animator anim;
    public int numOClicks = 0;
    private bool activateTimerToReset;
    private float defaultComboTimer = 1f;
    private float currentComboTimer;

    private ComboState currentComboState;


    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
        if(gameObject.layer == 12)//12 Is Layer 2
        {
            p2 = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Manager.KeyboardEnabled)
        {
            ComboAttacks();
        }
        
        ResetComboState();
    }

    void ComboAttacks()
    {
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)) && !p2)
        {
            if (currentComboState == ComboState.ATTACK_3)
                return;

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.ATTACK_1)
            {
                anim.SetTrigger("Attack_1");
            }
            if (currentComboState == ComboState.ATTACK_2)
            {
                anim.SetTrigger("Attack_2");
            }
            if (currentComboState == ComboState.ATTACK_3)
            {
                anim.SetTrigger("Attack_3");
            }
        }
        if ((Gamepad.current.rightShoulder.wasReleasedThisFrame || Gamepad.current.rightTrigger.wasReleasedThisFrame) && p2)
        {
            if (currentComboState == ComboState.ATTACK_3)
                return;

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.ATTACK_1)
            {
                anim.SetTrigger("Attack_1");
            }
            if (currentComboState == ComboState.ATTACK_2)
            {
                anim.SetTrigger("Attack_2");
            }
            if (currentComboState == ComboState.ATTACK_3)
            {
                anim.SetTrigger("Attack_3");
            }
        }
    }

    private void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }

    // USED ONLY FOR HIT DETECTION
    void LeftHandOn()
    {
        leftHandAttackPoint.SetActive(true);
    }
    void LeftHandOff()
    {
        if (leftHandAttackPoint.activeInHierarchy)
            leftHandAttackPoint.SetActive(false);
    }
    void RightHandOn()
    {
        rightHandAttackPoint.SetActive(true);
    }
    void RightHandOff()
    {
        if (rightHandAttackPoint.activeInHierarchy)
            rightHandAttackPoint.SetActive(false);
    }
}
