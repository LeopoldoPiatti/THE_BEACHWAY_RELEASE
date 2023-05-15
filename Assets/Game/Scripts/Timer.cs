using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float duration; // Duración del temporizador en segundos
    private bool isTimerRunning = false; // Estado del temporizador
    public string gameOverSceneName;
    public TextMeshProUGUI tiempo;
    public float startTime;

    private void Start()
    {
        // Iniciar el temporizador al comienzo
        StartTimer();
    }

    private void Update()
    {
        tiempo.text = (startTime - Time.time).ToString("00.00");
        // Verificar si el temporizador está en funcionamiento
        if (isTimerRunning)
        {
            // Restar el tiempo en cada actualización del marco
            duration -= Time.deltaTime;

            // Verificar si el temporizador ha terminado
            if (duration <= 0f)
            {
                // Temporizador completado, matar al jugador
                KillPlayer();
            }
        }
    }

    private void StartTimer()
    {
        // Iniciar el temporizador
        isTimerRunning = true;
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}

