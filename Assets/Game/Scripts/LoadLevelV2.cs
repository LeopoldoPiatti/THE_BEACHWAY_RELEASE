using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelV2 : MonoBehaviour
{
    //public GameObject objectToActivate;
    //public GameObject objectToDeactivate;

    public Transform newPosition;

    public string sceneName;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Salir()
    {
        Debug.Log("He Salido");
        Application.Quit();
    }
    public void setactive(GameObject canvasactive)
    {
        canvasactive.SetActive(true);
    }

    public void setdesactive(GameObject canvasdesactive)
    {
        canvasdesactive.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Set the time scale back to 1 to resume the game
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //objectToActivate.SetActive(true);
            //objectToDeactivate.SetActive(false);
            SceneManager.LoadScene(sceneName);
            // Cambiar la posición del jugador en los ejes X e Y
            other.transform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
        }

}
}

