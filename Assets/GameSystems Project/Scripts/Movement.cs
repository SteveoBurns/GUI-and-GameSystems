using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {

        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;

        private CharacterController _charC;
        private Animator characterAnimator;


        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
            
        }
        private void Update()
        {
            Move();
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
                if (Input.GetButton("Sprint"))
                {
                    moveSpeed = runSpeed;
                    characterAnimator.SetFloat("speed", 2);
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                    characterAnimator.SetFloat("speed", 0.5f);
                }
                else
                {
                    moveSpeed = walkSpeed;
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
        
    }
}
