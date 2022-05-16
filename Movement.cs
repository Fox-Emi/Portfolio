using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameObject Player_Collection = null;
    private GameObject Player = null;
    private GameObject Viewpoint = null;
    private GameObject Viewpoint_Collection = null;
    private GameObject Direction = null;
    private GameObject Plane = null;
    private bool canJump = true;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 maxHeight = Vector3.zero;
    private Vector3 previousPos = Vector3.zero;
    private float shiftPressed = 1.0f;
    private float jumpSpeed = 9.8f;
    public float velocity = 5.0f;
    public float turnSpeed = 10.0f;
    public float gravity = 9.8f;
    public float stamina = 5000f;
    private Vector2 input;
    private float angle;
    private Quaternion targetRotation;
    

    // Start is called before the first frame update; assigns GameObjects in this case, to their appropriate variables for later use.
    void Start()
    {
        if (Player_Collection == null)
        {
            Player_Collection = GameObject.FindGameObjectWithTag("PlayerCollection");
        }
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Viewpoint == null)
        {
            Viewpoint = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if (Viewpoint_Collection == null)
        {
            Viewpoint_Collection = GameObject.FindGameObjectWithTag("CameraCollection");
        }
        if (Direction == null)
        {
            Direction = GameObject.FindGameObjectWithTag("DirectionPointer");
        }
        if (Plane == null)
        {
            Plane = GameObject.FindGameObjectWithTag("Floor");
        }
        
    }

    // Update is called once per frame.
    void Update()
    {
        GetInput(); // calls GetInput function.
        
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return; // skips the current frame, or Update(), if no movement keys are being pressed.
        CalculateDirection(); // calls CalculateDirection function.
        Rotate(); // calls Rotate function.
        Move(); // calls Move function.
    }
    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal"); // gathers input from a and d.
        input.y = Input.GetAxisRaw("Vertical"); // gathers input from w and s.
        if (Input.GetKeyDown(KeyCode.LeftShift) == false && stamina < 5000f)
        {
            //while (Input.GetKeyDown(KeyCode.LeftShift) == false)
            {
                stamina += 1f;
            }
            UnityEngine.Debug.Log(stamina);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) == true && stamina > 0f) // gathers input from left shift key.
        {
            shiftPressed = 2.0f; // adjusts overall velocity of player movement.
            //while (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {
                stamina -= 1f;
            }
            UnityEngine.Debug.Log(stamina);
            UnityEngine.Debug.Log("Shift Pressed!"); // debug line to help with testing.
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) == true) // gathers input from left shift key.
        {
            shiftPressed = 1.0f; // adjusts overall velocity of player movement.
            UnityEngine.Debug.Log("Shift Released!"); // debug line to help with testing.
        }
        
    }
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg; // finds the corresponding vector caused by pressing one or more movement keys.
        angle += Viewpoint_Collection.transform.eulerAngles.y; // adds on the camera's vector (the way it is facing, this causes it to move according to the camera)
    }
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0.0f, angle, 0.0f); // converts the angle earlier to a quaternion
        Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, targetRotation, turnSpeed * Time.deltaTime); // Spherically interpolates between quaternions Player.transform.rotation and targetRotation by ratio turnSpeed * Time.deltaTime.
    }
    void Move()
    {
        Player.transform.position += Player.transform.forward * Time.deltaTime * velocity * shiftPressed; // applies an appropriate change in position based on time and the rotation.
    }
}
