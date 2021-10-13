using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{   
    public float y;

    public GameObject enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


           
            enemy.GetComponent<enemyscript2>().SafeField = false;
            enemy.GetComponent<enemyscript2>().SafeTiming = false;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            enemy.GetComponent<enemyscript2>().SafeField = false;

            //집 안이면 그냥 반환
            if (this.CompareTag("House"))
            {
                Debug.Log("house");
                return;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("FirstPersonController").GetComponent<ShelterInteract>().ClosetExitUI.SetActive(false);
                GameObject.Find("FirstPersonController").transform.localPosition = new Vector3(11.06f, y, -10.58f);             
                GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = true;
                GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = true;               
                GameObject.Find("FirstPersonController").GetComponent<ShelterInteract>().ClosetInside = false;
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<enemyscript2>().SafeField = true;
            
        }

    }

    
    
}
