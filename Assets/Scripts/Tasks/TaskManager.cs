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
    Dictionary<int, TaskContainer> taskUiCards = new Dictionary<int, TaskContainer>();

    LevelConfig level;

    public bool gameFinished = false;
    int currentTaskCount = 0;
    int maxTaskCount = 3;

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
        StartCoroutine(InstantiateTask(0.3f));
        StartCoroutine(InstantiateTask(0.6f));
        StartCoroutine(InstantiateTask(0.9f));
    }

    IEnumerator InstantiateTask(float delay)
    {
        yield return new WaitForSeconds(delay);
        LevelTask task = GetUnfinishedTask();
        if (task != null)
        {
            openTasks.Add(task.id, task);
            GameObject t = Instantiate(The.gameGui.taskItem, Vector3.zero, Quaternion.identity);
            t.transform.parent = The.gameGui.taskParent.transform;
            t.transform.localScale = Vector3.one;
            t.GetComponent<TaskContainer>().Sync(task);
            taskUiCards.Add(task.id, t.GetComponent<TaskContainer>());
            currentTaskCount++;
        }
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
        StartCoroutine(InstantiateTask(3.3f));
        CheckLEvelFinished();
    }
    public void FinishTask(int id)
    {
        openTasks[id].completed = true;
        taskUiCards[id].Win();
        score += openTasks[id].rewardPoints;
        The.gameGui.levelScore.text = score.ToString();
        StartCoroutine(InstantiateTask(3.3f));
        CheckLEvelFinished();
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

    void CheckLEvelFinished()
    {
        bool gotUnfinishedTasks = false;
        foreach(KeyValuePair<int, LevelTask> task in openTasks)
        {
            if (!task.Value.completed) gotUnfinishedTasks = true;
        }

        if((GetUnfinishedTask() == null && !gotUnfinishedTasks) || levelTime < 0)
        {
            active = false;
            ProcessLevelEnd();
        }
    }

    void ProcessLevelEnd()
    {
        if(score >= level.minScoreToWin)
        {
            The.gameGui.winPopup.SetActive(true);
        }
        else
        {
            The.gameGui.losePoup.SetActive(true);
        }
        StartCoroutine("PrepareNextLevel");
    }

    IEnumerator PrepareNextLevel()
    {
        yield return new WaitForSeconds(5f);
        The.gameGui.winPopup.SetActive(false);
        The.gameGui.winPopup.SetActive(false);
        currentLevel++;
        if(currentLevel >= levels.Count)
        {
            Debug.Log("GAME ENDED!!!!!");
        }
        else
        {
            openTasks = new Dictionary<int, LevelTask>();
            score = 0;
            active = true;
            StartLevel();
        }

    }
}

[Serializable]
public class LevelConfig
{
    public float time;
    public int minScoreToWin;
    public List<LevelTask> tasks;
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
    [HideInInspector]
    public LevelTask t;
}
