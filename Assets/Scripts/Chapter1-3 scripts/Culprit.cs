using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culprit : MonoBehaviour
{
    [SerializeField] Arch3Node source;
    [SerializeField] Arch3Node goal;
    [SerializeField] Arch3Edge path;

    private int stepsTaken;
    private int stepsToTake;
    private bool shouldMove = false;
    private float lerpPercent = 0.0f;
    private float endVal;

    private void Awake()
    {
        path.setCulpritPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    public void reset()
    {
        stepsTaken = 0;
        stepsToTake = 0;
        shouldMove = false;
        lerpPercent = 0.0f;
        endVal = (float)path.GetWeight();
        transform.position = new Vector3(source.transform.position.x, source.transform.position.y + 0.3f, 0);
    }

    public void move(int steps)
    {
        if (!hasEscaped())
        {
            stepsToTake += steps;
            shouldMove = true;
        }
    }

    // Update is called once per frame  
    void Update()
    {
        if (shouldMove)
        {
            // move position
            lerpPercent += 0.01f;
            transform.position = Vector2.Lerp(source.gameObject.transform.position, goal.gameObject.transform.position, lerpPercent);

            // check if cur percent is whole number
            float temp = endVal * lerpPercent;
            if (temp >= stepsTaken + 1)
            {
                stepsToTake--;
                stepsTaken++;
            }

             //stop if reached distance
            if (stepsToTake == 0 || hasEscaped())
            {
                shouldMove = false;
            }
        }
        if (hasEscaped())
            Arch3Manager.GetInstance().retry();
    }

    public bool hasEscaped()
    {
        return stepsTaken == endVal;
    } 
}
