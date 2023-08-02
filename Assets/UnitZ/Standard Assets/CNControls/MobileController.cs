using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

[RequireComponent(typeof(FPSController))]
public class MobileController : MonoBehaviour
{

    public GameObject[] controls;
    void Start()
    {

        if (controls.Length <= 0)
        {
            controls = new GameObject[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                controls[i] = this.transform.GetChild(i).gameObject;
            }
        }
    }
    void SetVisible(bool visible)
    {
        for (int i = 0; i < controls.Length; i++)
        {
            controls[i].SetActive(visible);
        }
    }

    public void SwithView()
    {
        if (UnitZ.playerManager.PlayingCharacter != null)
        {
            FPSController fpsControl = UnitZ.playerManager.PlayingCharacter.GetComponent<FPSController>();
            if (fpsControl)
                fpsControl.SwithView();
        }
    }
    public void SwithSideView()
    {
        if (UnitZ.playerManager.PlayingCharacter != null)
        {
            FPSController fpsControl = UnitZ.playerManager.PlayingCharacter.GetComponent<FPSController>();
            if (fpsControl)
                fpsControl.SwithSideView();
        }
    }


    void Update()
    {

        if (UnitZ.playerManager.PlayingCharacter != null)
        {
            SetVisible(true);
            FPSInputController oldController = UnitZ.playerManager.PlayingCharacter.GetComponent<FPSInputController>();
            if (oldController)
            {
                oldController.enabled = false;
            }

            FPSController fpsControl = UnitZ.playerManager.PlayingCharacter.GetComponent<FPSController>();
            if (fpsControl)
            {
                MouseLock.IsMobileControl = true;
                fpsControl.MoveCommand(new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical")), CnInputManager.GetButton("Jump"));
                fpsControl.Aim(new Vector2(CnInputManager.GetAxis("Touch X"), CnInputManager.GetAxis("Touch Y")));
                fpsControl.Trigger1(CnInputManager.GetButton("Touch Fire1"));
                fpsControl.Trigger2(CnInputManager.GetButtonDown("Fire2"));

                if (CnInputManager.GetButtonDown("Fire3"))
                {
                    fpsControl.OutVehicle();
                    fpsControl.Interactive();
                }

                if (CnInputManager.GetButtonDown("Submit"))
                {
                    fpsControl.Reload();
                }

                fpsControl.Checking();
            }

        }
        else
        {
            SetVisible(false);
        }
    }
}
