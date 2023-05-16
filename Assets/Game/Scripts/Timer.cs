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
        float timeRemaining = duration - (Time.time - startTime);
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        tiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Verificar si el temporizador está en funcionamiento
        if (isTimerRunning)
        {
            // Verificar si el temporizador ha terminado
            if (timeRemaining <= 0f)
            {
                // Temporizador completado, matar al jugador
                KillPlayer();
            }
        }
    }

    private void StartTimer()
    {
        // Iniciar el temporizador
        startTime = Time.time;
        isTimerRunning = true;
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}


