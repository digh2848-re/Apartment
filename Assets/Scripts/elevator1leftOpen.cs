using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator1leftOpen : MonoBehaviour
{
    public bool leftopen = false;

    private float DelayTime = 4f;
    private float DelayTime2 = 2f;
    private float Timer;
    private float Timer2;
    // Start is called before the first frame update

    //최적화(불필요한 검색대신 캐쉬 레퍼런스를 쓰기위한 변수
    private GameObject playerObject;

    void Start()
    {
        Timer = 0f;
        Timer2 = 0f;

        playerObject = GameObject.Find("FirstPersonController");
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
            if (!GameObject.FindWithTag("elein").transform.GetComponent<heightMove>().movstop)
            {
                playerObject.GetComponent<InsideRaycastCheck>().checkimage = false;
                GameObject.FindWithTag("elein").transform.GetComponent<heightMove>().movstop = true;
            }

            if (transform.localPosition.x > 24.18f) //닫힌걸 열려고 할떄
            {
                transform.Translate(Vector3.forward * -Time.deltaTime * 0.25f, Space.World);
            }
            else
            {   //왼쪽문 열림
                transform.localPosition = new Vector3(24.16f, transform.localPosition.y, -28.86f);
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

            if (transform.localPosition.x < 24.49f) //열린 것이 자동적으로 닫힐떄
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 0.25f, Space.World);

                //문열기 활성화 키 딜레이
                if (!playerObject.GetComponent<interact>().delayerror &&
                    !transform.parent.parent.Find("elevator_inside").transform.GetComponent<heightMove>().mov)
                {


                    playerObject.GetComponent<interact>().Dcheck();
                }


                Timer2 += Time.deltaTime;
                //지연 2초
                if (Timer2 > DelayTime2)
                {
                    if (GameObject.FindWithTag("elein").transform.GetComponent<heightMove>().movstop)
                    {
                        playerObject.GetComponent<InsideRaycastCheck>().checkimage = true;
                        GameObject.FindWithTag("elein").transform.GetComponent<heightMove>().movstop = false;
                    }

                }
                

                
            }
            else
            {   //왼쪽문 닫힘
                transform.localPosition = new Vector3(24.51f, transform.localPosition.y, -29.51f);
                Timer2 = 0;

            }
        }

        //외부 문이 닫혀있을때만 레이캐스트 가능 
        if (transform.localPosition.x == 24.51f && transform.localPosition.z == -29.51f &&
            !GameObject.FindWithTag("elein").transform.GetComponent<heightMove>().mov &&
            playerObject.GetComponent<InsideRaycastCheck>().checkimage)
        {

            playerObject.GetComponent<InsideRaycastCheck>().insidesafe = true;
        }
        else
        {

            playerObject.GetComponent<InsideRaycastCheck>().insidesafe = false;
        }


    }


}


