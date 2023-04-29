using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public Animator talkPanel;
    public Animator PortraitAnim;
    public QuestManager questManager;
    public Image portraitImg;
    public Sprite prePortrait;
    public TypeEffect talk;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
        GetComponent<Animator>();
    }
    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        // Set talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if (isNpc)
        {
            talk.SetMsg(talkData.Split(":")[0]);

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            if(prePortrait != portraitImg.sprite)
            {
                PortraitAnim.SetTrigger("doEffect");
                prePortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
