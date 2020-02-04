using System.ComponentModel;

/// <summary>
/// This class is used to save basic attributes for each card.
/// <summary>
public class Card
{

    public enum TypeNameOptions
    {
        [Description("人偶牌")]
        MARIONETTE,
        [Description("图纸牌")]
        DRAWING,
        [Description("物品牌")]
        GOOD
    }

    #region properity
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsSelected { get; set; }
    public TypeNameOptions TypeName { get; set; }
    #endregion
    public Card(int ID, string Name, string Description, bool IsSelected = false)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.IsSelected = IsSelected;
    }

}

/// <summary>
/// This class is used to save extended attributes for each marionette card.
/// <summary>
public class MarionetteCard : Card
{
    public MarionetteCard(int ID, string Name, string Description) : base(ID, Name, Description)
    {
        this.TypeName = TypeNameOptions.MARIONETTE;
    }
}

/// <summary>
/// This class is used to save extended attributes for each drawing card.
/// <summary>
public class DrawingCard : Card
{
    public DrawingCard(int id, string name, string description) : base(id, name, description)
    {
        this.TypeName = TypeNameOptions.DRAWING;
    }
}

/// <summary>
/// This class is used to save extended attributes for each good card.
/// <summary>
public class GoodCard : Card
{

    public GoodCard(int id, string name, string description) : base(id, name, description)
    {
        this.TypeName = TypeNameOptions.GOOD;
    }
}
