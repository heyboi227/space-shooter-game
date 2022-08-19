using UnityEngine;

public class Enemy_Asteroid1 : Enemy
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Laser":
                DamageEnemy(other);
                if (lives <= 0)
                {
                    uiManager.UpdateScore(points);
                    StartCoroutine(AnimationRoutine());
                }
                break;

            case "Player":
                player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.DamagePlayer();
                }
                StartCoroutine(AnimationRoutine());
                break;

            default:
                break;
        }
    }
}
