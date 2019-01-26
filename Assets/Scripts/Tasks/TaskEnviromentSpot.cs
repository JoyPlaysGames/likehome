using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEnviromentSpot : MonoBehaviour
{
    public TaskKind kind;
    public GameObject taskActive;
    public GameObject taskIdle;
    public int taskId = -1;
    public float actionTime;
    float currentTime;

    private void Start()
    {
        taskIdle.SetActive(true);
        taskActive.SetActive(false);
    }
    public void SetActiveEnviromentTask(LevelTask levelTask)
    {
        TaskConfig taskConfig = The.tasks.GetTaskByKind(levelTask.taskKind);
        taskId = levelTask.id;
        actionTime = taskConfig.performingTime;
        taskIdle.SetActive(false);
        taskActive.SetActive(true);
    }

    public void FinishTask()
    {
        if (taskId == -1) return;
        taskIdle.SetActive(true);
        taskActive.SetActive(false);
        The.taskManager.FinishTask(taskId);
        taskId = -1;
    }   

    public void FailAndResetTask()
    {
        Debug.Log("FAIL-AND-RESET");
        taskId = -1;
        taskIdle.SetActive(true);
        taskActive.SetActive(false);
    }
}
