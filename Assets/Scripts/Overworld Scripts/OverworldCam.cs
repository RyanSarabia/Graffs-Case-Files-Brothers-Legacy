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
    [SerializeField] private Vector3 Unzoom2MaxVector;
    [SerializeField] private Vector3 Unzoom2MinVector;
    [SerializeField] private Vector3 CurMaxUnzoom;
    [SerializeField] private Vector3 CurMinUnzoom;
    [SerializeField] private Vector3 CurMaxBounds;
    [SerializeField] private Vector3 CurMinBounds;

    //[SerializeField] private float zoomSpeed = 2f;

    [SerializeField] Vector3 movement;

    private bool isTargeting = false;
    private bool isChangingZoom = false;

    private static float defaultZoom = 5;
    private static float fullscreen = 10;
    private float zoomInVal = 3; // used for zooming in
    private float curZoomSize = defaultZoom;
    private bool isZoomedIn = false;
    private bool isDefaultZoom = true;

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

    public void SetZoomToDefault(bool shouldDefault)
    {
        if (shouldDefault)
        {
            isDefaultZoom = true;
            curZoomSize = defaultZoom;
            CurMaxUnzoom = UnzoomMaxVector;
            CurMinUnzoom = UnzoomMinVector;
        } else
        {
            isDefaultZoom = false;
            curZoomSize = fullscreen;
            CurMaxUnzoom = Unzoom2MaxVector;
            CurMinUnzoom = Unzoom2MinVector;
        }

        if (!isZoomedIn)
        {
            isChangingZoom = true;
            lerpPercent = 0.0f;
            shouldLerp = true;
        }
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
            Vector3 curMaxVecA;
            Vector3 curMaxVecB;
            Vector3 curMinVecA;
            Vector3 curMinVecB;
            float zoomSizeA;
            float zoomSizeB;

            if (isTargeting)
            {
                curMaxVecA = CurMaxUnzoom;
                curMaxVecB = ZoomMaxVector;
                curMinVecA = CurMinUnzoom;
                curMinVecB = ZoomMinVector;
                zoomSizeA = curZoomSize;
                zoomSizeB = zoomInVal;
            }
            else if (isChangingZoom)
            {
                if (isDefaultZoom)
                {
                    curMaxVecA = Unzoom2MaxVector;
                    curMaxVecB = CurMaxUnzoom;
                    curMinVecA = Unzoom2MinVector;
                    curMinVecB = CurMinUnzoom;
                    zoomSizeA = fullscreen;
                    zoomSizeB = curZoomSize;
                }
                else
                {
                    curMaxVecA = UnzoomMaxVector;
                    curMaxVecB = CurMaxUnzoom;
                    curMinVecA = UnzoomMinVector;
                    curMinVecB = CurMaxUnzoom;
                    zoomSizeA = defaultZoom;
                    zoomSizeB = curZoomSize;
                }
            }
            else
            {
                curMaxVecA = ZoomMaxVector;
                curMaxVecB = CurMaxUnzoom;
                curMinVecA = ZoomMinVector;
                curMinVecB = CurMinUnzoom;
                zoomSizeA = zoomInVal;
                zoomSizeB = curZoomSize;
            }

            CurMaxBounds = new Vector3(
                Mathf.Lerp(curMaxVecA.x, curMaxVecB.x, lerpPercent),
                Mathf.Lerp(curMaxVecA.y, curMaxVecB.y, lerpPercent),
                Mathf.Lerp(curMaxVecA.z, curMaxVecB.z, lerpPercent)
            );
            CurMinBounds = new Vector3(
                Mathf.Lerp(curMinVecA.x, curMinVecB.x, lerpPercent),
                Mathf.Lerp(curMinVecA.y, curMinVecB.y, lerpPercent),
                Mathf.Lerp(curMinVecA.z, curMinVecB.z, lerpPercent)
            );
            mainCamera.orthographicSize = Mathf.Lerp(zoomSizeA, zoomSizeB, lerpPercent);
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
                isChangingZoom = false;
            } else
            {
                lerpPercent += 0.025f;
            }
        }
    }
}
