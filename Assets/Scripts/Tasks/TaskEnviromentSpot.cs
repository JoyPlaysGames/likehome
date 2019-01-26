using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEnviromentSpot : MonoBehaviour
{
    public TaskKind kind;
    public GameObject taskActive;
    public GameObject taskIdle;
    public int taskId;

    private void Start()
    {
        taskIdle.SetActive(true);
        taskActive.SetActive(false);
    }
    public void SetActive(int id)
    {
        taskId = id;
        taskIdle.SetActive(false);
        taskActive.SetActive(true);
    }

    public void FinishTask()
    {
        taskIdle.SetActive(true);
        taskActive.SetActive(false);
        The.taskManager.FinishTask(taskId);
    }   
}
