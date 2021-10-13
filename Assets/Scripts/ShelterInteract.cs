using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterInteract : MonoBehaviour
{
    public float interactDiastance;
    public GameObject ClosetEnterUI;
    public GameObject ClosetExitUI;


    public bool ClosetInside; //옷장안에 있는지?


    //최적화(불필요한 검색대신 캐쉬 레퍼런스를 쓰기위한 변수
    private GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        ClosetInside = false;

        playerObject = GameObject.Find("FirstPersonController");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDiastance))
        {

            if (hit.collider.CompareTag("closet") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 20.61f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

            if (hit.collider.CompareTag("closet2") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 18.1f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

            if (hit.collider.CompareTag("closet3") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 15.27f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

            if (hit.collider.CompareTag("closet4") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 23.32f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

            if (hit.collider.CompareTag("closet5") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 26f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

            if (hit.collider.CompareTag("closet6") && !ClosetInside)
            {
                ClosetEnterUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ClosetEnterUI.SetActive(false);
                    ClosetInside = true;
                    ClosetExitUI.SetActive(true);
                    playerObject.transform.localPosition = new Vector3(11.06f, 28.71f, -11.76f);
                    playerObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameObject.FindWithTag("MainCamera").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    playerObject.GetComponent<FirstPersonController>().cameraCanMove = false;
                    playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                }
            }

        }

        //아이템 UI띄운후 아이템 먹지않고 이동할 경우 raycast 거리 멀어질때 UI없애기
        if (!Physics.Raycast(ray, out hit, interactDiastance + 0.01f))
        {            
            ClosetEnterUI.SetActive(false);
        }
    }
}
