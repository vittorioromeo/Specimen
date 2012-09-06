#region
using SFML.Graphics;
using SFML.Window;
using SFMLStart.Data;
using SFMLStart.Utilities;
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter.Components
{
    public class CRender : Component
    {
        private readonly CBody _cBody;
        private readonly SPGame _game;
        private readonly string _labelName;
        private readonly float _rotation;
        private readonly string _textureName;
        private readonly string _tilesetName;

        public CRender(SPGame mGame, CBody mCBody, string mTextureName, string mTilesetName = null,
                       string mLabelName = null, float mRotation = 0)
        {
            _game = mGame;
            _cBody = mCBody;
            _textureName = mTextureName;
            _tilesetName = mTilesetName;
            _labelName = mLabelName;
            _rotation = mRotation;
        }

        public Sprite Sprite { get; set; }
        public Animation Animation { get; set; }
        public float Torque { get; set; }

        private void Draw() { _game.GameWindow.RenderWindow.Draw(Sprite); }

        public override void Added()
        {
            var x = _cBody.X.ToPixels();
            var y = _cBody.Y.ToPixels();

            Sprite = new Sprite(Assets.GetTexture(_textureName));
            if (_tilesetName != null && _labelName != null)
                Sprite.TextureRect = Assets.Tilesets[_tilesetName].GetTextureRect(_labelName);
            Sprite.Rotation = _rotation;
            Sprite.Position = new Vector2f(x, y);
            Sprite.Origin = new Vector2f(Sprite.GetGlobalBounds().Width/2, Sprite.GetGlobalBounds().Height/2);

            _game.AddDrawAction(Draw);
        }

        public override void Update(float mFrameTime)
        {
            var x = _cBody.X.ToPixels();
            var y = _cBody.Y.ToPixels();

            Sprite.Position = new Vector2f(x, y);
            Sprite.Rotation += Torque*mFrameTime;

            if (Animation == null) return;
            Animation.Update(mFrameTime);
            Sprite.TextureRect = Assets.Tilesets[_tilesetName].GetTextureRect(Animation.GetCurrentLabel());
        }

        public override void Removed() { _game.RemoveDrawAction(Draw); }
    }
}