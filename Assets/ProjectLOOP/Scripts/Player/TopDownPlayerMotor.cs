using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectLOOP
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class TopDownPlayerMotor : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float gravity = -20f;

        CharacterController _controller;
        float _verticalVelocity;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            var input = ReadMoveInput();
            var move = new Vector3(input.x, 0f, input.y);
            if (move.sqrMagnitude > 1f)
            {
                move.Normalize();
            }

            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -1f;
            }
            else
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }

            var velocity = move * moveSpeed;
            velocity.y = _verticalVelocity;
            _controller.Move(velocity * Time.deltaTime);

            if (move.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(move, Vector3.up);
            }
        }

        static Vector2 ReadMoveInput()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return Vector2.zero;
            }

            var x = 0f;
            var y = 0f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) x -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) x += 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) y -= 1f;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) y += 1f;
            return new Vector2(x, y);
        }
    }
}
