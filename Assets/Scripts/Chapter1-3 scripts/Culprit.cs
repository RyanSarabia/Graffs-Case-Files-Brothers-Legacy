using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culprit : MonoBehaviour
{
    [SerializeField] Arch3Node source;
    [SerializeField] Arch3Node goal;
    [SerializeField] Arch3Edge path;
    [SerializeField] GameObject square;

    [SerializeField] private bool shouldAnimate;

    private int stepsTaken;
    private int stepsToTake;
    private bool shouldMove = false;
    private float lerpPercent = 0.0f;
    private float endVal;
    bool hasCalledRetry = false;
    Animator animator;

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
            Arch3Manager.GetInstance().setWait(true);
        }

        if (shouldAnimate)
        {
            if (animator = this.GetComponent<Animator>())
            {
                animator.Play("CulpritMove");
            }
        }
    }

    public void setSquare(bool state)
    {
        square.SetActive(state);
    }

    // Update is called once per frame  
    void FixedUpdate()
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
                Arch3Manager.GetInstance().setWait(false);
                if (animator = this.GetComponent<Animator>())
                {
                    this.GetComponent<Animator>().Play("Idle");
                }
            }
           
           
        }
        

        if (hasEscaped() && hasCalledRetry == false)
        {
            Arch3Manager.GetInstance().retry();
            hasCalledRetry = true;
        }
            
    }

    public bool hasEscaped()
    {
        return stepsTaken == endVal;
    } 
}
