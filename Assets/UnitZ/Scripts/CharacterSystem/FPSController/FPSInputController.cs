//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(FPSController))]
public class FPSInputController : NetworkBehaviour
{
    private FPSController fpsControl;

    void Start()
    {
        fpsControl = this.GetComponent<FPSController>();
        MouseLock.MouseLocked = true;
    }

    void Update()
    {

        if (isLocalPlayer && fpsControl != null)
        {
            fpsControl.MoveCommand(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), Input.GetButton("Jump"));

            if (Input.GetKeyDown(KeyCode.C))
            {
                fpsControl.Sit();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                fpsControl.OutVehicle();
            }

            fpsControl.Sprint(Input.GetKey(KeyCode.LeftShift));

            if (MouseLock.MouseLocked)
            {
                fpsControl.Aim(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
                fpsControl.Trigger1(Input.GetButton("Fire1"));
                fpsControl.Trigger2(Input.GetButtonDown("Fire2"));
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                fpsControl.Interactive();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                fpsControl.SwithView();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                fpsControl.SwithSideView();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                fpsControl.Reload();
            } 
            fpsControl.Checking();
        }
    }

}
