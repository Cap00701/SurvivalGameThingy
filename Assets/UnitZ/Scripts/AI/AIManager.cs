//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

/// <summary>
// This class made for reducing call of FindGameObjectsWithTag function in every AI objects
// FindGameObjectsWithTag is too much cost in update loop.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AIManager : MonoBehaviour
{
    public Dictionary<string, TargetCollector> TargetList = new Dictionary<string, TargetCollector>();
    public int TargetTypeCount = 0;
    public float UpdateInterval = 0.1f;
    private float timeTmp;
    public string PlayerTag = "Player";

    void Start()
    {
        TargetList = new Dictionary<string, TargetCollector>();
    }

    [HideInInspector]
    public float scaleSize = 10;
    [HideInInspector]
    public Vector3 MapOffset;
    [HideInInspector]
    public float MapSize;

    public Vector3 GetPositionFromIndex(int index)
    {
        float z = index / MapSize;
        float x = index % MapSize;
        return (MapOffset + (new Vector3(x - (MapSize / 2), 0, z - (MapSize / 2)))) / scaleSize;
    }

    public int GetIndexFromPosition(Vector3 pos)
    {
        pos = pos * scaleSize;
        pos = new Vector3(pos.x + (MapSize / 2), 0, pos.z + (MapSize / 2));
        return (int)(((int)pos.z * MapSize) + (int)pos.x);
    }

    public void Clear()
    {
        foreach (var target in TargetList)
        {
            if (target.Value != null)
                target.Value.Clear();
        }
        TargetList.Clear();
        TargetList = new Dictionary<string, TargetCollector>(0);
    }

    public TargetCollector FindTargetTag(string tag)
    {

        if (TargetList.ContainsKey(tag))
        {
            TargetCollector targetcollector;
            if (TargetList.TryGetValue(tag, out targetcollector))
            {
                targetcollector.IsActive = true;
                return targetcollector;
            }
            else
            {
                return null;
            }
        }
        else
        {
            TargetList.Add(tag, new TargetCollector(tag));
        }
        return null;
    }

    void Update()
    {
        if (Time.time > timeTmp + UpdateInterval)
        {
            int count = 0;

            foreach (var target in TargetList)
            {
                if (target.Value != null)
                {
                    if (target.Value.IsActive)
                    {
                        target.Value.SetTarget(target.Key);
                        target.Value.IsActive = false;
                        count += 1;
                    }
                }
            }
            TargetTypeCount = count;
            timeTmp = Time.time;
        }
    }

    public bool IsPlayerAround(Vector3 position, float distance)
    {
        TargetCollector player = FindTargetTag(PlayerTag);
        if (player != null && player.Targets.Length > 0)
        {
            for (int i = 0; i < player.Targets.Length; i++)
            {
                if (player.Targets[i] != null)
                {
                    if (Vector3.Distance(position, player.Targets[i].transform.position) <= distance)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

}

public class TargetCollector
{
    public GameObject[] Targets;
    public bool IsActive;

    public TargetCollector(string tag)
    {
        SetTarget(tag);
    }
    public void Clear()
    {
        Targets = null;
    }
    public void SetTarget(string tag)
    {
        Targets = (GameObject[])GameObject.FindGameObjectsWithTag(tag);
    }

}

