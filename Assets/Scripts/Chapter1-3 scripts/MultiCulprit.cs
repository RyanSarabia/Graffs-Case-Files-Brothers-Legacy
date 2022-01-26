using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCulprit : MonoBehaviour
{
    private static MultiCulprit instance;
    public static MultiCulprit GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);
    }

    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 movement;
    [SerializeField] private List<Culprit> culprits = new List<Culprit>();
    [SerializeField] private List<Arch3Node> nodeCheckPoints = new List<Arch3Node>();
    [SerializeField] private Animator anim;
    [SerializeField] private List<string> animationName = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public void changeInit(Arch3Node node)
    {
        for (int i = 0; i < nodeCheckPoints.Count; i++)
        {
            if (node == nodeCheckPoints[i])
            {
                Arch3Manager.GetInstance().getCulprit().setSquare(false);
                Arch3Manager.GetInstance().setCulprit(culprits[i]);
                Arch3Manager.GetInstance().getCulprit().setSquare(true);
                anim.Play(animationName[i]);
                Arch3Manager.GetInstance().setStepCount(0);
            }
        }
    }

}
