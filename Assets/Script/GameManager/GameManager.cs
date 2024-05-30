using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // タイマーテキスト
    float countTime = 0; // タイマー
    [SerializeField] TextMeshProUGUI resultTimeText;//リザルト画面のTimeText
    [SerializeField] TextMeshProUGUI resultScoreText;//リザルト画面のScoreText
    [SerializeField] TextMeshProUGUI resultitemQty;//リザルト画面のアイテム個数
    [SerializeField] Image checkGetItemImage;//checkGetItemの画像
    [SerializeField] Image checkTimeImage;//checkTimeの画像
    [SerializeField] Image checkScoreImage;//checkScoreの画像
    [SerializeField] TextMeshProUGUI recordText;//リザルトのrecordText
    [SerializeField] TextMeshProUGUI letsAimText;//ミニマップの案内Text
    [SerializeField] GameObject pauseMenu;
    

    ScoreManager scoreManager;
    PauseManager pauseManager;

    // シングルトン化
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    _instance = gameManagerObject.AddComponent<GameManager>();
                    DontDestroyOnLoad(gameManagerObject);
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        scoreManager = ScoreManager.Instance;
        pauseManager = PauseManager.Instance;
    }
    void Update()
    {
        ResultTime();
        ResuluScore();
        checkPanel();
        checkResultRank();
        pauseManager.ShowPauseMenu();
    }

    //タイマー処理
    public void TimerSystem() 
    {
        countTime += Time.deltaTime;
        timerText.text = countTime.ToString("F1");
    }

    //minimapのガイド削除処理

    //ゴール後のResulttext処理
    void ResultTime()
    {
        resultTimeText.text = timerText.text;
    }

    //ゴール後のresultScoreTextの処理
    void ResuluScore()
    {
        resultScoreText.text = scoreManager.scoreText.text;
    }
    
    //checkできるかどうかの判定処理
    void checkPanel()
    {
        //アイテムが8個以上の場合
        if(scoreManager.itemQty >= 8)
        {
            checkGetItemImage.gameObject.SetActive(true);
        }

        //80秒以内の場合
        else if(countTime <= 70)
        {
            checkTimeImage.gameObject.SetActive(true);
        }

        //スコアが800点以上の場合
        if(scoreManager.score >= 700)
        {
            Debug.Log("100!");
            checkScoreImage.gameObject.SetActive(true);
        }

        //それ以外の場合(条件を満たさない場合)
        else
        {
            return;
        }
    }

    //rankの判定処理
    void checkResultRank()
    {
        // Sランク条件
        if(scoreManager.itemQty >= 8 && countTime <= 80 && scoreManager.score >= 800)
        {
            recordText.text = "S";
        }
        
        // Aランク条件
        else if(scoreManager.itemQty >= 6 && countTime <= 110 && scoreManager.score >= 600)
        {
            recordText.text = "A";
        }

        // Bランク条件
        else if(scoreManager.itemQty >= 4 && countTime <= 130 && scoreManager.score >= 400)
        {
            recordText.text = "B";
        }

        // Cランク条件 (他の条件を満たさない場合)
        else
        {
            recordText.text = "C";
        }
        return;
    }

}
