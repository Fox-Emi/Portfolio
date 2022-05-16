using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ProtoJump : MonoBehaviour
{
    private GameObject Player_Collection = null;
    private GameObject Player = null;
    private GameObject Viewpoint = null;
    private GameObject Viewpoint_Collection = null;
    private GameObject Direction = null;
    private GameObject Plane = null;
    private Rigidbody rb = null;
    private bool canJump = true;
    private bool IsGrounded = true;
    private Vector3 maxHeight = Vector3.zero;
    private Vector3 jumpPos = Vector3.zero;
    private float jumpSpeed = 9.8f;
    private bool grounded = true;
    private LayerMask Ground;
    private RaycastHit hitInfo;
    private Collider other;
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
        if (other == null)
        {
            other = Player.GetComponent<MeshCollider>();
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
        if (Input.GetKeyDown(KeyCode.Space) == true && IsGrounded == true)
        {
            rb.AddForce(Vector3.up);
        }
        /*
        Above this else statement should be the code, within an else if statement, to allow jumping on slopes; but currently need to work out a solution before I start coding. 
        */  
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            IsGrounded = true;
            UnityEngine.Debug.Log("Grounded");
        }
        else
        {
            IsGrounded = false;
            UnityEngine.Debug.Log("Not Grounded");
        }
    }
    
}
