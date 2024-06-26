using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public int score = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        score = 0;
    }

    void Update()
    {
        
    }
}
