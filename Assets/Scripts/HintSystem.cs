using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HintSystem : MonoBehaviour
{
    private static HintSystem instance;
    public static HintSystem GetInstance()
    {
        return instance;
    }

    [SerializeField] private int hintLvl;
    [SerializeField] private Button hintBtn;
    [SerializeField] private Sprite hintBtnDefault;
    [SerializeField] private Sprite hintBtnAlt;
    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject doneBtn;

    [SerializeField] private List<Sprite> pics = new List<Sprite>();
    [SerializeField] private List<string> texts = new List<string>();

    private bool newHint = false;
    private string hintString;
    private string newHintString;

    private int curIdx = 0; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);

        hintString = SceneManager.GetActiveScene().name + "hints";
        newHintString = SceneManager.GetActiveScene().name + "newHint";

        hintLvl = PlayerPrefs.GetInt(hintString);
        int newHintInt = PlayerPrefs.GetInt(newHintString);
        newHint = newHintInt == 1 ? true : false;
    }    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        if (newHint)
        {
            triggerNewHint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void triggerNewHint()
    {
        if(hintLvl == 1)
        {
            hintBtn.gameObject.SetActive(true);
        }
        hintBtn.GetComponent<Image>().sprite = hintBtnAlt;

    }

    public void newHintOpened()
    {
        PlayerPrefs.SetInt(newHintString, 0);
        newHint = false;
        hintBtn.GetComponent<Image>().sprite = hintBtnDefault;
    }

    public void hintUp()
    {
        Debug.Log("Hint Level: "+ hintLvl);
        PlayerPrefs.SetInt(hintString, PlayerPrefs.GetInt(hintString) + 1);
        if (PlayerPrefs.GetInt(hintString) + 1 <= 3)
        {
            PlayerPrefs.SetInt(newHintString, 1);
        }
    }

    public int getHintLvl()
    {
        return hintLvl;
    }    
    public void resetHintLvl()
    {
        PlayerPrefs.SetInt(hintString, 0);
    }


    private void changeContent(int idx)
    {
        if (pics.Count > 0 && texts.Count > 0)
        {
            img.sprite = pics[idx];
            textBox.text = texts[idx];

            if (idx == hintLvl -1)
            {
                nextBtn.SetActive(false);
                doneBtn.SetActive(true);
            }
            else
            {
                nextBtn.SetActive(true);
                doneBtn.SetActive(false);
            }

            if (idx == 0)
            {
                prevBtn.SetActive(false);
            }
            else
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
