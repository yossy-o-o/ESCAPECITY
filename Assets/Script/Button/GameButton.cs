using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private AudioClip buttonClip; // Buttonの効果音

    private AudioManager audioManager;
    private AudioSource audioSource;

    void Start()
    {
        audioManager = AudioManager.Instance;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // ゲームクリア時のMainMenuButton処理
    public void OnclickMainMenuButton()
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(buttonClip);
        SceneManager.LoadScene("MainMenu");
    }

    // ゲームクリア時のEscapeButton処理
    public void OnclickEscapeButton()
    {
        audioSource.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnclickTutorialEscapeButton()
    {
        audioSource.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    
    // MainMenuでEscapeButtonを押したときの処理
    public void OnclickMainmenuEscapeButton()
    {
        audioSource.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    // MainMenuでQuitButtonを押したときの処理
    public void OnclickQuitButton()
    {
        audioSource.PlayOneShot(buttonClip);
        quitPanel.gameObject.SetActive(true);
    }

    // MainMenuでQuitYesボタンを押したときの処理
    public void OnclickQuitYesButton()
    {
        audioSource.PlayOneShot(buttonClip);
        Application.Quit();
    }

    // MainMenuでQuitNoボタンを押したときの処理
    public void OnclickQuitNoButton()
    {
        audioSource.PlayOneShot(buttonClip);
        quitPanel.SetActive(false);
    }

    // pause画面でEscapeボタンを押したときの処理
    public void ResumeGame()
    {
        audioSource.PlayOneShot(buttonClip);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1; // ゲームを再開
    }
}
