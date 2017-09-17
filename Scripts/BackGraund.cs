using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackGraund : MonoBehaviour
{

    //public MovieTexture movieTexture;
    //public RawImage movieRaw;

    public RawImage[] backGraundSpr;
    public Color[] bColor;

    public float changeTime = 15;   //切り替わるまでの時間
    
    private int counter = 0;        //whileの回数カウント
    private float timeLeap = 0;     //Whileが回った回数を0から1に変換された値を保持

    public int mode = 1;           //0=朝,1=夕方,2=夜,3=深夜

    bool isMid = false;            //深夜画像フラグ

    void Start()
    {
        /*
        //movieTexture = (MovieTexture)GetComponent<RawImage>().material.mainTexture;
        MovieTexture m = (MovieTexture)movieRaw.material.mainTexture;
        m.loop = true;
        m.Play();
	    */
        bColor = new Color[backGraundSpr.Length];
        /*
        for (int i = 0; i < backGraundSpr.Length; i++)
        {
            bColor[i] = backGraundSpr[i].color;
        }*/
    }

    void Update()
    {

        //for (int alpha = 0; alpha <= 255; alpha++)

        

        while(counter <= (changeTime*60))
        {
            float countLeap = counter / (changeTime * 60);//0～1化
            float alpha = 0;
            if(isMid)//深夜の画像から朝になる
            {
                alpha = Mathf.Lerp(0f, 1f, (countLeap - 1) * -1);
            }
            else
            {
                alpha = Mathf.Lerp(0f, 1f, countLeap);
            }
            backGraundSpr[mode].color = new Color(255f, 255f, 255f, alpha);
            
            counter++;
            return;
        }
        counter = 0;
        if (mode == (backGraundSpr.Length - 1) && !isMid)
        {
            backGraundSpr[mode - 1].color = new Color(255f, 255f, 255f, 0);
            backGraundSpr[0].color = new Color(255f, 255f, 255f, 255f);
            isMid = true;
            return;
        }
        else if(isMid)
        {
            mode = 1;
            isMid = false;
            return;
        }
        else
        {
            //Debug.Log("test");
            backGraundSpr[mode - 1].color = new Color(255f, 255f, 255f, 0);
            mode++;
        }
    }
}