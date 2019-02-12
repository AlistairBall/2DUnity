using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public Button Button;
    public GameObject player;


    // Use this for initialization
    void Start () {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
	
public void TaskOnClick()
    {
        if(gameObject.tag == "MainMenu")
        {
            SceneManager.LoadScene("Main menu");
        }
        else if(gameObject.tag == "Load")
        {
            PlayerPrefs.SetInt("Bool", 1);
            player_controller.horizontal = true;
            SceneManager.LoadScene("Level");
        }
        else if(gameObject.tag == "Start")
        {
            PlayerPrefs.SetInt("Bool", 0);
            player_controller.horizontal = false;
            SceneManager.LoadScene("Level");
        }
       
    }
}
