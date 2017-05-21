using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanoid {
	void ChangeBehaviour();
	void Attack(int i);
	void Die();
	void OnCollisionEnter(Collision col);
}
