using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culprit : MonoBehaviour
{
    [SerializeField] Arch3Node source;
    [SerializeField] Arch3Node goal;
    [SerializeField] Arch3Edge path;
    [SerializeField] int stepsTaken;
    // [SerializedField] Slider maybeslider;

    // Start is called before the first frame update
    void Start()
    {
        stepsTaken = 0;
    }

    public void move(int steps)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
