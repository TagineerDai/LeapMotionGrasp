using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class Detector : MonoBehaviour {
    //Round 1 : Grab count
    int MAXEPOCH = 10;
    LeapServiceProvider provider;
    int holdL, holdR;
    int unholdL, unholdR;
    bool lastL, lastR;
    bool startEpoch2;

    //Round 2: Hold cube
    public GameObject sphere;
    Vector3 pLeft, pRight;
    public GameObject left, right;

    // Use this for initialization
    void Start () {
        provider = FindObjectOfType<LeapServiceProvider>() as LeapServiceProvider;
        lastL = lastR = false;
        holdL = holdR = unholdL = unholdR = 0;

        pLeft = left.transform.position;
        pRight = right.transform.position;

        startEpoch2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Grasp visualizer
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft && hand.GrabStrength > 0.9)
            {
                left.transform.position = pLeft;
            }
            if(hand.IsRight && hand.GrabStrength > 0.9)
            {
                right.transform.position = pRight;
            }
        }
        if (startEpoch2 == false)
        {
            if (holdL > MAXEPOCH && unholdL > MAXEPOCH && holdR > MAXEPOCH && unholdR > MAXEPOCH)
            {
                startEpoch2 = true;
            }
            else
                Update_Grab();
        }
    }

    //Grb count
    void Update_Grab () {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                if (lastL == false && hand.GrabStrength>0.95) //hold
                {
                    lastL = true;
                    holdL++;
                }else if(lastL==true && hand.GrabStrength < 0.05) {
                    lastL = false;
                    unholdL++;
                }
            }
            else
            {
                if (lastR == false && hand.GrabStrength > 0.95) //hold & his_unhold
                {
                    lastR = true;
                    holdR++;
                }
                else if (lastR == true && hand.GrabStrength < 0.05) //unhold & his_hold
                {
                    lastR = false;
                    unholdR++;
                }
            }
        }
	}
}
