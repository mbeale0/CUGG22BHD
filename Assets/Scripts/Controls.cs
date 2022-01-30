using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class Controls : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private MeshRenderer playerMesh;
    [SerializeField] private AudioClip JumpSFX = null;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerConfigData playerConfig;
    private Vector2 mvmtInput = Vector2.zero;
    private bool hasJumped = false;
    private GameObject otherPlayer = null;

    private Vector3 originalPos;
    PlayerConfigData[] playerConfigs;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        //originalPos = gameObject.GetComponent<PlayerDetails>().GetStartPos();
        playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
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
        if(playerConfig.PlayerIndex == 0 && GameObject.Find("BluePistonController").GetComponent<PistonController>().timer > 3)
        {
            GameObject.Find("BluePistonController").GetComponent<PistonController>().Activate();
        }
        
        else if(playerConfig.PlayerIndex == 1 && GameObject.Find("RedPistonController").GetComponent<PistonController>().timer > 3)
        {
            GameObject.Find("RedPistonController").GetComponent<PistonController>().Activate();
        }
    }
    void Update()
    {
        if(otherPlayer == null)
        {
            if (playerConfig.PlayerIndex == 0)
            {
                gameObject.tag = "PlayerOne";
                otherPlayer = GameObject.FindGameObjectWithTag("PlayerTwo");
            }
            else if (playerConfig.PlayerIndex == 1)
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
            gameObject.GetComponent<AudioSource>().PlayOneShot(JumpSFX);
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

    public void InitializePlayer(PlayerConfigData pcd)
    {
        playerConfig = pcd;
        playerMesh.material = playerConfig.PlayerMaterial;
    }

    public void ResetToStartPos()
    {
        controller.enabled = false;
        GetComponentInParent<Transform>().position = originalPos;
        controller.enabled = true;
    }
}
