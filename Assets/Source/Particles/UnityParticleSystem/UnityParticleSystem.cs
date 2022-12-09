namespace Particle
{

    public class UnityParticleSystem
    {
        public static readonly int DefaultCapacity = 1024;
        public static readonly int MaxSize = 1024 * 1024;
        public UnityEngine.Pool.IObjectPool<UnityEngine.ParticleSystem> Pool;


        public void InitStage1()
        {
            Pool = new UnityEngine.Pool.ObjectPool<UnityEngine.ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, DefaultCapacity, MaxSize);
        }

        


        public void InitStage2()
        {

        }


        UnityEngine.ParticleSystem CreatePooledItem()
        {
            var go = new UnityEngine.GameObject("Pooled Particle System");
            var ps = go.AddComponent<UnityEngine.ParticleSystem>();
            ps.Stop(true, UnityEngine.ParticleSystemStopBehavior.StopEmittingAndClear);

            var main = ps.main;
            main.duration = 1;
            main.startLifetime = 1;
            main.loop = false;

            // This is used to return ParticleSystems to the pool when they have stopped.
            var returnToPool = go.AddComponent<ReturnToPool>();
            returnToPool.Pool = Pool;

            return ps;
        }


        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(UnityEngine.ParticleSystem system)
        {
            system.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(UnityEngine.ParticleSystem system)
        {
            system.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(UnityEngine.ParticleSystem system)
        {
            UnityEngine.GameObject.Destroy(system.gameObject);
        }

    }


    [UnityEngine.RequireComponent(typeof(UnityEngine.ParticleSystem))]
    public class ReturnToPool : UnityEngine.MonoBehaviour
    {
        public UnityEngine.ParticleSystem System;
        public UnityEngine.Pool.IObjectPool<UnityEngine.ParticleSystem> Pool;

        void Start()
        {
            System= GetComponent<UnityEngine.ParticleSystem>();
            var main = System.main;
            main.stopAction = UnityEngine.ParticleSystemStopAction.Callback;
        }

        void OnParticleSystemStopped()
        {
            // Return to the pool
            Pool.Release(System);
        }
    }
}