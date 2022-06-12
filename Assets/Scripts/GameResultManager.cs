using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text ScoreUI;
    public Text MaxComboUI;
    public Image RankUI;

    // Start is called before the first frame update
    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        ScoreUI.text ="Score: " + (int) PlayerInformation.score;
        MaxComboUI.text = "Max Combo: " +  PlayerInformation.maxCombo;
        //리소스에서 비트 텍스트 파일을 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.SelectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //첫번째 줄과 두번째 줄 무시
        reader.ReadLine(); //첫번째 줄 읽기
        reader.ReadLine(); //두번쨰 줄 읽기
        //세번째 줄에 있는 점수 정보 받아오기 S점수 A점수 B점수 읽어오기
        string beatInformation = reader.ReadLine();
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[3]); //B점수
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]); //A점수
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[5]); //S점수

        if(PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }
        else if(PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if(PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
    }

    public void RePlay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
