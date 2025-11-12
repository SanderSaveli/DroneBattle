using DG.Tweening;
using UnityEngine;

namespace Sander.DroneBattle
{
    [RequireComponent(typeof(Resource))]
    public class ResourceView : MonoBehaviour
    {
        public float _destroyAnimationDuration = 0.5f;

        private Resource _resource;

        private void OnEnable()
        {
            gameObject.transform.localScale = Vector3.one;
            if (_resource == null)
            {
                _resource = GetComponent<Resource>();
            }
            _resource.OnResourceCollected += PlayRemoveAnimation;
        }

        private void OnDisable()
        {
            _resource.OnResourceCollected -= PlayRemoveAnimation;
        }

        private void PlayRemoveAnimation(Resource resource)
        {
            transform.DOScale(0, _destroyAnimationDuration)
                .OnComplete(resource.RemoveFromField)
                .SetLink(gameObject);
        }
    }
}
