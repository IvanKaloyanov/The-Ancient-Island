using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonePrefab : MonoBehaviour
{
    // Define our stone in the prefab
    private Stone stone;

    public StonePrefab()
    {
        // instantiate the stone with empty ctor (only his state is set to active)
        stone = new Stone();
    }

    public Stone Stone
    {
        get
        {
            return this.stone;
        }
        set
        {
            this.stone = value;
        }
    }

    public void StoneSetup(int stoneID)
    {
            Stone.StoneID = stoneID;
    }
}
