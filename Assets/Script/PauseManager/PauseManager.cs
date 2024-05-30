using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] Button escapeButtom;

    private static PauseManager _instance;

    //シングルトン化
    public static PauseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PauseManager>();

                if (_instance == null)
                {
                    GameObject PauseManagerObject = new GameObject("GameManager");
                    _instance = PauseManagerObject.AddComponent<PauseManager>();
                    DontDestroyOnLoad(PauseManagerObject);
                }
            }
            return _instance;
        }
    }


    public void ShowPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // pauseMenuがアクティブかどうかをチェック
            if (pauseMenu.gameObject.activeSelf)
            {
                // pauseMenuがアクティブなら非アクティブにする
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1; // ゲームを再開
            }
            else
            {
                // pauseMenuが非アクティブならアクティブにする
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0; // ゲームを一時停止
            }
        }
    }

}
