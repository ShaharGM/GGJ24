using UnityEngine;

namespace Weapons
{
    // Rename this class to SwordBase so it can act as a base class for different types of swords
    [System.Serializable]
    public class SwordBase : MonoBehaviour
    {
        // Basic sword configurations
        public float power = 10.0f;
        public float attackRange = 1.0f;
        public float circleCastRadius = 0.5f;
        public LayerMask enemyLayer;
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
            // Check if the sword has a parent and use its transform, otherwise use the sword's own transform
            Transform attackOrigin = transform.parent ? transform.parent : transform;

            Debug.Log($"Sword attacking with power {power}, length {attackRange}, and width {circleCastRadius}.");
            Vector2 direction = attackOrigin.right; // Assuming the parent's right is the forward direction
            RaycastHit2D hit = Physics2D.CircleCast(attackOrigin.position, circleCastRadius, direction, attackRange, enemyLayer);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                // Call the OnHit function of the enemy
                Debug.Log("Enemy Hit!");
                hit.collider.gameObject.GetComponent<EnemyBase>().OnHit(10, this.gameObject);
            }
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

        void OnDrawGizmos()
        {
            // Draw the circle cast (ray) for debug purposes
            Gizmos.color = Color.yellow;
            Transform attackOrigin = transform.parent ? transform.parent : transform;
            Vector2 direction = attackOrigin.right; // Assuming the parent's right is the forward direction
            Gizmos.DrawLine(attackOrigin.position, attackOrigin.position + new Vector3(direction.x, direction.y, 0) * attackRange);
            Gizmos.DrawWireSphere(attackOrigin.position + new Vector3(direction.x, direction.y, 0) * attackRange, circleCastRadius);
        }
    }
}
