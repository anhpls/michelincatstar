using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint; 
    public float attackRange = 0.5f; 
    public LayerMask enemyLayers; 
    public int AttackDamage = 10;

    private Animator anim;
    private PlayerMovement playerMovement;
    private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            playerMovement.SetAttacking(true);
            anim.SetTrigger("attack");
            InvokeRepeating(nameof(Attack), 0, 0.5f);
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(AttackDamage);
                Debug.Log("Hit: " + enemy.name + ", Health remaining: " + enemy.GetComponent<EnemyHealth>().health);
            }
            CheckEnemyHealth();
        }
        else
        {
            Debug.Log("No enemies in range. Stopping attack.");
            StopAttacking();
        }
    }

    private void CheckEnemyHealth()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        bool enemyDefeated = false;

        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<EnemyHealth>().IsAlive())
            {
                Debug.Log("Enemy defeated: " + enemy.name);
                enemyDefeated = true;
                break;
            }
        }

        if (enemyDefeated)
        {
            playerMovement.AllowMovement();
        }
        else
        {
            Invoke(nameof(CheckEnemyHealth), 1f);
        }
    }

    private void StopAttacking()
    {
        isAttacking = false;
        playerMovement.AllowMovement();
        CancelInvoke(nameof(Attack));
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
