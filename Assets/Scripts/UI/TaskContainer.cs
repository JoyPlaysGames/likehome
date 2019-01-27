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

    public RecipieItemPanel recipieItem;
    public GameObject recipieIemWrap;

    float startTime;
    float time;

    bool active = false;
    int id;
    float progWidth;
    float progHeight;

    public void Sync(LevelTask task )
    {
        blockFail.SetActive(false);
        blockWin.SetActive(false);

        startTime = task.time;
        time = task.time;
        id = task.id;

        rewardText.text = task.rewardPoints.ToString();

        if(task.taskKind != TaskKind.None)
        {
            taskImage.sprite = The.tasks.GetTaskByKind(task.taskKind).icon;
        } else
        {
            taskImage.sprite = The.gameGui.recipieIcon;
            RecipieConfig c = The.recipies.GetRecipie(task.recipieKind);
            foreach(RecipieRequirements r in c.reqirements)
            {
                RecipieItemPanel p = Instantiate(recipieItem, Vector3.zero, Quaternion.identity);
                p.transform.parent = recipieIemWrap.transform;
                p.transform.localScale = Vector3.one;
                p.count.text = r.count.ToString();
                p.icon.sprite = The.ingredients.GetIngredientConfig(r.ingredient).icon;
            }
        }

        progWidth = progressBar.rect.width;
        progHeight = progressBar.rect.height;
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
            progressBar.sizeDelta = new Vector2(progWidth * p / 100, progHeight);//new Vector2(p, 9.5f);
            timeCounter.text = Mathf.Round(time).ToString();

        }
        else
        {
            active = false;
            The.taskManager.FailTask(id);
            The.taskManager.FailEnviromentTaskSpot(id);
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
        StartCoroutine("Destroy");
        active = false;
        blockCasual.SetActive(false);
        blockWin.SetActive(true);
        
    }


    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(transform.gameObject);
    }
}
