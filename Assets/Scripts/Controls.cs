using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Controls : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 mvmtInput = Vector2.zero;
    private bool hasJumped = false;
    private int playerID = -1;
    private GameObject otherPlayer = null;

    private Vector3 originalPos;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerID = gameObject.GetComponent<PlayerDetails>().GetPlayerID();
        originalPos = gameObject.GetComponent<PlayerDetails>().GetStartPos();

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mvmtInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        hasJumped = context.action.triggered;
    }

    public void OnAction()
    {
        //otherPlayer.GetComponent<Renderer>().material.color = Color.black;
        if(playerID == 1 && GameObject.Find("RedPistonController").GetComponent<PistonController>().timer > 3)
        {
            GameObject.Find("RedPistonController").GetComponent<PistonController>().Activate();
        }
        
        if(playerID == 2 && GameObject.Find("BluePistonController").GetComponent<PistonController>().timer > 3)
        {
            GameObject.Find("BluePistonController").GetComponent<PistonController>().Activate();
        }
    }
    void Update()
    {
        if(otherPlayer == null)
        {
            if (playerID == 1)
            {
                gameObject.tag = "PlayerOne";
                otherPlayer = GameObject.FindGameObjectWithTag("PlayerTwo");
            }
            else if (playerID == 2)
            {
                gameObject.tag = "PlayerTwo";
                otherPlayer = GameObject.FindGameObjectWithTag("PlayerOne");
            }
        }
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(mvmtInput.x, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);


        if (hasJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.transform.position.y >= transform.position.y)
        {
            playerVelocity.y = -.1f;
        }
    }

    public void ResetToStartPos()
    {
        controller.enabled = false;
        GetComponentInParent<Transform>().position = originalPos;
        controller.enabled = true;
    }
}
