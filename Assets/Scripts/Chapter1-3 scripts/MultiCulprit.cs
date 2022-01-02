using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCulprit : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 movement;
    [SerializeField] private List<Culprit> culprits = new List<Culprit>();
    [SerializeField] private List<Arch3Node> nodeCheckPoints = new List<Arch3Node>();
    [SerializeField] private Animator anim;
    [SerializeField] private List<string> animation = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < nodeCheckPoints.Count; i++)
        {
            if(Arch3Manager.GetInstance().getCurNode() == nodeCheckPoints[i])
            {
                Arch3Manager.GetInstance().setCulprit(culprits[i]);
                anim.Play(animation[i]);
                Arch3Manager.GetInstance().setStepCount(0);
            }
        }
    }
       
}
