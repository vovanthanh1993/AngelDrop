using UnityEngine;

public class OldWoman : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            AudioController.Ins.PlaySound(AudioController.Ins.oldwoman);
            GameController.Ins.UpdateScore(-1);
            Destroy(gameObject);
        }
    }
}
