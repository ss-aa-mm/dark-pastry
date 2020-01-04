using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float DamageInflicted;
        protected float MovementTime;
        protected float Speed;
        protected float Health;
        private static Animator _animator;
        protected GameObject ItemDropped;
        private GameObject _heart;
        private GameObject _agata;
        public bool dropsHeart;
        public bool dropsItem;
        private static bool _isAttacking;
        private static bool _isActive;
        private static bool _paused;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Die");
        private float _timeLeft = 1f;

        private void Awake()
        {
            _heart = Resources.Load<GameObject>("Prefabs/Heart");
            _agata = GameObject.Find("Agata");
            _isAttacking = false;
            _paused = false;
            _isActive = true;
            _animator = GetComponent<Animator>();
            AssignReferences();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if (!_paused)
                UpdateAnimator(_isAttacking);

            if (Vector2.Distance(_agata.transform.position, transform.position) < 1)
                _isAttacking = true;

            MovementPattern();
        }

        private static void UpdateAnimator(bool isAttacking)
        {
            if (isAttacking)
            {
                _animator.SetTrigger(Attack);
            }
        }

        private void OnDeath()
        {
            if (dropsHeart)
                Instantiate(_heart, transform.position, Quaternion.identity);
            if (dropsItem)
                Instantiate(ItemDropped, transform.position, Quaternion.identity);

            _isActive = false;
            _animator.SetTrigger(Death);
        }

        public void OnHit()
        {
            Health--;
            if (Health <= 0)
                OnDeath();
        }

        protected abstract void AssignReferences();

        protected abstract void MovementPattern();

        public float GetDamage()
        {
            return DamageInflicted;
        }

        protected void MoveRandomly()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            var dir = Random.Range(0, 4);

            switch (dir)
            {
                case 0:
                    transform.Translate(Speed * Time.deltaTime, 0f, 0f);
                    break;
                case 1:
                    transform.Translate(0f, Speed * Time.deltaTime, 0f);
                    break;
                case 2:
                    transform.Translate(-Speed * Time.deltaTime, 0f, 0f);
                    break;
                case 3:
                    transform.Translate(0f, -Speed * Time.deltaTime, 0f);
                    break;
            }

            _timeLeft += MovementTime;
        }

        protected void Chase()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            transform.position =
                Vector2.MoveTowards(transform.position, _agata.transform.position, Speed * Time.deltaTime);

            _timeLeft += MovementTime;
        }

        protected void Escape()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft > 0)
                return;

            transform.position =
                Vector2.MoveTowards(transform.position, _agata.transform.position * -1, Speed * Time.deltaTime);

            _timeLeft += MovementTime;
        }

        public static int GetAnimatorHash()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).tagHash;
        }
    }
}