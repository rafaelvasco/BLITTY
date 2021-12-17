using BLITTY;
using BLITTY.Audio;

namespace BLITTY_Demos
{
    internal class AudioDemo : Scene
    {
        private Sound? soundBlip;
        private Sound? music;
        private Channel? musicChannel;
        private RenderView _view;

        public override void Load()
        {
            soundBlip = Content.Get<Sound>("BlipSelect");
            music = Content.Get<Sound>("StageKen");
            _view = Graphics.CreateDefaultView();

            musicChannel = music.Play();
        }

        public override void Unload()
        {
        }

        public override void FixedUpdate(float dt)
        {
        }

        public override void Update(float dt)
        {
            if (Input.KeyPressed(Keys.Space))
            {
                soundBlip?.Play();
            }
        }

        public override void Draw()
        {
            Graphics.ApplyRenderView(_view);

            Graphics.ApplyRenderState(RenderState.Default);
        }
    }
}
