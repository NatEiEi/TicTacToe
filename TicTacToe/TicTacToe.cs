using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _line, _circle, _cross , _therock , _lineArrow , _lineArrow2 , _Trident;
        List<Vector2> _lineList;

        bool _isCircleTurn;
        bool _isGameEnded;
        bool _isOWon;
        SpriteFont _font;
        int[,] _gameTable;
        MouseState _mouseState;

        Point _win1, _win2;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            _gameTable = new int[3, 3];

            _isCircleTurn = true;
            _isGameEnded = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load a Texture2D from a file
            _line = this.Content.Load<Texture2D>("Line");
            _circle = this.Content.Load<Texture2D>("Circle");
            _cross = this.Content.Load<Texture2D>("Cross");
            _therock = this.Content.Load<Texture2D>("therock");

            _lineArrow = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _lineArrow2 = new Texture2D(_graphics.GraphicsDevice, 10, 100);

            Color[] data = new Color[1];
            Color[] data2 = new Color[1000];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            for (int i = 0; i < data2.Length; ++i) data2[i] = Color.White;
            _lineArrow.SetData(data);
            _lineArrow2.SetData(data2);

            _font = this.Content.Load<SpriteFont>("GameFont");

            _Trident = this.Content.Load<Texture2D>("Trident");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState state = Mouse.GetState();
            _mouseState = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && !_isGameEnded)
            {
                int iPos = state.X / 200;
                int jPos = state.Y / 200;

                if (iPos >= 0 && iPos < 3 && jPos >= 0 && jPos < 3)
                {
                    //check feasibility
                    if (_gameTable[jPos, iPos] == 0)
                    {
                        if (_isCircleTurn)
                        {
                            _gameTable[jPos, iPos] = 1;
                        }
                        else
                        {
                            _gameTable[jPos, iPos] = -1;
                        }

                        //flip turn
                        _isCircleTurn = !_isCircleTurn;
                    }
                }
            }

            //check winning condition
            if (_gameTable[0, 0] == _gameTable[1, 0] &&
                _gameTable[1, 0] == _gameTable[2, 0] &&
                _gameTable[0, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 0);
                _win2 = new Point(0, 2);
                _isOWon = _gameTable[0, 0] > 0 ? true : false;
            }
            else if (_gameTable[0, 1] == _gameTable[1, 1] &&
                _gameTable[1, 1] == _gameTable[2, 1] &&
                _gameTable[0, 1] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(1, 0);
                _win2 = new Point(1, 2);
                _isOWon = _gameTable[1, 0] > 0 ? true : false;
            }
            else if (_gameTable[0, 2] == _gameTable[1, 2] &&
               _gameTable[1, 2] == _gameTable[2, 2] &&
                _gameTable[0, 2] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(2, 0);
                _win2 = new Point(2, 2);
                _isOWon = _gameTable[2, 0] > 0 ? true : false;
            }
            else if (_gameTable[0, 0] == _gameTable[0, 1] &&
               _gameTable[0, 1] == _gameTable[0, 2] &&
                _gameTable[0, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 0);
                _win2 = new Point(2, 0);
                _isOWon = _gameTable[0, 0] > 0 ? true : false;
            }
            else if (_gameTable[1, 0] == _gameTable[1, 1] &&
               _gameTable[1, 1] == _gameTable[1, 2] &&
                _gameTable[1, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 1);
                _win2 = new Point(2, 1);
                _isOWon = _gameTable[1, 0] > 0 ? true : false;
            }
            else if (_gameTable[2, 0] == _gameTable[2, 1] &&
              _gameTable[2, 1] == _gameTable[2, 2] &&
                _gameTable[2, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 2);
                _win2 = new Point(2, 2);
                _isOWon = _gameTable[2, 0] > 0 ? true : false;
            }
            else if (_gameTable[0, 0] == _gameTable[1, 1] &&
             _gameTable[1, 1] == _gameTable[2, 2] &&
                _gameTable[0, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 0);
                _win2 = new Point(2, 2);
                _isOWon = _gameTable[0, 0] > 0 ? true : false;
            }
            else if (_gameTable[2, 0] == _gameTable[1, 1] &&
            _gameTable[1, 1] == _gameTable[0, 2] &&
                _gameTable[2, 0] != 0)
            {
                _isGameEnded = true;
                _win1 = new Point(0, 2);
                _win2 = new Point(2, 0);
                _isOWon = _gameTable[2, 0] > 0 ? true : false;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            

            _spriteBatch.Begin();
            _spriteBatch.Draw(_therock, new Vector2(200 , 200 ), null, Color.ForestGreen, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            
            
            
            
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_gameTable[i, j] == 0)
                    {
                        //nothing
                    }
                    else if (_gameTable[i, j] == 1)
                    {
                        //circle
                        _spriteBatch.Draw(_circle, new Vector2(200 * j, 200 * i), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else if (_gameTable[i, j] == -1)
                    {
                        //cross
                        _spriteBatch.Draw(_cross, new Vector2(200 * j, 200 * i), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }

            //Lines
            _spriteBatch.Draw(_line, new Vector2(0, 200), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(0, 400), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);


            _spriteBatch.Draw(_line, new Vector2(200, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(400, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //Finish line






            







            if (_isGameEnded)
            {
                if (_win1.X == _win2.X)
                {
                    _spriteBatch.Draw(_line, new Vector2(_win1.X * 200 + 100, 0), null, Color.Red, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                else if (_win1.Y == _win2.Y)
                {
                    _spriteBatch.Draw(_line, new Vector2(0, _win1.Y * 200 + 100), null, Color.Red, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                else if (_win1.X == _win1.Y)
                {
                    _spriteBatch.Draw(_line, new Vector2(0, 0), null, Color.Red, MathHelper.Pi / 4, Vector2.Zero, 1.414f, SpriteEffects.None, 0f);
                }
                else if (_win2.X == _win1.Y)
                {
                    _spriteBatch.Draw(_line, new Vector2(0, 600), null, Color.Red, -MathHelper.Pi / 4, Vector2.Zero, 1.414f, SpriteEffects.None, 0f);
                }
            }




            


          
            float _rotateAngle;
            Point TridentPos = new Point(300 , 600);


            if ((_mouseState.X - TridentPos.X) != 0 && _mouseState.X > TridentPos.X) 
            {
                _rotateAngle = -MathHelper.Pi / 2 + (float)Math.Atan((float)(TridentPos.Y - _mouseState.Y) / (float)(_mouseState.X - TridentPos.X));
            }
            else if ((_mouseState.X - TridentPos.X) != 0 && _mouseState.X < TridentPos.X)
            {
                _rotateAngle = -MathHelper.Pi / 2 + MathHelper.Pi - (float)Math.Atan((float)(TridentPos.Y - _mouseState.Y) / (float)(TridentPos.X - _mouseState.X ));

            }
            else
            {
                _rotateAngle = 0;
            }
   

            _spriteBatch.Draw(_Trident, new Vector2(TridentPos.X , TridentPos.Y), null, Color.White, -_rotateAngle, new Vector2(0+80 , 300), 1f, SpriteEffects.None, 0f);
            
            
            
            //_spriteBatch.DrawString(_font, "PosX : "  + " : " + (300 - _mouseState.Y) + " " +Math.Atan(1)+ " ::::::  " + _Rotate, new Vector2(0,  0), Color.White);
            float _Range = (float)Math.Sqrt(Math.Pow((TridentPos.Y - _mouseState.Y), 2) + Math.Pow((_mouseState.X - 300), 2));
            _spriteBatch.Draw(_lineArrow, new Vector2(300, 300), null, Color.Red, -_rotateAngle, Vector2.Zero, new Vector2(_Range, 2f), SpriteEffects.None, 0f);








            _spriteBatch.End();

            _graphics.BeginDraw();

            base.Draw(gameTime);
        }

        protected void getLinePos()
        {
            
        }
    }
}
