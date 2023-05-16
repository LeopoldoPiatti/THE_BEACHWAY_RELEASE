using UnityEngine;

public class FixYPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Mantener la posici�n en el eje Y fija
        transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
    }
}
