using UnityEngine;

namespace Platformer
{
	public class CharacterPhysicsPipeline : MonoBehaviour
	{
		[SerializeField] private RigidbodyPhysics _rigidbodyPhysics;
		[SerializeField] private CharacterFoot _characterFoot;
		[SerializeField] private FootRaycast _footRaycast;
		[SerializeField] private CharacterPhysicsModule[] _physicsModules;

		public void ManualUpdate()
		{
			_footRaycast.UpdateFootHit();
			_characterFoot.UpdateGroundState();

			foreach (var module in _physicsModules)
				module.Affect(_rigidbodyPhysics);
			
			_rigidbodyPhysics.ApplyVelocityChanges();
		}
	}
}