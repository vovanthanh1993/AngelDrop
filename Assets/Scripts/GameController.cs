using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public GameObject[] prefabs;
    public GameObject player;
    public GameObject wall;
    public float spawnTime;
    public float speedWithTimePortrait;
    public float speedWithTimeLandscape;

    float m_spawnTime;
    int m_score;
    bool m_isGameOver;
    bool isStart;
    float startTime;
    Camera mainCamera;
    GameObject leftWall;
    GameObject righttWall;

    public int Score { get => m_score; set => m_score = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public bool IsStart { get => isStart; set => isStart = value; }

    public override void Awake()
    {
        MakeSingleton(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        AudioController.Ins.PlayBackgroundMusic();
        isStart = false;
        m_isGameOver = false;
        m_spawnTime = 0;
        m_score = 0;
        UIManager.Ins.updateScore(m_score);
        UIManager.Ins.showGameGui(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStart) return;

        if (m_isGameOver) {
            Prefs.bestScore = m_score;
            Time.timeScale = 0f;
            UIManager.Ins.showGameOverDialog();
            return;
        }

        m_spawnTime -=Time.deltaTime;
        if(m_spawnTime <=0)
        {
            spawnFood();
            m_spawnTime = spawnTime;
        }

        mainCamera = Camera.main;
        Vector3 midLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0));
        Vector3 midRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
        leftWall.transform.position = midLeft;
        righttWall.transform.position = midRight;
    }

    // Getter cho m_score
    public int getScore()  {
        return m_score;
    }

    // Setter cho m_score
    public void setScore(int score)
    {
        m_score = score;
    }

    // Getter cho m_isGameOver
    public bool isGameOver() {
            return m_isGameOver;
        }

    // Setter cho m_isGameOver
    public void setGameOver(bool isGameOver)
    {
        m_isGameOver = isGameOver;
    }

    public void UpdateScore(int point)
    {
        m_score+= point;
        UIManager.Ins.updateScore(m_score);

        if(point > 0)
        {
            if (m_score> 0 && m_score % 10 == 0)
            {
                AudioController.Ins.playCheerSound();
            }
            else AudioController.Ins.PlaySound(AudioController.Ins.getScore);
        }
    }

    public void spawnFood()
    {
        mainCamera = Camera.main;
        // Lấy vị trí của góc trên bên trái
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(20, Screen.height, 0));
        // Lấy vị trí của góc trên bên phải
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width-20, Screen.height, 0));

        float speed = 0.2f + (Time.time - startTime)* speedWithTimePortrait;
        if (Screen.width > Screen.height) speed =0.05f + (Time.time - startTime) * speedWithTimeLandscape;

        // Chọn ngẫu nhiên một prefab từ mảng
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject prefabToSpawn = prefabs[randomIndex];
        prefabToSpawn.GetComponent<Rigidbody2D>().gravityScale = speed;

        Vector2 spwanPos = new Vector2(Random.Range(topLeft.x, topRight.x), topRight.y+0.6f);
        Instantiate(prefabToSpawn, spwanPos, Quaternion.identity);
    }

    public void playGame()
    {
        UIManager.Ins.showGameGui(true);
        isStart = true;
        Instantiate(player, new Vector3(-0.06f, -3.85f, 5), Quaternion.identity);
        mainCamera = Camera.main;
        Vector3 midLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 0));
        Vector3 midRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));

        leftWall = Instantiate(wall, midLeft, Quaternion.identity);
        righttWall = Instantiate(wall, midRight, Quaternion.identity);
        startTime = Time.time;
    }
}
