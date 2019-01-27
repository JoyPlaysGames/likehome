using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

	public Animator houseAnimator;

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

    /// <summary>
    /// Create any yet unfinished task from levels tasklist and does a full setup for both classes, visuals, ui
    /// </summary>
    /// <param name="delay">Initial delay for doing that</param>
    /// <returns></returns>
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
    /// <summary>
    /// Finds a free stask spot in scene and assign selected level task to it
    /// </summary>
    /// <param name="levelTask">Currently selected level task</param>
    void CreateAndAssignTaskInScene(LevelTask levelTask)
    {
        List<TaskEnviromentSpot> listToUse = new List<TaskEnviromentSpot>();
        foreach(TaskEnviromentSpot enviromentSpot in taskSpots)
        {
            if (enviromentSpot.kind == levelTask.taskKind && enviromentSpot.taskId < 0) listToUse.Add(enviromentSpot);
        }
        listToUse[UnityEngine.Random.Range(0, listToUse.Count)].SetActiveEnviromentTask(levelTask);
    }

    LevelTask GetUnfinishedTask()
    {
        foreach(LevelTask task in level.tasks)
        {
            if (!task.completed && !openTasks.ContainsKey(task.id)) return task;
        }
        return null;
    }
    /// <summary>
    /// Resets enviroment task spot and marks it as done
    /// </summary>
    /// <param name="id">Task id from current level tasklist</param>
    public void FailEnviromentTaskSpot(int id)
    {
        for(int i = 0; i < taskSpots.Count; i++)
        {
            if (taskSpots[i].taskId == id)
            {
                taskSpots[i].FailAndResetTask();
                break;
            }
        }
    }

    /// <summary>
    /// Fails a task is timer has ran out to do it
    /// </summary>
    /// <param name="id">Task id from current level tasklist</param>
    public void FailTask(int id)
    {
        openTasks[id].completed = true;
        score -= openTasks[id].failPoints;
        The.gameGui.levelScore.text = score.ToString();
        StartCoroutine(InstantiateTask(3.3f));
        CheckLevelFinished();
    }

    /// <summary>
    /// Finish successful task, reward player and remove from current queue
    /// </summary>
    /// <param name="id">Task id from current level task list</param>
    public void FinishTask(int id)
    {
		houseAnimator.SetTrigger("MonsterFeed");
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
            levelTimeText.text = The.gameGui.FormatTime(Mathf.Round(levelTime));
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
				houseAnimator.SetTrigger("MonsterFeed");
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
		houseAnimator.SetTrigger("MonsterYack");
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
            taskUiCards = new Dictionary<int, TaskContainer>();
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
