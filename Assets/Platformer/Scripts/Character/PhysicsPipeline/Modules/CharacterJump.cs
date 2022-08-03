using UnityEngine;

namespace Platformer
{
	public class CharacterJump : CharacterPhysicsModule
	{
		[SerializeField] private CharacterFoot _characterFoot;
		[SerializeField] private float _jumpMinHeight = 2.5f;
		[SerializeField] private float _jumpMaxHeight = 4.5f;
		[SerializeField] private float _jumpGravity = 1f;
		[SerializeField] private float _fallGravityMultiplier = 1.5f;
		[SerializeField] private float _earlyFallGravityMultiplier = 3.0f;
		[SerializeField, Range(0, 2)] private int _airJumps = 0;
		[SerializeField] private float _jumpBuffer = 0.4f;
		
		private bool _inJump;
		private bool _fallRequested;
		private bool _falling;
		private int _availableAirJumps;

		private bool _jumpQueued;
		private float _jumpBufferElapsedTime;

		private float JumpTime => Mathf.Sqrt(2f * _jumpMaxHeight / _jumpGravity);
		
		private float FallGravity => _jumpGravity * _fallGravityMultiplier;
		
		private float EarlyFallGravity => FallGravity * _earlyFallGravityMultiplier;

		private float JumpStartVelocity => 2f * _jumpMaxHeight / JumpTime;

		private float FallMinVelocity => JumpStartVelocity - _jumpGravity * MinJumpTime;
		
		private float MinJumpTime => (-JumpStartVelocity + Mathf.Sqrt(JumpStartVelocity * JumpStartVelocity - 2f * _jumpGravity * _jumpMinHeight)) / -_jumpGravity;
		
		public override void Affect(IPhysics physics)
		{
			bool isCharacterFalling = physics.Velocity.y < 0f;
			bool isFallVelocityReached = physics.Velocity.y <= FallMinVelocity;

			if (isCharacterFalling && _characterFoot.IsOnGround)
			{
				_availableAirJumps = _airJumps;
				_inJump = false;
			}

			if (isCharacterFalling)
			{
				_falling = true;
			}
			
			Vector3 gravity;
			if (_fallRequested && isFallVelocityReached && !isCharacterFalling)
				gravity = Vector3.down * (EarlyFallGravity * Time.deltaTime);
			else if (_falling)
				gravity = Vector3.down * (FallGravity * Time.deltaTime);
			else
				gravity = Vector3.down * (_jumpGravity * Time.deltaTime);

			if (!_jumpQueued)
			{
				physics.AddForce(gravity);
				return;
			}

			_jumpBufferElapsedTime += Time.deltaTime;

			bool isBufferExpired = _jumpBufferElapsedTime > _jumpBuffer;
			bool isInAir = _inJump || _characterFoot.IsInAir;
			bool dontHaveAirJumps = _availableAirJumps == 0;
				
			if (isBufferExpired || isInAir && dontHaveAirJumps)
			{
				physics.AddForce(gravity);
				return;
			}
			
			_jumpQueued = false;
				
			if (_inJump)
				_availableAirJumps -= 1;
			
			_inJump = true;
			_fallRequested = false;
			_falling = false;

			physics.AddForce(new Vector3(0f, JumpStartVelocity - physics.Velocity.y, 0f));
		}

		public void Jump()
		{
			_jumpQueued = true;
			_jumpBufferElapsedTime = 0f;
		}

		public void Fall()
		{
			_jumpQueued = false;
			_fallRequested = true;
		}
	}
}