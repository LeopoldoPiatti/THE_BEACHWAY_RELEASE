using System.Collections;
using System.Collections.Generic;
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
    AudioSource audioSource;
    public AudioClip audioCrash;

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
    }

    void Update()
    {

    }

    public void Damage(int amount)
    {
        life -= amount;
        EmitCrashParticle();
        audioSource.PlayOneShot(audioCrash);

        if (life == 0)
        {
            StartCoroutine(DeathWithDelay());
        }
        if (life != 0)
        {
            StartCoroutine(DeathWithDelay());
        }

        if (OnLifeChanged != null)
        {
            OnLifeChanged.Invoke(life);
        }
    }

    IEnumerator DeathWithDelay()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(deathDelay);
        Time.timeScale = 1f;
        Death();
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
        else if (checkpoint != null && deathMode == DeathMode.LastCheckpoint)
        {
            LastCheckpoint();
        }
    }

    public void LastCheckpoint()
    {
        life = life_max;
        transform.position = checkpoint.position;
        bikeRigidbody.velocity = Vector2.zero;
        bikeRigidbody.angularVelocity = 0f;
    }
    private void EmitCrashParticle()
    {

        Vector2 particlePosition = new Vector2(transform.position.x + 5f, transform.position.y + 1f);
        GameObject dustParticles = Instantiate(crashParticlePrefab, particlePosition, Quaternion.identity);
        Destroy(dustParticles, 1f);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
