﻿using System;
using UnityEngine;

namespace Hourai.SmashBrew {

    [DisallowMultipleComponent]
    [RequiredCharacterComponent]
    public class CharacterMovement : RestrictableCharacterComponent {

        [SerializeField]
        private float _airSpeed = 3f;

        [SerializeField]
        private float _dashSpeed = 5f;

        [SerializeField]
        private float _walkSpeed = 3f;

        public void Move(Vector2 direction) {
            if (Restricted || Mathf.Abs(direction.x) < float.Epsilon)
                return;

            Vector3 vel = Character.Velocity;

            if (Character.IsGrounded) {
                if (Character.IsDashing)
                    vel.x = _dashSpeed;
                else
                    vel.x = _walkSpeed;
            } else
                vel.x = _airSpeed;
            vel.x = Util.MatchSign(vel.x, direction.x);

            Character.Velocity = vel;
        }

    }

}