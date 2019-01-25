using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskContainer : MonoBehaviour
{

    public Image taskImage;
    public Image progressBar;
    public Text timeCounter;

    float startTime;
    float time;

    bool active = false;
    int id;

    public void Sync(int taskId, LevelTask task )
    {
        startTime = task.time;
        time = task.time;
        id = taskId;

        active = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            active = false;
            
        }
    }

    void UpdateProgressBar()
    {

    }
}
