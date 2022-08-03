using UnityEngine;

namespace Platformer
{
	public class WorldSimulation : MonoBehaviour
	{
		private CharacterPhysicsPipeline[] _characterPhysicsPipelines;
		private MovingPlatform[] _movingPlatforms;
		private RotatingPlatform[] _rotatingPlatforms;
		
		private void Awake()
		{
			_characterPhysicsPipelines = FindObjectsOfType<CharacterPhysicsPipeline>();
			_movingPlatforms = FindObjectsOfType<MovingPlatform>();
			_rotatingPlatforms = FindObjectsOfType<RotatingPlatform>();
		}

		private void FixedUpdate()
		{
			foreach (var platform in _movingPlatforms)
			{
				platform.ManualUpdate();
			}
			
			foreach (var platform in _rotatingPlatforms)
			{
				platform.ManualUpdate();
			}

			foreach (var physicsPipeline in _characterPhysicsPipelines)
			{
				physicsPipeline.ManualUpdate();
			}
		}
	}
}