using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class interact : MonoBehaviour
{
    



    public float interactDiastance;
    public GameObject elevator1UI;
    public GameObject UIOFF;
    public GameObject DoorOpenUI;
    public GameObject DoorCloseUI;
    public GameObject KeyUI;
    public GameObject Key;
    public GameObject ComputerUI;
    public GameObject ComputerScreen;
    


    private string tagg;
    private float Inheight;
    private float Outheight;
    
    private int floorIs;  //몇층에서 눌렀는지?
    private bool equalcheck; // 같은 층인지?
    public bool delayerror; // 문이 열릴때까지 F키 동작안되게
    public int floornum; //내부에서 몇층 가는지?


    // 다른세계로 가기위한 조건 수(총 5번)
    public int WorldNum;

    // 키를 획득했는지 조건
    private bool KeyCheck;

    // UI자동적으로 사라지기 위한 딜레이
    private float Timer;
    private float DelayTime = 7f;
    private bool Delay;


    //최적화(불필요한 검색대신 캐쉬 레퍼런스를 쓰기위한 변수
    private GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        equalcheck = false;
        floorIs = 0;
        delayerror = true;
        floornum = 0;

        WorldNum = 0;

        KeyCheck = false;
        Timer = 0;
        Delay = true;



        playerObject = GameObject.Find("FirstPersonController");
    }


    public void Echeck()
    {

        equalcheck = !equalcheck;
    }

    public void Dcheck()
    {

        delayerror = true; //끝

    }

    public void changingss(int x)
    {
        floornum = x;
    }

    public void PlayerMove()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerObject.GetComponent<FirstPersonController>().cameraCanMove = true;
        playerObject.GetComponent<FirstPersonController>().playerCanMove = true;
    }

    // Update is called once per frame
    void Update()
    {

       

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;


        Timer += Time.deltaTime;
        //지연 2초
        if (Timer > DelayTime)
        {
            DoorCloseUI.SetActive(false);
            Timer = 0;
            Delay = true;
        }


        if (Physics.Raycast(ray, out hit, interactDiastance))
        {

            
            //현관문 레이캐스트
            if (hit.collider.CompareTag("Door")&& Delay)
            {
                DoorOpenUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (KeyCheck)
                    {
                        hit.collider.transform.GetComponent<DoorScript>().ChangeDoorState();  //문열리는 스크립트
                        DoorOpenUI.SetActive(false);
                    }
                    else{
                        DoorOpenUI.SetActive(false);
                        DoorCloseUI.SetActive(true);
                        Delay = false;

                    }
                   
                }
            }

            //열쇠 오브젝트 레이캐스트
            if (hit.collider.CompareTag("Key"))
            {
                KeyUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    KeyUI.SetActive(false);
                    Key.SetActive(false);
                    KeyCheck = true;
                }
            }

            //컴퓨터 오브젝트 레이캐스트
            if (hit.collider.CompareTag("computer"))
            {
                ComputerUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ComputerUI.SetActive(false);
                    ComputerScreen.SetActive(true);

                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;

                    //화면 커서 띄움
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }


            //1층 활성화
            if ((hit.collider.CompareTag("elevator1floor") && delayerror) || floornum == 1)
            {

                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 1)
                {
                    //귀신이 탄 상태에서 1층을 누르면 격하게 흔들리거나 계기판이 고장난다
                    if(floornum == 1 && WorldNum == 5)
                    {

                        UIOFF.SetActive(false);
                        GameObject.FindWithTag("Display").transform.GetComponent<ImageChange>().FastChange = true;
                        floornum = 0;
                        return;
                    }

                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator1floor";

                    delayerror = false; //딜레이 시작



                    elevator1UI.SetActive(false);
                    Outheight = 3.67f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;


                    //엘리베이터가 같은 위치가 아닐떄
                    if (Outheight != Inheight)
                    {


                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else //같은 위치일때
                    {
                        equalcheck = !equalcheck;
                    }


                }
            }

            //2층 활성화
            if ((hit.collider.CompareTag("elevator2floor") && delayerror) || floornum == 2)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 2)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator2floor";
                    delayerror = false; //딜레이 시작

                    Debug.Log("2층 활성화 키 수");

                    elevator1UI.SetActive(false);
                    Outheight = 6.39f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else
                    {
                        equalcheck = !equalcheck;
                    }


                }
            }

            //3층 활성화 조건
            if ((hit.collider.CompareTag("elevator3floor") && delayerror) || floornum == 3)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 3)
                {

                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator3floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 9.07f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else
                    {
                        equalcheck = !equalcheck;
                    }
                }
            }

            //4층 활성화 조건
            if ((hit.collider.CompareTag("elevator4floor") && delayerror) || floornum == 4)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 4)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화

                    tagg = "elevator4floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);


                    Outheight = 11.77f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else
                    {
                        equalcheck = !equalcheck;
                    }
                }
            }

            //5층 활성화 조건
            if ((hit.collider.CompareTag("elevator5floor") && delayerror) || floornum == 5)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 5)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator5floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 14.46f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else
                    {
                        equalcheck = !equalcheck;
                    }
                }
            }

            //6층 활성화 조건
            if ((hit.collider.CompareTag("elevator6floor") && delayerror) || floornum == 6)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 6)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator6floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 17.14f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);
                    }
                    else
                    {
                        equalcheck = !equalcheck;
                    }
                }
            }

            //7층 활성화 조건
            if ((hit.collider.CompareTag("elevator7floor") && delayerror) || floornum == 7)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 7)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator7floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 19.86f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);

                    }
                    else
                    {
                        equalcheck = !equalcheck;

                    }
                }
            }

            //8층 활성화 조건
            if ((hit.collider.CompareTag("elevator8floor") && delayerror) || floornum == 8)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 8)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator8floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 22.58f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);

                    }
                    else
                    {
                        equalcheck = !equalcheck;

                    }
                }
            }

            //9층 활성화 조건
            if ((hit.collider.CompareTag("elevator9floor") && delayerror) || floornum == 9)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 9)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator9floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 25.28f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);

                    }
                    else
                    {
                        equalcheck = !equalcheck;

                    }
                }
            }

            //10층 활성화 조건
            if ((hit.collider.CompareTag("elevator10floor") && delayerror) || floornum == 10)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 10)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator10floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 27.97f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);

                    }
                    else
                    {
                        equalcheck = !equalcheck;

                    }
                }
            }

            //11층 활성화 조건
            if ((hit.collider.CompareTag("elevator11floor") && delayerror) || floornum == 11)
            {
                elevator1UI.SetActive(true);



                if (Input.GetKeyDown(KeyCode.F) || floornum == 11)
                {
                    //외부에서 엘리베이터 층을 누른 것이면 worldnum를 초기화 시킴
                    if (floornum == 0)
                    {
                        WorldNum = 0;
                    }

                    //귀신이 탄 상태에서는 다른 버튼이 동작하지않음
                    if (WorldNum == 5)
                    {
                        return;
                    }

                    floornum = 0; // 한번만 이동 후 초기화
                    tagg = "elevator11floor";
                    delayerror = false; //딜레이 시작
                    elevator1UI.SetActive(false);
                    Outheight = 30.68f;

                    //엘리베이터의 현재 높이
                    Inheight = hit.collider.transform.parent.parent.Find("elevator_inside").
                                transform.localPosition.y;

                    //엘리베이터가 위에 있을때
                    if (Outheight != Inheight)
                    {
                        //인자 전달 및 움직임
                        hit.collider.transform.parent.parent.Find("elevator_inside").
                            transform.GetComponent<heightMove>().StartMov(Outheight);

                    }
                    else
                    {
                        equalcheck = !equalcheck;

                    }
                }
            }





            /*
            // outside elevator 1floor raycast 
            if (hit.collider.CompareTag("elevator1floor"))
            {
                elevator1UI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // outside left door
                    hit.collider.transform.GetComponent<elevator1leftOpen>().LeftOpen(); 

                    // outside right door
                    hit.collider.transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();
                    elevator1UI.SetActive(false);

                    // inside left door
                    hit.collider.transform.parent.parent.Find("elevator_inside").Find("leftdoorIn").
                        transform.GetComponent<EINleftdoor>().LeftOpen();

                    hit.collider.transform.parent.parent.Find("elevator_inside").Find("rightdoorIn").
                        transform.GetComponent<EINrightdoor>().RightOpen();

                }
            }
            */


        }


        if (equalcheck)
        {
            elevator1UI.SetActive(false);

            //문열리는 스크립트
            switch (tagg)
            {
                case "elevator1floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }
                    

                    GameObject.FindWithTag("elevator1floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator1floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator2floor":

                    //다른 세계로 가는 4번째 관문
                    if (WorldNum == 3)
                    {
                        WorldNum = 4;
                    }
                    else
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator2floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator2floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator3floor":

                    //다른 세계로 가는 2번째 관문
                    if (WorldNum==1)
                    {
                        WorldNum = 2;
                    }
                    else
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator3floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator3floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator4floor":

                    //다른 세계로 가는 마지막 관문
                    if (WorldNum == 4)
                    {
                        WorldNum = 5;
                    }
                    else
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator4floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator4floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator5floor":

                    //다른 세계로 가는 3번째 관문
                    if (WorldNum == 2)
                    {
                        WorldNum = 3;
                    }
                    else
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator5floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator5floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator6floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator6floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator6floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator7floor":
                    
                    //다른 세계로 가는 첫번째 관문
                    WorldNum = 1;

                    GameObject.FindWithTag("elevator7floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator7floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator8floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator8floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator8floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator9floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator9floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator9floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator10floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator10floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator10floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                case "elevator11floor":

                    // 다른세계로 가는 조건 도중에 관련되지않은 층으로 가면 처음으로 초기화
                    if (WorldNum >= 1)
                    {
                        WorldNum = 0;
                    }

                    GameObject.FindWithTag("elevator11floor").transform.parent.Find("leftdoorOut").transform.GetComponent<elevator1leftOpen>().LeftOpen();

                    // outside right door
                    GameObject.FindWithTag("elevator11floor").transform.parent.Find("rightdoorOut").transform.GetComponent<elevator1rightOpen>().RightOpen();

                    elevator1UI.SetActive(false);

                    break;
                default:
                    break;
            }

            // inside left door
            GameObject.FindWithTag("elevator1floor").transform.parent.parent.Find("elevator_inside").Find("leftdoorIn").
            transform.GetComponent<EINleftdoor>().LeftOpen();

            GameObject.FindWithTag("elevator1floor").transform.parent.parent.Find("elevator_inside").Find("rightdoorIn").
            transform.GetComponent<EINrightdoor>().RightOpen();

            equalcheck = !equalcheck;
            tagg = null;


        }

        //아이템 UI띄운후 아이템 먹지않고 이동할 경우 raycast 거리 멀어질때 UI없애기
        if (!Physics.Raycast(ray, out hit, interactDiastance + 0.01f))
        {
            elevator1UI.SetActive(false);
            DoorOpenUI.SetActive(false);
            KeyUI.SetActive(false);
            ComputerUI.SetActive(false);           
        }

    }



}