using UnityEngine;

public class Girl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            GameController.Ins.setGameOver(true);
            Destroy(gameObject);
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
        }
        if (collision.CompareTag("Player"))
        {
            GameController.Ins.UpdateScore(1);
            Destroy(gameObject);
        }
    }
}
