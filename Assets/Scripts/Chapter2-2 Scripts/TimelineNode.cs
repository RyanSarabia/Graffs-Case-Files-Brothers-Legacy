using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimelineNode : MonoBehaviour
{
    // UI
    [SerializeField] private GameObject curNodeHighlight;
    [SerializeField] private GameObject youAreHere;
    [SerializeField] private GameObject questionMark;

    [SerializeField] private GameObject branchBox;
    [SerializeField] private GameObject branchArrow;
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private TextMeshProUGUI branchText;
    
    [SerializeField] private GameObject nextSpawn;

    // attributes
    [SerializeField] private State_Bridge state;
    [SerializeField] private bool isCurNode = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isCurNode)
        {
            curNodeHighlight.SetActive(true);
            youAreHere.SetActive(true);
            questionMark.SetActive(true);

            branchArrow.SetActive(false);
            branchBox.SetActive(false);
            branchText.gameObject.SetActive(false);
        }
    }

    public void setState(State_Bridge newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
