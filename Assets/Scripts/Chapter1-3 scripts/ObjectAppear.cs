using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppear : MonoBehaviour
{
    private static ObjectAppear instance;
    public static ObjectAppear GetInstance()
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
   
    [SerializeField] private List<Crosshair> objects = new List<Crosshair>();
    private List<Crosshair> toChange = new List<Crosshair>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void objectAppear(int turnChange, int curTurn, int i)
    {           
        if (turnChange - 1 == curTurn)
        {           
            objects[i].setOn(true);
            toChange.Add(objects[i]);            
        }

        foreach (var obj in toChange)
        {
            obj.turnUpdate(curTurn);           
        }
    }
}
