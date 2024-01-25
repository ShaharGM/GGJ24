using UnityEngine;

namespace Weapons{
// Rename this class to SwordBase so it can act as a base class for different types of swords
    public class SwordBase : MonoBehaviour
    {
        // Basic sword configurations
        public float length = 1.0f;
        public float power = 10.0f;
        public float width = 0.2f;

        public bool is_attacking = false;

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
            if(is_attacking)
            {
                Move();
            }
        }

        private void Attack()
        {
            Debug.Log($"Sword attacking with power {power}, length {length}, and width {width}.");
            PerformBasicSlash();
        }

        // Make this method virtual so it can be overridden by subclasses
        protected virtual void PerformBasicSlash()
        {
            Debug.Log("SwordBase performed a basic slash attack.");
        }

        protected virtual void Move()
        {
            Debug.Log("SwordBase performed a basic Movement.");
            is_attacking = false;
        }
    }
}