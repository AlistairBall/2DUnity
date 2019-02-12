using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    private float y;
    private float x;
    //Public variable to store a reference to the player game object


    public Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        // offset = transform.position - target.transform.position;
        y = 1000f;
        x = 1000f;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player_controller.horizontal == true)
        {
            if (y == 1000f)
            {
                y = target.transform.position.y + 3f;
                if(PlayerPrefs.GetInt("Bool") == 1)
                {
                    y += 2f;
                }
            }
            transform.position = new Vector3(target.transform.position.x + 5.33f, y, target.transform.position.z * 2);
            
        }
        else
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + 5.23f, target.transform.position.z * 2);
        }
    }

  
}
