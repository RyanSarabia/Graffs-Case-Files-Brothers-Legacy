using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingChange : MonoBehaviour
{
    private static RepeatingChange instance;
    public static RepeatingChange GetInstance()
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

    [SerializeField] private int startingTurn;
    [SerializeField] private int oddChange;
    [SerializeField] private int evenChange;
    [SerializeField] private List<Arch3Edge> oddEdges = new List<Arch3Edge>();
    [SerializeField] private List<Arch3Edge> evenEdges = new List<Arch3Edge>();

    [SerializeField] private bool ch2;
    [SerializeField] private GameObject color1;
    [SerializeField] private GameObject color2;
    [SerializeField] private List<GameObject> firstHalf = new List<GameObject>();
    [SerializeField] private List<GameObject> secondHalf = new List<GameObject>();
    private Color c1;
    private Color c2;


    // Start is called before the first frame update
    void Start()
    {
        c1 = color1.GetComponent<SpriteRenderer>().color;
        c2 = color2.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeInit(int turn)
    {
        if(turn >= startingTurn)
        {
            foreach(var edge in oddEdges)
            {
                if(turn % 2 == 0)
                    edge.addWeight(oddChange);
                else
                    edge.addWeight(evenChange);
            }
            foreach (var edge in evenEdges)
            {
                if (turn % 2 == 0)
                    edge.addWeight(evenChange);
                else
                    edge.addWeight(oddChange);
            }

            if (ch2)
                colorSwap(turn);
        }
    }

    public void colorSwap(int turn)
    {
        foreach (var edge in firstHalf)
        {
            if (turn % 2 == 0)
                edge.GetComponent<SpriteRenderer>().color = c1;
            else
                edge.GetComponent<SpriteRenderer>().color = c2;
        }
        foreach (var edge in secondHalf)
        {
            if (turn % 2 == 0)
                edge.GetComponent<SpriteRenderer>().color = c2;
            else
                edge.GetComponent<SpriteRenderer>().color = c1;
        }
    }
}
