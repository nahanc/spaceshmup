
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{

    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0b0000, //0
        offRight = 0b0001, //1s
        offLeft = 0b0010, //2
        offUp = 0b0100, //4
        offDown = 0b1000 //8
    }
    public enum eType { center, inset, outset };


    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) {checkRadius = -radius; }
        if (boundsType == eType.outset) {checkRadius = radius; }
        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth;
            screenLocs |= eScreenLocs.offRight;
        }
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth;
            screenLocs |= eScreenLocs.offLeft;
        }
        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight;
            screenLocs |= eScreenLocs.offUp;
        }
        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight;
            screenLocs |= eScreenLocs.offDown;
        }


        if (!isOnScreen && keepOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
        }
    }
    public bool isOnScreen
    {
        get { return (screenLocs == eScreenLocs.onScreen); }
    }

    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) {return isOnScreen;}
        return ((screenLocs & checkLoc) == checkLoc);
    }
}
