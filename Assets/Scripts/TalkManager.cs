using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "안녕하세요!,"반가워요"});
        talkData.Add(101, new string[] { "루도라고 해요", "반가워요" });
        talkData.Add(1000, new string[] { "나무상자다." });
        talkData.Add(1001, new string[] { "오래된 책상이다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}