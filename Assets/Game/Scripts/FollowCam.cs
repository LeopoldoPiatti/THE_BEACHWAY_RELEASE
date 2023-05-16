using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target; // The object that the camera should follow
    public float smoothing = 5f; // The speed with which the camera should follow the target
    public Vector3 offset; // The distance between the camera and the target

    private void Start()
    {
        // Calculate the initial offset
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // Calculate the target position
        Vector3 targetCamPos = target.position + offset;
        // Limit the target position along the y-axis
        targetCamPos.y = transform.position.y;
        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}