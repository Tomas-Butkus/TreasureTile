using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    [SerializeField] private int damage = 50;

    private GameObject nearbyEnemy;

    private Animator playerAnimator;

    public HealthBar healthBar;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        CheckEnemyNearby();
    }

    private void AttackEnemy()
    {
        int enemyHealth = nearbyEnemy.GetComponent<Enemy>().GetEnemyHealth();

        if (enemyHealth > 0)
        {
            playerAnimator.Play("Player_Attack");
            nearbyEnemy.GetComponent<Enemy>().TakeDamage(damage);
            transform.position = GetComponent<PlayerMovement>().GetLastPlayerPosition();
        }
    }

    private void CheckEnemyNearby()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyList)
        {
            if (transform.position == enemy.transform.position)
            {
                nearbyEnemy = enemy;
                AttackEnemy();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public int GetPlayerHealth()
    {
        return currentHealth;
    }
}
