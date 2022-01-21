using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCam : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    //private float zoomBoundTop = 5;
    //private float zoomBoundRight = 8.85f;
    //private float unZoomBoundTop = 6.99f;
    //private float unZoomBoundRight = 12.42f;

    [SerializeField] private Vector3 ZoomMaxVector;
    [SerializeField] private Vector3 ZoomMinVector;
    [SerializeField] private Vector3 UnzoomMaxVector;
    [SerializeField] private Vector3 UnzoomMinVector;
    [SerializeField] private float zoomSpeed = 2f;

    [SerializeField] Vector3 movement;

    private bool isTargeting = false;

    private static float defaultZoom = 5;
    private float zoomInVal = 3; // used for zooming in
    private bool isZoomedIn = false;

    private float lerpPercent = 0.0f;
    private bool shouldLerp = false;

    private Vector3 startPos;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //isTargeting = true;
        //shouldLerp = true;
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.OVERWORLD_NODE_CLICKED, ToggleTargetOn);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * 0.2f;
        movement.y = Input.GetAxisRaw("Vertical") * 0.2f;
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.OVERWORLD_NODE_CLICKED);
    }

    private void FixedUpdate()
    {
        if (isTargeting)
        {
            HandleTarget();
        }
        else
        {
            HandleMovement();
        }
        HandleZoom();

        updateLerper();
    }

    private void ToggleTargetOn(Parameters par)
    {
        startPos = new Vector3(
               mainCamera.transform.position.x,
               mainCamera.transform.position.y,
               mainCamera.transform.position.z
               );

        targetPos = new Vector3(
               Mathf.Clamp(par.GetFloatExtra(OverworldIcons.VECTOR_X, 0.0f) + 2.5f, ZoomMinVector.x, ZoomMaxVector.x),
               Mathf.Clamp(par.GetFloatExtra(OverworldIcons.VECTOR_Y, 0.0f), ZoomMinVector.y, ZoomMaxVector.y),
               -10f
               );

        isTargeting = true;
        isZoomedIn = true;
        lerpPercent = 0.0f;
        shouldLerp = true;
    }
    public void ToggleTargetOff()
    {
        isTargeting = false;
        isZoomedIn = false;
        lerpPercent = 0.0f;
        shouldLerp = true;
    }

    private void HandleTarget()
    {
        mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, lerpPercent); 
    }

    private void HandleMovement()
    {
        Vector3 newPosition = mainCamera.transform.position + movement;

        if (isZoomedIn)
        {
            newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, ZoomMinVector.x, ZoomMaxVector.x),
                Mathf.Clamp(newPosition.y, ZoomMinVector.y, ZoomMaxVector.y),
                Mathf.Clamp(newPosition.z, ZoomMinVector.z, ZoomMaxVector.z)
                );
        } else
        {
            newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, UnzoomMinVector.x, UnzoomMaxVector.x),
                Mathf.Clamp(newPosition.y, UnzoomMinVector.y, UnzoomMaxVector.y),
                Mathf.Clamp(newPosition.z, UnzoomMinVector.z, UnzoomMaxVector.z)
                );
        }

        mainCamera.transform.position = newPosition;
    }

    private void HandleZoom()
    {
        if (shouldLerp)
        {
            if (isTargeting)
            {
                mainCamera.orthographicSize = Mathf.Lerp(defaultZoom, zoomInVal, lerpPercent);
            }
            else
            {
                mainCamera.orthographicSize = Mathf.Lerp(zoomInVal, defaultZoom, lerpPercent);
            }
        }
    }

    private void updateLerper()
    {
        if (shouldLerp)
        {
            if (lerpPercent >=1)
            {
                shouldLerp = false;
                isTargeting = false;
            } else
            {
                lerpPercent += 0.025f;
            }
        }
    }
}
