using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject goal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Key")
        {
            goal.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
