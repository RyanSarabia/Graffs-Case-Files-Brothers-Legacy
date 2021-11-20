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

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    void reset()
    {
        stepsTaken = 0;
        stepsToTake = 0;
        shouldMove = false;
        lerpPercent = 0.0f;
        endVal = (float)path.GetWeight();
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
    }

    public bool hasEscaped()
    {
        return stepsTaken == endVal;
    } 
}
