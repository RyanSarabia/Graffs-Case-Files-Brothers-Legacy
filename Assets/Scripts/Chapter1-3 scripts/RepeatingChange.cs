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
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
