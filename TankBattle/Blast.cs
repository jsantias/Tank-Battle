﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{
	/// <summary>
	/// Type of WeaponEffect that represents the payload attached to a Shell. An Blast will inflict damage on tanks
	/// and destroy terrain within a radius.
	/// 
	/// Author John Santias and Hoang Nguyen October 2017
	/// </summary>
	public class Blast : WeaponEffect
    {
        private int explosionDamage, explosionRadius, earthDestructionRadius;
        private float x, y, lifespan;

		/// <summary>
		/// The Blast takes the explosion damage, explosion radius and earth destruction radius values it
		/// is passed and stores them as private fields.
		/// 
		/// Author John Santias October 2017
		/// </summary>
		/// <param name="explosionDamage">The amount of damage the blast can do</param>
		/// <param name="explosionRadius">The radius of the explosion</param>
		/// <param name="earthDestructionRadius">Radius of the damange done to the earth by the explosion</param>
		public Blast(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.earthDestructionRadius = earthDestructionRadius;
        }

		/// <summary>
		/// Blast is detonated at the specified location. 
		/// 
		/// Author John Santias October 2017
		/// </summary>
		/// <param name="x">Center position of the explosion</param>
		/// <param name="y">Center y position of the explosion</param>
		public void Explode(float x, float y)
        {
            this.x = x;
            this.y = y;
            lifespan = 1.0f;
        }

		/// <summary>
		/// Reduces the Blast's lifespan by 0.02. When the Blast's life is less than or equal to zero.
		/// The blast damages the objects at where it landed.
		/// 
		/// Author John Santias October 2017
		/// </summary>
		public override void Process()
        {
            lifespan -= 0.02f;
            if (lifespan <= 0)
            {
				lifespan = 0;
                i.InflictDamage(x, y, explosionDamage, earthDestructionRadius);
                Terrain theterrain = i.GetLevel();
                theterrain.DestroyGround(x, y, earthDestructionRadius);
                i.EndEffect(this);
            }
        }

		/// <summary>
		/// Draws one frame of the Blast. Draws a circle that expands, cycling from yellow to red and then
		/// fading out. 
		/// 
		/// Author John Santias October 2017
		/// </summary>
		/// <param name="graphics">The looks of the shell</param>
		/// <param name="displaySize">The size of the shell</param>
		public override void Draw(Graphics graphics, Size displaySize)
        {
            float x = (float)this.x * displaySize.Width / Terrain.WIDTH;
            float y = (float)this.y * displaySize.Height / Terrain.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - lifespan) * explosionRadius * 3.0 / 2.0) / Terrain.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (lifespan < 1.0 / 3.0)
            {
                red = 255;
                alpha = (int)(lifespan * 3.0 * 255);
            }
            else if (lifespan < 2.0 / 3.0)
            {
                red = 255;
                alpha = 255;
                green = (int)((lifespan * 3.0 - 1.0) * 255);
            }
            else
            {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((lifespan * 3.0 - 2.0) * 255);
            }

            RectangleF rect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
