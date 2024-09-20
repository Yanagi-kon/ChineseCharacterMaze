using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject[] portals = new GameObject[16];
    private int j = 0;
    private bool portalFlag = false;
    void Start()
    {
        for (int i = 2; i < portals.Length; i++)
        {
            portals[i].SetActive(false);
        }
    }

    void Update()
    {
        if (j < portals.Length - 2 && !portals[j].activeInHierarchy && !portals[j + 1].activeInHierarchy && portalFlag)
        {
            j += 2;
            portalFlag = false;
            Debug.Log("╪сак");
        }
        if (portals[j] && portals[j + 1] && !portalFlag)
        {
            portals[j].SetActive(true);
            portals[j + 1].SetActive(true);
            portalFlag = true;
        }
        
    }
}
