namespace MathFunction
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class MathFunctionGame : Game
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private WaveFunction _waveFunction;
        private MathFunctionRenderer _mathFunctionRenderer;
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public MathFunctionGame()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _waveFunction = new WaveFunction();
            _mathFunctionRenderer = new MathFunctionRenderer(GraphicsDevice, 1000) { Width = 700, Height = 400, RangeX = 2.0 / _waveFunction.Frequency, RangeY = 1.2, MathFunction = _waveFunction.MathFunction };
            _font = Content.Load<SpriteFont>("arial");
        }

        protected override void Update(GameTime gameTime)
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();

            float elapsedSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;

            // You shouldn't call MathFunctionRenderer.Update every frame!
            // Only call it when something changes in your MathFunction.

            if (_currentState.IsKeyDown(Keys.Right))
            {
                _waveFunction.Frequency += 150.0 * elapsedSeconds;
                _mathFunctionRenderer.Update();
            }

            if (_currentState.IsKeyDown(Keys.Left))
            {
                _waveFunction.Frequency -= 150.0 * elapsedSeconds;
                _waveFunction.Frequency = Math.Max(1.0, _waveFunction.Frequency);
                _mathFunctionRenderer.Update();
            }

            if (_currentState.IsKeyDown(Keys.Up))
            {
                _waveFunction.Amplitude += 1.0 * elapsedSeconds;
                _waveFunction.Amplitude = Math.Min(1.0, _waveFunction.Amplitude);
                _mathFunctionRenderer.Update();
            }

            if (_currentState.IsKeyDown(Keys.Down))
            {
                _waveFunction.Amplitude -= 1.0 * elapsedSeconds;
                _waveFunction.Amplitude = Math.Max(0.0, _waveFunction.Amplitude);
                _mathFunctionRenderer.Update();
            }

            if (_currentState.IsKeyDown(Keys.Space) && _previousState.IsKeyUp(Keys.Space))
            {
                _waveFunction.NextType();
                _mathFunctionRenderer.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _mathFunctionRenderer.Draw(_spriteBatch, 50, 50);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Amplitude: " + _waveFunction.Amplitude.ToString("0.0") + "\nFrequency: " + _waveFunction.Frequency.ToString("0.0") + "Hz\nWave Type: " + _waveFunction.Type, new Vector2(50, 475), Color.White);
            _spriteBatch.DrawString(_font, "Up / Down: Change amplitude\nLeft / Right: Change frequency\nSpacebar: Change wave type", new Vector2(460, 475), Color.White);
            _spriteBatch.DrawString(_font, "Source at http://www.david-gouveia.com", new Vector2(420, 565), Color.Blue);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
