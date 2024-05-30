using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager1 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed; // プレイヤーの移動速度
    private float inputHorizontal; // 横方向
    private float inputVertical; // 縦方向
    private Vector3 cameraForward; // カメラの前方向
    private Vector3 moveForward; // カメラの移動方向

    [SerializeField] GameObject GameOverPanel; // GameOverPanelを取得
    [SerializeField] GameObject GoalPanel; // GoalPanelを取得
    [SerializeField] TextMeshProUGUI crushedText; // Enemytagに倒された場合
    [SerializeField] TextMeshProUGUI exitedGame;
    [SerializeField] private AudioClip medicalClip; // Medicalの効果音
    [SerializeField] private AudioClip clearClip; // ゲームクリアの効果音
    [SerializeField] private AudioClip overClip; // ゲームオーバーの効果音

    GameManager gameManager;
    ScoreManager scoreManager;
    AudioSource audioSource; // メディカルの効果音を再生するためのAudioSource

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        scoreManager = ScoreManager.Instance;
        audioSource = gameObject.AddComponent<AudioSource>(); // メディカルの効果音用のAudioSourceを追加
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        gameManager.TimerSystem();
    }

    void FixedUpdate()
    {
        // カメラの前方向を計算
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 移動方向を計算
        moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動速度を適用し、プレイヤーに移動させる
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        // 移動方向がゼロでなければ、プレイヤーの向きを移動方向に変更する
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Enemyに衝突
        if (other.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            crushedText.gameObject.SetActive(true);
            GameOverPanel.SetActive(true);
            audioSource.PlayOneShot(overClip);//GameOverの効果音再生
        }

        //GameClear
        if (other.CompareTag("GameClear"))
        {
            Time.timeScale = 0;
            audioSource.PlayOneShot(clearClip);//GameClearの効果音再生
            GoalPanel.SetActive(true);
        }

        //GameOver
        if (other.CompareTag("GameOver"))
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            exitedGame.gameObject.SetActive(true);
            audioSource.PlayOneShot(overClip);//GameOverの効果音再生
        }

        //Item取得
        if (other.CompareTag("Medical"))
        {
            // メディカルの効果音再生
            audioSource.PlayOneShot(medicalClip);
            // スコア処理
            scoreManager.Score(other.gameObject);
        }
    }
}
