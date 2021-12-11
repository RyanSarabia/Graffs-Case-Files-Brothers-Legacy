using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCulprit : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 movement;
    [SerializeField] private List<Culprit> culprits = new List<Culprit>();
    [SerializeField] private List<Arch3Node> nodeCheckPoints = new List<Arch3Node>();
    [SerializeField] private List<Animator> anim = new List<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < nodeCheckPoints.Count; i++)
        {
            if(Arch3Manager.GetInstance().getCurNode() == nodeCheckPoints[i])
            {
                Arch3Manager.GetInstance().setCulprit(culprits[i]);
                anim[i].Play("2-3 Cam to the right");
                Arch3Manager.GetInstance().setStepCount(0);
            }
        }
    }
       
}
