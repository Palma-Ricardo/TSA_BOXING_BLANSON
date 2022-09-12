using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public enum AnimState { Idle, Forward, Left, Right, Back, Atk }

public class Movement_Controller : MonoBehaviour
{

    [Header("Variables")]
    public CharacterController characterController;
    public Vector3 moveDir = Vector3.zero;
    public static bool CanMove;
    public float moveSpeed = 0.5f;
    public bool isLeft;
    public bool isRight;
    public bool isForward;
    public bool isBack;
    public bool is2;


    public AnimState currentState;
    public Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if(gameObject.layer == 12) //Layer 12 is P2
        {
            is2 = true;
        }
     
    }

    void Update()
    {
        //Initializing Objects
        if (!anim)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }



        Move();

        
    }

    void Move()
    {
        if (Game_Manager.KeyboardEnabled)
        {
            if (is2)
            {
                isInput2();
            }
            else
            {
                isInput();
            }
        }
        
        
        

        switch (currentState)
        {
            case AnimState.Idle:
                moveDir = transform.TransformDirection(Vector3.forward * 0);
                break;
            case AnimState.Forward:
                moveDir = transform.TransformDirection(Vector3.forward * moveSpeed);
                break;
            case AnimState.Left:
                moveDir = transform.TransformDirection(Vector3.right * -moveSpeed);
                break;
            case AnimState.Right:
                moveDir = transform.TransformDirection(Vector3.right * moveSpeed);
                break;
            case AnimState.Back:
                moveDir = transform.TransformDirection(Vector3.forward * -moveSpeed);
                break;
            case AnimState.Atk:
                moveDir = transform.TransformDirection(Vector3.forward * 0);
                break;
            default:
                break;
        }

        
        characterController.Move(moveDir * Time.deltaTime);
    }


    bool isInput()
    {
        if (isLeft = Input.GetKey(KeyCode.A))
        {
            currentState = AnimState.Left;
            StartCoroutine(SwitchState());
            return true;
        }
            
        if (isRight = Input.GetKey(KeyCode.D))
        {
            currentState = AnimState.Right;
            StartCoroutine(SwitchState());
            return true;
        }
            
        if (isForward = Input.GetKey(KeyCode.W))
        {
            currentState = AnimState.Forward;
            StartCoroutine(SwitchState());
            return true;
        }
            
        if (isBack = Input.GetKey(KeyCode.S))
        {
            currentState = AnimState.Back;
            StartCoroutine(SwitchState());
            return true;
        }
        else
        {
            currentState = AnimState.Idle;
            StartCoroutine(SwitchState());
            return true;
        }
    }

    bool isInput2()
    {
        if (isLeft = Gamepad.current.leftStick.x.ReadValue() < -0.5f)
        {
            currentState = AnimState.Left;
            StartCoroutine(SwitchState());
            return true;
        }

        if (isRight = Gamepad.current.leftStick.x.ReadValue() > 0.5f)
        {
            currentState = AnimState.Right;
            StartCoroutine(SwitchState());
            return true;
        }

        if (isForward = Gamepad.current.leftStick.y.ReadValue() > 0.5f) 
        {
            currentState = AnimState.Forward;
            StartCoroutine(SwitchState());
            return true;
        }

        if (Gamepad.current.leftStick.y.ReadValue() < -0.5f)
        {
            currentState = AnimState.Back;
            StartCoroutine(SwitchState());
            return true;
        }
        else
        {
            currentState = AnimState.Idle;
            StartCoroutine(SwitchState());
            return true;
        }

    }

    public IEnumerator SwitchState()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Forward", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("Back", false);
        anim.SetBool(currentState.ToString(), true);
        yield return new WaitForSeconds(0.01f);

    }

}
