using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentStateBridge : MonoBehaviour
{
    [SerializeField] private NPC child_1;
    [SerializeField] private NPC man_2;
    [SerializeField] private NPC woman_5;
    [SerializeField] private NPC oldie_8;

    private int timeTotal;
    private bool isLanternLeft;
    private List<NPC> leftSide = new List<NPC>();
    private List<NPC> rightSide = new List<NPC>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurState(int timeTotal, bool isLanternLeft, string left, string right)
    {
        leftSide.Clear();
        rightSide.Clear();
        this.timeTotal = timeTotal;
        this.isLanternLeft = isLanternLeft;
        string[] leftChars = left.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
        string[] rightChars = right.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var letter in leftChars)
        {
            switch (letter)
            {
                case "c": leftSide.Add(child_1); break;
                case "m": leftSide.Add(man_2); break;
                case "w": leftSide.Add(woman_5); break;
                case "o": leftSide.Add(oldie_8); break;
            }
        }

        foreach (var letter in rightChars)
        {
            switch (letter)
            {
                case "c": rightSide.Add(child_1); break;
                case "m": rightSide.Add(man_2); break;
                case "w": rightSide.Add(woman_5); break;
                case "o": rightSide.Add(oldie_8); break;
            }
        }

        Debug.Log(leftSide);
        Debug.Log(rightSide);
    }
}
