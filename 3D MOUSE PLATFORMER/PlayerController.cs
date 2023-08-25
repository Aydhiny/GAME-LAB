using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    bool onground;
    AnimationHandler handler;
    public float GroundTimer;

    public float PlayerHealth = 10;

    SkinnedMeshRenderer _skin;
    public Color flashColor;
    private Color normalColor;
    public float intensity;

    // Start is called before the first frame update
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        handler = gameObject.GetComponent<AnimationHandler>();
        _skin = GetComponentInChildren<SkinnedMeshRenderer>();
        normalColor = _skin.material.GetVector("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer) 
        {
            GroundTimer = 0.2f;
        }

        if(GroundTimer > 0) 
        {
            GroundTimer -= Time.deltaTime;
            handler.Landed();
        }
        else 
        {
            handler.AirBorn();
        }

        if(groundedPlayer && playerVelocity.y < 0) 
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(move != Vector3.zero) 
        {
            handler.Run();
            gameObject.transform.forward = move;
        } 
        else 
        {
            handler.Idle();
        }
        if(Input.GetButtonDown("Jump")) 
        {
            if(GroundTimer > 0) 
            {
                GroundTimer = 0;
                handler.Jump();
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                onground = false;
            }
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void TakeDamage(float _damage, Vector3 _push)
    {
        PlayerHealth = PlayerHealth - _damage;
        controller.Move(-_push);
        flashRed();
        //REDUCE THE PLAYER'S HEALTH
        //APPLY A SMALL PUSH BACK
    }

    void flashRed() 
    {
        _skin.material.SetVector("_EmissionColor", flashColor * intensity);
        Invoke("normal", 0.12f);
    }
    void normal() 
    {
        _skin.material.SetVector("_EmissionColor", normalColor);
    }
}
