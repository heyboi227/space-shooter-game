using UnityEngine;

public class StartingUFO : MonoBehaviour
{
    [SerializeField]
    private float speed = StartingUFOConfig.rotateSpeed;
    private Animator animator;
    private new Collider2D collider;
    private SpawnManager spawnManager;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosionSoundClip;
    private bool lockRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioSource = GetComponent<AudioSource>();
        if (animator == null)
        {
            Debug.LogError("Error: Animator is null!");
        }
        if (collider == null)
        {
            Debug.LogError("Error: Collider is null!");
        }
        if (spawnManager == null)
        {
            Debug.LogError("Error: SpawnManager is null!");
        }
        if (audioSource == null)
        {
            Debug.LogError("Error: AudioSource is null!");
        }
        else
        {
            audioSource.clip = explosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockRotation)
        {
            transform.Rotate(speed * Time.deltaTime * Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Laser":
                lockRotation = true;
                animator.SetTrigger("OnDeath");
                spawnManager.StartSpawn();
                audioSource.Play();
                Destroy(other.gameObject);
                Destroy(this.gameObject, 2.1f);
                break;

            default:
                break;
        }
    }
}
