using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyscript2 : MonoBehaviour
{
    // 길찾아 이동할 enemy
    NavMeshAgent nav;

   

    public GameObject[] targets;

    public GameObject player;

    public float PlayerHeight;

    
    private float heightcheck;
    private int point = 0;
    public bool SafeTiming;
    public bool SafeField;
    // Start is called before the first frame update


    //순찰 함수
    public void next()

    {

        if (targets.Length == 0) return;

        


        nav.destination = targets[point].transform.position;
        // 순찰시 속도 및 가속도
        nav.speed = 2f;
        nav.acceleration = 2f;

        point = (point + 1) % targets.Length;

    }

    void Start()
    {
        heightcheck = 0f;
        SafeField = true;
        SafeTiming = true;
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장 
        nav = GetComponent<NavMeshAgent>();
        next();
    }

    // Update is called once per frame
    void Update()
    {
        

        heightcheck = player.transform.localPosition.y;

        //발각후 추적
        if((heightcheck> PlayerHeight-0.1f && heightcheck < PlayerHeight+0.1f) && SafeField)
        {
            nav.destination = player.transform.position;
            //발각후 추적속도
            nav.speed = 3.5f;
            nav.acceleration = 3.5f;
            
        }
        else //순찰
        {
            //은신처에 숨게되는 타이밍에 맞게 정지후 다시 수색
            if (!SafeTiming)
            {
               
                nav.speed = 0f;
                nav.acceleration = 0f;
                next();
                SafeTiming = true;
            }

            if (!nav.pathPending && nav.remainingDistance < 2f)
            {
               
                next();
            }
        }        
           
        

        
     
    }
}
