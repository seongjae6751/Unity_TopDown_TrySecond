using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;
    string targetMsg;
    TextMeshProUGUI msgText;
    int index;
    float interval;

    public void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    // Update is called once per frame
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        EndCursor.SetActive(true);
    }
}
