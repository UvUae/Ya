using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}
