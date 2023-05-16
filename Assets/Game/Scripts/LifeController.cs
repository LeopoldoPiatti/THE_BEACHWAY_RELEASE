using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    public int life;
    public int life_max = 3;
    private Rigidbody2D bikeRigidbody;
    public enum DeathMode { LastCheckpoint, ChangeScene }
    public DeathMode deathMode;
    public Transform checkpoint;
    public float deathDelay = 2.0f;
    public string sceneToLoad;

    public GameObject crashParticlePrefab;
    public float crashParticleDuration;
    public float crashParticlesXposition;
    public float crashParticlesYposition;

    AudioSource audioSource;
    public AudioClip audioCrash;

    public float invencible_max = 3;
    float invencible_time;

    public SpriteRenderer otherObjectRenderer;
    private Color otherObjectOriginalColor;
    public Color damageOtherObjectColor;


    public delegate void LifeChangedEventHandler(int lives);
    public event LifeChangedEventHandler OnLifeChanged;

    public int Life
    {
        get { return life; }
    }

    void Start()
    {
        bikeRigidbody = GetComponent<Rigidbody2D>();
        life = life_max;
        audioSource = GetComponent<AudioSource>();
        otherObjectRenderer = GameObject.Find("BikeCarretera").GetComponent<SpriteRenderer>();
        otherObjectOriginalColor = otherObjectRenderer.color;

    }

    void Update()
    {
        invencible_time -= Time.deltaTime;
        if (invencible_time <= 0)
        {
            otherObjectRenderer.color = otherObjectOriginalColor;
        }
    }

    public void Damage(int amount)
    {
        if (invencible_time <= 0)
        {
            invencible_time = invencible_max;
            life -= amount;
            EmitCrashParticle();
            audioSource.PlayOneShot(audioCrash);
            otherObjectRenderer.color = damageOtherObjectColor;

            if (life == 0)
            {
                StartCoroutine(DeathWithDelay1());
            }
            else
            {
                StartCoroutine(DeathWithDelay2());
            }

            if (OnLifeChanged != null)
            {
                OnLifeChanged.Invoke(life);
            }
        }
    }
    IEnumerator DeathWithDelay1()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(deathDelay);
        Time.timeScale = 1f;
        Death();
    }
    IEnumerator DeathWithDelay2()
    {
        bikeRigidbody.velocity = Vector3.zero;
        //Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(deathDelay);
        //Time.timeScale = 1f;
        LastCheckpoint();
    }

    
    public void LastCheckpoint()
    {
        if (life != 0)
        {        
            if (checkpoint != null && deathMode == DeathMode.LastCheckpoint)
        {
            life = life_max;
            transform.position = checkpoint.position;
            bikeRigidbody.velocity = Vector2.zero;
            bikeRigidbody.angularVelocity = 0f;
        }
        }
    }
    public void Death()
    {
        if (life == 0)
        {
            if (deathMode == DeathMode.ChangeScene)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
    private void EmitCrashParticle()
    {
        Vector2 particlePosition = new Vector2(transform.position.x + crashParticlesXposition, transform.position.y + crashParticlesYposition);
        GameObject dustParticles = Instantiate(crashParticlePrefab, particlePosition, Quaternion.identity);
        Destroy(dustParticles, crashParticleDuration);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
