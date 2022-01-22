using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCam : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject overlayCanvas;

    //private float zoomBoundTop = 4.99;
    //private float zoomBoundRight = 8.85f;
    //private float unZoomBoundTop = 6.99f;
    //private float unZoomBoundRight = 12.42f;

    [SerializeField] private Vector3 ZoomMaxVector;
    [SerializeField] private Vector3 ZoomMinVector;
    [SerializeField] private Vector3 UnzoomMaxVector;
    [SerializeField] private Vector3 UnzoomMinVector;
    [SerializeField] private Vector3 CurMaxBounds;
    [SerializeField] private Vector3 CurMinBounds;

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

        if(isZoomedIn && !isTargeting && (movement.x != 0 || movement.y != 0))
        {
            ToggleTargetOff();
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.OVERWORLD_NODE_CLICKED);
    }

    private void FixedUpdate()
    {
        HandleZoom();
        if (isTargeting)
        {
            HandleTarget();
        }
        else
        {
            HandleMovement();
        }
        updateLerper();
    }

    private void ToggleTargetOn(Parameters par)
    {
        if (!isZoomedIn && !shouldLerp)
        {
            overlayCanvas.SetActive(true);
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
    }
    public void ToggleTargetOff()
    {
        if (isZoomedIn && !shouldLerp)
        {
            overlayCanvas.SetActive(false);
            isTargeting = false;
            isZoomedIn = false;
            lerpPercent = 0.0f;
            shouldLerp = true;
        }
    }

    private void HandleTarget()
    {
        mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, lerpPercent); 
    }

    private void HandleMovement()
    {
        Vector3 newPosition = mainCamera.transform.position + movement;

        //if (isZoomedIn)
        //{
        //    newPosition = new Vector3(
        //        Mathf.Clamp(newPosition.x, ZoomMinVector.x, ZoomMaxVector.x),
        //        Mathf.Clamp(newPosition.y, ZoomMinVector.y, ZoomMaxVector.y),
        //        Mathf.Clamp(newPosition.z, ZoomMinVector.z, ZoomMaxVector.z)
        //        );
        //} else
        //{
        //    newPosition = new Vector3(
        //        Mathf.Clamp(newPosition.x, UnzoomMinVector.x, UnzoomMaxVector.x),
        //        Mathf.Clamp(newPosition.y, UnzoomMinVector.y, UnzoomMaxVector.y),
        //        Mathf.Clamp(newPosition.z, UnzoomMinVector.z, UnzoomMaxVector.z)
        //        );
        //}

        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, CurMinBounds.x, CurMaxBounds.x),
            Mathf.Clamp(newPosition.y, CurMinBounds.y, CurMaxBounds.y),
            Mathf.Clamp(newPosition.z, CurMinBounds.z, CurMaxBounds.z)
            );

        mainCamera.transform.position = newPosition;
    }

    private void HandleZoom()
    {
        if (shouldLerp)
        {
            if (isTargeting)
            {
                CurMaxBounds = new Vector3(
                    Mathf.Lerp(UnzoomMaxVector.x, ZoomMaxVector.x, lerpPercent),
                    Mathf.Lerp(UnzoomMaxVector.y, ZoomMaxVector.y, lerpPercent),
                    Mathf.Lerp(UnzoomMaxVector.z, ZoomMaxVector.z, lerpPercent)
                );
                CurMinBounds = new Vector3(
                    Mathf.Lerp(UnzoomMinVector.x, ZoomMinVector.x, lerpPercent),
                    Mathf.Lerp(UnzoomMinVector.y, ZoomMinVector.y, lerpPercent),
                    Mathf.Lerp(UnzoomMinVector.z, ZoomMinVector.z, lerpPercent)
                );
                mainCamera.orthographicSize = Mathf.Lerp(defaultZoom, zoomInVal, lerpPercent);
            }
            else
            {
                CurMaxBounds = new Vector3(
                    Mathf.Lerp(ZoomMaxVector.x, UnzoomMaxVector.x, lerpPercent),
                    Mathf.Lerp(ZoomMaxVector.y, UnzoomMaxVector.y, lerpPercent),
                    Mathf.Lerp(ZoomMaxVector.z, UnzoomMaxVector.z, lerpPercent)
                );
                CurMinBounds = new Vector3(
                    Mathf.Lerp(ZoomMinVector.x, UnzoomMinVector.x, lerpPercent),
                    Mathf.Lerp(ZoomMinVector.y, UnzoomMinVector.y, lerpPercent),
                    Mathf.Lerp(ZoomMinVector.z, UnzoomMinVector.z, lerpPercent)
                );
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
