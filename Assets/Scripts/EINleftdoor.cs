using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EINleftdoor : MonoBehaviour
{
    public bool leftopen = false;

    private float DelayTime = 4f;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
    }

    public void LeftOpen()
    {
        leftopen = !leftopen;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftopen)
        {
            if (transform.localPosition.x > 29.471f) //닫힌걸 열려고 할떄
            {
                transform.Translate(Vector3.forward * -Time.deltaTime * 0.25f, Space.World);
            }
            else
            {   //왼쪽문 열림
                transform.localPosition = new Vector3(29.451f, transform.localPosition.y, 4.671f);
                // StartCoroutine("Leftclose");
                Timer += Time.deltaTime;
                //지연 2초
                if (Timer > DelayTime)
                {
                    leftopen = !leftopen;
                }
            }
        }
        else
        {
            //반복적인 딜레이를 위한 초기화
            Timer = 0f;

            if (transform.localPosition.x < 30.041f) //열린 것이 자동적으로 닫힐떄
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 0.25f, Space.World);

                
            }
            else
            {   //왼쪽문 닫힘
                transform.localPosition = new Vector3(30.061f, transform.localPosition.y, 4.323f);
                
            }
        }
    }


}


