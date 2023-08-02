//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AnimalCharacter : CharacterSystem
{

	public float DamageDirection = 0.5f;
	public float Force = 70;
	public float StandAttackDuration = 0.5f;
	private float timeTmp;

    public override void Update()
    {
		if (Time.time > timeTmp + StandAttackDuration) {
			spdMovAtkMult = 1;
		}
        base.Update();
    }

    public void Leap ()
	{	

	}
	
	public void AfterAttack ()
	{

	}
	
	public void DoAttack ()
	{
		DoOverlapDamage (this.transform.position + DamageOffset, this.transform.forward * Force, Damage, DamageLength, DamageDirection, NetID, Team);
	}

	public override void PlayMoveAnimation (float magnitude)
	{
		if (animator) {
			if (magnitude > 0.4f) {
				animator.SetInteger ("StateAnimation", 1);
			} else {
				animator.SetInteger ("StateAnimation", 0);
			}
		}

		base.PlayMoveAnimation (magnitude);
	}

	public override void PlayAttackAnimation (bool attacking, int attacktype)
	{
		if (animator) {
			if (attacking) {
				animator.SetTrigger ("Attacking");
				spdMovAtkMult = 0;
				timeTmp = Time.time;
			}
		}
		base.PlayAttackAnimation (attacking, attacktype);
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

    public override void OnThisThingDead ()
	{
		// reset when dead

		if(NetID != -1){
			RemoveCharacterData();
		}

		if (UnitZ.NetworkObject().scoreManager) {
			UnitZ.NetworkObject().scoreManager.AddDead (1, NetID);
			if (NetID != LastHitByID){
				UnitZ.NetworkObject().scoreManager.AddScore (1, LastHitByID);
			}
		}

		if (isServer) {
			ItemDropAfterDead dropafterdead = this.GetComponent<ItemDropAfterDead> ();
			if (dropafterdead)
				dropafterdead.DropItem ();
		}

		base.OnThisThingDead ();
	}
}
