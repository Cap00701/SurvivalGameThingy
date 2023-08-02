//----------------------------------------------
//      UnitZ : FPS Sandbox Starter Kit
//    Copyright © Hardworker studio 2015 
// by Rachan Neamprasert www.hardworkerstudio.com
//----------------------------------------------
using UnityEngine;
using System.Collections;


public class ApplyFood : MonoBehaviour
{
	
	public byte FoodPlus = 10;
	public byte DrinkPlus = 10;
	public byte HealthPlus = 10;
	
	void Start ()
	{
		CharacterSystem character;
		if (this.transform.root) {
			character = this.transform.root.GetComponent<CharacterSystem> ();
		} else {
			character = this.transform.GetComponent<CharacterSystem> ();
		}
		if (character) {
			CharacterLiving living = character.GetComponent<CharacterLiving>();
			if(living){
				living.Eating (FoodPlus);
				living.Drinking (DrinkPlus);
				living.Healing(HealthPlus);
			}
		}
		GameObject.Destroy (this.gameObject);
	}
}
