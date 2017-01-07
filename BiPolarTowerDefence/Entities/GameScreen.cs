using Microsoft.Xna.Framework;

namespace BiPolarTowerDefence.Entities
{
    public enum ScreenState
    {
        Active,
        Hidden
    }

    public abstract class GameScreen
    {
        public ScreenManager ScreenManager { get; internal set; }

        public ScreenState ScreenState { get; protected set; } = ScreenState.Hidden;

        public virtual void Activate()
        {
            ScreenState = ScreenState.Active;
        }

        public virtual void Deactivate()
        {
            ScreenState = ScreenState.Hidden;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void Load()
        {
        }
    }
}