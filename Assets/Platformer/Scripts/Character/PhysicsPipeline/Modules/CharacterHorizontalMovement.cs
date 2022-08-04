using UnityEngine;

namespace Platformer
{
	public class CharacterHorizontalMovement : CharacterPhysicsModule
	{
		[SerializeField] private FootRaycast _footRaycast;
		[SerializeField] private CharacterGravity _characterGravity;
		[SerializeField] private float _speed = 12f;
		[SerializeField] private float _acceleration = 80f;
		[SerializeField] private float _deceleration = 200f;

		[Header("Ground specs")] 
		[SerializeField, Range(0f, 90f)] private float _tolerantGroundAngle = 45f;
		[SerializeField, Range(0f, 90f)] private float _maxGroundAngle = 60f;
		[SerializeField] private float _maxGroundDistance = 0.01f;

		private Vector3 _moveDirection;
		
		public override void Affect(IPhysics physics)
		{
			Vector3 groundUp = _characterGravity.WorldUp;
			
			if (_footRaycast.FootHit.HasHit)
			{
				float distanceToGround = _footRaycast.FootHit.MinDistance();

				if (distanceToGround < _maxGroundDistance)
					groundUp = _footRaycast.FootHit.NearestNormal();
			}

			Vector3 desiredVelocity = _speed * _moveDirection.magnitude * Vector3.ProjectOnPlane(_moveDirection, groundUp).normalized;

			float angleToWorldUp = desiredVelocity.sqrMagnitude > 0.001f 
				? Vector3.Angle(desiredVelocity, _characterGravity.WorldUp) 
				: 90f;
			float angleAgainstGround = 90f - angleToWorldUp;
			float groundResistance = 1f - Mathf.Clamp01(Mathf.InverseLerp(_tolerantGroundAngle, _maxGroundAngle, angleAgainstGround));

			Vector3 lastVelocity = Vector3.ProjectOnPlane(physics.Velocity, groundUp);

			Vector3 velocityDifference = desiredVelocity - lastVelocity;
			
			float decelerationFactor = Mathf.Clamp01(-Vector3.Dot(lastVelocity, velocityDifference) / (_speed * _speed));
			
			float acceleration = Mathf.Lerp(_acceleration, _deceleration, decelerationFactor);
			
			Vector3 velocity = Vector3.MoveTowards(lastVelocity, desiredVelocity * groundResistance, acceleration * Time.deltaTime);

			Vector3 fightAgainstGravity = Vector3.ProjectOnPlane(-_characterGravity.Gravity * Time.deltaTime, groundUp);

			Vector3 velocityCorrection = Vector3.ClampMagnitude(velocity - lastVelocity, _speed) + fightAgainstGravity;
			
			physics.AddForce(velocityCorrection);
		}

		public void Move(Vector3 movement)
		{
			_moveDirection = Vector3.Scale(Vector3.ClampMagnitude(movement, 1f), new Vector3(1f, 0f, 1f));
		}
	}
}