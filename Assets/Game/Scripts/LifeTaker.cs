using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTaker : MonoBehaviour
{
    public LifeController lifeController;
    public int damageAmount;    
    public CameraShake cameraShake;
    //public GameObject crashParticlePrefab;
    //AudioSource audioSource;
    //public AudioClip audioCrash;


    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si la colisión es con el jugador
        if (collision.CompareTag("Player"))
        {
            // Llamar al método Damage del LifeController con el daño especificado
            lifeController.Damage(damageAmount);
            cameraShake.ShakeCamera();
            //EmitCrashParticle();
            //audioSource.PlayOneShot(audioCrash);
        }
    }
    //private void EmitCrashParticle()
    //{

    //    Vector2 particlePosition = new Vector2(transform.position.x + 0f, transform.position.y - 0f);
    //    GameObject dustParticles = Instantiate(crashParticlePrefab, particlePosition, Quaternion.identity);
    //    Destroy(dustParticles, 5f);
    //}
}