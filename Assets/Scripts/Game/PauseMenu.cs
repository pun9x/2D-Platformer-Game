//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause() //Set Button Dừng lại
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home() //Set Button trở về menu chính
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Resume() //Set Button chơi tiếp
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart() //Set Button khởi động lại từ đầu scene level đang chơi
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
