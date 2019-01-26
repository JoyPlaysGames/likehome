using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskContainer : MonoBehaviour
{

    public Image taskImage;
    public RectTransform progressBar;
    public Text timeCounter;
    public Text rewardText;

    public GameObject blockCasual;
    public GameObject blockFail;
    public GameObject blockWin;

    float startTime;
    float time;

    bool active = false;
    int id;

    public void Sync(LevelTask task )
    {
        blockFail.SetActive(false);
        blockWin.SetActive(false);

        startTime = task.time;
        time = task.time;
        id = task.id;

        rewardText.text = task.rewardPoints.ToString();

        if(task.task != TaskKind.None)
        {
            taskImage.sprite = The.tasks.GetTaskByKind(task.task).icon;
        } else
        {
            taskImage.sprite = The.gameGui.recipieIcon;
        }


        active = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if(time > 0)
        {
            time -= Time.deltaTime;

            float p = time * 100 / startTime;
            progressBar.sizeDelta = new Vector2(p, 9.5f);
            timeCounter.text = Mathf.Round(time).ToString();

        }
        else
        {
            active = false;
            The.taskManager.FailTask(id);
            Fail();
        }
    }


    void Fail()
    {
        blockCasual.SetActive(false);
        blockFail.SetActive(true);
        StartCoroutine("Destroy");
    }
    public void Win()
    {
        active = false;
        blockCasual.SetActive(false);
        blockWin.SetActive(true);
        StartCoroutine("Destroy");
    }


    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(transform.gameObject);
    }
}
