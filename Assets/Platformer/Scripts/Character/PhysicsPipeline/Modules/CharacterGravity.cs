using UnityEngine;

namespace Platformer
{
	public class CharacterGravity : CharacterPhysicsModule
	{
		[field: SerializeField] public Vector3 Gravity = new Vector3(0f, -78f, 0f);
		
		public float GravityMagnitude => Gravity.magnitude;
		public Vector3 WorldUp => Vector3.Normalize(-Gravity);
		
		public override void Affect(IPhysics physics)
		{
			physics.AddForce(Gravity * Time.deltaTime);
		}
	}
}