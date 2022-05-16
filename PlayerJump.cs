using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private GameObject Player_Collection = null;
    private GameObject Player = null;
    private GameObject Viewpoint = null;
    private GameObject Viewpoint_Collection = null;
    private GameObject Direction = null;
    private GameObject Plane = null;
    private Rigidbody rb = null;
    private bool canJump = true;
    private bool hasLanded = true;
    private Vector3 maxHeight = Vector3.zero;
    private Vector3 previousPos = Vector3.zero;
    private float jumpSpeed = 9.8f;
    private bool grounded = true;
    private LayerMask Ground;
    private RaycastHit hitInfo;
    public float gravity = 9.8f;
    
    
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
        if (Plane == null)
        {
            Plane = GameObject.FindGameObjectWithTag("Floor");
        }
        if (rb == null)
        {
            rb = Player.GetComponent<Rigidbody>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput(); // calls function GetInput.
    }
    void GetInput()
    {
        /*
        Allows for jumping anywhere except when falling and going up/down slopes.
        */
        if (Input.GetKeyDown(KeyCode.Space) == true && canJump == true && grounded == true) // makes sure the player can jump, is on the ground and has pressed the spacebar.
        {
            previousPos.y = Player.transform.localPosition.y; // stores the position on the y-axis before jumping.
            // UnityEngine.Debug.Log(previousPos.y + " and " + maxHeight.y); // debug line to help with testing.
            rb.AddForce(Vector3.up * 343); // applies a force to the rigidbody/player according to the input.
            canJump = false; // states the player cannot jump.
            grounded = false; // states the player is no longer grounded, wouldn't have to do this if controller.isGrounded just worked.
        }
        Vector3 pointVelocity = rb.GetPointVelocity(Player.transform.localPosition); //finds the current velocity of the player at a point in time.
        if (Mathf.Round(pointVelocity.y) == 0 && grounded == false)
        {
            maxHeight.y = Player.transform.localPosition.y;
        }
        if (Mathf.Round(pointVelocity.y) == 0 && grounded == true)
        {
            maxHeight.y = previousPos.y + 2.416f;
        }
        //UnityEngine.Debug.Log(Player.transform.localPosition.y + " | " + Mathf.Log10(Mathf.Abs(pointVelocity.y)) + " / " + Mathf.Abs(pointVelocity.y) + " | " + canJump + " " + grounded + " " + maxHeight.y); // debug line to help with testing.
        if ((Mathf.Log10(Mathf.Abs(pointVelocity.y)) < 0 && Mathf.Log10(Mathf.Abs(pointVelocity.y)) > -10) && Player.transform.localPosition.y != maxHeight.y)
        {
            canJump = true; // states the player can jump.
            grounded = true; // states the player is grounded.
        }
        else if (pointVelocity.y == 0 && Player.transform.localPosition.y == previousPos.y) // makes sure that the player is grounded
        {
            canJump = true; // states the player can jump.
            grounded = true; // states the player is grounded.
        }
        else
        {
            canJump = false;
            grounded = false;
        }
        if (Player.transform.localPosition.y == 1.192093e-07)
        {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, 0, Player.transform.localPosition.z);
        }

        /*
        Above this else statement should be the code, within an else if statement, to allow jumping on slopes; but currently need to work out a solution before I start coding. 
        */
    }
    
}
