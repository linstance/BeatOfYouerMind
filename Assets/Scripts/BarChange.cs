using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    public Button btn1;//버튼
    public Sprite inbtn1;//눌린 이미지
    public Sprite outbtn1;//안눌린 이미지

    public Button btn2;//버튼
    public Sprite inbtn2;//눌린 이미지
    public Sprite outbtn2;//안눌린 이미지

    public Button btn3;//버튼
    public Sprite inbtn3;//눌린 이미지
    public Sprite outbtn3;//안눌린 이미지

    public Button btn4;//버튼
    public Sprite inbtn4;//눌린 이미지
    public Sprite outbtn4;//안눌린 이미지

    private bool CheckBtn1 = false;
    private bool CheckBtn2 = false;
    private bool CheckBtn3 = false;
    private bool CheckBtn4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
}

    // Update is called once per frame
    void Update()
    {
        Key();
    }


    public void Key()
    {
        //첫번째 버튼
        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckBtn1 = true;
            if(CheckBtn1 == true)
            {
                btn1.GetComponent<Image>().sprite = inbtn1;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
              CheckBtn1 = false;
            if (CheckBtn1 == false)
            {
                btn1.GetComponent<Image>().sprite = outbtn1;
            }
        }

        //두번째 버튼
        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckBtn2 = true;
            if (CheckBtn2 == true)
            {
                btn2.GetComponent<Image>().sprite = inbtn2;
            }

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            CheckBtn2 = false;
            if (CheckBtn2 == false)
            {
                btn2.GetComponent<Image>().sprite = outbtn2;
            }
        }

        //세번째 버튼
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckBtn3 = true;
            if (CheckBtn3 == true)
            {
                btn3.GetComponent<Image>().sprite = inbtn3;
            }

        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            CheckBtn3 = false;
            if (CheckBtn3 == false)
            {
                btn3.GetComponent<Image>().sprite = outbtn3;
            }
        }

        //네번째 버튼
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckBtn4 = true;
            if (CheckBtn4 == true)
            {
                btn4.GetComponent<Image>().sprite = inbtn4;
            }

        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            CheckBtn4 = false;
            if (CheckBtn4 == false)
            {
                btn4.GetComponent<Image>().sprite = outbtn4;
            }
        }

    }

}
