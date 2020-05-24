using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird
{
    class Columns : IMovable, IDrawable, IColidable
    {
        float movingSpeed = 5;
        public float x;
        float passHeight;
        float topHeight;
        float bottomHeight;
        float hole = 150;
        public Texture2D columnTexture;
        Rectangle upBody;
        Rectangle downBody;
        bool giveTick = false;

        public Columns(Texture2D texture, float x)
        {
            this.x = x;
            columnTexture = texture;
            upBody = new Rectangle(Convert.ToInt32(x), 0, texture.Width, texture.Height);
            downBody = new Rectangle(Convert.ToInt32(x), 0, texture.Width, texture.Height);
            SetHeight();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(columnTexture,
             new Vector2(x, topHeight),
             null,
             Color.White,
             0,
             new Vector2(columnTexture.Width / 2, columnTexture.Height / 2),
             1f,
             SpriteEffects.FlipVertically,
             0);

            spriteBatch.Draw(columnTexture,
             new Vector2(x, bottomHeight),
             null,
             Color.White,
             0,
             new Vector2(columnTexture.Width / 2, columnTexture.Height / 2),
             1f,
             SpriteEffects.None,
             0);
        }

        public void Move(float deltatime)
        {
            x -= movingSpeed;

            upBody.X = Convert.ToInt32(x);
            downBody.X = Convert.ToInt32(x);
        }

        void SetHeight()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            passHeight = random.Next(200, 400);
            topHeight = passHeight - columnTexture.Height;
            bottomHeight = passHeight + hole;

            upBody.Y = Convert.ToInt32(topHeight);
            downBody.Y = Convert.ToInt32(bottomHeight);
        }

        public void Colide(ref Bird bird)
        {
            if (bird.body.X + bird.body.Width > upBody.X && bird.body.X < upBody.X + upBody.Width)
            {
                if (bird.body.Y < upBody.Y + upBody.Height - hole)
                {
                    bird.isDead = true;
                }

                if (bird.body.Y + bird.body.Height > downBody.Y - 150)
                {
                    bird.isDead = true;
                }
            }
            if (bird.body.X > upBody.X + upBody.Width && !giveTick)
            {
                bird.score++;
                giveTick = true;
            }
        }
    }
}
