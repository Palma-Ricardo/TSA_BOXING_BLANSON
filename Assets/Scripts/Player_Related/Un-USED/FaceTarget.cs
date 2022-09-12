using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class FaceTarget : MonoBehaviour
{

    public Transform Target;
    public bool P1;
    public float hSpeed = 7;
    public float vSpeed = 7;
    public Cinemachine.CinemachineVirtualCamera pcam;
    // Start is called before the first frame update
    void Start()
    {
        //pcam = GameObject.FindWithTag("P1").GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        if(this.gameObject.layer == 11)
        {
            P1 = true;
        }
        Debug.Log("GamePad: "+Gamepad.current);

    

        //if (P1)
        //{
        //    Target = GameObject.FindWithTag("P2").GetComponent<Transform>();
        //}
        //else
        //{
        //    Target = GameObject.FindWithTag("P1").GetComponent<Transform>();
        //}
       
    }
    

    // Update is called once per frame
    void Update()
    {
        int loops = 0;
        if (!pcam && loops < 10000)
        {
            pcam = GameObject.FindWithTag("P1").GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            loops++;
        }
            //Rotate();
            MouseRotation();
        

        
    }

    void Rotate()
    {
        if (Target)
        {
            //Gets Target Position While Mainaining The Y Axis
            Vector3 TargetPos = new Vector3(Target.position.x, transform.position.y, Target.position.z);

            //Gets Rotation Angle By Subtracing The Two Positions
            Vector3 RotFinder = TargetPos - transform.position;

            //Creates A Quaterion Rotation Based On The Giving Angle
            Quaternion rotation = Quaternion.LookRotation(RotFinder);

            //Rotates To Face Given Rotation
            transform.rotation = rotation;
        }
       
    }

    void MouseRotation()
    {
        if (P1)
        {
            float Horizontal = hSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, Horizontal, 0);
            float Vertical = vSpeed * Input.GetAxis("Mouse Y");
            pcam.transform.Rotate(0, 0, 0);
        }
        else
        {
            float Horizontal = (hSpeed/6) * Gamepad.current.rightStick.x.ReadValue();
            transform.Rotate(0, Horizontal, 0);
            float Vertical = (vSpeed/6) * Gamepad.current.rightStick.y.ReadValue();
            pcam.transform.Rotate(0, 0, 0);
        }
        
    }
}
