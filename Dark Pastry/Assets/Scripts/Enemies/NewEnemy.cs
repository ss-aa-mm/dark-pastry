using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class NewEnemy : MonoBehaviour
    {
        protected float DamageInflicted;
        protected float MovementTime;
        protected float Unit;
        protected float Health;
        protected GameObject ItemDropped;
        private GameObject _heart;
        private GameObject _agata;
        public bool dropsHeart;
        public bool dropsItem;
        private float _timeLeft = 1f;

        private void Awake()
        {
            _heart = Resources.Load<GameObject>("Prefabs/Heart");
            _agata = GameObject.Find("Agata");
            AssignReferences();
        }

        private void Update()
        {
            MovementPattern();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Agata")) return;
            AgataNew.SetLife(-DamageInflicted);
        }

        private void OnDeath()
        {
            if (dropsHeart)
                Instantiate(_heart, transform.position, Quaternion.identity);
            if (dropsItem)
                Instantiate(ItemDropped, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void OnHit()
        {
            Health--;
            if (Health <= 0)
                OnDeath();
        }

        protected abstract void AssignReferences();

        protected abstract void MovementPattern();

        protected void MoveRandomly()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            var dir = UnityEngine.Random.Range(0, 4);

            switch (dir)
            {
                case 0:
                    transform.Translate(Unit, 0f, 0f);
                    break;
                case 1:
                    transform.Translate(0f, Unit, 0f);
                    break;
                case 2:
                    transform.Translate(-Unit, 0f, 0f);
                    break;
                case 3:
                    transform.Translate(0f, -Unit, 0f);
                    break;
            }

            _timeLeft += MovementTime;
        }

        protected void Chase()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            transform.position = Vector2.MoveTowards(transform.position, _agata.transform.position, Unit);

            _timeLeft += MovementTime;
        }

        protected void Escape()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            transform.position = Vector2.MoveTowards(transform.position, _agata.transform.position * -1, Unit);

            _timeLeft += MovementTime;
        }
    }
}