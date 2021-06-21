using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




/// <summary>
/// Controls player movement and stamina usage
/// </summary>
[AddComponentMenu("RPG/Player/Movement")]
//[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public static Movement TheMovement;


    [Header("Speed Vars")]
    public float moveSpeed;
    public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
    public int baseSpeed;

    [Header("Stamina Vars")]
    public int staminaMax;
    public float stamina;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private TMP_Text staminaText;

    public Rigidbody rb;
    private Animator characterAnimator;

    Collider _collider;
    bool isGrounded;

    public void Awake()
    {
        _collider = GetComponent<Collider>();
        if (TheMovement == null)
        {
            TheMovement = this;
        }
        else if (TheMovement != null)
            Destroy(this);
    }

    private void Start()
    {
        characterAnimator = GetComponentInChildren<Animator>();

        SetValues();
    }

    /// <summary>
    /// Sets stamina values after a new game
    /// </summary>
    public void SetValues()
    {
        //Sets stamina values after a new game, pulls from Customisation Get
        staminaMax = CustomisationGet.stamina;
        staminaSlider.maxValue = staminaMax;
        stamina = staminaMax;
        baseSpeed = CustomisationGet.speed / 2;
    }

    private void FixedUpdate()
    {
        isGrounded = CheckIsGrounded();
    }

    private void Update()
    {
        Move();
        LevelUp();

        #region Stamina Bar update and regen
        // Updates stamina slider and value text
        staminaSlider.value = stamina;
        staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina) + "/" + staminaMax;

        if (stamina < staminaMax && !Input.GetButton("Sprint"))
        {
            // Regenerate stamina if not using it
            stamina += Time.deltaTime;
        }

        #endregion
    }

    bool CheckIsGrounded()
    {
        float DisstanceToTheGround = _collider.bounds.extents.y;
        Debug.DrawRay(transform.position, Vector3.down * (DisstanceToTheGround + 0.2f));
        return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.2f);
       
    }

    /// <summary>
    /// Controls the character movement using the saved keybinds from BindingManager
    /// </summary>
    private void Move()
    {
        //print(isGrounded);
        characterAnimator.SetBool("moving", false);

        Vector3 gravity = Physics.gravity * 4f;
        if(isGrounded)
        {
            gravity = Vector3.down * 2f;
        }


        if (BindingManager.BindingHeld("Forward"))
        {
            rb.AddForce(transform.forward * moveSpeed + gravity);
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            characterAnimator.SetBool("moving", true);

        }

        if (BindingManager.BindingHeld("Right"))
        {
            rb.AddForce(transform.right * moveSpeed + gravity);
            //transform.position += transform.right * moveSpeed * Time.deltaTime;
            characterAnimator.SetBool("moving", true);
        }


        if (BindingManager.BindingHeld("Backward"))
        {
            rb.AddForce(-transform.forward * moveSpeed + gravity);
            //transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            characterAnimator.SetBool("moving", true);

        }

        if (BindingManager.BindingHeld("Left"))
        {
            rb.AddForce(-transform.right * moveSpeed + gravity);
            //transform.position -= transform.right * moveSpeed * Time.deltaTime;
            characterAnimator.SetBool("moving", true);
        }


        // Controls speeds and animations for Sprint/Crouch/Base and Jump.
        if (isGrounded)
        {
            if (BindingManager.BindingHeld("Run") && stamina > 0)
            {
                moveSpeed = runSpeed * baseSpeed;
                stamina -= Time.deltaTime;
                characterAnimator.SetFloat("speed", 2);
            }
            else if (BindingManager.BindingHeld("Crouch"))
            {
                moveSpeed = crouchSpeed * baseSpeed;
                characterAnimator.SetFloat("speed", 0.5f);
            }
            else
            {
                moveSpeed = walkSpeed * baseSpeed;
                characterAnimator.SetFloat("speed", 1);
            }

            // Still not working correctly
            if (BindingManager.BindingPressed("Jump"))
            {
                rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Acceleration);

            }
        }
        else
        {
            moveSpeed = walkSpeed * baseSpeed;
            characterAnimator.SetFloat("speed", 1);
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Controls the stamina bar when levelling up.
    /// </summary>
    public void LevelUp()
    {
        if (Input.GetButtonDown("LevelUp"))
        {
            staminaMax += Mathf.RoundToInt(staminaMax * 0.3f);

            staminaSlider.maxValue = staminaMax;
        }
    }



}
/*Journal
 * Had some issues with this not becoming a singleton until after the load was called from customisation get.
 * I had to move the singleton into awake and the load functions into start.
 * 
 * The basespeed variable was getting divided by 2 each time it was loaded into the scene, so eventually would bcome 0 and the character would not move.
 * I fixed by multiplying it by 2 when loading the data into PlayerDataInGame to save.
 * 
 * 9/6
 * I had to get rid of the character controller so I can use the keybindings to control the movement of the character.
 * i actually thought the keybindings changed the player settings of unity, hence why I had set it in, but not actualy changed how the inputs are used, oops!
 * This meant I then needed to change how it controls the animation and also, now I can't get the jump working again.
 * I've added a rigidbody and still it doesn't seem to work properly. The assessment doesn't need a Jump, so i might just take it out.
 * 
 * 10/6
 * Now the character is clipping through everything, including the terrain. gravity also isn't affecting it. I'm trying to work out what is happening!
 * Apparantly there were 2 colliders on the character, so they werent working properly, now they are.
 * 
 * 
 */

