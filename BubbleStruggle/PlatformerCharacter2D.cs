using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        public AudioClip dieSound;
        AudioSource die;
        public Rigidbody2D platform;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private float x;
        private float y;

        bool rosnie = false;
        bool maleje = false;

        private void Start()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            die = GetComponent<AudioSource>();

            x = platform.position.x;
            y = platform.position.y;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if(x >= 8.74f)
            {
                maleje = true;
                rosnie = false;
            }
            else if(x <= -8.81f)
            {
                rosnie = true;
                maleje = false;
            }
            if (rosnie)
            {
                x += 0.1f;
                platform.MovePosition(new Vector2(x, y));
            }
            else if (maleje)
            {
                x -= 0.1f;
                platform.MovePosition(new Vector2(x, y));
            }

        }


        public void Move(float move, bool crouch, bool jump)
        {
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
            m_Anim.SetBool("Crouch", crouch);
            if (m_Grounded || m_AirControl)
            {
                move = (crouch ? move*m_CrouchSpeed : move);
                m_Anim.SetFloat("Speed", Mathf.Abs(move));
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
                if (move > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (move < 0 && m_FacingRight)
                {
                    Flip();
                }
            }
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            m_FacingRight = !m_FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.tag == "Ball")
            {
                //Smierc
                die.Play();
                //Debug.Log("Smierc");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }

}
