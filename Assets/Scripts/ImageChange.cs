using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChange : MonoBehaviour
{
    public MeshRenderer ImageRenderer;
    public Texture oneTexture;
    public Texture twoTexture;
    public Texture threeTexture;
    public Texture fourTexture;
    public Texture fiveTexture;
    public Texture sixthTexture;
    public Texture sevenTexture;
    public Texture eightTexture;
    public Texture nineTexture;
    public Texture tenTexture;
    public Texture elevenTexture;
    
    private float Floorcheck;


    private float Timer;
    public bool FastChange;
    private int num;

    private CameraShake shake;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
        FastChange = false;
        shake = GameObject.Find("FirstPersonController").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        Floorcheck = GameObject.FindWithTag("elein").transform.localPosition.y;


        

        if (FastChange) //다른세계로 갈때 효과
        {
            StartCoroutine(shake.ShakeCamera());
            Timer += Time.deltaTime*2.0f;
            num = (int)Timer % 10; 
            
            switch (num)
            {

                case 1:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", fiveTexture);
                        break;
                    }
                case 2:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", sixthTexture);
                        break;
                    }
                case 3:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", sevenTexture);
                        break;
                    }
                case 4:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", eightTexture);
                        break;
                    }
                case 5:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", nineTexture);
                        break;
                    }
                case 6:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", tenTexture);
                        break;
                    }
                case 7:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", elevenTexture);
                        break;
                    }
                case 8:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", oneTexture);
                        break;
                    }
                case 9:
                    {
                        ImageRenderer.material.SetTexture("_MainTex", twoTexture);
                        break;
                    }
                

            }

        }
        else //현실세계에서 작동방식
        {
            //오차간격 0.03f 
            // 1층 3.67 2층 6.39 3층 9.07 4층 11.77 5층 14.46 6층 17.14 7층 19.86 8층 22.58 9층 25.28 10층 27.97 11층 30.68
            if (3.64f < Floorcheck && Floorcheck < 6.36f)
            {
                ImageRenderer.material.SetTexture("_MainTex", oneTexture);

            }
            else if (6.36f < Floorcheck && Floorcheck < 9.04f)
            {
                ImageRenderer.material.SetTexture("_MainTex", twoTexture);
            }
            else if (9.04f < Floorcheck && Floorcheck < 11.74f)
            {
                ImageRenderer.material.SetTexture("_MainTex", threeTexture);
            }
            else if (11.74f < Floorcheck && Floorcheck < 14.43f)
            {
                ImageRenderer.material.SetTexture("_MainTex", fourTexture);
            }
            else if (14.43f < Floorcheck && Floorcheck < 17.11f)
            {
                ImageRenderer.material.SetTexture("_MainTex", fiveTexture);
            }
            else if (17.11f < Floorcheck && Floorcheck < 19.83f)
            {
                ImageRenderer.material.SetTexture("_MainTex", sixthTexture);
            }
            else if (19.83f < Floorcheck && Floorcheck < 22.55f)
            {
                ImageRenderer.material.SetTexture("_MainTex", sevenTexture);
            }
            else if (22.55f < Floorcheck && Floorcheck < 25.25f)
            {
                ImageRenderer.material.SetTexture("_MainTex", eightTexture);
            }
            else if (25.25f < Floorcheck && Floorcheck < 27.94f)
            {
                ImageRenderer.material.SetTexture("_MainTex", nineTexture);
            }
            else if (27.94f < Floorcheck && Floorcheck < 30.65f)
            {
                ImageRenderer.material.SetTexture("_MainTex", tenTexture);
            }
            else if (30.65f < Floorcheck)
            {
                ImageRenderer.material.SetTexture("_MainTex", elevenTexture);
            }

        }

    }
}
