using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    public GameObject[] pistons;
    public Vector3[] inPositions;
    public Vector3[] outPositions;

    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        ExtendPistons();
        RetractPistons();
    }

    private void RetractPistons()
    {
        if (timer > 1.5f && timer < 2.0f)
        {
            for (int i = 0; i < pistons.Length; i++)
            {
                pistons[i].transform.position = Vector3.MoveTowards(pistons[i].transform.position, inPositions[i], 0.05f);
            }
        }
    }

    private void ExtendPistons()
    {
        if (timer < 0.5f)
        {
            for (int i = 0; i < pistons.Length; i++)
            {
                pistons[i].transform.position = Vector3.MoveTowards(pistons[i].transform.position, outPositions[i], 0.05f);
            }
        }
    }

    public void Activate()
    {
        timer = 0;
    }
}