using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int killCount = 0;

    private Text scoreText;

    private PlayerCombat playerCombatComponent;

    private void Start()
    {
        playerCombatComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = killCount.ToString();
    }

    private void Update()
    {
        CheckPlayerHealth();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CheckPlayerHealth()
    {
        if (playerCombatComponent.GetPlayerHealth() <= 0)
        {
            ReloadLevel();
        }
    }

    public void SetScore(int newScore)
    {
        killCount += newScore;
        scoreText.text = killCount.ToString();
    }
}
