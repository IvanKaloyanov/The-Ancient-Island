using UnityEngine;
using UnityEngine.UI;

public class Stone
{
    // Class Stone which holds stone's id, stone's image and his active state 
    private SpriteRenderer stoneImage;
    private int stoneID;
    private bool isStoneActive;

    public Stone()
    {
        IsStoneActive = true;
    }
    public Stone(SpriteRenderer img)
    {
        IsStoneActive = true;
        StoneImage = img;
    }

    public int StoneID
    {
        get
        {
            return this.stoneID;
        }
        set
        {
            stoneID = value;
        }

    }
    public SpriteRenderer StoneImage
    {
        get
        {
            return stoneImage;
        }

        set
        {
            stoneImage = value;
        }
    }
    public bool IsStoneActive
    {
        get
        {
            return this.isStoneActive;
        }

        set
        {
            this.isStoneActive = value;
        }
    }
}
