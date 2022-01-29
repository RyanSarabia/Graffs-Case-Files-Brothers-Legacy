using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialGameManager : MonoBehaviour
{
    private static DialGameManager instance;
    public static DialGameManager GetInstance()
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

    [SerializeField] private List<GameObject> dialLights = new List<GameObject>();
    private int nLights;
    [SerializeField] private List<int> targetLights = new List<int>();

    private int currLight = 0;

    [SerializeField] private GameObject victoryCard;    
    [SerializeField] private GameObject clickBlocker;
    
    // Start is called before the first frame update
    void Start()
    {
        nLights = dialLights.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lightUpN(int num)
    {
        currLight = (currLight + num) % nLights;

        if (!dialLights[currLight].activeSelf)
        {
            dialLights[currLight].SetActive(true);
        }
        else
        {
            dialLights[currLight].SetActive(false);
        }
            
        checkFinal();
    }

    public void checkFinal()
    {
        int targetCount = targetLights.Count;
        int count = 0;        
        
        foreach(var num in targetLights)
        {
            if (dialLights[num].activeSelf)
            {
                count++;
            }
        }

        if(count == targetCount)
        {
            int litUp = 0;

            foreach (var light in dialLights)
            {
                if (light.activeSelf)
                {
                    litUp++;
                }
            }

            if (litUp == targetCount)
            {
                victoryCard.SetActive(true);
                
                clickBlocker.gameObject.SetActive(true);
            }
        }
    }
}
