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

    public List<TaskEnviromentSpot> taskSpots;
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
        LevelTask levelTask = GetUnfinishedTask();
        if (levelTask != null)
        {
            openTasks.Add(levelTask.id, levelTask);
            GameObject t = Instantiate(The.gameGui.taskItem, Vector3.zero, Quaternion.identity);
            t.transform.parent = The.gameGui.taskParent.transform;
            t.transform.localScale = Vector3.one;
            t.GetComponent<TaskContainer>().Sync(levelTask);
            taskUiCards.Add(levelTask.id, t.GetComponent<TaskContainer>());
            currentTaskCount++;
            if(levelTask.taskKind != TaskKind.None)
            {
                CreateAndAssignTaskInScene(levelTask);
            }
        }
    }

    void CreateAndAssignTaskInScene(LevelTask levelTask)
    {
        List<TaskEnviromentSpot> listToUse = new List<TaskEnviromentSpot>();
        foreach(TaskEnviromentSpot enviromentSpot in taskSpots)
        {
            if (enviromentSpot.kind == levelTask.taskKind && enviromentSpot.taskId < 0) listToUse.Add(enviromentSpot);
        }
        Debug.Log(listToUse.Count);
        listToUse[UnityEngine.Random.Range(0, listToUse.Count)].SetActiveEnviromentTask(levelTask);

        //if (listToUse.Count == 0) Debug.LogError("TASKMANAGER: no task spots for needed task kind!");

        //listToUse[0].SetActiveEnviromentTask(levelTask);
    }

    LevelTask GetUnfinishedTask()
    {
        foreach(LevelTask task in level.tasks)
        {
            if (!task.completed && !openTasks.ContainsKey(task.id)) return task;
        }
        return null;
    }

    public void FailEnviromentTaskSpot(int id)
    {
        for(int i = 0; i < taskSpots.Count; i++)
        {
            if (taskSpots[i].taskId == id) taskSpots[i].FailAndResetTask();
            break;
        }
    }

    public void FailTask(int id)
    {
        openTasks[id].completed = true;
        score -= openTasks[id].failPoints;
        The.gameGui.levelScore.text = score.ToString();
        StartCoroutine(InstantiateTask(3.3f));
        CheckLevelFinished();
    }
    public void FinishTask(int id)
    {
        openTasks[id].completed = true;
        taskUiCards[id].Win();
        score += openTasks[id].rewardPoints;
        The.gameGui.levelScore.text = score.ToString();
        StartCoroutine(InstantiateTask(3.3f));
        CheckLevelFinished();
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

    void CheckLevelFinished()
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

    public void ConsumeRecipie(Dictionary<IngredientKind, int> items)
    {
        

        if(items == null || items.Count == 0)
        {
            BadRecipieConsumed();
            return;
        }
        RecipieKind r = The.recipies.DoesRecipieExistByIngredients(items);
		if (r != RecipieKind.None)
		{
			int id = IsRecipieAnActualTask(r);
			if (id >= 0)
			{
				FinishTask(id);
			}
			else
			{
				BadRecipieConsumed();
			}
		}
		else
		{
			BadRecipieConsumed();
		}
    }

    void BadRecipieConsumed()
    {
        Debug.Log("YO GIB BAD SHIT");
    }

    public int IsRecipieAnActualTask(RecipieKind kind)
    {
        foreach(LevelTask task in level.tasks)
        {
            if (task.recipieKind == kind && openTasks.ContainsKey(task.id)) return task.id;
        }
        return -1;
    }

    IEnumerator PrepareNextLevel()
    {
        yield return new WaitForSeconds(5f);
        The.gameGui.winPopup.SetActive(false);
        The.gameGui.losePoup.SetActive(false);
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
    public RecipieKind recipieKind;
    public TaskKind taskKind;
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
