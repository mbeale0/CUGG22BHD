using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class Controls : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float debuffTimer = 0f;
    [SerializeField] private MeshRenderer playerMesh;
    [SerializeField] private AudioClip JumpSFX = null;
    [SerializeField] private GameObject[] playerCharacters = null;
    [SerializeField] private GameObject lookAtCube = null;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerConfigData playerConfig;
    private Vector2 mvmtInput = Vector2.zero;
    private bool hasJumped = false;
    private GameObject otherPlayer = null;
    private Vector3 originalPos;
    private int playerIndex = -1;
    public PlayerConfigData GetPlayerConfig()
    {
        return playerConfig;
    }
    private void Start()
    {
        if(playerConfig.PlayerIndex == 0)
        {
            GameObject playerSelection = GameObject.FindGameObjectWithTag("PlayerSelection");
            playerIndex = playerSelection.GetComponent<PlayerSelection>().GetOneCharacter();
            playerCharacters[playerIndex].SetActive(true);
        }
        else if(playerConfig.PlayerIndex == 1)
        {
            GameObject playerSelection = GameObject.FindGameObjectWithTag("PlayerSelection");
            playerIndex = playerSelection.GetComponent<PlayerSelection>().GetTwoCharacter();
            playerCharacters[playerIndex].SetActive(true);
        }
        lookAtCube.transform.localPosition = new Vector3(2.7f, -1f, 0);
        controller = gameObject.GetComponent<CharacterController>();
        originalPos = transform.position;

        playerConfig.PlayerMaterial = playerMesh.material;
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

        if(playerConfig.PlayerIndex == 0 && GameObject.Find("BluePistonController").GetComponent<PistonController>().timer > 3 && debuffTimer <= 0)
        {
            GameObject.Find("BluePistonController").GetComponent<PistonController>().Activate();
        }
        
        else if(playerConfig.PlayerIndex == 1 && GameObject.Find("RedPistonController").GetComponent<PistonController>().timer > 3 && debuffTimer <= 0)
        {
            GameObject.Find("RedPistonController").GetComponent<PistonController>().Activate();
        }
    }

    public void OnHit(float debuffTime)
    {
        debuffTimer = debuffTime;
    }

    void Update()
    {
        if(debuffTimer > 0)
        {
            debuffTimer -= Time.deltaTime;
        }

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
        if (debuffTimer > 0) move /= 2;
        controller.Move(move * Time.deltaTime * playerSpeed);

        Debug.Log("Grounded:  " + controller.isGrounded);
        if (controller.isGrounded)
        {
            
            playerCharacters[playerIndex].GetComponent<Animator>().ResetTrigger("Jump");
            playerCharacters[playerIndex].GetComponent<Animator>().SetTrigger("Idle");

        }
        if (hasJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            gameObject.GetComponent<AudioSource>().PlayOneShot(JumpSFX);
            playerCharacters[playerIndex].GetComponent<Animator>().SetTrigger("Jump");
        }
        else if(mvmtInput.x != 0)
        {
            playerCharacters[playerIndex].GetComponent<Animator>().SetTrigger("Run");
        }
        else
        {
            playerCharacters[playerIndex].GetComponent<Animator>().SetTrigger("Idle");
            playerCharacters[playerIndex].GetComponent<Animator>().ResetTrigger("Run");
        }
        if(mvmtInput.x > 0)
        {
            lookAtCube.transform.localPosition = new Vector3(2.7f, -1f, 0);
        }
        else if (mvmtInput.x < 0)
        {
            lookAtCube.transform.localPosition = new Vector3(-2.7f, -.95f, 0);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        transform.GetChild(playerIndex).rotation = Quaternion.Euler(0, 90, 0);
        transform.GetChild(playerIndex).position = transform.position + new Vector3(0f, -1f, 0f);
        playerCharacters[playerIndex].transform.LookAt(lookAtCube.transform);
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
