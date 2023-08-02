//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;

public static class MouseLock
{
	private static bool mouseLocked;
    public static bool IsMobileControl = false;


    public static bool MouseLocked {
		get {
			return mouseLocked;
		}
		set {
            if (IsMobileControl)
            {
                mouseLocked = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                mouseLocked = value;
                if (value) { 
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
		}
	}
	

}

