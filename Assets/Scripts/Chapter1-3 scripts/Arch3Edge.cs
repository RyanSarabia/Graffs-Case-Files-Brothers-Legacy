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

    public void reveal()
    {
        // set textfield active
        this.weightText.gameObject.SetActive(true);
        nodeA.unlock();
        nodeB.unlock();
    }

    private void Awake()
    {
        nodeA.addEdge(this);
        nodeB.addEdge(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.weightText.SetText(weight.ToString());
        this.weightText.gameObject.SetActive(false);
        //nodeA.addEdge(this);
        //nodeB.addEdge(this);
    }

    public Arch3Node getNeighbor(Arch3Node source)
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

    public int GetWeight()
    {
        return weight;
    }
}
