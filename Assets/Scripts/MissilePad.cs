using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePad : MonoBehaviour
{
    public GameObject missileTrap;
    public GameObject missile;
    public Vector3[] missileDirections;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo"))
        {
            foreach (Vector3 direction in missileDirections)
            {
                GameObject newMissile = GameObject.Instantiate(missile);
                newMissile.transform.Rotate(Vector3.forward, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                newMissile.transform.position = missileTrap.transform.position + direction;
                if (other.CompareTag("PlayerOne") && GameObject.FindGameObjectWithTag("PlayerTwo") != null)
                {
                    newMissile.GetComponent<MissileController>().playerToTarget = GameObject.FindGameObjectWithTag("PlayerTwo").transform;
                } else
                {
                    newMissile.GetComponent<MissileController>().playerToTarget = GameObject.FindGameObjectWithTag("PlayerOne").transform;
                }
            }
        }

        Debug.Log(other.tag);
    }
}
