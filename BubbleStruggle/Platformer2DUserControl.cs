using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetKeyDown(KeyCode.Space);
            }
        }


        private void FixedUpdate()
        {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
