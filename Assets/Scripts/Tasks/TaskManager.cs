using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    int currentLevel = 0;
    public List<LevelConfig> levels;
    Dictionary<int, LevelTask> openTasks = new Dictionary<int, LevelTask>();

    LevelConfig level;
    public bool gameFinished = false;
    int currentTaskCount = 0;
    int maxTaskCount = 3;
    int currentTaskId = 0;

    private void Awake()
    {
        The.taskManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        level = levels[currentLevel];
        StartCoroutine(InstantiateTask());
    }

    IEnumerator InstantiateTask()
    {
        yield return new WaitForSeconds(0.3f);
        LevelTask task = GetUnfinishedTask();
        if(task != null)
        {
            openTasks.Add(currentTaskId, task);
            GameObject t = Instantiate(The.gameGui.taskItem, Vector3.zero, Quaternion.identity);
            t.transform.parent = The.gameGui.taskParent.transform;
            t.transform.localScale = Vector3.one;
            t.GetComponent<TaskContainer>().Sync(currentTaskId, task);
            currentTaskId++;
        }

        currentTaskCount++;
        if (currentTaskCount < maxTaskCount && task != null)
            StartCoroutine("InstantiateTask");
    }

    LevelTask GetUnfinishedTask()
    {
        foreach(LevelTask task in level.tasks)
        {
            if (!task.completed) return task;
        }
        return null;
    }

    public void FailTask(int id)
    {

    }
}

[Serializable]
public class LevelConfig
{
    public List<LevelTask> tasks;
    public float time;
}

[Serializable]
public class LevelTask
{
    public RecipieKind recipie;
    public TaskKind task;
    public int rewardPoints = 0;
    public float time;

    [HideInInspector]
    public bool completed;
}
