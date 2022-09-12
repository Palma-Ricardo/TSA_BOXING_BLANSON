using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;

    public bool isLeft;
    public bool isRight;
    public bool isForward;
    public bool isBack;
    public bool is2;


    public AnimState currentState;
    public Animator anim;

    void Awake()
    {
        controls = new PlayerControls();
        anim = gameObject.GetComponentInChildren<Animator>();

        controls.Gameplay.Attack.performed += ctx => Attack();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    void Update()
    {
        Move();
    }

    void Attack()
    {
        transform.localScale *= 1.1f;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();    
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Move()
    {

        Vector3 m = new Vector3(move.x, 0, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);

        Vector2 r = new Vector2(0f, rotate.x) * 100 * Time.deltaTime;
        transform.Rotate(r, Space.World);

        Debug.Log(m.x + " " + m.z);

        isInput(m);

    }


    bool isInput(Vector3 m)
    {
        if (m.x < 0.00025 && m.x != 0)
        {
            currentState = AnimState.Left;
            StartCoroutine(SwitchState());
            return true;
        }

        else if (m.x > -0.00025 && m.x != 0)
        {
            currentState = AnimState.Right;
            StartCoroutine(SwitchState());
            return true;
        }

        else if (m.z > 0.00025 && m.z != 0)
        {
            currentState = AnimState.Forward;
            StartCoroutine(SwitchState());
            return true;
        }

        else if (m.z < -0.00025 && m.z != 0)
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
