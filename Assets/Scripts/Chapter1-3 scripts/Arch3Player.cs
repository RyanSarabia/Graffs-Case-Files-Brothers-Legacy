using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch3Player : MonoBehaviour
{
    [SerializeField] Arch3Node startNode;
    [SerializeField] Arch3Node curNode;

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    void reset()
    {
        curNode = startNode;
        // move character sprite back
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
