using UnityEngine;

namespace Platformer
{
	public class CharacterMovement : CharacterPhysicsModule
	{
		[SerializeField] private float _speed = 12f;
		[SerializeField] private float _acceleration = 80f;
		[SerializeField] private float _deceleration = 200f;
		[SerializeField, Min(1f)] private float _changeLinearity = 1f;

		private Vector3 _moveDirection;
		
		public override void Affect(IPhysics physics)
		{
			Vector3 desiredPlaneVelocity = _speed * _moveDirection;
			Vector3 lastVelocity = new Vector3(physics.Velocity.x, 0f, physics.Velocity.z);
			Vector3 velocityDifference = desiredPlaneVelocity - lastVelocity;
			
			float decelerationFactor = Mathf.Clamp01(-Vector3.Dot(lastVelocity, velocityDifference) / (_speed * _speed));
			
			float acceleration = Mathf.Lerp(_acceleration, _deceleration, 
				Mathf.Clamp01(Mathf.Pow(decelerationFactor, _changeLinearity)));

			Vector3 velocity = Vector3.MoveTowards(lastVelocity, desiredPlaneVelocity, acceleration * Time.deltaTime);
			
			Vector3 velocityCorrection = Vector3.ClampMagnitude(velocity - lastVelocity, _speed);
			
			physics.AddForce(velocityCorrection);
		}

		public void Move(Vector3 movement)
		{
			_moveDirection = Vector3.Scale(Vector3.ClampMagnitude(movement, 1f), new Vector3(1f, 0f, 1f));
		}
	}
}