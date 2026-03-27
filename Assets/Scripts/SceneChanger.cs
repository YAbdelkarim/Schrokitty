using UnityEngine;
using UnityEngine.SceneManagement; // Essential for scene switching

public class SceneChanger : MonoBehaviour
{
    public GameObject _controlsPanel;
    public void MoveToScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Debug.Log("Quit button was pressed!");
        Application.Quit();
    }
    public void ShowControls()
    {
        if (_controlsPanel != null)
        {
            _controlsPanel.SetActive(true);
        }
    }
    public void HideControls()
    {
        if (_controlsPanel != null)
        {
            _controlsPanel.SetActive(false);
        }
    }
}