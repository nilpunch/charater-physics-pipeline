using System.Linq;
using UnityEngine;

namespace Platformer
{
	public class CharacterPlatformVelocityInheritance : CharacterPhysicsModule
	{
		[SerializeField] private CharacterFoot _characterFoot;
		[SerializeField] private FootRaycast _footRaycast;
		
		public override void Affect(IPhysics physics)
		{
			if (!_characterFoot.CanJumpOff)
				return;

			foreach (var collider in _footRaycast.FootHit.Colliders)
			{
				if (collider.TryGetComponent<IMovingPlatform>(out var movingPlatform))
				{
					movingPlatform.ForwardVelocityTo(physics);
				}
			}
		}
	}
}