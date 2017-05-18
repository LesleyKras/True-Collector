using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanoid {
	void Move();
	void Attack(int i);
	void Die();
	void OnCollisionEnter(Collision col);
}
