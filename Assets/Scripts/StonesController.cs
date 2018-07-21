using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StonesController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform uiStonePossition;
    public Text turnsText;

    private readonly Vector3 gameCmpStonePossition = new Vector3(51.28f, -253.17f, -279.83f);
    public static StonePrefab firstStone;
    private static int stoneCouplesLeft;
    private static int turns;
    private bool isSpinning;
    
    public int StoneCouplesLeft
    {
        get
        {
            return stoneCouplesLeft;
        }
        set
        {
            stoneCouplesLeft = value;
        }
    }
    public int Turns
    {
        get
        {
            return turns;
        }
        set
        {
            turns = value;
        }
    }

    public StonePrefab FirstStone
    {
        get
        {
            return firstStone;
        }
        set
        {
            firstStone = value;
        }
    }

    public bool IsSpinning
    {
        get
        {
            return this.isSpinning;
        }
        set
        {
            isSpinning = value;
        }
    }

    void Start()
    {
        Turns = 0;
        IsSpinning = false;
        StoneCouplesLeft = StonesLoader.StonesCount / 2;
        UpdateMovesText();
    }
    
    private void Update()
    {
        // Raycast on touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            RaycastStone();
        }
    }

    /* Raycast from center eye camera and check if it's 
    a StonePrefab then pass it to the OnStonePressed() */
    private void RaycastStone()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("StonePrefab"))
            {
                StonePrefab st = hit.collider.GetComponent<StonePrefab>();
                OnStonePressed(st);
            }
        }
    }
    // Update UI for turns count
    private void UpdateMovesText()
    {
        turnsText.text = "Turns: " + turns.ToString();
    }

    //Rotate a Stone on 180 degree
    private void RotateStone(StonePrefab st)
    {
        MusicController.Instance.spinStoneSound.Play();
        StartCoroutine(RotateObject(st.transform, 0f, 180f, 1f));
    }

    //Overload for two stones
    private void RotateStone(StonePrefab st, StonePrefab st2)
    {
        MusicController.Instance.spinStoneSound.Play();
        StartCoroutine(RotateObjectsAfterSomeTime(st.transform, st2.transform, 180f, 0f, 1f));
    }

    //Popup stone with your stats when you finish the game
    private IEnumerator ShowStoneWhenFinish()
    {
        float totalTime = 6f;
        float timer = 1f;
        float lerpValue;
        float stonePossition = -217.01f;
        uiStonePossition.transform.localRotation = new Quaternion(0, 90, 0, 0);
        yield return new WaitForSeconds(2);
        while (timer > 0f)
        {
            timer -= Time.deltaTime/2;
            lerpValue = Mathf.Lerp(gameCmpStonePossition.y ,stonePossition, 1f - timer / totalTime);
            uiStonePossition.transform.localPosition = new Vector3(gameCmpStonePossition.x, lerpValue, gameCmpStonePossition.z);

            yield return null;
        }
    }

    // Very abstract method for forating objects from 'x' to 'y' degree for 'z' time
    private IEnumerator RotateObject(Transform t, float startAnge, float endAngle, float totalTime)
    {
        IsSpinning = true;
        Vector3 rotation = Vector3.zero;
        float timer = 1f;
        float lerpValue;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            lerpValue = Mathf.Lerp(startAnge, endAngle, 1f - timer / totalTime);
            rotation.y = lerpValue;
            t.localEulerAngles = rotation;

            yield return null;
        }
    }

    // Overload for two objects with wait time
    private IEnumerator RotateObjectsAfterSomeTime(Transform t1, Transform t2, float startAnge, float endAngle,
        float totalTime, float waitTime = 1.7f)
    {
        IsSpinning = true;
        Vector3 rotation = Vector3.zero;
        float timer = 1f;
        float lerpValue;
        yield return new WaitForSeconds(waitTime);
        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            lerpValue = Mathf.Lerp(startAnge, endAngle, 1f - timer / totalTime);
            rotation.y = lerpValue;
            t1.localEulerAngles = rotation;
            t2.localEulerAngles = rotation;

            yield return null;
        }
        new WaitForSeconds(2.0f);
        IsSpinning = false;       
    }

    // Eliminate a pair and check if there are more stones left
    private void TwoSameStones(StonePrefab st, StonePrefab st2)
    {
        MusicController.Instance.twoSameSound.PlayDelayed(1.10f);
        StoneCouplesLeft--;
        if (StoneCouplesLeft == 0)
        {
            LevelComplete();
        }
    }

    // Popup the stone with your end game stats
    private void LevelComplete(float soundDelay = 1.6f)
    {
        MusicController.Instance.levelCompleteSound.PlayDelayed(soundDelay);
        StartCoroutine(ShowStoneWhenFinish());
    }

    
    public void OnStonePressed(StonePrefab st)
    {
        // Check if the stone is available (not spinning and active)
        if (st.Stone.IsStoneActive == true && IsSpinning == false)
        {
            // If so update the turns count, spin the stone and make it inactive
            turns++;
            UpdateMovesText();
            RotateStone(st);
            st.Stone.IsStoneActive = false;

            //Check if it's the first clicked stone if so asign it to 'FirstStone'
            if (FirstStone == null)
            {
                FirstStone = st;
                IsSpinning = false;
            }
            //If it's the second one, compere both ID's
            else
            {
                /* If the absolute value of both ID's is the same 
                  call TwoSameStones method and set stones to null (You've guessed one pair) */
                if (Mathf.Abs(st.Stone.StoneID) == Mathf.Abs(FirstStone.Stone.StoneID))
                {
                    IsSpinning = false;
                    TwoSameStones(st, FirstStone);
                    st = null;
                    FirstStone = null;
                }
                /*If not rotate them back, and set them to null*/
                else
                {
                    RotateStone(FirstStone, st);
                    FirstStone.Stone.IsStoneActive = true;
                    st.Stone.IsStoneActive = true;
                    FirstStone = null;
                    st = null;
                }
            }
        }
    }
}
