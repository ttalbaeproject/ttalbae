using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public float playTime;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        playTime += Time.deltaTime;
    }
}
