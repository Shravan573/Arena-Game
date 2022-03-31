using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerinput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    public float pickuprange = 0.5f;
    bool withintherange;
    bool pickedup = false;
    public Transform player;
    public Transform Object;
    public float downwardforce = 1f;
    public float forwardforce = 1f;
   



    private void Awake()
    {
        playerinput = new Player();
        controller = GetComponent<CharacterController>();

    }
    private void OnEnable()
    {
        playerinput.Enable();
    }
    private void OnDisable()
    {
        playerinput.Disable();
    }

   

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movementInput = playerinput.Playermain.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Vector3 distaceToPlayer = player.position - transform.position;
        if (distaceToPlayer.magnitude<=pickuprange&&!pickedup&&playerinput.Playermain.pickup.triggered)
        {
            Object.GetComponent<Rigidbody>().useGravity = false;
            //Object.GetComponent<Rigidbody>().isKinematic = true;
            Object.transform.position = player.position;
            Object.transform.parent = GameObject.Find("Player").transform;
            Debug.Log("pickedup");
            pickedup = true;
        }
        if(pickedup&&playerinput.Playermain.Drop.triggered)
        {
            Object.transform.parent = null;
            Object.GetComponent<Rigidbody>().useGravity = true;
            /*Object.GetComponent<Rigidbody>().isKinematic = false*/;
            Debug.Log("droped");
            //Rigidbody rb = Object.GetComponent<Rigidbody>();
            //rb.AddForce(player.forward *forwardforce , ForceMode.Impulse);
            //rb.AddForce(player.up * downwardforce, ForceMode.Impulse);
            //float random = Random.Range(-1f, 1f);
            //rb.AddTorque(new Vector3(random, random, random) * 10);

            pickedup = false;
        }



    }
}
