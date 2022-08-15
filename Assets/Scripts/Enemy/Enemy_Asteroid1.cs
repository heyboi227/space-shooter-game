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
                    player.IncreaseScore(points);
                    StartCoroutine(AnimationRoutine());
                }
                break;

            case "Player":
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
