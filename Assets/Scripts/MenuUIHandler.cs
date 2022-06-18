using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;
    [SerializeField] private GameObject worker;
    [SerializeField] private GameObject text;
    private bool isRainingMen = false;


    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveColor();

    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
            Application.Quit(); // original code to quit Unity player
    #endif
    }

    public void ChangeRain()
    {
        if (!isRainingMen)
        {
            isRainingMen = true;
            text.SetActive(true);
            StartCoroutine(RainMen());
        }
        else
        {
            text.SetActive(false);
            isRainingMen = false;
        }
    }

    IEnumerator RainMen()
    {
        while(isRainingMen)
        {
            Rigidbody workerRb = Instantiate(worker, new Vector3(UnityEngine.Random.Range(-10, 10), 10, 0), Quaternion.identity).GetComponent<Rigidbody>();
            workerRb.AddTorque(new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10)), ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
