using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour
{
    public GameObject[] Heart;
    public int Life;



    void Update()
    {
        if (Life < 1)
        {
            Destroy(Heart[0].gameObject);
        }
        else if (Life < 2)
        {
            Destroy(Heart[1].gameObject);
        }
        else if (Life < 3)
        {
            Destroy(Heart[2].gameObject);
        }

    }

    public void TakeDamage(int d)
    {
        Life -= d;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("DeadZone"))
        {
            TakeDamage(1);
        }

    }
}