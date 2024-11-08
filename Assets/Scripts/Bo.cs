using UnityEngine;

public class Bo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            
            int s = Random.Range(0, 2);
            if (s > 0)
                AudioController.Ins.PlaySound(AudioController.Ins.mewmew);
            else 
                AudioController.Ins.PlaySound(AudioController.Ins.mewmew2);
            GameController.Ins.UpdateScore(-GameController.Ins.getScore()/2);
            Destroy(gameObject);
        }
    }
}
