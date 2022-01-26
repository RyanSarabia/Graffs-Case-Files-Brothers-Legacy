using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldIcons : MonoBehaviour
{
    public static readonly string VECTOR_X = "VECTOR_X";
    public static readonly string VECTOR_Y = "VECTOR_Y";
    public static readonly string VECTOR_Z = "VECTOR_Z";
    public static readonly string LOC_NAME = "LOC_NAME";
    public static readonly string DESC = "DESC";
    public static readonly string SCENE = "SCENE";

    [SerializeField] private string locName;
    [SerializeField] private string description;
    [SerializeField] private string sceneName;

    [SerializeField] private bool mainIconEnabled = true;
    [SerializeField] private bool exclamationEnabled = true;
    [SerializeField] private UnityEngine.UI.RawImage mainIcon;
    [SerializeField] private UnityEngine.UI.RawImage nodeBase;
    [SerializeField] private UnityEngine.UI.RawImage exclamationPoint;
    [SerializeField] private int id;

    private new BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        //if (!mainIconEnabled)
        //    mainIcon.gameObject.SetActive(false);
        //if (!exclamationEnabled)
        //    exclamationPoint.gameObject.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        nodeBase.color = Color.cyan;
    }
    private void OnMouseExit()
    {
        nodeBase.color = Color.white;
    }

    private void OnMouseDown()
    {
        //SceneLoader.GetInstance().loadScene(id);
        Parameters par = new Parameters();
        par.PutExtra(VECTOR_X, this.gameObject.transform.position.x);
        par.PutExtra(VECTOR_Y, this.gameObject.transform.position.y);
        par.PutExtra(LOC_NAME, locName);
        par.PutExtra(DESC, description);
        par.PutExtra(SCENE, sceneName);
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.OVERWORLD_NODE_CLICKED, par);
    }

    public void exclamationState(bool state)
    {
        exclamationPoint.gameObject.SetActive(state);
    }

    public void setCollider(bool state)
    {
        collider.enabled = state;
    }
}
