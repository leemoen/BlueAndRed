using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject card;
    public Text total;
    public Text acard;
    public Text bcard;
    public Text ccard;
    public Text acarda;
    public Text acardb;
    public Text bcarda;
    public Text bcardb;
    public Text ccarda;
    public Text ccardb;
    public Text red;
    public Text blue;
    public Text redred;
    public Text redblue;
    public Text thiscard;
    public Text thiscardturn;
    public Text result;
    public Text autotimes;
    public Sprite[] cardcolor;

    private int ntotal = 0;
    private int nacard = 0;
    private int nbcard = 0;
    private int nccard = 0;
    private int nacarda = 0;
    private int nacardb = 0;
    private int nbcarda = 0;
    private int nbcardb = 0;
    private int nccarda = 0;
    private int nccardb = 0;
    private int nred = 0;
    private int nblue = 0;
    private int nredred = 0;
    private int nredblue = 0;
    private string nthiscard = "N/A";
    private string nthiscardturn = "N/A";
    private float nresult = 0.0f;

    private minicard carda = new minicard();
    private minicard cardb = new minicard();
    private minicard cardc = new minicard();
    private minicard[] cards = new minicard[3];
    private Card cardscript;
    private bool nextcard = true;
    class minicard
    {
        public Sprite front;
        public Sprite back;
    }
    private float timer = 1.0f;
    private bool turncard = false;
    private bool hidecard = false;
    // Use this for initialization
    void Awake()
    {
        //初始化3张卡
        carda.front = cardcolor[0];
        carda.back = cardcolor[0];
        cardb.front = cardcolor[0];
        cardb.back = cardcolor[1];
        cardc.front = cardcolor[1];
        cardc.back = cardcolor[1];
        //扔到袋子里
        cards[0] = carda;
        cards[1] = cardb;
        cards[2] = cardc;

        //获取卡片对象script
        cardscript = card.GetComponent<Card>();
        UpdateData();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (turncard)
        {
            if (!cardscript.turned)
            {
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    cardscript.ClickCard();
                    turncard = false;
                    hidecard = true;
                    timer = 1.0f;
                }
            }
        }
        if (hidecard)
        {

            if (cardscript.turned)
            {
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    NextCard();
                    hidecard = false;
                }
            }
        }
    }
    public void GetCard()
    {
        if (nextcard)
        {
            card.SetActive(true);
            ntotal++;
            //重置反面标识
            cardscript.turned = false;
            //随机抽卡
            int id = Random.Range(0, 2);
            
            //随机正反
            int fob = Random.Range(0, 2);
            if (fob == 0)//正
            {
                cardscript.front = cards[id].front;
                cardscript.back = cards[id].back;
                
            }
            else//反
            {
                cardscript.front = cards[id].back;
                cardscript.back = cards[id].front;
            }
            if (id == 0)//抽到卡1
            {
                nacard++;
                if (fob == 0)//正面
                {
                    nacarda++;
                    nred++;
                    nthiscard = "红色";
                    nthiscardturn = "红色";
                    nredred++;
                }
                else
                {
                    nacardb++;
                    nred++;
                    nthiscard = "红色";
                    nthiscardturn = "红色";
                    nredred++;
                }
            }
            else if (id == 1)//卡2
            {
                nbcard++;
                if (fob == 0)//正面
                {
                    nbcarda++;
                    nred++;
                    nthiscard = "红色";
                    nthiscardturn = "蓝色";
                    nredblue++;
                }
                else
                {
                    nbcardb++;
                    nblue++;
                    nthiscard = "蓝色";
                    nthiscardturn = "红色";
                }
            }
            else if (id == 2) //为了方便2333 抽到卡3
            {
                nccard++;
                if (fob == 0)//正面
                {
                    nccarda++;
                    nblue++;
                    nthiscard = "蓝色";
                    nthiscardturn = "蓝色";
                }
                else
                {
                    nccardb++;
                    nblue++;
                    nthiscard = "蓝色";
                    nthiscardturn = "蓝色";
                }
            }
            if (nred != 0)
            {
                nresult = (float)nredblue / (float)nred;
            }
            else
            {
                nresult = 0.0f;
            }
            
            UpdateData();
            card.GetComponent<Image>().sprite = cardscript.front;
            nextcard = false;
            turncard = true;
            timer = 1.0f;
        }
        else
        {
            
            if (turncard)
            {
                timer = 0;
                
            }
            else if(!turncard && !cardscript.turned)
            {
                card.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                cardscript.turned = true;
            }
            else if (hidecard)
            {
                timer = 0;
            }
                
        }
    }
    public void NextCard()
    {
        if (cardscript.turned)
        {
            nextcard = true;
            card.SetActive(false);
        }
    }
    public void ResetGame()
    {
        ntotal = 0;
        nacard = 0;
        nbcard = 0;
        nccard = 0;
        nacarda = 0;
        nacardb = 0;
        nbcarda = 0;
        nbcardb = 0;
        nccarda = 0;
        nccardb = 0;
        nred = 0;
        nblue = 0;
        nredred = 0;
        nredblue = 0;
        nthiscard = "N/A";
        nthiscardturn = "N/A";
        nresult = 0.0f;
        UpdateData();
    }
    public void UpdateData()
    {
         total.text= "抽卡总次数：" + ntotal;
         acard.text= "抽卡 A 次数：" + nacard;
         bcard.text= "抽卡 B 次数：" + nbcard;
         ccard.text= "抽卡 C 次数：" + nccard;
         acarda.text= "抽卡 A 正面 次数：" + nacarda;
         acardb.text= "抽卡 A 背面 次数：" + nacardb;
         bcarda.text= "抽卡 B 正面 次数：" + nbcarda;
         bcardb.text= "抽卡 B 背面 次数：" + nbcardb;
         ccarda.text= "抽卡 C 正面 次数：" + nccarda;
         ccardb.text= "抽卡 C 背面 次数：" + nccardb;
         red.text= "抽出 红色 次数：" + nred;
         blue.text= "抽出 蓝色 次数：" + nblue;
         redred.text= "翻面 红色 次数：" + nredred;
         redblue.text= "翻面 蓝色  次数：" + nredblue;
         thiscard.text= "抽卡情况：" + nthiscard;
         thiscardturn.text= "翻面情况：" + nthiscardturn;
         result.text= "蓝卡概率 " + nresult;
    } 

}
