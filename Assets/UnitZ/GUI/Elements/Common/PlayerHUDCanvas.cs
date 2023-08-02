//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUDCanvas : PanelsManager
{
    public GUIIStockItemLoader SecondItemLoader;
    private CharacterLiving living;
    public RectTransform Canvas;
    public ValueBar HPbar, FoodBar, WaterBar;
    public Text AmmoText;
    public Image ImageDrag;


    void Start()
    {
        // no script here.
    }

    void Awake()
    {
        // make sure every panels are closed.
        if (Pages.Length > 0)
            ClosePanel(Pages[0]);
    }
    public Vector2 GetWorldSpaceUIposition(Vector3 position)
    {
        if (Camera.main == null)
            return Vector3.zero;

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * Canvas.sizeDelta.x) - (Canvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * Canvas.sizeDelta.y) - (Canvas.sizeDelta.y * 0.5f)));
        return WorldObject_ScreenPosition;
    }

    public Text Info;
    private bool isShowinfo;
    private float timeInfo;

    public void ShowInfo(string info,Vector3 position)
    {
        if (Info != null)
        {
            isShowinfo = true;
            Info.gameObject.SetActive(true);
            RectTransform inforec = Info.GetComponent<RectTransform>();
            inforec.anchoredPosition = GetWorldSpaceUIposition(position);
            Info.text = info;
            timeInfo = Time.time;
        }
    }

    void InputController()
    {
        // This is a GUI trigger function 
        // you can press E to open Inventroy or ESC to open mainmenu or etc.. here.

        if (Input.GetKeyDown(KeyCode.E))
        {
            MouseLock.MouseLocked = !TogglePanelByName("Inventory");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MouseLock.MouseLocked = !TogglePanelByName("InGameMenu");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePanelByName("Scoreboard");
        }
        // Show mouse cursor only when Game Menu , Inventory , Craft are still showing.
        if (IsPanelOpened("InGameMenu") || IsPanelOpened("Inventory") || IsPanelOpened("Craft") || IsPanelOpened("InventoryTrade"))
        {
            MouseLock.MouseLocked = false;
        }
        else
        {
            MouseLock.MouseLocked = true;
        }

    }

    void Update()
    {
        if (UnitZ.playerManager == null || Canvas == null)
            return;

        if (UnitZ.playerManager.PlayingCharacter == null || (UnitZ.playerManager.PlayingCharacter && !UnitZ.playerManager.PlayingCharacter.IsAlive))
        {
            // if main character is die or unable to play
            living = null;
            Canvas.gameObject.SetActive(false);

        }
        else
        {

            Canvas.gameObject.SetActive(true);
            InputController();

            // Update all GUI ValueBar here.
            // ValueBar working by assign the current value and max value.

            if (HPbar)
            {
                HPbar.Value = UnitZ.playerManager.PlayingCharacter.HP;
                HPbar.ValueMax = UnitZ.playerManager.PlayingCharacter.HPmax;
            }
            if (FoodBar && living)
            {
                FoodBar.Value = living.Hungry;
                FoodBar.ValueMax = living.HungryMax;
            }
            if (WaterBar && living)
            {
                WaterBar.Value = living.Water;
                WaterBar.ValueMax = living.WaterMax;
            }
            if (AmmoText != null)
            {
                if (UnitZ.playerManager.PlayingCharacter.inventory.FPSEquipment != null)
                    AmmoText.text = UnitZ.playerManager.PlayingCharacter.inventory.FPSEquipment.Info;
            }

            if (living == null)
                living = UnitZ.playerManager.PlayingCharacter.GetComponent<CharacterLiving>();
        }
        if (Info != null)
        {
            Info.gameObject.SetActive(isShowinfo);
            if (Time.time >= timeInfo + 0.1f)
            {
                isShowinfo = false;
            }
        }
    }

    public void OpenSecondInventory(CharacterInventory inventory, string type)
    {
        if (IsPanelOpened("InventoryTrade"))
        {
            ClosePanelByName("InventoryTrade");
        }
        else
        {
            SecondItemLoader.OpenInventory(inventory, type);
            OpenPanelByName("InventoryTrade");
        }

    }

    public void CloseSecondInventory()
    {
        ClosePanelByName("InventoryTrade");
    }

}
