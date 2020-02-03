using System.Collections.Generic;
using TMPro;
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
    private GameObject cardPreview;
    [SerializeField]
    private List<GameObject> barList;
    [SerializeField]
    private List<GameObject> cardList;
    private void Awake()
    {
        presetBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonPrep").GetComponent<Button>();
        layerList = new List<GameObject>();
        btnList = new List<GameObject>();
        barList = new List<GameObject>();
        cardList = new List<GameObject>();
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
        ExtractBars();
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

    private void ExtractBars()
    {
        GameObject cc = GameObject.Find("Canvas/Background/CardContainer/CountingChamber");
        Transform[] bars = cc.GetComponentsInRealChildren<Transform>();
        int count = 0;
        foreach(Transform tf in bars)
        {
            if (count % 5 == 0)
            {
                barList.Add(tf.gameObject);
            }
            count++;
        }
    }
    private void InitInterface()
    {
        Utilities.stageID = Utilities.PRESET_STAGE;
        layerList[0].SetActive(true);
        layerList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask+"yspz", typeof(Sprite)) as Sprite;
        CreateBlankCards();
        cardPreview = GameObject.Find("Canvas/Background/CardContainer/CardPreview");
        cardPreview.SetActive(false);
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
            GameObject go = (GameObject)Resources.Load(Utilities.res_folder_path_prefabs+"card");            
            go.name = "card "+i;     
            go = Instantiate(go);            
            go.transform.SetParent(cardPool);

            //go.AddComponent<Image>();
            //go.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_cards+ "blank", typeof(Sprite)) as Sprite;
            //go.AddComponent<Button>();
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

    private void ExtractCards()
    {
        GameObject cardPool = GameObject.Find("Canvas/Background/CardContainer/CardPool");
        Transform[] cards = cardPool.GetComponentsInRealChildren<Transform>();
        foreach(Transform tf in cards)
        {
            cardList.Add(tf.gameObject);
        }
    }

    private void ChangeSpriteState(Button btn,string pathPrefix,bool isBlank = false)
    {
        if (!isBlank)
        {
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = Resources.Load(pathPrefix + "_h", typeof(Sprite)) as Sprite; ;
            ss.pressedSprite = Resources.Load(pathPrefix + "_p", typeof(Sprite)) as Sprite; ;
            btn.spriteState = ss;
        }
        else
        {
            SpriteState ss = new SpriteState();
            ss.highlightedSprite = Resources.Load(pathPrefix + "", typeof(Sprite)) as Sprite; ;
            ss.pressedSprite = Resources.Load(pathPrefix + "", typeof(Sprite)) as Sprite; ;
            btn.spriteState = ss;
        }   
    }

    private void DisplayMessageWhenMouseEnter(Button btn,Utilities.Card card)
    {
        btn.onClick.AddListener(() =>
        {
            cardPreview.SetActive(true);
            Transform[] tfs = cardPreview.GetComponentsInRealChildren<Transform>();
            tfs[0].gameObject.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_cards+card.Name+"_n", typeof(Sprite)) as Sprite;
            tfs[1].gameObject.GetComponent<TextMeshProUGUI>().text = card.Name;
            tfs[3].gameObject.GetComponent<TextMeshProUGUI>().text = card.TypeName;
            tfs[4].gameObject.GetComponent<TextMeshProUGUI>().text = card.Description;
        });
    }

    private void AttachCard()
    {
        barList[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Utilities.cp1.McardsNum.ToString();
        barList[1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Utilities.cp1.DcardsNum.ToString();
        barList[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Utilities.cp1.GcardsNum.ToString();
        barList[3].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = Utilities.cp1.TotalNum.ToString();
        ExtractCards();
        int j = 0;
        for(int i = 0; i < Utilities.cp1.McardsNum;i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + Utilities.cp1.Mcards[i].Name;
            Button btn = cardList[j].GetComponent<Button>();
            ChangeSpriteState(btn, pathPrefix);
            DisplayMessageWhenMouseEnter(btn, Utilities.cp1.Mcards[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for (int i = 0; i < Utilities.cp1.DcardsNum; i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + Utilities.cp1.Dcards[i].Name;
            ChangeSpriteState(cardList[j].GetComponent<Button>(), pathPrefix);
            Button btn = cardList[j].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, Utilities.cp1.Dcards[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for (int i = 0; i < Utilities.cp1.GcardsNum; i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + Utilities.cp1.Gcards[i].Name;
            ChangeSpriteState(cardList[j].GetComponent<Button>(), pathPrefix);
            Button btn = cardList[j].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, Utilities.cp1.Gcards[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for(int k = j; k < Utilities.cp1.MAX_TOTAL_NUM; k++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + "blank";
            ChangeSpriteState(cardList[k].GetComponent<Button>(), pathPrefix,true);
            cardList[k].GetComponent<Image>().sprite = Resources.Load(pathPrefix, typeof(Sprite)) as Sprite;
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

        randBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Utilities.cp1.GenerateCardsNumberRandomly();
            Utilities.cp1.GenerateCardRandomly();
            AttachCard();
        });
    }
}
