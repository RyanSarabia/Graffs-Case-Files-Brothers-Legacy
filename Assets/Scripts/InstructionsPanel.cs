using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsPanel : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject prevNextBox;
    [SerializeField] private GameObject doneBtn;

    [SerializeField] private List<Sprite> pics = new List<Sprite>();
    [SerializeField] private List<string> texts = new List<string>();

    private int curIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        changeContent(curIdx);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void changeContent(int idx)
    {
        img.sprite = pics[idx];
        textBox.text = texts[idx];

        if (idx == pics.Count - 1)
        {
            prevBtn.SetActive(false);
            prevNextBox.SetActive(false);
            doneBtn.SetActive(true);
        } else
        {
            doneBtn.SetActive(false);
            prevNextBox.SetActive(true);
            if (idx == 0)
            {
                prevBtn.SetActive(false);
            } else
            {
                prevBtn.SetActive(true);
            }
        }
    }

    public void next()
    {
        curIdx++;
        changeContent(curIdx);
    }
    public void prev()
    {
        curIdx--;
        changeContent(curIdx);
    }
    public void done()
    {
        curIdx = 0;
        changeContent(curIdx);
    }
}
