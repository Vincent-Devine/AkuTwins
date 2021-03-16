using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SettingButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindown()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
