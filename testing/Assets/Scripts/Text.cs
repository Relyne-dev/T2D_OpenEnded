using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextScript : MonoBehaviour
{
    public Text counterText;
    public float counter;

    public void Update()
    {
        counterText.text = counter.ToString();
    }
}