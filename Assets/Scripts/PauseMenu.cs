using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool settingopen = false;

    public GameObject pauseUI;
    public GameObject settingUI;
    public AudioMixer audioMixer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !settingopen)
            {
                Resume();
            }
            else if (GameIsPaused && settingopen)
            {
                settingClose();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // 플레이어 움직임 및 카메라 작동
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = true;
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = true;

        // 화면 커서 없어짐
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        // 플레이어 움직임 및 카메라 멈춤
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = false;
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = false;

        //화면 커서 띄움
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void setting()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
        settingopen = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }


    public void SetBGVolume(float BGVolume)
    {
        audioMixer.SetFloat("BGVolume", BGVolume);
    }

    public void SetSFXVolume(float SFXVolume)
    {
        audioMixer.SetFloat("SFXVoulme", SFXVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void settingClose()
    {
        settingUI.SetActive(false);

        // 플레이어 움직임 및 카메라 작동
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().cameraCanMove = true;
        GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>().playerCanMove = true;

        // 화면 커서 없어짐
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingopen = false;
        GameIsPaused = false;
    }

}
