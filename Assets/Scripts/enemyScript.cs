using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    // 길찾아 이동할 enemy
    NavMeshAgent nav;
    public GameObject target;

    private void Awake()
    { // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장 
        nav = GetComponent<NavMeshAgent>(); 

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.transform.position);
    }
}
