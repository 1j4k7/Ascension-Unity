using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject AboutPage;

    void Awake() {
        // Make the fullscreen toggle state match the actual fullscreen state
        OptionsMenu.GetComponentInChildren<Toggle>(true).isOn = Screen.fullScreen;
    }
    
    // Add different methods for different Button clicks
    public void OfflineButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void OptionsButton() {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void ExitButton() {
        Application.Quit();
    }
    public void AboutButton() {
        MainMenu.SetActive(false);
        AboutPage.SetActive(true);
    }
    public void OnlineButton() {
        Debug.Log("Online capabilities not implemented yet!");
    }

    public void FullScreenToggle(bool fullScreen) {
        Screen.fullScreen = fullScreen;
    }
    
    public void BackButton() {
        OptionsMenu.SetActive(false);
        AboutPage.SetActive(false);
        MainMenu.SetActive(true);
    }
}
