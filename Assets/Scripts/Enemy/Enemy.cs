using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float enemySpeed;
    protected Player player;
    protected UIManager uiManager;
    private Animator animator;
    private new Collider2D collider;
    [SerializeField]
    protected int lives;
    [SerializeField]
    protected int points;

    protected AudioSource audioSource;
    [SerializeField]
    protected AudioClip explosionSoundClip;

    private enum EnemyTypes
    {
        Default,
        Asteroid,
        Asteroid2
    };

    [SerializeField]
    private EnemyTypes enemyType;

    void Awake()
    {
        InitalizeDefault();
    }

    // Update is called once per frame
    void Update()
    {
        MovementLogic();
    }

    protected void InitalizeDefault()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager is null!");
        }
        if (animator == null)
        {
            Debug.LogError("Error: Animator is null!");
        }
        if (collider == null)
        {
            Debug.LogError("Error: Collider is null!");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null!");
        }
        else
        {
            audioSource.clip = explosionSoundClip;
        }
    }

    protected void MovementLogic()
    {
        transform.Translate(enemySpeed * Time.deltaTime * Vector3.down);

        if (transform.position.y <= Config.lowerLimit)
        {
            float randomX = Random.Range(Config.leftLimit, Config.rightLimit);
            transform.position = new Vector3(randomX, Config.upperLimit, transform.position.z);
        }
    }

    protected void DamageEnemy(Collider2D other)
    {
        Destroy(other.gameObject);
        lives--;
    }

    protected IEnumerator AnimationRoutine()
    {
        animator.SetTrigger("OnDeath");
        enemySpeed = 1f;
        collider.enabled = !collider.enabled;
        audioSource.Play();
        yield return new WaitForSeconds(2.1f);
        Destroy(this.gameObject);
    }
}
