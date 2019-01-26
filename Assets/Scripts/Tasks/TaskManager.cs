using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    int score = 0;

    int currentLevel = 0;
    public List<LevelConfig> levels;
    Dictionary<int, LevelTask> openTasks = new Dictionary<int, LevelTask>();

    LevelConfig level;

    public bool gameFinished = false;
    int currentTaskCount = 0;
    int maxTaskCount = 3;
    int currentTaskId = 0;

    bool active = true;

    float levelTime;
    Text levelTimeText;

    private void Awake()
    {
        The.taskManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        The.gameGui.levelScore.text = score.ToString();
        StartLevel();
    }

    void StartLevel()
    {
        level = levels[currentLevel];
        for(int i = 0; i < level.tasks.Count; i++)
        {
            level.tasks[i].id = i;
        }
        levelTime = level.time;
        levelTimeText = The.gameGui.levelTimer;
        StartCoroutine(InstantiateTask());
    }

    IEnumerator InstantiateTask()
    {
        yield return new WaitForSeconds(0.3f);
        LevelTask task = GetUnfinishedTask();
        if (task != null)
        {
            openTasks.Add(task.id, task);
            GameObject t = Instantiate(The.gameGui.taskItem, Vector3.zero, Quaternion.identity);
            t.transform.parent = The.gameGui.taskParent.transform;
            t.transform.localScale = Vector3.one;
            t.GetComponent<TaskContainer>().Sync(task);
            currentTaskCount++;
        }
        else
        {
            ProcessLevelEnd();
        }
        if (currentTaskCount < maxTaskCount && task != null)
            StartCoroutine("InstantiateTask");

    }

    LevelTask GetUnfinishedTask()
    {
        foreach(LevelTask task in level.tasks)
        {
            if (!task.completed && !openTasks.ContainsKey(task.id)) return task;
        }
        return null;
    }

    public void FailTask(int id)
    {
        openTasks[id].completed = true;
        score -= openTasks[id].failPoints;
        The.gameGui.levelScore.text = score.ToString();
    }

    private void FixedUpdate()
    {
        if (!active) return; 

        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            levelTimeText.text = Mathf.Round(levelTime).ToString();
        }
        else
        {

        }
    }

    void ProcessLevelEnd()
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
    public int failPoints = 0;
    public float time;

    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public int id;
}
