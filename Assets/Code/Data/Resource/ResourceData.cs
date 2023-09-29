using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{
      [CreateAssetMenu(fileName = nameof(ResourceData),
                     menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(ResourceData))]
    public sealed class ResourceData : ScriptableObject
    {
        [SerializeField] private List<Resource> _resources;
        private List<Resource> _cachedResources;
        public List<Resource> Resources => _resources;
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!(_resources?.Count > 0))
            {
                return;
            }

            if (_cachedResources == null || _cachedResources.Count < _resources.Count)
            {
                SynchronizeCache();
            }
            else if (_cachedResources.Count > _resources.Count)
            {
                Debug.LogWarning($"The number of elements of {nameof(ResourceData)} has been reduced.");
                SynchronizeCache();
            }

            for (var i = 0; i < _resources.Count; i++)
            {
                if (_cachedResources[i].ID != _resources[i].ID)
                {
                    Debug.LogWarning($"The ID of {_cachedResources[i].Name} has been changed!");
                }
            }

            SynchronizeCache();

            var resourcesWithSameId = _resources.GroupBy(_ => _.ID, _ => _).Where(_ => _.Count() > 1).ToArray();
            foreach (var resources in resourcesWithSameId)
            {
                var stringBuilder = new StringBuilder($"Resources:");
                foreach (var resource in resources)
                {
                    stringBuilder.Append($" {resource.Name},");
                }

                stringBuilder.Remove(stringBuilder.Length - 1, 1).Append($" has the same ID: {resources.Key} ");
                Debug.LogWarning(stringBuilder.ToString());
            }
        }

        private void SynchronizeCache()
        {
            _cachedResources = new List<Resource>();
            foreach (var resource in _resources)
            {
                _cachedResources.Add(resource);
            }
        }
#endif
    }
}