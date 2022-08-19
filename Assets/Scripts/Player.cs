using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = PlayerConfig.speed;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private float fireRate = LaserConfig.fireRate;
    private float canFire = -1f;
    [SerializeField]
    private int lives = PlayerConfig.lives;
    private UIManager uIManager;
    private GameManager gameManager;
    [SerializeField]
    private bool canTripleShoot = false;
    [SerializeField]
#pragma warning disable IDE0052 // Remove unread private members
    private bool canSpeedBoost = false;
#pragma warning restore IDE0052 // Remove unread private members
    private bool isSpeedBoost = false;
    [SerializeField]
    private bool canShield = false;

    [SerializeField]
    private GameObject shieldVisual1;
    [SerializeField]
    private GameObject shieldVisual2;
    [SerializeField]
    private GameObject shieldVisual3;

    [SerializeField]
    private bool isPlayerOne;

    private int shieldLives = 0;

    private AudioSource audioSource;
    private AudioSource thrusterAudioSource;
    [SerializeField]
    private AudioClip laserSoundClip;
    [SerializeField]
    private AudioClip tripleShotSoundClip;
    [SerializeField]
    private AudioClip shieldUpSoundClip;
    [SerializeField]
    private AudioClip shieldDownSoundClip;
    [SerializeField]
    private AudioClip powerupPickupSoundClip;
    [SerializeField]
    private AudioClip lifeDownSoundClip;
    [SerializeField]
    private AudioClip thrusterSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        thrusterAudioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();

        if (uIManager == null)
        {
            Debug.LogError("UIManager is null!");
        }
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null!");
        }
        if (audioSource == null || thrusterAudioSource == null)
        {
            Debug.LogError("AudioSource is null!");
        }
        else
        {
            audioSource.clip = laserSoundClip;
            thrusterAudioSource.clip = thrusterSoundClip;
            thrusterAudioSource.loop = true;
            thrusterAudioSource.Play();
        }

        if (!gameManager.GetIsCoOpMode())
        {
            transform.position = new Vector3(PlayerConfig.startXPosition, PlayerConfig.startYPosition, PlayerConfig.startZPosition);
        }

        uIManager.UpdateLives(lives, isPlayerOne);
    }

    // Update is called on each frame
    void Update()
    {
        MovementLogic();
        ShootingLogic();
    }

    void ShootingLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && isPlayerOne)
        {
            FireLaser();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > canFire && !isPlayerOne)
        {
            FireLaser();
        }
    }

    void MovementLogic()
    {
        Vector3 direction;
        if (isPlayerOne)
        {
            // Inputs for horizontal and vertical movement (directional arrows)
            float horizontalInput = Input.GetAxis("Horizontal-Arrows");
            float verticalInput = Input.GetAxis("Vertical-Arrows");

            direction = new(horizontalInput, verticalInput, 0);
        }
        else
        {
            // Inputs for horizontal and vertical movement (W, A, S and D keys)
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            direction = new(horizontalInput, verticalInput, 0);


        }

        // Move the player to the given direction with 5.0 speed
        transform.Translate(speed * Time.deltaTime * direction);

        // Limit player movement on vertical axis
        // upper and lower limit
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, PlayerConfig.lowerLimit, PlayerConfig.upperLimit), transform.position.z);

        // Limit player movement on horizontal axis

        // left and right limit
        if (transform.position.x <= PlayerConfig.leftLimit)
        {
            transform.position = new Vector3(PlayerConfig.rightLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= PlayerConfig.rightLimit)
        {
            transform.position = new Vector3(PlayerConfig.leftLimit, transform.position.y, transform.position.z);
        }
    }

    public void DamagePlayer()
    {
        audioSource.clip = shieldDownSoundClip;
        if (canShield)
        {
            shieldLives--;
            switch (shieldLives)
            {
                case 1:
                    shieldVisual2.SetActive(false);
                    shieldVisual1.SetActive(true);
                    audioSource.Play();
                    break;
                case 2:
                    shieldVisual3.SetActive(false);
                    shieldVisual2.SetActive(true);
                    audioSource.Play();
                    break;
                default:
                    canShield = false;
                    shieldVisual1.SetActive(false);
                    audioSource.Play();
                    break;
            }
            return;
        }

        audioSource.clip = lifeDownSoundClip;
        audioSource.Play();
        lives--;

        uIManager.UpdateLives(lives, isPlayerOne);

        if (lives <= 0)
        {
            Destroy(this.gameObject);

            thrusterAudioSource.Stop();
            thrusterAudioSource.loop = false;
            thrusterAudioSource.clip = null;
        }
    }

    public void ActivateTripleShot()
    {
        audioSource.clip = powerupPickupSoundClip;
        canTripleShoot = true;
        audioSource.Play();
        StartCoroutine(DeactivateTripleShot());
    }

    public void ActivateSpeedBoost()
    {
        audioSource.clip = powerupPickupSoundClip;
        canSpeedBoost = true;
        if (!isSpeedBoost)
        {
            speed *= 2;
        }
        audioSource.Play();
        isSpeedBoost = true;
        StartCoroutine(DeactivateSpeedBoost());
    }

    public void ActivateShield()
    {
        audioSource.clip = shieldUpSoundClip;
        canShield = true;

        if (shieldLives < 3)
        {
            shieldLives++;
        }

        switch (shieldLives)
        {
            case 2:
                shieldVisual1.SetActive(false);
                shieldVisual2.SetActive(true);
                audioSource.Play();
                break;
            case 3:
                shieldVisual2.SetActive(false);
                shieldVisual3.SetActive(true);
                audioSource.Play();
                break;
            default:
                shieldVisual1.SetActive(true);
                audioSource.Play();
                break;
        }
    }

    private IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(PowerupConfig.timeLimit);
        canTripleShoot = false;
    }

    private IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(PowerupConfig.timeLimit);
        speed /= 2;
        isSpeedBoost = false;
        canSpeedBoost = false;
    }

    public void FireLaser()
    {
        canFire = Time.time + fireRate;
        audioSource.clip = laserSoundClip;
        Instantiate(laserPrefab, transform.position + LaserConfig.offsetSpawn, Quaternion.identity);
        audioSource.Play();

        if (canTripleShoot)
        {
            audioSource.clip = tripleShotSoundClip;
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            audioSource.Play();
        }
    }

    public void IncreaseScore(int points)
    {
        uIManager.UpdateScore(points);
    }
}
