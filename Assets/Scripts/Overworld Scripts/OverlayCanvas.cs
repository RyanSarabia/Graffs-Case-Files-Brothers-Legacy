using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlayCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI locName;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private Button goBtn;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        this.goBtn.onClick.AddListener(handleBtnClick);

        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.OVERWORLD_NODE_CLICKED, setModalContent);
    }

    public void setModalContent(Parameters par)
    {
        locName.SetText("Chapter " + par.GetStringExtra(OverworldIcons.LOC_NAME, "X-X: Location"));
        desc.SetText(par.GetStringExtra(OverworldIcons.DESC, "description not found"));

        //set button params here
    }

    private void handleBtnClick()
    {
        //set button params in setModalContent()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
