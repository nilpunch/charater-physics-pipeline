using UnityEngine;

namespace Platformer
{
	public class RigidbodyPhysics : MonoBehaviour, IPhysics
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField, Min(0f)] private float _maxFallVelocity = 5;

		private Vector3 _constantForce;
		private Vector3 _localForce;
		
		private Vector3 _lastConstantForce;

		private Vector3 _expectedVelocity;

		public Vector3 Velocity => _rigidbody.velocity - _lastConstantForce;
		
		public Vector3 AbsoluteVelocity => _rigidbody.velocity;

		public Vector3 Depenetration => _expectedVelocity - _rigidbody.velocity;

		public Vector3 Position => _rigidbody.position;

		public void AddConstantForce(Vector3 velocity)
		{
			_constantForce += velocity;
		}
		
		public void AddForce(Vector3 velocity)
		{
			_localForce += velocity;
		}

		public void ApplyVelocityChanges()
		{
			Vector3 newLocalVelocity = Velocity + _localForce;
			
			newLocalVelocity = new Vector3(newLocalVelocity.x, Mathf.Max(newLocalVelocity.y, -_maxFallVelocity), newLocalVelocity.z);

			Vector3 newAbsoluteVelocity = newLocalVelocity + _constantForce;

			_rigidbody.velocity = newAbsoluteVelocity;
			_expectedVelocity = newAbsoluteVelocity;

			_lastConstantForce = _constantForce;
			
			_localForce = Vector3.zero;
			_constantForce = Vector3.zero;
		}
	}
}