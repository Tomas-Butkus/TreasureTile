using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    [SerializeField] private int damage = 5;
    [SerializeField] private int chaseDistance = 3;

    [SerializeField][Range(1, 10)] private float attackDelay = 3;
    [SerializeField][Range(0, 5)] private float healthBarOffsetX = 1;
    [SerializeField][Range(0, 5)] private float healthBarOffsetY = 1;

    private bool isCoroutineExecuting = false;

    private Animator enemyAnimator;

    private HealthBar playerHealthBar;
    private HealthBar enemyHealthBar;

    private GameObject player;

    private GameManager gameManager;

    private GridManager gridManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("GridHolder").GetComponent<GridManager>();
        enemyAnimator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();

        enemyHealthBar = gameObject.transform.Find("Canvas/EnemyHealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        CheckIfAlive();
        SetHealthBarPosition();
        StartCoroutine(ChaseDelay(attackDelay));
    }

    private void CheckPlayerNearby()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 1 && distance <= chaseDistance)
        {
            ChasePlayer();
        }
        else if (distance <= 1)
        {
            enemyAnimator.Play("EnemyWeak_Attack");
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        PlayerCombat playerCombatComponent = player.GetComponent<PlayerCombat>();
        int damagedHealth = playerCombatComponent.GetPlayerHealth() - damage;

        playerHealthBar.SetHealth(damagedHealth);
        playerCombatComponent.TakeDamage(damage);
    }

    private IEnumerator ChaseDelay(float attackDelay)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;
        yield return new WaitForSeconds(attackDelay);
        CheckPlayerNearby();
        isCoroutineExecuting = false;
    }

    private void ChasePlayer()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.x > transform.position.x && CheckIfCanWalk(transform.position + new Vector3(1, 0, 0)) != false)
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else if(playerPosition.x < transform.position.x && CheckIfCanWalk(transform.position - new Vector3(1, 0, 0)) != false)
        {
            transform.position -= new Vector3(1, 0, 0);
        }
        else if(playerPosition.y > transform.position.y && CheckIfCanWalk(transform.position + new Vector3(0, 1, 0)) != false)
        {
            transform.position += new Vector3(0, 1, 0);
        }
        else if(playerPosition.y < transform.position.y && CheckIfCanWalk(transform.position - new Vector3(0, 1, 0)) != false)
        {
            transform.position -= new Vector3(0, 1, 0);
        }
    }

    private bool CheckIfCanWalk(Vector3 nextStep)
    {
        for (int row = 0; row < gridManager.gridArray.GetLength(0); row++)
        {
            for (int column = 0; column < gridManager.gridArray.GetLength(1); column++)
            {
                if (nextStep.x == gridManager.gridArray[row, column].GetXPosition() && nextStep.y == gridManager.gridArray[row, column].GetYPosition()
                    && gridManager.gridArray[row, column].GetIsWalkable() && gridManager.gridArray[row, column].GetIsOccupied() != true)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SetHealthBarPosition()
    {
        enemyHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(healthBarOffsetX, healthBarOffsetY, 0));
    }

    private void CheckIfAlive()
    {
        if(currentHealth > 0)
        {
            enemyHealthBar.SetHealth(currentHealth);
        }
        else
        {
            gameManager.SetScore(1);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public int GetEnemyHealth()
    {
        return currentHealth;
    }
}
