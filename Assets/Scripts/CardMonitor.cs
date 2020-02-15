using System;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject backBtn;
    private GameObject abandonBtn;
    private GameObject startBtn;
    private GameObject randBtn;
    private GameObject clearBtn;
    private GameObject prepBtn;
    private GameObject editBtn;
    private GameObject saveBtn;
    private List<GameObject> btnList;
    private GameObject cardContainer;
    private GameObject cardPreview;
    private GameObject innerEditWrapper;
    [SerializeField]
    private List<GameObject> barList;
    [SerializeField]
    private List<GameObject> cardList;
    [SerializeField]
    private List<GameObject> presetList;
    private Dictionary<Button, CardPreset> btnToCp;
    private List<TextMeshProUGUI> cardTypeCounter_tmps;
    [SerializeField]
    private List<Toggle> marionetteToggles;
    [SerializeField]
    private List<Toggle> drawingToggles;
    [SerializeField]
    private List<Toggle> goodToggles;
    [SerializeField]
    private List<Toggle> displayToggles;

    const float HIGHLIGHT_SIZE = 24;
    const float NORMAL_SIZE = 18;
    private CardPreset cp_temp;
    private int marionetteCardNumber_temp;
    private int drawingCardNumber_temp;
    private int goodCardNumber_temp;
    private bool disableToggleListener;
    private void Awake()
    {
        //enableToggleListener = false;
        layerList = new List<GameObject>();
        btnList = new List<GameObject>();
        barList = new List<GameObject>();
        presetList = new List<GameObject>();
        cardList = new List<GameObject>();
        marionetteToggles = new List<Toggle>();
        drawingToggles = new List<Toggle>();
        goodToggles = new List<Toggle>();

        btnToCp = new Dictionary<Button, CardPreset>();
        cardPreview = GameObject.Find("Canvas/Background/CardContainer/CardPreview");
        innerEditWrapper = GameObject.Find("Canvas/Background/InnerEditWrapper");
        presetBtn = GameObject.Find("Canvas/Background/BottomMask/BottomPanel/ButtonPrep").GetComponent<Button>();
        GameObject cardTypeCounter = GameObject.Find("Canvas/Background/InnerEditWrapper/CardTypeCounter");
        cardTypeCounter_tmps = new List<TextMeshProUGUI>(cardTypeCounter.GetComponentsInChildren<TextMeshProUGUI>());
        GameObject cardTypeToggles = GameObject.Find("Canvas/Background/InnerEditWrapper/CardTypeToggles");
        displayToggles = new List<Toggle>(cardTypeToggles.GetComponentsInChildren<Toggle>());

        cp_temp = new CardPreset();
    }

    private void Start()
    {
        ExtractBottomBtns();
        randBtn.SetActive(false);
        editBtn.SetActive(false);
        ExtractPresets();
        ExtractLayers();        
        cardContainer = layerList[4];
        ExtractBars();
        GeneratePresetCard();
        ExtractCards();
        cardContainer.SetActive(false);
        BuildCardScrollView();
        BindingEvent();        
        innerEditWrapper.SetActive(false);
        abandonBtn.SetActive(false);
        clearBtn.SetActive(false);
        saveBtn.SetActive(false);
        SetPresets(false);
        InitMap();
    }

    private void InitMap()
    {
        CardPreset[] cps = { Utilities.cp1, Utilities.cp2, Utilities.cp3, Utilities.cp4 };
        for (int i = 0; i < presetList.Count(); i++)
        {
            btnToCp.Add(presetList[i].GetComponent<Button>(), cps[i]);
        }
        
    }
    private void SetPresets(bool activeSelf)
    {
  
        foreach(GameObject preset in presetList)
        {
            preset.SetActive(activeSelf);
        }
    }

    private void ExtractBottomBtns()
    {
        string btnPath = "Canvas/Background/BottomMask/BottomPanel/";        
        backBtn = GameObject.Find(btnPath + "ButtonBack");
        abandonBtn = GameObject.Find(btnPath + "ButtonAbandon");
        startBtn = GameObject.Find(btnPath + "ButtonStart");
        randBtn = GameObject.Find(btnPath + "ButtonRand");
        clearBtn = GameObject.Find(btnPath + "ButtonClear");
        prepBtn = GameObject.Find(btnPath + "ButtonPrep");
        editBtn = GameObject.Find(btnPath + "ButtonEdit");
        saveBtn = GameObject.Find(btnPath + "ButtonSave");
        btnList.Add(startBtn);
        btnList.Add(randBtn);
        btnList.Add(prepBtn);
        btnList.Add(editBtn);
    }

    private void ExtractPresets()
    {
        string presetPath = "Canvas/Background/TopMask/";
        string[] presetSet = { "PresetOne", "PresetTwo", "PresetThree", "PresetFour" };
        for(int i = 0; i < 4; i++)
        {
            presetList.Add(GameObject.Find(presetPath+ presetSet[i]));
        }
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
        SetPresets(true);
        Utilities.stageID = Utilities.StageOptions.PRESET_STAGE;
        layerList[0].SetActive(true);
        layerList[2].SetActive(true);
        layerList[0].GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_mask+"yspz", typeof(Sprite)) as Sprite;
        DisplayPresetCard();
        cardPreview.SetActive(false);
    }

    private void DisplayPresetCard()
    {
        //GameObject mask = layerList[1];
        //mask.SetActive(true);
        foreach(GameObject btn in btnList)
        {
            btn.SetActive(!btn.activeSelf);
        }
        cardContainer.SetActive(true);

    }

    private void GeneratePresetCard()
    {

        Transform cardPool = cardContainer.transform.GetChild(1);

        //Vector3 bounds = cardContainer.GetComponent<MeshFilter>().mesh.bounds.size;
        //print(bounds.x* cardContainer.transform.localScale.x);
        //print(bounds.y* cardContainer.transform.localScale.y);

        GridLayoutGroup glg = cardPool.GetComponent<GridLayoutGroup>();
        int k = 20;
        glg.padding.left = 2;
        glg.padding.top = 2;
        glg.cellSize = new Vector2(3 * k, 4 * k);
        glg.spacing = new Vector2(5, 5);

        for (int i = 0; i < 32; i++)
        {
            GameObject go = (GameObject)Resources.Load(Utilities.res_folder_path_prefabs + "card");
            go.name = "card " + i;
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

    private void DisplayMessageWhenMouseEnter(Button btn, Card card, bool isBlank = false)
    {
        if (!isBlank)
        {
            btn.onClick.AddListener(() =>
            {
                cardPreview.SetActive(true);
                Transform[] tfs = cardPreview.GetComponentsInRealChildren<Transform>();
                tfs[0].gameObject.GetComponent<Image>().sprite = Resources.Load(Utilities.res_folder_path_cards + card.Name + "_n", typeof(Sprite)) as Sprite;
                tfs[1].gameObject.GetComponent<TextMeshProUGUI>().text = card.Name;
                tfs[3].gameObject.GetComponent<TextMeshProUGUI>().text = MyTool.GetEnumDescription(card.TypeName);
                tfs[4].gameObject.GetComponent<TextMeshProUGUI>().text = card.Description;
            });
        }
        else
        {
            btn.onClick.AddListener(() =>
            {
                cardPreview.SetActive(false);
            });
        }
    }

    private void ReplaceAllCardsWithBlank(CardPreset cp)
    {
        barList[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.MarionetteCardNumber.ToString();
        barList[1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.DrawingCardNumber.ToString();
        barList[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.GoodCardNumber.ToString();
        barList[3].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.TotalNumber.ToString();

        for (int k = 0; k < 32; k++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + "blank";
            ChangeSpriteState(cardList[k].GetComponent<Button>(), pathPrefix, true);
            Button btn = cardList[k].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, null, true);
            cardList[k].GetComponent<Image>().sprite = Resources.Load(pathPrefix, typeof(Sprite)) as Sprite;
        }
    }

    private void AttachCard(CardPreset cp, bool isBlank = false)
    {
        if (isBlank)
        {
            ReplaceAllCardsWithBlank(cp);
            return;
        }

        barList[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.MarionetteCardNumber.ToString();
        barList[1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.DrawingCardNumber.ToString();
        barList[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.GoodCardNumber.ToString();
        barList[3].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = cp.TotalNumber.ToString();
        int j = 0;
        for(int i = 0; i < cp.MarionetteCardNumber;i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + cp.MarionetteCardList[i].Name;
            Button btn = cardList[j].GetComponent<Button>();
            ChangeSpriteState(btn, pathPrefix);
            DisplayMessageWhenMouseEnter(btn, cp.MarionetteCardList[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for (int i = 0; i < cp.DrawingCardNumber; i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + cp.DrawingCardList[i].Name;
            ChangeSpriteState(cardList[j].GetComponent<Button>(), pathPrefix);
            Button btn = cardList[j].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, cp.DrawingCardList[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for (int i = 0; i < cp.GoodCardNumber; i++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + cp.GoodCardList[i].Name;
            ChangeSpriteState(cardList[j].GetComponent<Button>(), pathPrefix);
            Button btn = cardList[j].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, cp.GoodCardList[i]);
            cardList[j++].GetComponent<Image>().sprite = Resources.Load(pathPrefix + "_n", typeof(Sprite)) as Sprite;
        }
        for(int k = j; k < /*cp.MAX_TOTAL_NUM*/32; k++)
        {
            string pathPrefix = Utilities.res_folder_path_cards + "blank";
            ChangeSpriteState(cardList[k].GetComponent<Button>(), pathPrefix,true);
            Button btn = cardList[k].GetComponent<Button>();
            DisplayMessageWhenMouseEnter(btn, null, true);
            cardList[k].GetComponent<Image>().sprite = Resources.Load(pathPrefix, typeof(Sprite)) as Sprite;
        }
    }

    private void ChangeFontSizeAndColor(Button presetBtn)
    {


        foreach (GameObject preset in presetList)
        {
            var btn = preset.GetComponent<Button>();
            TextMeshProUGUI _tmp = btn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if(_tmp.fontSize == HIGHLIGHT_SIZE)
            {
                _tmp.fontSize = NORMAL_SIZE;
                TMP_ColorGradient _colorGradient = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp + "ColorGradient/Gray - Single");
                _tmp.colorGradientPreset = _colorGradient;
                break;
            }
            
        }

        TextMeshProUGUI tmp = presetBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        tmp.fontSize = 24;
        if (!tmp.enableVertexGradient)
        {
            tmp.enableVertexGradient = true;
        }
        TMP_ColorGradient colorGradient = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp+ "ColorGradient/Yellow to Orange - Vertical");
        tmp.colorGradientPreset = colorGradient;
        //tmp.colorGradientPreset = ScriptableObject.CreateInstance<TMP_ColorGradient>();
        //tmp.colorGradientPreset.colorMode = ColorMode.FourCornersGradient;
    }


    // To avoid onValueChanged function being called when setting toggle.isOn through code.
    public void SetIsOn(Toggle tg, bool isOn)
    {
        disableToggleListener = true;
        tg.isOn = isOn;
        disableToggleListener = false;

    }

    private void BindingEvent()
    {
        foreach(GameObject preset in presetList)
        {
            var btn = preset.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                cardPreview.SetActive(false);
                ChangeFontSizeAndColor(btn);
                AttachCard(btnToCp[btn], !btnToCp[btn].RandomTag);
            });
        }

        presetBtn.onClick.AddListener(() =>
        {
            HideAllLayers();
            InitInterface();
            AttachCard(Utilities.cp1);
        });;

        backBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Utilities.stageID == Utilities.StageOptions.PRESET_STAGE)
            {
                layerList[1].SetActive(true);
                layerList[2].SetActive(true);
                layerList[3].SetActive(true);
                layerList[4].SetActive(false);
                foreach (GameObject btn in btnList)
                {
                    btn.SetActive(!btn.activeSelf);
                }
                SetPresets(false);
                //DestroyCards();
            }
        });

        randBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            CardPreset curCp = CheckCurrentCp();
            Utilities.DisableAllCards();
            curCp.ClearAll();
            curCp.GenerateCardPresetRandomly();
            //curCp.PrintAll();
            AttachCard(curCp);
        });

        editBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            //enableToggleListener = true;
            //cp_temp = CheckCurrentCp();
            //print(cp_temp.MarionetteCardNumber);
            GoToEditInterface();
            CardPreset curCp = CheckCurrentCp();
            saveBtn.SetActive(curCp.IsLegal);
            UpdateTempData(curCp.MarionetteCardNumber, curCp.DrawingCardNumber, curCp.GoodCardNumber);
            UpdateCardTypeCounter();
            UpdateCardScrollView(curCp);
        });

        abandonBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            //enableToggleListener = false;
            cardPreview.SetActive(false);
            InvertEditMode(false);
            //UpdateTempData(CheckCurrentCp());
        });

        clearBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            //SetCardScrollView(false);
            //cp_temp = new CardPreset(0, 0, 0);
            saveBtn.SetActive(false);
            UpdateTempData(0, 0, 0);
            UpdateCardTypeCounter();
            SetCardScrollView(false);
        });

        saveBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            //if (CanBeSaved())
            //{
                SaveTempCardPreset();
                CardPreset curCp = CheckCurrentCp();
                //curCp.PrintAll();
                curCp = cp_temp;
                //curCp.PrintAll();
                AttachCard(curCp);
            //}
            //else
            //{
            //    print(string.Format("{0} {1} {2}", marionetteCardNumber_temp, drawingCardNumber_temp, goodCardNumber_temp));
            //}
        });

        foreach (Toggle tg in marionetteToggles)
        {
            tg.onValueChanged.AddListener(isSelected=>
            {
                if (!disableToggleListener)
                {
                    OnToggleValueChanged(tg);
                    //print(isSelected);
                    UpdateCardTypeCounter();
                    saveBtn.SetActive(CanBeSaved());
                }
            });
        }

        foreach (Toggle tg in drawingToggles)
        {
            tg.onValueChanged.AddListener((bool value) =>
            {
                if (!disableToggleListener)
                {
                    OnToggleValueChanged(tg);
                    //print(isSelected);
                    UpdateCardTypeCounter();
                    saveBtn.SetActive(CanBeSaved());
                }
            });
        }

        foreach (Toggle tg in goodToggles)
        {
            tg.onValueChanged.AddListener((bool value) =>
            {
                if (!disableToggleListener)
                {
                    OnToggleValueChanged(tg);
                    //print(isSelected);
                    UpdateCardTypeCounter();
                    saveBtn.SetActive(CanBeSaved());
                }
            });
        }

        displayToggles[0].onValueChanged.AddListener((bool value) =>
        {
                for(int i = 0; i < marionetteToggles.Count(); i++)
                {
                    marionetteToggles[i].gameObject.SetActive(displayToggles[0].isOn);
                }
        });

        displayToggles[1].onValueChanged.AddListener((bool value) =>
        {
            for (int i = 0; i < drawingToggles.Count(); i++)
            {
                drawingToggles[i].gameObject.SetActive(displayToggles[1].isOn);
            }
        });

        displayToggles[2].onValueChanged.AddListener((bool value) =>
        {
            for (int i = 0; i < goodToggles.Count(); i++)
            {
                goodToggles[i].gameObject.SetActive(displayToggles[2].isOn);
            }
        });
    }

    private void OnToggleValueChanged(Toggle tg)
    {
        string typeName = GetToggleType(tg);
        bool isSelected = tg.isOn;
        switch (typeName)
        {
            case "人偶牌":
                marionetteCardNumber_temp += (isSelected ? 1 : -1);
                break;
            case "图纸牌":
                drawingCardNumber_temp += (isSelected ? 1 : -1);
                break;
            case "物品牌":
                goodCardNumber_temp += (isSelected ? 1 : -1);
                break;
            default:
                break;
        }
    }

    private string GetToggleType(Toggle tg)
    {
        string cardName = tg.gameObject.name;
        cardName = cardName.Substring(0, cardName.Length - 7);
        //print(cardName);
        foreach (Card card in Utilities.MarionetteCardList)
        {
            if(card.Name == cardName)
            {
                return MyTool.GetEnumDescription(card.TypeName);
            }
        }
        foreach (Card card in Utilities.DrawingCardList)
        {
            if (card.Name == cardName)
            {
                return MyTool.GetEnumDescription(card.TypeName);
            }
        }
        foreach (Card card in Utilities.GoodCardList)
        {
            if (card.Name == cardName)
            {
                return MyTool.GetEnumDescription(card.TypeName);
            }
        }
        return "";
    }

    private void SaveTempCardPreset()
    {
        cp_temp.ClearAll();
        cp_temp.MarionetteCardNumber = marionetteCardNumber_temp;
        cp_temp.DrawingCardNumber = drawingCardNumber_temp;
        cp_temp.GoodCardNumber = goodCardNumber_temp;
        cp_temp.TotalNumber = marionetteCardNumber_temp + drawingCardNumber_temp + goodCardNumber_temp;
        cp_temp.CheckIsLegalOrNot();
        for (int i = 0; i < Utilities.MARIONETTE_CARD_NUM; i++)
        {
            if (marionetteToggles[i].isOn)
            {
                cp_temp.MarionetteCardList.Add(Utilities.MarionetteCardList[i]);
            }
        }
        for (int i = 0; i < Utilities.DRAWING_CARD_NUM; i++)
        {
            if (drawingToggles[i].isOn)
            {
                cp_temp.DrawingCardList.Add(Utilities.DrawingCardList[i]);
            }
        }
        for (int i = 0; i < Utilities.GOOD_CARD_NUM; i++)
        {
            if (goodToggles[i].isOn)
            {
                cp_temp.GoodCardList.Add(Utilities.GoodCardList[i]);
            }
        }
        //if (cp_temp.IsLegal)
        //{
        //    cp_temp.PrintAll();
        //}
        //else
        //{
        //    print(string.Format("{0} {1} {2} is illegal", marionetteCardNumber_temp, drawingCardNumber_temp, goodCardNumber_temp));
        //}
        
    }

    private void InvertEditMode(bool state)
    {
        layerList[0].SetActive(!state);
        cardContainer.SetActive(!state);
        backBtn.SetActive(!state);
        randBtn.SetActive(!state);
        editBtn.SetActive(!state);
        abandonBtn.SetActive(state);
        clearBtn.SetActive(state);
        saveBtn.SetActive(state); 
        innerEditWrapper.SetActive(state);        

    }

    private void UpdateTempData(int mcn, int dcn, int gcn)
    {
        marionetteCardNumber_temp = mcn;
        drawingCardNumber_temp = dcn;
        goodCardNumber_temp = gcn;
    }

    private void GoToEditInterface()
    {        
        InvertEditMode(true);
        //CardPreset curCp = CheckCurrentCp();
        //UpdateTempData(cp_temp);
        //print(cp_temp.MarionetteCardNumber);
    }

    private void BuildCardScrollView()
    {
        GameObject cardScrollView = GameObject.Find("Canvas/Background/InnerEditWrapper/CardScrollView");
        GameObject contents = cardScrollView.GetComponentInChildren<GridLayoutGroup>().gameObject;
        foreach (Card card in Utilities.MarionetteCardList)
        {
            marionetteToggles.Add(AppendCardContent(card, contents.transform));
        }
        foreach (Card card in Utilities.DrawingCardList)
        {
            drawingToggles.Add(AppendCardContent(card, contents.transform));
        }
        foreach (Card card in Utilities.GoodCardList)
        {
            goodToggles.Add(AppendCardContent(card, contents.transform));
        }
    }

    private void SetCardScrollView(bool state)
    {
        foreach(Toggle tg in marionetteToggles)
        {
            SetIsOn(tg, state);
        }
        foreach (Toggle tg in drawingToggles)
        {
            SetIsOn(tg, state);
        }
        foreach (Toggle tg in goodToggles)
        {
            SetIsOn(tg, state);
        }
        //cardTypeCounter_tmps[1].text = "0";
        //cardTypeCounter_tmps[4].text = "0";
        //cardTypeCounter_tmps[7].text = "0";
        //foreach (TextMeshProUGUI tmp in cardTypeCounter_tmps)
        //{
        //    tmp.colorGradientPreset = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp + "ColorGradient/Red - Single");
        //}
    }

    private void UpdateCardScrollView(CardPreset cp)
    {
        GameObject cardScrollView = GameObject.Find("Canvas/Background/InnerEditWrapper/CardScrollView");
        GameObject contents = cardScrollView.GetComponentInChildren<GridLayoutGroup>().gameObject;
        for(int i = 0; i < Utilities.MarionetteCardList.Length; i++)
        {
            bool state = cp.MarionetteCardList.Exists(t => t == Utilities.MarionetteCardList[i]);
            SetIsOn(marionetteToggles[i], state);
        }
        for (int i = 0; i < Utilities.DrawingCardList.Length; i++)
        {
            bool state = cp.DrawingCardList.Exists(t => t == Utilities.DrawingCardList[i]);
            SetIsOn(drawingToggles[i], state);
        }
        for (int i = 0; i < Utilities.GoodCardList.Length; i++)
        {
            bool state = cp.GoodCardList.Exists(t => t == Utilities.GoodCardList[i]);
            SetIsOn(goodToggles[i], state);
        }
    }

    private Toggle AppendCardContent(Card card,Transform parent)
    {
            
            GameObject go = (GameObject)Resources.Load(Utilities.res_folder_path_prefabs + "CardContent");
            go.name = card.Name;
            go = Instantiate(go);
            go.transform.SetParent(parent);
            string pathPrefix = Utilities.res_folder_path_cards + card.Name + "_n";
            go.GetComponentInChildren<Image>().sprite = Resources.Load(pathPrefix, typeof(Sprite)) as Sprite;
            go.GetComponentInChildren<TextMeshProUGUI>().text = card.Description;

           return go.GetComponent<Toggle>();
    }


    private void UpdateCardTypeCounter()
    {
        //cp.PrintAll();
        cardTypeCounter_tmps[1].text = marionetteCardNumber_temp.ToString();
        //print(cardTypeCounter_tmps[1].text);
        cardTypeCounter_tmps[4].text = drawingCardNumber_temp.ToString();
        cardTypeCounter_tmps[7].text = goodCardNumber_temp.ToString();
        TMP_ColorGradient _colorGradient_yellow = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp + "ColorGradient/Yellow - Single");
        TMP_ColorGradient _colorGradient_red = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp + "ColorGradient/Red - Single");
        for(int i = 0; i < cardTypeCounter_tmps.Count(); i++)
        {
            if (i <= 2)
            {
                cardTypeCounter_tmps[i].colorGradientPreset = marionetteCardNumber_temp >= 8 ?  _colorGradient_yellow: _colorGradient_red;
            }
            else if(i <= 5)
            {
                cardTypeCounter_tmps[i].colorGradientPreset = drawingCardNumber_temp >= 8 ? _colorGradient_yellow : _colorGradient_red;
            }
            else if(i <= 8)
            {
                cardTypeCounter_tmps[i].colorGradientPreset = goodCardNumber_temp >= 8 ? _colorGradient_yellow : _colorGradient_red;
            }
        }
    }

    private bool CanBeSaved()
    {
        TMP_ColorGradient _colorGradient_red = Resources.Load<TMP_ColorGradient>(Utilities.res_folder_path_tmp + "ColorGradient/Red - Single");
        foreach (var tmp in cardTypeCounter_tmps)
        {
            if(tmp.colorGradientPreset == _colorGradient_red)
            {
                return false;
            }
        }
        return true;
    }

    private CardPreset CheckCurrentCp()
    {
        foreach (GameObject preset in presetList)
        {
            var btn = preset.GetComponent<Button>();
            TextMeshProUGUI _tmp = btn.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (_tmp.fontSize == HIGHLIGHT_SIZE)
            {
                return btnToCp[btn];
            }

        }
        return null;
    }
}
