using System.Linq;
using UnityEngine;

namespace Platformer
{
	public class CharacterPlatformVelocityInheritance : CharacterPhysicsModule
	{
		[SerializeField] private FootRaycast _footRaycast;

		[SerializeField] private float _maxInheritanceAngle = 90f;
		[SerializeField] private float _minInheritanceDistance = 0.1f;
		
		public override void Affect(IPhysics physics)
		{
			if (!_footRaycast.FootHit.HasHit)
				return;

			float distanceToGround = _footRaycast.FootHit.MinDistanceWithAngleConstraint(_maxInheritanceAngle);

			if (distanceToGround > _minInheritanceDistance)
				return;

			foreach (var collider in _footRaycast.FootHit.CollidersWithAngleAndDistance(_maxInheritanceAngle, _minInheritanceDistance))
			{
				if (collider.TryGetComponent<IMovingPlatform>(out var movingPlatform))
				{
					movingPlatform.ForwardVelocityTo(physics);
				}
			}
		}
	}
}