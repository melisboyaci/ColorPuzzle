using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<GameObject> SolutionList = new List<GameObject>();
    public List<GameObject> MainList = new List<GameObject>();
    public int trueBlockCount = 0;
    public GameObject panel;
    public static int levelCount = 0;


    void Start()
    {
       
        GameObject solutionObject = GameObject.Find("SolutionBlocks");

        if (solutionObject != null)
        {
            foreach (Transform child in solutionObject.transform)
            {
                SolutionList.Add(child.gameObject);
            }
        }
        panel.SetActive(false);
       
       
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    
    
    public bool IsPuzzledSolved()
    {
        trueBlockCount = 0;
        MainList.Clear();

        GameObject mainObject = GameObject.Find("MainBlocks");


        if (mainObject != null)
        {
            foreach (Transform child in mainObject.transform)
            {
                MainList.Add(child.gameObject);
            }
        }



        for (int j = 0; j < SolutionList.Count; j++)
        {
            if (MainList[j].GetComponent<Renderer>().material.color != SolutionList[j].GetComponent<Renderer>().material.color)
            {
                return false;
            }
        }
        
        GameManager.Instance.lineRenderer.enabled = false;
        return true;
    }

   

}
