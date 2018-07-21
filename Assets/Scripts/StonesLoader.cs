using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StonesLoader : MonoBehaviour
{
    public GameObject stonePref;
    public Transform perant;
    public Sprite[] stonesImagesHolder;

    private const int maxStonesCount = 30;
    private static List<StonePrefab> stones = new List<StonePrefab>(maxStonesCount);
    private static int stonesCount;
    private List<Vector3> stonesPossitions = new List<Vector3>(StonesCount);

    //Class for craeting stone's possitions and instantiate the stones on them

    public List<Vector3> StonesPossitions
    {
        get
        {
            return this.stonesPossitions;
        }
        set
        {
            stonesPossitions = value;
        }
    }
    public GameObject StonePref
    {
        get
        {
            return this.stonePref;
        }
        set
        {
            stonePref = value;
        }
    }
    public Sprite[] StonesImagesHolder
    {
        get
        {
            return this.stonesImagesHolder;
        }
        set
        {
            stonesImagesHolder = value;
        }
}
    public List<StonePrefab> Stones
    {
        get
        {
            return stones;
        }
        set
        {
            stones = value;
        }
    }
    // Used from outside to set the stones count for the particular level
    public static int StonesCount
    {
        get
        {
            return stonesCount;
        }
        set
        {
            stonesCount = value;
        }
    }

    void Start()
    {
        CreateStones();
    }

    // Create possition for each stone, put them in List<Vector3> and randomize the list in the end
    private void CreateStonePossitions(float startPossition = 0f, float endPossition = 20f,
        float yPossition = 2.75f, float zPossition = 4.15f)
    {
        float lerpValue = 0;
        int linesCount = 3;
        Vector3 stonePossition = new Vector3();
        for (int j = 0; j < linesCount; j++)
        {
            for (int i = 0, n = StonesCount / linesCount; i < n; i++)
            {
                lerpValue = Mathf.Lerp(startPossition, endPossition, 1f / (n - 1) * i);

                stonePossition = new Vector3(lerpValue, yPossition, zPossition);
                StonesPossitions.Add(stonePossition);
            }
            yPossition += 1.89f;
        }
        System.Random rnd = new System.Random();
        StonesPossitions = StonesPossitions.OrderBy(x => rnd.Next()).ToList();
    }


    /* Create instance of StonePrefab on each possition we've made and setup it's image, and id */
    private void CreateStones()
    {
        float startStoneCord = 0f + (StonesCount % 10)/2;
        float endStoneCord = 20f - (StonesCount % 10) / 2;

        CreateStonePossitions(startStoneCord,endStoneCord);
        int reverseStoneIndex = 1;
        for (int i = 1, n = StonesCount; i <= n; i++) 
        {
            GameObject go = Instantiate(stonePref, StonesPossitions[i-1], Quaternion.identity);
            go.GetComponent<StonePrefab>().StoneSetup(((i + 1) / 2) * reverseStoneIndex);
            go.GetComponentInChildren<SpriteRenderer>().sprite=StonesImagesHolder[(i - 1) / 2];
            go.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(3f, 3f, 1f);
            Stones.Add(go.GetComponent<StonePrefab>());
            go.transform.parent = perant.transform;
            reverseStoneIndex *= -1;
        }
    }

    
}
