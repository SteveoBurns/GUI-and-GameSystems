using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




    /// <summary>
    /// Controls player movement and stamina usage
    /// </summary>
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public static Movement TheMovement;
        

        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        public int baseSpeed;

        private float _gravity = 20.0f;
        private Vector3 _moveDir;

        [Header("Stamina Vars")]
        public int staminaMax;
        public float stamina;
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private TMP_Text staminaText;


        private CharacterController _charC;
        private Animator characterAnimator;

    public void Awake()
    {
        if (TheMovement == null)
        {
            TheMovement = this;
        }
        else if (TheMovement != null)
            Destroy(this);
    }

    private void Start()
    {
        _charC = GetComponent<CharacterController>();
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

    /// <summary>
    /// Sets stamina values when loading a saved game
    /// </summary>
    //public void SetLoadedValues()
    //{
    //    //Sets stamina values when loading a saved game by pulling from playerdataInGame
    //    staminaMax = PlayerDataInGame.ThePlayerDataInGame.stamina;
    //    staminaSlider.maxValue = staminaMax;
    //    stamina = staminaMax;
    //    baseSpeed = PlayerDataInGame.ThePlayerDataInGame.speed / 2;
    //}

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

    private void Move()
        {
            Vector2 controlVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            //Controls character animations
            if(controlVector.magnitude >= 0.05f)
            {
                characterAnimator.SetBool("moving", true);
            }
            else
            {
                characterAnimator.SetBool("moving", false);

            }
            // can also use characterAnimator.SetBool("moving", controlVector.magnitude >= 0.05f); as the test is true or false

            // Controls speeds and animations for Sprint/Crouch/Base and Jump.
            if (_charC.isGrounded)
            {
                if (Input.GetButton("Sprint") && stamina > 0)
                {
                    moveSpeed = runSpeed * baseSpeed;
                    stamina -= Time.deltaTime;
                    characterAnimator.SetFloat("speed", 2);
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed * baseSpeed;
                    characterAnimator.SetFloat("speed", 0.5f);
                }
                else
                {
                    moveSpeed = walkSpeed * baseSpeed;
                    characterAnimator.SetFloat("speed", 1);
                }
                _moveDir = transform.TransformDirection(new Vector3(controlVector.x, 0, controlVector.y) * moveSpeed); 
                
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                   
                }                
            }
            
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }

    /// <summary>
    /// Controls the stamina bar when levelling up.
    /// </summary>
    public void LevelUp()
        {
            if (Input.GetButtonDown("LevelUp"))
            {
                staminaMax  += Mathf.RoundToInt(staminaMax * 0.3f);
                
                staminaSlider.maxValue = staminaMax;
            }
        }

       

    }
/*Journal
 * Had some issues with this not becoming a singleton until after the load was called from customisation get.
 * I had to move the singleton into awake and the load functions into start.
 * 
 */

