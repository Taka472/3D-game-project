using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    public string[] textMessage;

    public Text[] messageBox;

    public void ChangeFirstText()
    {
        messageBox[0].text = textMessage[0];
    }

    public void ChangeSecond()
    {
        messageBox[1].text = textMessage[1];
    }

    public void ChangeThird()
    {
        messageBox[2].text = textMessage[2];
    }

    public void ChangeFourth()
    {
        messageBox[0].text = textMessage[3];
    }

    public void ChangeFifth()
    {
        messageBox[1].text = textMessage[4];
    }

    public void ChangeSixth()
    {
        messageBox[2].text = textMessage[5];
    }

    public void ChangeSeventh()
    {
        messageBox[0].text = textMessage[6];
    }
}
