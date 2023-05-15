using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    private LifeController lifeController;

    //public TextMeshProUGUI timerText;
    //public Timer timer;

    void Start()
    {
        lifeController = FindObjectOfType<LifeController>();
        //timer = GetComponent<Timer>();

        if (lifeController != null)
        {
            lifeController.OnLifeChanged += UpdateLivesText;
        }

        UpdateLivesText(lifeController != null ? lifeController.Life : 0);
    }

    void UpdateLivesText(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
        //UpdateTimerUI();
    }

    void OnDestroy()
    {
        if (lifeController != null)
        {
            lifeController.OnLifeChanged -= UpdateLivesText;
        }
    }
    //private void UpdateTimerUI()
    //{
    //    // Convertir el tiempo restante en segundos a un formato legible
    //    string timeRemaining = Mathf.Max(0f, timer.duration).ToString("00.00");

    //    // Actualizar el texto en la UI
    //    timerText.text = "Time: " + timeRemaining;
    //}
}


