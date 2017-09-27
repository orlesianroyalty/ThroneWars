using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CombatClass {
	float getMovementSpeed ();
	float getMaxHP ();
	float getMaxMP ();
	float getHP ();
	float getMP ();
	float getAbility1Cooldown ();
	float getAbility2Cooldown ();
	float getAbility3Cooldown ();
	float getAbility4Cooldown ();
	//float getAbility1Cost();
	//float getAbility2Cost();
	//float getAbility3Cost();
	//float getAbility4Cost();
	void ability1 ();
	void ability2 ();
	void ability3 ();
	void ability4 ();
	void takeDamage(int dealt);
}
