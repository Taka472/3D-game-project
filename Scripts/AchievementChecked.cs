using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementChecked : MonoBehaviour
{
    private Image checkBox;
    public Sprite check;

    // Start is called before the first frame update
    void Start()
    {
        checkBox = GetComponent<Image>();
    }

    public void Checking()
    {
        checkBox.sprite = check;
    }
}
