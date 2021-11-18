using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Arch3Edge : MonoBehaviour
{
    [SerializeField] Arch3Node nodeA;
    [SerializeField] Arch3Node nodeB;
    [SerializeField] int weight;
    [SerializeField] TextMeshProUGUI weightText;

    void reveal()
    {
        // set textfield active
        this.weightText.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.weightText.SetText(weight.ToString());
        this.weightText.enabled = false;
    }

    Arch3Node getNeighbor(Arch3Node source)
    {
        if (ReferenceEquals(source, nodeA))
        {
            return nodeB;
        }
        else
        {
            return nodeA;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
