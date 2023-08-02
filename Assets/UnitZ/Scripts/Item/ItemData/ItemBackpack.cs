﻿//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ItemBackpack : ItemData
{

    public List<ItemCollector> Items = new List<ItemCollector>();
    public string SyncItemdata;

    void Start()
    {

    }


    public void SetDropItem(string itemdata)
    {
        SyncItemdata = itemdata;
    }


    public override void Pickup(CharacterSystem character)
    {

        character.SendMessage("PickupItemBackpackCallback", this);
        RemoveItem();

    }

}
