using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // ScoreText
    [SerializeField] List<GameObject> medicalItem; // スコア増加のアイテム
    public int itemQty = 0;//アイテムの個数
    [SerializeField] TextMeshProUGUI resultitemQty;
    public int score = 0;

    private static ScoreManager _instance;

    //シングルトン化
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();

                if (_instance == null)
                {
                    GameObject ScoreManagerObject = new GameObject("ScoreManager");
                    _instance = ScoreManagerObject.AddComponent<ScoreManager>();
                    DontDestroyOnLoad(ScoreManagerObject);
                }
            }
            return _instance;
        }
    }

    //Itemの処理
    public void Score(GameObject item)
    {
        score += 100; //スコア処理
        itemQty += 1; //リザルトのitem個数処理

        scoreText.text = score.ToString();
        resultitemQty.text = itemQty.ToString();

        // アイテムを削除し、リストから取り除く
        if (medicalItem.Contains(item))
        {
            Destroy(item);
            medicalItem.Remove(item);
        }
    }
}
