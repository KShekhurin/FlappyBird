using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class Bird : IDrawable, IMovable
    {
        
        public bool isDead = false;
        public int score = 0;
        float angle = 0;
        const float maxAngle = 50;
        const float minAngle = -350;
        float rotationSpeed = 25;
        float downSpeed;
        float poisitionX;
        float positionY;
        float startHeight;
        Texture2D[] textures;
        int currentFrame = 0;
        int oneFrameTime = 5;
        int ticks = 0;
        public Rectangle body;

        public Bird(float x, float y, Texture2D[] textures)
        {
            poisitionX = x;
            positionY = y;
            startHeight = positionY;
            this.textures = textures;
            body = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), textures[0].Width, textures[0].Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentFrame++;
            int index = 0;
            if (currentFrame < oneFrameTime)
            {
                index = 0;
            }
            else if (currentFrame < oneFrameTime * 2)
            {
                index = 1;
            }
            else if (currentFrame < oneFrameTime * 3)
            {
                index = 2;
            }
            else if (currentFrame < oneFrameTime * 4)
            {
                index = 1;
            }
            else if (currentFrame == oneFrameTime * 4 + 1)
            {
                index = 0;
                currentFrame = 0;
            }

            if (angle <= -80)
            {
                index = 0;
                currentFrame = oneFrameTime * 2;
            }

            spriteBatch.Draw(textures[index],
            new Vector2(poisitionX, positionY),
            null,
            Color.White,
            angle,
            new Vector2(textures[index].Width / 2, textures[index].Height / 2),
            1f,
            SpriteEffects.None,
            0);
        }

        public void Jump()
        {
            downSpeed = -10.5f;
            ticks = 0;
        }

        public void Move(float deltatime)
        {
            ticks++;
            float d = (float)(Math.Pow(Convert.ToDouble(ticks), 2));
            float deltaY = (downSpeed * ticks + 1.5f * d) * deltatime;

            if (deltaY >= 16)
            {
                deltaY = 16;
            }
            if (deltaY < 0)
            {
                deltaY -= 2;
            }

            positionY += deltaY;

            body.Y = Convert.ToInt32(positionY);

            if (deltaY < 0 /*|| positionY < startHeight + 50f*/)
            {
                if (angle < maxAngle)
                {
                    angle = maxAngle;
                }
            }
            else
            {
                if (angle > minAngle)
                {
                    angle -= rotationSpeed;
                }
            }
        }
    }
}
