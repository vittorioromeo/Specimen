#region
using SFML.Graphics;
using SFMLStart.Utilities;
using SFMLStart.Vectors;
using TestGenericShooter.Components;
using TestGenericShooter.Resources;
using VeeCollision;
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter
{
    public class Factory
    {
        private readonly SPGame _game;
        private readonly Manager _manager;
        private readonly World _world;

        public Factory(SPGame mGame, Manager mManager, World mWorld)
        {
            _game = mGame;
            _manager = mManager;
            _world = mWorld;
        }

        #region Components
        private CBody Body(SSVector2I mPosition, int mWidth, int mHeight, bool mIsStatic = false) { return new CBody(new Body(_world, mPosition, mIsStatic, mWidth, mHeight)); }

        private CRender Render(CBody mCBody, string mTextureName, string mTilesetName = null, string mLabelName = null,
                               float mRotation = 0) { return new CRender(_game, mCBody, mTextureName, mTilesetName, mLabelName, mRotation); }

        private CMovement Movement(CBody mCBody) { return new CMovement(mCBody); }

        private CControl Control(CBody mCBody, CStats mCStats, CMovement mCMovement, CRender mCRender) { return new CControl(_game, mCStats, mCBody, mCMovement, mCRender); }

        private CChild Child(Entity mParent, CBody mCBody) { return new CChild(mParent, mCBody); }

        private CAI AI(CStats mCStats, CBody mCBody, CMovement mCMovement, CRender mCRender, CLineOfSight mCLineOfSight) { return new CAI(_game, mCStats, mCBody, mCMovement, mCRender, mCLineOfSight); }
        #endregion

        #region Environment
        public Entity Wall(int mX, int mY, string mLabelName = "fill", float mRotation = 0)
        {
            var result = new Entity(_manager);

            var cBody = Body(new SSVector2I(mX, mY), 16.ToUnits(), 16.ToUnits(), true);
            var cRender = Render(cBody, Textures.Wall, Tilesets.Wall, mLabelName, mRotation);

            cBody.Body.AddGroups(Groups.Obstacle);

            result.AddComponents(cBody, cRender);
            result.AddTags(Tags.Wall, Tags.DestroysBullets);

            return result;
        }

        public Entity BreakableWall(int mX, int mY)
        {
            var result = new Entity(_manager);

            var cHealth = new CHealth(85);
            var cBody = Body(new SSVector2I(mX, mY), 16.ToUnits(), 16.ToUnits(), true);
            var cRender = Render(cBody, Textures.Wall, Tilesets.Wall, "breakable");

            cBody.Body.AddGroups(Groups.Obstacle);

            result.AddComponents(cHealth, cBody, cRender);
            result.AddTags(Tags.Wall, Tags.DamagedByAny);

            cRender.Sprite.Rotation = Utils.Random.Next(0, 4)*90;

            return result;
        }

        public Entity Decoration(int mX, int mY, string mTextureName, string mTilesetName, string mLabelName,
                                 float mRotation = 0)
        {
            var result = new Entity(_manager);

            var cBody = Body(new SSVector2I(mX, mY), 1600, 1600, true);
            var cRender = Render(cBody, mTextureName, mTilesetName, mLabelName, mRotation);

            cBody.Body.AddGroups(Groups.Decoration);

            result.AddComponents(cBody, cRender);
            result.AddTags(Tags.Decoration);

            return result;
        }
        #endregion

        #region Characters
        public Entity Player(int mX, int mY)
        {
            var result = new Entity(_manager);

            var cStats = new CStats(1, 1, 1, 50);
            var cHealth = new CHealth(cStats);
            var cBody = Body(new SSVector2I(mX, mY), 10.ToUnits(), 10.ToUnits());
            var cMovement = Movement(cBody);
            var cRender = Render(cBody, Textures.CharPlayer, Tilesets.Char, "normal");
            var cControl = Control(cBody, cStats, cMovement, cRender);
            var cShadower = new CShadower(_game, cBody, true, new Color(77, 77, 175, 255), 1);

            cBody.Body.AddGroups(Groups.Character, Groups.Friendly);
            cBody.Body.AddGroupsToCheck(Groups.Obstacle);

            result.AddComponents(cStats, cHealth, cBody, cMovement, cControl, cRender, cShadower);
            result.AddTags(Tags.Char, Tags.Friendly, Tags.DamagedByEnemy);

            return result;
        }

        public Entity Enemy(int mX, int mY)
        {
            var result = new Entity(_manager);

            var cStats = new CStats(1, 1, 1, 75);
            var cHealth = new CHealth(cStats);
            var cBody = Body(new SSVector2I(mX, mY), 10.ToUnits(), 10.ToUnits());
            var cMovement = Movement(cBody);
            var cRender = Render(cBody, Textures.CharEnemy, Tilesets.Char, "normal");
            var cShadower = new CShadower(_game, cBody, true, new Color(125, 255, 125, 125));
            var cLineOfSight = new CLineOfSight(_game, cBody, cShadower) {TargetTag = Tags.Friendly, Angle = 90, Amplitude = 65};
            var cAI = AI(cStats, cBody, cMovement, cRender, cLineOfSight);

            cBody.Body.AddGroups(Groups.Character, Groups.Enemy);
            cBody.Body.AddGroupsToCheck(Groups.Obstacle);

            result.AddComponents(cHealth, cBody, cMovement, cRender, cShadower, cLineOfSight, cAI);
            result.AddTags(Tags.Char, Tags.Enemy, Tags.DamagedByFriendly);

            return result;
        }
        #endregion

        private Entity BulletBase(int mX, int mY, float mDegrees, int mSpeed, string mTextureName, bool mEnemy)
        {
            var result = new Entity(_manager);

            var cBody = Body(new SSVector2I(mX, mY), 250, 250);
            var cMovement = Movement(cBody);
            var cRender = Render(cBody, mTextureName);

            cBody.Body.AddGroupsToCheck(Groups.Obstacle, Groups.Character);
            cBody.Body.AddGroupsToIgnoreResolve(Groups.Obstacle, Groups.Character);
            cBody.OnCollision += (mCollisionInfo) =>
                                 {
                                     var entity = (Entity)mCollisionInfo.UserData;
                                     var cHealth = entity.GetComponent<CHealth>();

                                     if (entity.HasTag(Tags.DamagedByAny))
                                     {
                                         cHealth.Health--;
                                         result.Destroy();
                                     }
                                     else if (result.HasTag(Tags.BulletFriendly) &&
                                              entity.HasTag(Tags.DamagedByFriendly))
                                     {
                                         cHealth.Health--;
                                         result.Destroy();
                                     }
                                     else if (result.HasTag(Tags.BulletEnemy) &&
                                              entity.HasTag(Tags.DamagedByEnemy))
                                     {
                                         cHealth.Health--;
                                         result.Destroy();
                                     }

                                     if (entity.HasTag(Tags.DestroysBullets)) result.Destroy();
                                 };

            cMovement.Angle = mDegrees;
            cMovement.Speed = mSpeed;

            cRender.Torque = 8;

            result.AddComponents(cBody, cMovement, cRender);
            result.AddTags(Tags.Bullet);

            return result;
        }

        public Entity Bullet(int mX, int mY, float mDegrees, int mSpeed, bool mEnemy)
        {
            var texture = !mEnemy ? Textures.BulletFriendly : Textures.BulletEnemy;
            var result = BulletBase(mX, mY, mDegrees, mSpeed, texture, mEnemy);
            result.AddTags(!mEnemy ? Tags.BulletFriendly : Tags.BulletEnemy);
            return result;
        }
    }
}