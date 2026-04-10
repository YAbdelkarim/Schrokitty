using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
        [SerializeField] private GameObject pauseMenu;
        private bool _paused = false;

        [SerializeField] private GameOverUI gameOverUI;

        [SerializeField] private CharacterDeath characterDeath;
        void Update()
        {
                if (Input.GetKeyDown(KeyCode.R))
                {
                        RestartLevel();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                        TogglePause();
                }
        }

        void RestartLevel()
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void TogglePause()
        {
            if (characterDeath.isDead)
            {
                if (!_paused)
                {
                    gameOverUI.HideGameOver();
                    pauseMenu.SetActive(true);
                    _paused = true;
                    Time.timeScale = 0f;
                }
                else
                {
                    gameOverUI.ReshowGameOver();
                    pauseMenu.SetActive(false);
                    _paused = false;
                    Time.timeScale = 1f;
                }
            }
            else if (_paused) 
            { 
                pauseMenu.SetActive(false);
                _paused = false;
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                _paused = true;
                Time.timeScale = 0f;
            }
        
        }
}
