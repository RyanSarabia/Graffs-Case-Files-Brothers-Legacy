using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentStateHolder : MonoBehaviour
{

    [SerializeField] Pitcher p1Object;
    [SerializeField] Pitcher p2Object;
    [SerializeField] Pitcher p3Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurState(int p1, int p2, int p3)
    {
        p1Object.setWater(p1);
        p2Object.setWater(p2);
        p3Object.setWater(p3);

    }
}
