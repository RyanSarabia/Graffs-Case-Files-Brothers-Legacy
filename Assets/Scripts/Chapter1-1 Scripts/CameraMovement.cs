using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    Vector3 movement;
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
        mainCamera.transform.position += movement;

    }
}
