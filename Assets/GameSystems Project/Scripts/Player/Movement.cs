using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Debugging.Player
{
    /// <summary>
    /// Controls player movement
    /// </summary>
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        

        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        public int baseSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;

        public int staminaMax;
        public float stamina;
        [SerializeField] public Slider staminaSlider;
        [SerializeField] public TMP_Text staminaText;


        private CharacterController _charC;
        private Animator characterAnimator;


        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
            baseSpeed = CustomisationGet.speed / 2;
            
            //Sets stamina values
            staminaMax = CustomisationGet.stamina;
            staminaSlider.maxValue = staminaMax;
            stamina = staminaMax;
            
            

        }
        private void Update()
        {
            Move();
            LevelUp();
            UpdateStamina();
            #region Stamina Bar update and regen
            staminaSlider.value = stamina;            
            staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina) + "/" + staminaMax;

            if (stamina < staminaMax && !Input.GetButton("Sprint"))
            {
                stamina += Time.deltaTime;
            }
            #endregion
        }
        private void Move()
        {
            Vector2 controlVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            if(controlVector.magnitude >= 0.05f)
            {
                characterAnimator.SetBool("moving", true);
            }
            else
            {
                characterAnimator.SetBool("moving", false);

            }
            // can also use characterAnimator.SetBool("moving", controlVector.magnitude >= 0.05f); as the test is true or false

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
                UpdateStamina();
            }
        }

        public void UpdateStamina()
        {
            staminaSlider.maxValue = staminaMax;
        }

    }
}
