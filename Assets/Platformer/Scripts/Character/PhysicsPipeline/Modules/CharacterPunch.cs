using System;
using UnityEngine;

namespace Platformer
{
	public class CharacterPunch : CharacterPhysicsModule, IPunchable
	{
		private Vector3 _accumulatedPunch;
		
		public override void Affect(IPhysics physics)
		{
			physics.AddForce(_accumulatedPunch);
			_accumulatedPunch = Vector3.zero;
		}

		public void Punch(Vector3 force)
		{
			_accumulatedPunch += force;
		}
	}
}