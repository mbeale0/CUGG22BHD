﻿using UnityEngine;
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

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerConfigData playerConfig;
    private Vector2 mvmtInput = Vector2.zero;
    private bool hasJumped = false;
    private GameObject otherPlayer = null;

    private Vector3 originalPos;
    PlayerConfigData[] playerConfigs;
    public PlayerConfigData GetPlayerConfig()
    {
        return playerConfig;
    }
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        originalPos = transform.position;
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
        } else if(GetComponent<Renderer>().material.color != playerConfig.PlayerMaterial.color)
        {
            GetComponent<Renderer>().material.color = playerConfig.PlayerMaterial.color;
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
