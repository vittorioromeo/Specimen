#region
using System;
using System.Linq;
using SFMLStart.Utilities;
using SFMLStart.Utilities.Timelines;
using SFMLStart.Vectors;
using Specimen.Resources;
using VeeEntity;

#endregion

namespace Specimen.Components
{
    public class CAI : Component
    {
        private const int StateWandering = 0;
        private const int StateFoundTarget = 1;
        private const int StateWait = 2;

        private const float TurnAroundDelayMax = 5;

        private readonly CBody _cBody;
        private readonly CLineOfSight _cLineOfSight;
        private readonly CMovement _cMovement;
        private readonly CRender _cRender;
        private readonly CStats _cStats;
        private readonly SPGame _game;

        public int BulletSpeedBase;
        public int BulletSpeedIncrease;
        public int BulletSpeedMax;
        public float ChaseSpeed;
        public float ShootDelayMax;
        public float WaitAmplitude;
        public float WaitDelayMax;
        public float WanderingDirectionDelayMax;
        public float WanderingSpeed;
        private int _bulletSpeed;

        private float _currentDirection = Utils.Random.Next(0, 360);
        private float _shootDelay;
        private int _state;

        private Timeline _timeline;
        private float _turnAroundDelay;
        private float _waitAngle;
        private float _waitDelay;
        private bool _waitDirection;
        private float _wanderingDirectionDelay;

        public CAI(SPGame mGame, CStats mCStats, CBody mCBody, CMovement mCMovement, CRender mCRender,
                   CLineOfSight mCLineOfSight)
        {
            _game = mGame;
            _cStats = mCStats;
            _cBody = mCBody;
            _cLineOfSight = mCLineOfSight;
            _cMovement = mCMovement;
            _cRender = mCRender;

            WaitAmplitude = _cStats.Speed.Total/5f + 50;
            WaitDelayMax = 450 - _cStats.Speed.Total/5 + _cStats.Endurance.Total*5f;
            BulletSpeedBase = 150 + _cStats.Agility.Total*8;
            BulletSpeedMax = 400 + _cStats.Agility.Total*10;
            BulletSpeedIncrease = 25 + _cStats.Agility.Total*3;
            WanderingDirectionDelayMax = 50 - _cStats.Speed.Total/8f;
            WanderingSpeed = _cStats.Speed.Total;
            ChaseSpeed = _cStats.Speed.Total/10f;
            ShootDelayMax = 40 - _cStats.Agility.Total/8f;

            _bulletSpeed = BulletSpeedBase;

            mCBody.OnCollision += (mCollisionInfo) =>
                                  {
                                      var entity = (Entity) mCollisionInfo.UserData;
                                      if (entity.HasTag(Tags.Wall))
                                      {
                                          if (_state == StateWandering && _turnAroundDelay <= 0)
                                          {
                                              _turnAroundDelay = TurnAroundDelayMax;
                                              _currentDirection += 90*Utils.Random.NextSign();
                                          }
                                      }

                                      if (entity.HasTag(Tags.BulletFriendly))
                                      {
                                          _state = StateWait;
                                          var vector = mCollisionInfo.Body.Position - mCollisionInfo.Body.Velocity*5;
                                          _currentDirection =
                                              Utils.Math.Angles.TowardsDegrees(new SSVector2F(_cBody.X, _cBody.Y),
                                                                               new SSVector2F(vector));
                                      }
                                  };
        }

        public override void Update(float mFrameTime)
        {
            if (_turnAroundDelay >= 0) _turnAroundDelay -= 1*mFrameTime;

            // Look for targets in the field of view
            var target = _cLineOfSight.Targets.FirstOrDefault();

            // No targets = wander around
            // Target = check it out
            if (target == null)
            {
                if (_state == StateFoundTarget) _state = StateWait;
            }
            else _state = StateFoundTarget;

            if (_state != StateFoundTarget) _bulletSpeed = BulletSpeedBase;
            if (_state != StateWait) _waitDelay = 0;

            if (_state == StateWandering)
            {
                // Look where the AI is moving
                _cLineOfSight.Angle = Utils.Math.Angles.RotateTowardsAngleDegrees(_cLineOfSight.Angle, _currentDirection, 3.25f*mFrameTime);

                _wanderingDirectionDelay += 1*mFrameTime;

                if (_wanderingDirectionDelay >= WanderingDirectionDelayMax)
                {
                    _wanderingDirectionDelay = 0;
                    _currentDirection += Utils.Random.Next(-35, 35);
                    if (Utils.Random.Next(0, 100) > 90) _currentDirection += 180;
                }

                _cMovement.Speed = WanderingSpeed;
                _cMovement.Angle = _currentDirection;
            }
            else if (_state == StateFoundTarget)
            {
                // Look where the AI is moving
                _cLineOfSight.Angle = Utils.Math.Angles.RotateTowardsAngleDegrees(_cLineOfSight.Angle, _currentDirection, 3.25f*mFrameTime);

                var targetAngle = Utils.Math.Angles.TowardsDegrees(new SSVector2F(_cBody.X, _cBody.Y),
                                                                   new SSVector2F(target.Item2));

                _currentDirection = targetAngle;

                _shootDelay += mFrameTime + (float) Utils.Random.NextDouble()/100f;

                if (_shootDelay > ShootDelayMax)
                {
                    _timeline = new Timeline();

                    _timeline.AddCommand(
                        new Do(
                            () =>
                            _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y, targetAngle, _bulletSpeed, true)));
                    _timeline.AddCommand(new Wait(3));
                    _timeline.AddCommand(
                        new Do(
                            () =>
                            _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y,
                                                 targetAngle + Utils.Random.Next(-5, 5), _bulletSpeed, true)));
                    _timeline.AddCommand(new Wait(3));
                    _timeline.AddCommand(
                        new Do(
                            () =>
                            _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y,
                                                 targetAngle + Utils.Random.Next(-5, 5), _bulletSpeed, true)));

                    _shootDelay = 0;

                    if (_bulletSpeed < BulletSpeedMax)
                        _bulletSpeed += BulletSpeedIncrease;
                }

                _cMovement.Speed = ChaseSpeed;
                _cMovement.Angle = _currentDirection;
            }
            else if (_state == StateWait)
            {
                _cMovement.Speed = 0;
                _cMovement.Angle = _currentDirection;

                _waitDelay += 1*mFrameTime;
                if (_waitDelay >= WaitDelayMax) _state = StateWandering;

                if (_waitDirection) _waitAngle += 0.5f*mFrameTime;
                else _waitAngle -= 0.5f*mFrameTime;

                // Look around
                _cLineOfSight.Angle = Utils.Math.Angles.RotateTowardsAngleDegrees(_cLineOfSight.Angle,
                                                                                  _currentDirection + _waitAngle,
                                                                                  3.25f*mFrameTime);

                if (Math.Abs(_waitAngle) >= WaitAmplitude) _waitDirection = !_waitDirection;
            }

            if (_timeline != null) _timeline.Update(mFrameTime);
        }
    }
}