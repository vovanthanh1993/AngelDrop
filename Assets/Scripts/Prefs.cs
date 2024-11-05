using UnityEngine;

public static class Prefs
{
    public static int bestScore
    {
        set {
            if(PlayerPrefs.GetInt("Best_Score",0) < value)
            {
                PlayerPrefs.SetInt("Best_Score", value);
            }
        }
        get => PlayerPrefs.GetInt("Best_Score", 0);
    }
    
}
