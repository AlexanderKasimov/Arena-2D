using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUIObject;

    public bool isPaused;

    private PlayerScript player;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        player = FindObjectOfType<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !player.isDead)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }

    private void Resume()
    {        
        pauseUIObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        isPaused = true;
        pauseUIObject.SetActive(true);
        Time.timeScale = 0f;
    }
        
    public void OnResume()
    {
        Resume();
    }

    public void OnExit()
    {
        Application.Quit();
    }

}
