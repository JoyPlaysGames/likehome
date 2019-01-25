using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TaskKind
{
    None = 0,
    WindowClean = 1,
    FloorMop = 2,
}

public class Task : MonoBehaviour
{
    public List<TaskConfig> taskConfig;

    private void Awake()
    {
        The.tasks = this;
    }

}


[Serializable]
public class TaskConfig
{
    public TaskKind kind;
    public Sprite icon;
    public GameObject prefab;
}
