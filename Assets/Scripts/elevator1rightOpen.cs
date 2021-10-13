using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator1rightOpen : MonoBehaviour
{
    public bool rightopen = false;
    private float DelayTime = 4f;
    private float Timer;

    
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
    }

    public void RightOpen()
    {
        rightopen = !rightopen;
    }



    // Update is called once per frame
    void Update()
    {
        

        if (rightopen) // 트리거 발생
        {
            if (transform.localPosition.x < 23.85f) //닫힌걸 열려고 할떄
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 0.25f, Space.World);
            }
            else
            {   //오른쪽문 열림
                transform.localPosition = new Vector3(23.87f, transform.localPosition.y, -28.41f);
                
                Timer += Time.deltaTime;
                //지연 2초
                if(Timer> DelayTime)
                {
                    rightopen = !rightopen;
                }

               
            }
        }
        else
        {   
            //반복적인 딜레이를 위한 초기화
            Timer = 0f;

            if (transform.localPosition.x > 23.54f) //열린 것이 자동적으로 닫힐떄
            {
                transform.Translate(Vector3.forward * -Time.deltaTime * 0.25f, Space.World);

                
                
            }
            else
            {   //오른쪽문 닫힘
                transform.localPosition = new Vector3(23.52f, transform.localPosition.y, -27.81f);
                
            }
        }
    }

}

    
