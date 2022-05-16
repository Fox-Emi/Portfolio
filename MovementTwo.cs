using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTwo : MonoBehaviour
{
    private GameObject Player_Collection = null;
    private GameObject Player = null;
    private GameObject Viewpoint = null;
    private GameObject Viewpoint_Collection = null;
    private GameObject Direction = null;
    private GameObject Plane = null;
    private CharacterController controller = null;
    private bool canJump = true;
    private bool groundedPlayer;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 maxHeight = Vector3.zero;
    private Vector3 previousPos = Vector3.zero;
    private float shiftPressed = 1.0f;
    private float jumpHeight = 1.0f;
    public float velocity = 2.0f;
    public float turnSpeed = 10.0f;
    public float gravityValue = 9.8f;
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
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * velocity);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(transform.rotation * playerVelocity * Time.deltaTime);
    }
}
