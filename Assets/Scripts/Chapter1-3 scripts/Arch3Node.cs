using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch3Node : MonoBehaviour
{
    [SerializeField] List<Arch3Edge> edges = new List<Arch3Edge>();
    //[SerializeField] bool locked = true;

    public void revealEdges()
    {
        // loop call edge.reveal
        foreach (var edge in edges)
        {
            edge.reveal();
        }
    }

    public void unlock()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void addEdge(Arch3Edge newEdge)
    {
        edges.Add(newEdge);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
