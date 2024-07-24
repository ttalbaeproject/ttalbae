using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Main {get; private set;}
    void Start()
    {
        Main = this;
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
