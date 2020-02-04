using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to save specific cards for each preset group.
/// <summary>
public class CardPreset
{
    #region properity
    public int MarionetteCardNumber { get; set; }
    public int DrawingCardNumber { get; set; }
    public int GoodCardNumber { get; set; }
    public int TotalNumber { get; set; }
    public List<MarionetteCard> MarionetteCardList { get; }
    public List<DrawingCard> DrawingCardList { get; }
    public List<GoodCard> GoodCardList { get; }
    #endregion

    const int MIN_EACH_NUM = 8;
    const int MAX_EACH_NUM = 16;
    const int MIN_TOTAL_NUM = 24;
    const int MAX_TOTAL_NUM = 32;

    public CardPreset()
    {
        MarionetteCardList = new List<MarionetteCard>();
        DrawingCardList = new List<DrawingCard>();
        GoodCardList = new List<GoodCard>();
    }

    public void ClearAll()
    {
        MarionetteCardList.Clear();
        DrawingCardList.Clear();
        GoodCardList.Clear();
    }

    public void GenerateCardPresetRandomly()
    {
        ClearAll();
        GenerateCardNumberRandomly();
        GenerateCardListRandomly();
    }

    private void GenerateCardNumberRandomly()
    {
        this.TotalNumber = 0;
        while (this.TotalNumber < MIN_TOTAL_NUM || this.TotalNumber > MAX_TOTAL_NUM)
        {
            this.MarionetteCardNumber = Random.Range(MIN_EACH_NUM, MAX_EACH_NUM+1);
            this.DrawingCardNumber = Random.Range(MIN_EACH_NUM, MAX_EACH_NUM+1);
            this.GoodCardNumber = Random.Range(MIN_EACH_NUM, MAX_EACH_NUM+1);
            this.TotalNumber = this.MarionetteCardNumber + this.DrawingCardNumber + this.GoodCardNumber;
        }
    }

    private void GenerateCardListRandomly()
    {

        int tmpNum = this.MarionetteCardNumber;
        while (tmpNum > 0)
        {
            int randomIndex = Random.Range(0, Utilities.MARIONETTE_CARD_NUM);
            MarionetteCard card = Utilities.MarionetteCardList[randomIndex];
            if (!card.IsSelected)
            {
                card.IsSelected = true;
                MarionetteCardList.Add(card);
                tmpNum--;
            }
        }

        tmpNum = this.DrawingCardNumber;
        while (tmpNum > 0)
        {
            int randomIndex = Random.Range(0, Utilities.DRAWING_CARD_NUM);
            DrawingCard card = Utilities.DrawingCardList[randomIndex];
            if (!card.IsSelected)
            {
                card.IsSelected = true;
                DrawingCardList.Add(card);  
                tmpNum--;
            }
        }

        tmpNum = this.GoodCardNumber;
        while (tmpNum > 0)
        {
            int randomIndex = Random.Range(0, Utilities.GOOD_CARD_NUM);
            GoodCard card = Utilities.GoodCardList[randomIndex];
            if (!card.IsSelected)
            {
                card.IsSelected = true;
                GoodCardList.Add(card);
                tmpNum--;
            }
        }
    }
}
