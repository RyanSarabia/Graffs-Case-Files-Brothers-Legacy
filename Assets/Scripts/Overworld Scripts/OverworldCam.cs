using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCam : MonoBehaviour
{
    private float boundTop = 5;
    private float boundBot = -5;
    private float boundLeft = -8.85f;
    private float boundRight = 8.85f;

    [SerializeField] private Vector3 maxVector;
    [SerializeField] private Vector3 minVector;

    [SerializeField] Camera mainCamera;
    Vector3 movement;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * 0.2f;
        movement.y = Input.GetAxisRaw("Vertical") * 0.2f;

    }
    private void FixedUpdate()
    {
        newPosition = mainCamera.transform.position + movement;

        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, minVector.x, maxVector.x),
            Mathf.Clamp(newPosition.y, minVector.y, maxVector.y),
            Mathf.Clamp(newPosition.z, minVector.z, maxVector.z)
            );

        mainCamera.transform.position = newPosition;
    }
}
