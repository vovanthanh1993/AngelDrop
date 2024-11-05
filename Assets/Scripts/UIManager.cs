using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;
    public Text scoreCountingText;
    public Dialog achievementDialog;
    public Dialog helpDialog;
    public Dialog gameoverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void showGameGui(bool isShow)
    {
        if(gameGUI != null)
        {
            gameGUI.SetActive(isShow);
        }
        if (homeGUI != null)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void updateScore(int score)
    {
        if (scoreCountingText)
        {
            scoreCountingText.text = score.ToString();

        }
    }
    public void showAchievementDialog()
    {
        if (achievementDialog != null) {
            achievementDialog.Show(true);
        }
    }
    public void showHelpDialog()
    {
        if (helpDialog != null)
        {
            helpDialog.Show(true);
        }
    }
    public void showGameOverDialog()
    {
        if (gameoverDialog != null)
        {
            gameoverDialog.Show(true);
        }
    }
}
