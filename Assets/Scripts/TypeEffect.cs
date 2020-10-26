using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int charPerSeconds;
    public GameObject endCursor;
    public bool isAnimPlay;

    string targetMsg;
    Text msgText;
    AudioSource audioSource;

    int typeIndex;
    float interval;
   
    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // 대화 문자열을 받는 함수
    public void SetMsg(string msg)
    {
        // Interrupt
        if (isAnimPlay)
        {
            msgText.text = targetMsg;   // 텍스트를 다 채워주기
            CancelInvoke();             // 반복하고 있는 Invoke 함수 취소
            EffectEnd();                // 강제로 타이핑 애니매이션 종료
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    // 애니매이션 재생 시작
    void EffectStart()
    {
        msgText.text = "";
        typeIndex = 0;
        endCursor.SetActive(false);

        // Start Animation
        interval = 1.0f / charPerSeconds;

        isAnimPlay = true;

        Invoke("EffectPlay", interval);
    }

    // 애니매이션 재생 중
    void EffectPlay()
    {
        // End Animation
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[typeIndex];

        // Sound
        if (targetMsg[typeIndex] != ' ' || targetMsg[typeIndex] != '.')
            audioSource.Play();

        typeIndex++;
        Invoke("EffectPlay", interval);
    }

    // 애니매이션 재생 종료
    void EffectEnd()
    {
        isAnimPlay = false;
        endCursor.SetActive(true);
    }
}
