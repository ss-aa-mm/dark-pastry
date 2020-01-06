using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float DamageInflicted;
        protected float MovementTime;
        protected float Speed;
        public float health;
        public Animator animator;
        protected GameObject ItemDropped;
        private GameObject _heart;
        private static GameObject _agata;
        public bool dropsHeart;
        public bool dropsItem;
        private static bool _isWalking;
        private static bool _isAttacking;
        public bool isActive;
        private static bool _paused;
        private static int _frames;
        private static float _x;
        private static float _y;
        private const float Multiplier = 0.1f;
        private static readonly int Walk = Animator.StringToHash("walk");
        private static readonly int AttackRight = Animator.StringToHash("attackRight");
        private static readonly int AttackLeft = Animator.StringToHash("attackLeft");
        public readonly int Death = Animator.StringToHash("die");
        private static AudioClip _agataHit;
        private float _timeLeft = 1f;

        private void Awake()
        {
            _heart = Resources.Load<GameObject>("Prefabs/Heart");
            _agata = GameObject.Find("Agata");
            isActive = true;
            _isWalking = true;
            _isAttacking = false;
            _paused = false;
            animator = GetComponent<Animator>();
            _y = -1;
            _agataHit = Resources.Load<AudioClip>("Assets/Resources/SOUND/sicuri/slightscream-07.mp3");
            AssignReferences();
        }

        private void Update()
        {
            if (!isActive)
                return;

            _isAttacking = Vector2.Distance(_agata.transform.position, transform.position) < 2;

            if (!_paused)
                UpdateAnimator(_isWalking, _isAttacking);

            _isWalking = true;
            MovementPattern();
        }

        private void UpdateAnimator(bool isWalking, bool isAttacking)
        {
            if (isAttacking)
            {
                animator.SetTrigger(_agata.transform.position.x >= transform.position.x ? AttackRight : AttackLeft);
            }

            animator.SetBool(Walk, isWalking);
        }

        public void OnDeath()
        {
            if (dropsHeart)
                Instantiate(_heart, transform.position, Quaternion.identity);
            if (dropsItem)
                Instantiate(ItemDropped, transform.position, Quaternion.identity);
        }

        protected abstract void AssignReferences();

        protected abstract void MovementPattern();

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

        protected void CrossMovement()
        {
            _frames++;
            transform.Translate(_x*Time.deltaTime,_y*Time.deltaTime,0);
            switch (_frames*Multiplier)
            {
                case 10:
                    _y = 1;
                    break;
                case 20:
                    _x = -1;
                    _y = 0;
                    break;
                case 30:
                    _x = 1;
                    break;
                case 40:
                    _x = 0;
                    _y = 1;
                    break;
                case 50:
                    _y = -1;
                    break;
                case 60:
                    _x = 1;
                    _y = 0;
                    break;
                case 70:
                    _x = -1;
                    break;
                case 80:
                    _x = 0;
                    _y = -1;
                    _frames = 0;
                    break;
            }
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Agata"))
                return;

            AgataNew.SetLife(-DamageInflicted);
            HitAgata(other);
        }

        private void HitAgata(Collision2D agata)
        {
            var enemyBody = transform.GetComponent<Rigidbody2D>();
            var agataBody = agata.transform.GetComponent<Rigidbody2D>();
            var newPosition = new Vector2();

            SoundManager.instance.PlaySingle(_agataHit);

            if (enemyBody.position.x > agataBody.position.x) //The enemy is to the right of Agata
            {
                if (agataBody.position.x >= 0)
                    newPosition.x = agataBody.position.x * -5;
                else
                    newPosition.x = agataBody.position.x * 5;
            }
            else //The enemy is to the left of Agata
            {
                if (agataBody.position.x >= 0)
                    newPosition.x = agataBody.position.x * 5;
                else
                    newPosition.x = agataBody.position.x * -5;
            }

            if (enemyBody.position.y > agataBody.position.y) //The enemy is above Agata
            {
                if (agataBody.position.y >= 0)
                    newPosition.y = agataBody.position.y * -5;
                else
                    newPosition.y = agataBody.position.y * 5;
            }
            else //The enemy is below Agata
            if (agataBody.position.y >= 0)
                newPosition.y = agataBody.position.y * 5;
            else
                newPosition.y = agataBody.position.y * -5;

            //Push Agata away after hit
            agataBody.AddForce(newPosition, ForceMode2D.Impulse);
        }
    }
}