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
        //���ҽ����� ��Ʈ �ؽ�Ʈ ������ �ҷ�����
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.SelectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        //ù��° �ٰ� �ι�° �� ����
        reader.ReadLine(); //ù��° �� �б�
        reader.ReadLine(); //�ι��� �� �б�
        //����° �ٿ� �ִ� ���� ���� �޾ƿ��� S���� A���� B���� �о����
        string beatInformation = reader.ReadLine();
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[3]); //B����
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]); //A����
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[5]); //S����

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
