using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverDialog : Dialog
{
    public Text bestScoreText;
    bool m_replayClick;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText != null)
        {
            bestScoreText.text = Prefs.bestScore.ToString();
        }
    }

    public void replay()
    {
        Time.timeScale = 1f;
        GameController.Ins.IsGameOver = false;
        GameController.Ins.setScore(0);
        m_replayClick = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToHome() {
        Time.timeScale = 1f;
        destroyAllObject();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (m_replayClick) { 
            UIManager.Ins.showGameGui(true);
            GameController.Ins.playGame();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void destroyAllObject() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Lấy tất cả các GameObject trong scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Xóa tất cả các GameObject
        foreach (GameObject obj in allObjects)
        {
            // Nếu đối tượng không phải là đối tượng script này, hãy xóa
            if (obj != this.gameObject)
            {
                Destroy(obj);
            }
        }
    }
}
