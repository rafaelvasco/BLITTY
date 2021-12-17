using BLITTY;
using System.Numerics;

namespace BLITTY_Demos
{
    internal class DrawQuadDemo : Scene
    {
        private Texture2D? _logoBig;
        private Texture2D? _logoSmall;
        private Canvas2D? _canvas;
        private Quad _quadBig;
        private Quad _quadSmall;

        private Vector2 _position;
        private Vector2 _position2;

        private float sx = 200.0f, sy = 200.0f;

        public override void Load()
        {
            _logoBig = Content.GetLogo();
            _logoSmall = Content.GetLogoSmall();
            _canvas = Graphics.GetCanvas2D(64);
            _quadBig = new Quad(_logoBig);
            _quadSmall = new Quad(_logoSmall);
            _position = new Vector2(Game.WindowSize.Width/2, Game.WindowSize.Height/2 );

            _position2 = _position + new Vector2(0f, 100f);

        }

        public override void Unload()
        {
        }

        public override void FixedUpdate(float dt)
        {
        }

        public override void Update(float dt)
        {
            _position2.X += sx * dt;
            _position2.Y += sy * dt;

            var halfW = _logoSmall!.Width / 2.0f;
            var halfH = _logoSmall!.Height / 2.0f;

            if (_position2.X < halfW)
            {
                _position2.X = halfW;
                sx = -sx;
            }
            else if (_position2.X > Game.WindowSize.Width - halfW)
            {
                _position2.X = Game.WindowSize.Width - halfW;
                sx = -sx;
            }

            if (_position2.Y < halfH)
            {
                _position2.Y = halfH;
                sy = -sy;
            }
            else if (_position2.Y > Game.WindowSize.Height - halfH)
            {
                _position2.Y = Game.WindowSize.Height - halfH;
                sy = -sy;
            }

        }

        public override void Draw()
        {
            _canvas?.Begin();

            _canvas?.DrawQuad(_logoBig!, _quadBig, _position);
            _canvas?.DrawQuad(_logoSmall!, _quadSmall, _position2);

            _canvas?.End();
        }
    }
}
