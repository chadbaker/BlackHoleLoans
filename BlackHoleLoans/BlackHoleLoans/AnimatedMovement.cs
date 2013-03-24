using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BlackHoleLoans
{
    public class AnimatedMovement
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int every;

        public AnimatedMovement(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            every = 0;
        }

        public void Update()
        {
            if (every == 15)
            {
                every = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
            every++;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);


            spriteBatch.Draw(Texture, location, sourceRectangle,
              Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

        }

        public void DrawStill(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height;
            int row = (int)((float)1 / (float)Columns);
            int column = 1 % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);


            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

            spriteBatch.Draw(Texture, location, sourceRectangle,
              Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

        }

    }
}