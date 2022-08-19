using UnityEngine;

public class Enemy_Asteroid2 : Enemy
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
                    SetSpritesVisibility(false, false);
                    StartCoroutine(AnimationRoutine());
                }
                else
                {
                    SetSpritesVisibility(false, true);
                }
                break;

            case "Player":
                player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.DamagePlayer();
                    SetSpritesVisibility(false, false);
                }
                StartCoroutine(AnimationRoutine());
                break;

            default:
                break;
        }
    }

    private void SetSpritesVisibility(bool sprite1, bool sprite2)
    {
        transform.GetChild(0).gameObject.SetActive(sprite1);
        transform.GetChild(1).gameObject.SetActive(sprite2);
    }
}
