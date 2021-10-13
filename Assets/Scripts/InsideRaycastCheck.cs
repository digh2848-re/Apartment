using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRaycastCheck : MonoBehaviour
{

    public bool insidesafe; // 문이 닫혀야 안에서 다른 층 누를수 있게
    public float interactDiastance;
    public GameObject buttonUItext;
    public GameObject buttonUI;

    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button10;
    public GameObject button11;

    public bool push;
    public bool checkimage;
    public bool pushcheck;
    // Start is called before the first frame update

    public void Safe()
    {
        insidesafe = !insidesafe;
    }

    public void ButtonPush()
    {
        push = !push;
    }



    void Start()
    {
        pushcheck = false;
        checkimage = true;
        insidesafe = true;
        push = false;
    }



    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, interactDiastance))
        {


            if (hit.collider.CompareTag("insidekey") && insidesafe)
            {
                if (checkimage)
                {
                    buttonUItext.SetActive(true); //층 누르기 자막 띄움
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    pushcheck = true;
                    checkimage = false;
                    insidesafe = false;
                    buttonUItext.SetActive(false); //층 누르기 자막 끔
                    buttonUI.SetActive(true);

                    //귀신이 탄상태에선 1층말고 다른버튼 동작 안되도록
                    if (GameObject.Find("FirstPersonController").GetComponent<interact>().WorldNum == 5)
                    {
                        button2.SetActive(false);
                        button3.SetActive(false);
                        button4.SetActive(false);
                        button5.SetActive(false);
                        button6.SetActive(false);
                        button7.SetActive(false);
                        button8.SetActive(false);
                        button9.SetActive(false);
                        button10.SetActive(false);
                        button11.SetActive(false);
                    }

                    //움직임 제어
                    GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = false;
                    GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = false;

                    //화면 커서 띄움
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                }
 
            }
         
        }

       

        //내부 엘리베이터 버튼 누를시 움직임 활성화 
        if (push&&pushcheck)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = true;
            GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = true;
            buttonUI.SetActive(false);
            buttonUItext.SetActive(false);
            checkimage = true;
            pushcheck = false;
        }

        if (!Physics.Raycast(ray, out hit, interactDiastance + 1f))
        {
            buttonUItext.SetActive(false);

        }

    }
}
