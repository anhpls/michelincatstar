using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject);
    }
}
