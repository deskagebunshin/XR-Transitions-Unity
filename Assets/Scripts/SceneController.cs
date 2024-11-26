using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneController : MonoBehaviour
{
    private SceneChanger sceneChanger;
    [SerializeField] private string [] levels;
    private int currentIndex = 0;
    private void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();

        // Load an initial scene
        sceneChanger.Initialize(levels[0]);
        currentIndex = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) OnButtonPreviousScene();
        if(Input.GetKeyDown(KeyCode.RightArrow)) OnButtonNextScene();
    }

    public void OnButtonNextScene()
    {
        currentIndex++;
        if (currentIndex >= levels.Length)
        {
            currentIndex = 0;
        }
        sceneChanger.ChangeScene(levels[currentIndex]);
    }

    public void OnButtonPreviousScene()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = levels.Length - 1;
        }
        sceneChanger.ChangeScene(levels[currentIndex]);
    }
}