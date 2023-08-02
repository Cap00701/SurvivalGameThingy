//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------
using UnityEngine;
using System.Collections;

public class PlayerView : MonoBehaviour
{

    public FPSCamera FPSCamera;
    public GameObject[] PlayerObjects;
    private CharacterSystem character;

    public bool ThirdView = false;

    void Start()
    {
        SetActive();
    }

    void Awake()
    {
        character = this.GetComponent<CharacterSystem>();
        FPSCamera = GetComponentInChildren<FPSCamera>();
    }

    public void SwithView()
    {
        ThirdView = !ThirdView;
    }
    public void SwithViewSide()
    {
        FPSCamera.ThirdViewInvert = FPSCamera.ThirdViewInvert * -1;
    }

    void LateUpdate()
    {
        SetActive();
        FPSCamera.IsThirdView = ThirdView;
        if (character.Motor.MotorPreset.Length > character.MovementIndex)
            FPSCamera.transform.localPosition = character.Motor.MotorPreset[character.MovementIndex].FPSCamOffset;
    }

    public void SetActive()
    {

        if (character && character.IsMine && character.isLocalPlayer && (!ThirdView || (ThirdView && FPSCamera.zooming && FPSCamera.hideWhenScoping)))
        {
            foreach (GameObject go in PlayerObjects)
            {
                //Hide (go.transform, false);
                go.SetActive(false);
            }
            FPSCamera.gameObject.SetActive(true);
        }
        else
        {
            if (!ThirdView)
                FPSCamera.gameObject.SetActive(false);

            foreach (GameObject go in PlayerObjects)
            {
                //Hide (go.transform, true);
                go.SetActive(true);
            }
        }
    }

    public void Hide(Transform trans, bool hide)
    {
        foreach (Transform ob in trans)
        {
            ob.gameObject.SetActive(hide);
        }
        trans.gameObject.SetActive(hide);
    }

}
