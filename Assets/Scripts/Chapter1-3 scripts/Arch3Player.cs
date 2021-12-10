using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch3Player : MonoBehaviour
{
    [SerializeField] private Arch3Node startNode;
    [SerializeField] private Arch3Node curNode;
    private Arch3Node nextNode;
    private Animator animator;

    private bool shouldMove = false;
    private float lerpPercent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        reset();
    }

    void reset()
    {
        curNode = startNode;
        // move character sprite back
    }
    public void move(int steps, Arch3Node next)
    {
        nextNode = next;
        lerpPercent = 0.0f;
        animator.Play("MainCharacter_Running");
        shouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // move position
            lerpPercent += 0.01f;
            Vector2 cur = this.curNode.gameObject.transform.position;
            Vector2 next = this.nextNode.gameObject.transform.position;

            cur.y += 0.3f;
            next.y += 0.3f;

            transform.position = Vector2.Lerp(cur, next, lerpPercent);

            //stop if reached distance
            if (lerpPercent >= 1)
            {
                animator.Play("Idle");
                this.curNode = this.nextNode;
                shouldMove = false;
            }
        }
    }
}
