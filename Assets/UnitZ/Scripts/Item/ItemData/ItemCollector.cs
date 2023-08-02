//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class ItemCollector {

	public ItemData Item;
	public int Index;
	public int Num;
	public int NumTag = -1;
	public bool Active;
	public int Shortcut = -1;
    public int EquipIndex = -1;
    public int InventoryIndex;
    public int ItemIndex;
    public bool InUse = false;

    public void Clear()
    {
        Item = null;
        NumTag = -1;
        Shortcut = -1;
        EquipIndex = -1;
        InUse = false;
        ItemIndex = -1;
        InventoryIndex = -1;
    }
}
