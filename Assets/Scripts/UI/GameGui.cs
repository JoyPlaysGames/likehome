using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public GameObject taskParent;
    public GameObject taskItem;


    public Text levelTimer;
    public Text levelScore;

    public Sprite recipieIcon;

    public GameObject winPopup;
    public GameObject losePoup;

    public RectTransform levelScoreProgress;

    float progWidth;
    float progHeight;

    // Start is called before the first frame update
    void Awake()
    {
        The.gameGui = this;
    }

    private void Start()
    {
		if (levelScoreProgress != null)
		{
			progWidth = levelScoreProgress.rect.width;
			progHeight = levelScoreProgress.rect.height;
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string FormatTime(float s)
    {
        var min = Mathf.Floor(s / 60);
        var sec = s - min * 60;
        return min.ToString() + ":" + sec.ToString();
    }
}
