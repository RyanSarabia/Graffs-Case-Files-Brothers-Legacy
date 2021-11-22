using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch3Node : MonoBehaviour
{
    [SerializeField] List<Arch3Edge> edges = new List<Arch3Edge>();
    private Dictionary<Arch3Node, int> neighbors = new Dictionary<Arch3Node, int>();
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
        this.GetComponent<SpriteRenderer>().color = Color.cyan;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
    public void disableClick()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void addEdge(Arch3Edge newEdge)
    {
        edges.Add(newEdge);
    }

    public List<Arch3Edge> getEdge()
    {
        return edges;
    }

    public Dictionary<Arch3Node, int> getNeighbors()
    {       
        return neighbors;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_LOCKNODES, this.disableClick);
        foreach (var edge in edges)
        {
            neighbors.Add(edge.getNeighbor(this), edge.GetWeight());
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH3_LOCKNODES);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
