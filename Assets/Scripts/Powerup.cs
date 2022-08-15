using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float powerupSpeed = PowerupConfig.speed;
    private Player player;

    private enum PowerupTypes
    {
        TripleShot,
        SpeedBoost,
        Shield
    };

    [SerializeField]
    private PowerupTypes powerupType;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Error: Player is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(powerupSpeed * Time.deltaTime * Vector3.down);

        if (transform.position.y <= Config.lowerLimit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                switch (powerupType)
                {
                    case PowerupTypes.Shield:
                        if (player != null)
                        {
                            player.ActivateShield();
                        }
                        break;

                    case PowerupTypes.TripleShot:
                        if (player != null)
                        {
                            player.ActivateTripleShot();
                        }
                        break;

                    case PowerupTypes.SpeedBoost:
                        if (player != null)
                        {
                            player.ActivateSpeedBoost();
                        }
                        break;
                }
                Destroy(this.gameObject);
                break;

            default:
                break;
        }
    }
}
