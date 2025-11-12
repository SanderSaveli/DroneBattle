using UnityEngine;
using Zenject;

namespace Sander.DroneBattle
{
    public abstract class AbstractFactory<T> : MonoBehaviour where T : MonoBehaviour, IHasCollider, IPoolableObject<T>
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private T _prefab;
        [SerializeField] private SpriteRenderer _boundsRenderer;

        private Bounds _bounds;
        private InjectionObjectPool<T> _pool;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _pool = new InjectionObjectPool<T>(diContainer, _prefab, _parent);
        }

        private void Awake()
        {
            _bounds = _boundsRenderer.bounds;
        }

        public T Spawn()
        {
            try
            {
                Vector3 spawnPos = GetPosition(_bounds);
                T obj = _pool.Get();
                obj.transform.position = spawnPos;
                OnCreate(obj);
                return obj;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                return default;
            }
        }

        public T SpawnInsideArea(Bounds bounds)
        {
            try
            {
                Vector3 spawnPos = GetPosition(bounds);
                T obj = _pool.Get();
                obj.transform.position = spawnPos;
                OnCreate(obj);
                return obj;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                return default;
            }
        }

        protected virtual void OnCreate(T obj)
        {

        }

        private Vector3 GetPosition(Bounds bounds)
        {
            bool isCorrectPosition = false;
            Vector2 spawnPos = Vector2.zero;
            int checkCount = 0;

            while (!isCorrectPosition)
            {
                spawnPos = GetRandomPosition(bounds);
                isCorrectPosition = !ColliderHelper.HasCollision(spawnPos, _prefab.Collider);
                checkCount++;
                if (checkCount > 1000)
                {
                    throw new System.InvalidOperationException($"Can't find empty space to spawn {nameof(T)}");
                }
            }
            return spawnPos;
        }

        private Vector2 GetRandomPosition(Bounds bounds)
        {
            Vector2 position = Vector2.zero;
            position.x = Random.Range(bounds.min.x, bounds.max.x);
            position.y = Random.Range(bounds.min.y, bounds.max.y);
            return position;
        }
    }
}
