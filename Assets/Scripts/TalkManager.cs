using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;  // 초상화 데이터를 저장할 Dictionary 변수 생성

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // Talk Data
        // NPC A: 1000, NPC B: 2000, Box: 100, Desk: 200
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1", "한번 둘러보도록 해.:0" });
        talkData.Add(2000, new string[] { "와 손님이다.:1", "이 옆에 있는 호수는 정말 아릅답지?:0", "사실 아름다운 이 호수에도 엄청난 비밀이 있다고 해:1" });
        talkData.Add(100, new string[] { "평범한 나무상자다" });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        for (int i = 0; i < portraitArr.Length / 2; i++)
        {
            portraitData.Add(1000 + i, portraitArr[i]);
        }

        for (int i = 4; i < portraitArr.Length; i++)
        {
            portraitData.Add(2000 + (i - 4), portraitArr[i]);
        }

        // Quest Talk
        talkData.Add(10 + 1000, new string[] { "어서 와.:0", "이 마을에 놀라운 전설이 있다는데:1", "오른쪽 호수 쪽에 루도가 알려줄꺼야.:0" });
        talkData.Add(11 + 1000, new string[] { "아직 못만났어?:0", "루도는 오른쪽 호수 쪽에 있어.:0" });

        talkData.Add(11 + 2000, new string[] { "셀리한테 소식은 들었어:1", 
                                               "이 호수의 전설을 들으러 온거지?:0", 
                                               "그런데... 내가 할 일이 많아서... 도와줄 수 있어?:1",
                                               "여기 근처에 떨어진 동전이 있는데 좀 주워줬으면 하는데...:0"});

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1",
                                               "돈을 흘리고 다니면 못쓰지!:3",
                                               "나중에 루도에게 한마디 해야겠어.!:3" });

        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다줘.:1"});
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        talkData.Add(21 + 2000, new string[] { "와, 찾아줘서 고마워.:2" });

    }
    
    // 지정된 대화 문장을 반환하는 함수
    public string GetTalk(int id, int talkIndex)
    {
        // 해당 퀘스트 진행 순서 대사가 없는 경우
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);  // Get First Talk
            else
                return GetTalk(id - id % 10, talkIndex);   // Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    // 지정된 초상화 스프라이트를 반환할 함수 생성
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}