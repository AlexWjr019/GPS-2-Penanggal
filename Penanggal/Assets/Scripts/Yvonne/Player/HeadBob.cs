using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Transform References")]
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head Bobbing")]
    public float bobFrequency;
    public float bobHorizonalAmplitude;
    public float bobVerticalAmplitude;
    [Range(0, 1)] public float headBobSmoothing;

    public bool isWalking;
    private float walkingTime;
    private Vector3 targetCameraPosition;

    void Start()
    {
        
    }

    private void Update()
    {
        if(!isWalking)
        {
            walkingTime = 0.0f;
        }
        else
        {
            walkingTime += Time.deltaTime;
        }

        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);

        if((cameraTransform.position - targetCameraPosition).magnitude <= 0.001)
        {
            cameraTransform.position = targetCameraPosition;
        }
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0.0f;
        float verticalOffset = 0.0f;
        Vector3 offset = Vector3.zero;

        if(t > 0)
        {
            // calculate offsets
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizonalAmplitude;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;

            // combine offsets relative to the head's position and calculate the camera's target position
            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }
        return offset;
    }
}
