using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;                    // 퀘스트 ID
    public int questActionIndex;           // 퀘스트 대화순서 변수
    public GameObject[] questObject;       // 퀘스트 오브젝트를 저장할 변수

    Dictionary<int, QuestData> questList;  // 퀘스트 데이터를 저장할 변수

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을사람과 대화하기", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("루도의 동전 찾아주기", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 }));    // 퀘스트 마무리
    }

    // NPC id를 받고 퀘스트 번호를 반환하는 함수 생성
    public int GetQuestTalkIdx(int id)
    {
        return questId + questActionIndex;
    }

    // 지정된 대화 문장을 반환하는 함수
    public string CheckQuest(int id)
    {
        // 순서에 맞게 대화 했을 때만 퀘스트 대화순서를 올리도록 작성
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        // 퀘스트 대화순서가 끝에 도달했을 때 퀘스트 번호 증가
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    // CheckQuest()함수 오버로드
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    // 다음 퀘스트를 위한 함수
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;   // 새로운 퀘스트 시작
    }

    // 퀘스트 오브젝트를 관리할 함수
    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
            default:
                break;
        }
    }
}