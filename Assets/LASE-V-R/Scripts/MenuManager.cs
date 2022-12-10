using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string previousLevel;

    private TMP_Text clockText;
    private GameObject prevLevelButton;

    private void Start()
    {
        prevLevelButton = GameObject.Find("Previous Level Button");
        prevLevelButton.SetActive(previousLevel != "");
        clockText = GameObject.Find("Clock Value").GetComponent<TMP_Text>();
    }

    public void LoadPreviousScene()
    {
        if (previousLevel != null)
            SceneManager.LoadScene(previousLevel);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        // Debug.Log(DateTime.Now.ToString("HH:mm:ss"));
        clockText.text = DateTime.Now.ToString("HH\\hmm");

    }
}
