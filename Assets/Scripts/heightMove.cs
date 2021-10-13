using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heightMove : MonoBehaviour
{
    public bool mov;
    private float Outheight;
    private float Inheight;
    public bool movstop;

    //최적화(불필요한 검색대신 캐쉬 레퍼런스를 쓰기위한 변수
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        movstop = false;
        mov = false;
        Outheight = 6.39f;

        playerObject = GameObject.Find("FirstPersonController");
    }
    public void StartMov(float x)
    {
        mov = !mov;
        Outheight = x;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Inheight = transform.localPosition.y;
       

        if (mov)
        {
            movstop = true;
            playerObject.GetComponent<InsideRaycastCheck>().checkimage = false;
            playerObject.GetComponent<InsideRaycastCheck>().insidesafe = false;

            if (Outheight > Inheight)
            {
                playerObject.transform.Translate(Vector3.up * Time.deltaTime * 0.05f, Space.World);

                // 엘리베이터 원하는 위치로 올라감
                transform.Translate(Vector3.up * Time.deltaTime * 0.9f, Space.World);

                if (Outheight - 0.1f < Inheight)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Outheight, transform.localPosition.z);

                    mov = !mov;

                    playerObject.GetComponent<InsideRaycastCheck>().checkimage = false;
                    playerObject.GetComponent<InsideRaycastCheck>().insidesafe = false;
                    playerObject.GetComponent<interact>().Echeck();
                }
            }
            else if (Outheight < Inheight)
            {
                playerObject.transform.Translate(Vector3.down * Time.deltaTime * 0.9f, Space.World);
                transform.Translate(Vector3.up * -Time.deltaTime * 0.9f, Space.World);

                if (Outheight + 0.1f > Inheight)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Outheight, transform.localPosition.z);

                    mov = !mov;
                    playerObject.GetComponent<InsideRaycastCheck>().checkimage = false;
                    playerObject.GetComponent<InsideRaycastCheck>().insidesafe = false;
                    playerObject.GetComponent<interact>().Echeck();
                }
            }

        }
        else
        {
            // 엘리베이터 멈출때 다시 버튼누르는창 띄움

            playerObject.GetComponent<InsideRaycastCheck>().push = false;
        }


        

    }
}
