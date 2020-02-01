using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to monitor cards in preset container. 
/// <summary>
public class CardMonitor : MonoBehaviour
{
    [SerializeField]
    private Button presetBtn;
    [SerializeField]
    private List<GameObject> layerList;
    private GameObject startBtn;
    private GameObject randBtn;
    private GameObject prepBtn;
    private GameObject editBtn;
    private GameObject backBtn;
    private List<GameObject> btnList;
    private GameObject cardContainer;
    private void Awake()
    {
        presetBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonPrep").GetComponent<Button>();
        layerList = new List<GameObject>();
        btnList = new List<GameObject>();
        string btnPath = "Canvas/Background/BottomMask/BottomPanel/";
        startBtn = GameObject.Find(btnPath+"ButtonStart");
        randBtn = GameObject.Find(btnPath + "ButtonRand");
        prepBtn = GameObject.Find(btnPath + "ButtonPrep");
        editBtn = GameObject.Find(btnPath + "ButtonEdit");
        backBtn = GameObject.Find(btnPath + "ButtonQuit");
        btnList.Add(startBtn);
        btnList.Add(randBtn);
        btnList.Add(prepBtn);
        btnList.Add(editBtn);
    }

    private void Start()
    {
        ExtractLayers();
        randBtn.SetActive(false);
        editBtn.SetActive(false);
        layerList[4].SetActive(false);
        BindingEvent();
    }

    private void ExtractLayers()
    {
        GameObject[] layerArr = GameObject.FindGameObjectsWithTag("Configuration");
        foreach (GameObject layer in layerArr)
        {
            layerList.Add(layer);
        }
    }
    private void InitInterface()
    {
        Utilities.stageID = Utilities.PRESET_STAGE;
        layerList[0].SetActive(true);
        layerList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask+"yspz", typeof(Sprite)) as Sprite;
        CreateBlankCards();
    }

    private void CreateBlankCards()
    {
        GameObject mask = layerList[1];
        mask.SetActive(true);
        foreach(GameObject btn in btnList)
        {
            btn.SetActive(!btn.activeSelf);
        }
       

        cardContainer = layerList[4];
        cardContainer.SetActive(true);
        Transform cardPool = cardContainer.transform.GetChild(1);

        //Vector3 bounds = cardContainer.GetComponent<MeshFilter>().mesh.bounds.size;
        //print(bounds.x* cardContainer.transform.localScale.x);
        //print(bounds.y* cardContainer.transform.localScale.y);

        GridLayoutGroup glg = cardPool.GetComponent<GridLayoutGroup>();
        int k = 20;
        glg.padding.left = 2;
        glg.padding.top = 2;
        glg.cellSize = new Vector2(3*k, 4*k);
        glg.spacing = new Vector2(5, 5);
        
        for(int i = 0; i < 32; i++)
        {
            GameObject go = new GameObject();
            go.name = "card "+i;
            go.transform.parent = cardPool;
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_cards+ "blank", typeof(Sprite)) as Sprite;
        }
       

    }

    private void HideAllLayers()
    {
        foreach(GameObject layer in layerList)
        {
            layer.SetActive(false);
        }
    }

    private void DestroyCards()
    {
        GameObject cardPool = cardContainer.transform.Find("CardPool").gameObject;
        //print(cardPool.name);
        Transform[] cards = cardPool.GetComponentsInChildren<Transform>();
        for(int i = 1; i < cards.Length; i ++)
        {
            //print(card.name);
            Destroy(cards[i].gameObject); 
        }
        
    }
    private void BindingEvent()
    {
        presetBtn.onClick.AddListener(() =>
        {
            HideAllLayers();
            InitInterface();
        });;

        backBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Utilities.stageID == Utilities.PRESET_STAGE)
            {
                layerList[2].SetActive(true);
                layerList[3].SetActive(true);
                layerList[4].SetActive(false);
                foreach (GameObject btn in btnList)
                {
                    btn.SetActive(!btn.activeSelf);
                }
                DestroyCards();
            }
        });
    }
}
