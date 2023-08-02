//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HumanCharacter : CharacterSystem
{
    public override void Start()
    {
        if (animator)
            animator.SetInteger("Shoot_Type", AttackType);
        base.Start();
    }

    public void OnEquipChanged(int type)
    {
        if (animator)
            animator.SetInteger("Shoot_Type", type);
    }

    public override void Update()
    {
        if (animator == null)
            return;

        animator.SetInteger("BodyState", MovementIndex);
        base.Update();
    }

    public override void PlayMoveAnimation(float magnitude)
    {
        if (animator)
        {
           animator.SetFloat("Velocity", magnitude);
        }

        base.PlayMoveAnimation(magnitude);
    }

    public override void PlayAttackAnimation(bool attacking, int attacktype)
    {
        if (animator)
        {
            if (attacking)
            {
                animator.SetTrigger("Shoot");
            }
            animator.SetInteger("Shoot_Type", attacktype);
        }
        base.PlayAttackAnimation(attacking, attacktype);
    }

    public override void OnKilled(int killer, int me, string killtype)
    {
        if (UnitZ.NetworkObject().scoreManager)
        {
            if (NetID != LastHitByID && LastHitByID != -1 && me != -1)
            {
                UnitZ.NetworkObject().scoreManager.AddKillText(LastHitByID, NetID, "Kill");
            }
        }
        base.OnKilled(killer, me, killtype);
    }

    public override void SetEnable(bool enable)
    {

        if (this.GetComponent<PlayerView>())
            this.GetComponent<PlayerView>().enabled = enable;

        if (this.GetComponent<FPSInputController>())
            this.GetComponent<FPSInputController>().enabled = enable;

        if (this.GetComponent<FPSController>())
            this.GetComponent<FPSController>().enabled = enable;

        if (this.GetComponent<CharacterMotor>())
        {
            this.GetComponent<CharacterMotor>().enabled = enable;
            this.GetComponent<CharacterMotor>().Reset();
        }

        if (this.GetComponent<CharacterController>())
            this.GetComponent<CharacterController>().enabled = enable;

        if (this.GetComponent<NetworkTransform>())
            this.GetComponent<NetworkTransform>().enabled = enable;

        if (this.GetComponent<CharacterDriver>())
            this.GetComponent<CharacterDriver>().NoVehicle();

        base.SetEnable(enable);
    }

    public override void OnThisThingDead()
    {
        if (NetID != -1)
        {
            RemoveCharacterData();
        }
        if (UnitZ.NetworkObject().scoreManager)
        {
            UnitZ.NetworkObject().scoreManager.AddDead(1, NetID);
            if (NetID != LastHitByID)
            {
                UnitZ.NetworkObject().scoreManager.AddScore(1, LastHitByID);
            }
        }

        CharacterItemDroper itemdrop = this.GetComponent<CharacterItemDroper>();
        if (itemdrop)
            itemdrop.DropItem();

        if (isServer)
        {
            ItemDropAfterDead dropafterdead = this.GetComponent<ItemDropAfterDead>();
            if (dropafterdead)
                dropafterdead.DropItem();
        }

        base.OnThisThingDead();
    }

    public override void OnRespawn()
    {
        if (this.GetComponent<CharacterInventory>())
            this.GetComponent<CharacterInventory>().SetupStarterItem();
        base.OnRespawn();
    }
}
