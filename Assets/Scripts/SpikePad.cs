using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePad : MonoBehaviour
{
    public GameObject spikeTrap;
    public GameObject spike;
    public Vector3[] spikeDirections;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo"))
        {
            foreach (Vector3 direction in spikeDirections)
            {
                ActivateSpikes(direction);
            }
        }
    }

    private void ActivateSpikes(Vector3 direction)
    {
        GameObject newSpike = GameObject.Instantiate(spike);
        newSpike.transform.Rotate(Vector3.forward, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        newSpike.transform.position = spikeTrap.transform.position + direction;
        newSpike.GetComponent<Rigidbody>().velocity = direction * 10;
    }
}
