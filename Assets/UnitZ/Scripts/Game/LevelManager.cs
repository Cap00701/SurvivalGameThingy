//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : NetworkBehaviour
{
    public LevelPreset[] LevelPresets;
    public GameObject Preloader;
    private void Update()
    {
        if (Preloader != null)
            Preloader.SetActive(!UnitZ.gameNetwork.clientLoadedScene && UnitZ.gameNetwork.isNetworkActive);
    }
}
