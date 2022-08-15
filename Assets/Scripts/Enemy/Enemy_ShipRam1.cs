using UnityEngine;

public class Enemy_ShipRam1 : Enemy
{
    [SerializeField]
    private Transform target;
    private bool lockRotation = false;

    [SerializeField]
    private AudioClip warningSoundClip;

    private void Start()
    {
        audioSource.clip = warningSoundClip;
        audioSource.Play();
        target = GameObject.Find("Player").GetComponent<Transform>();
        if (target == null)
        {
            Debug.Log("Error: Target(Player) is null!");
        }
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.Find("HomeDestination").GetComponent<Transform>();
        }
        if (!lockRotation)
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }

        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            transform.Translate(new Vector3(enemySpeed * Time.deltaTime, 0, 0));
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Laser":
                DamageEnemy(other);
                if (lives <= 0)
                {
                    player.IncreaseScore(points);
                    transform.GetChild(0).gameObject.SetActive(false);
                    lockRotation = true;
                    audioSource.clip = explosionSoundClip;
                    StartCoroutine(AnimationRoutine());
                }
                break;

            case "Player":
                if (player != null)
                {
                    player.DamagePlayer();
                }
                transform.GetChild(0).gameObject.SetActive(false);
                lockRotation = true;
                audioSource.clip = explosionSoundClip;
                StartCoroutine(AnimationRoutine());
                break;

            default:
                break;
        }
    }
}
