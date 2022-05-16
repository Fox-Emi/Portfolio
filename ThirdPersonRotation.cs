using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    private GameObject Player_Collection = null;
    private GameObject Player = null;
    private GameObject Viewpoint = null;
    private GameObject Viewpoint_Collection = null;
    private GameObject Direction = null;
    private float yaw = 0.000f;
    private float pitch = 0.000f;
    private float scroll = 0.000f;
    private float turnSpeed = 2.0f;
    private float angle;
    private Quaternion targetRotation;
    private float speedH = 2.0f;
    private float speedV = 2.0f;
    private Vector2 input = Vector2.zero;

    // Start is called before the first frame update
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
        Cursor.visible = false; // makes cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CalculateDirection();
        Rotate();
        Viewpoint_Collection.transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -45, 60), yaw, 0.0f); // limits x axis rotation between -45 and 60 degrees, and rotates the collection according to inputs above.
        

        scroll = Input.GetAxis("Mouse ScrollWheel"); // finds the scroll value
        
        if (scroll != 0) // when scrolling;
        {
            Viewpoint.transform.localPosition = new Vector3(0.0f, 0.5f, Mathf.Clamp(Viewpoint.transform.localPosition.z + scroll, -7.5f, -1.0f)); // adjusts camera position relative to the player accordingly.
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
    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal"); // gathers input from a and d.
        input.y = Input.GetAxisRaw("Vertical"); // gathers input from w and s.

        yaw += speedH * Input.GetAxis("Mouse X"); // finds the y axis rotation
        pitch -= speedV * Input.GetAxis("Mouse Y"); // finds the x axis rotation
    }
}
