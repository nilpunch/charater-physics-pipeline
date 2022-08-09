using UnityEngine;

namespace Platformer
{
	public class WorldSimulation : MonoBehaviour
	{
		private CharacterPhysicsPipeline[] _characterPhysicsPipelines;
		private Platform[] _platforms;
		
		private void Awake()
		{
			_characterPhysicsPipelines = FindObjectsOfType<CharacterPhysicsPipeline>();
			_platforms = FindObjectsOfType<Platform>();
		}

		private void FixedUpdate()
		{
			foreach (var platform in _platforms)
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