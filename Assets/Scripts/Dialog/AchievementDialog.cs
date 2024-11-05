using UnityEngine;
using UnityEngine.UI;
public class AchievementDialog : Dialog
{
    public Text bestScoreText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText != null) {
            bestScoreText.text = Prefs.bestScore.ToString();
        } 
    }
}
