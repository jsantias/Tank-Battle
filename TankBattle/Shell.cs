using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace TankBattle
{
	/// <summary>
	/// Type of WeaponEffect represents the projectile or shell launched by a ControlledTank. A shell
	/// is launched at a certain angle and velocity and is affected by gravity and wind.
	/// 
	/// Author John Santias and Hoang Nguyen October 2017
	/// </summary>
	public class Shell : WeaponEffect
    {
        private const int STEP_AMOUNT = 10;
        private float x, y, gravity, xVelocity, yVelocity;
        Blast explosion;
        Opponent player;

		/// <summary>
		/// Constructs a new Shell. The x, y, gravity, explosion and playerfields are stored in private
		/// fields of Shell. Two more field (x velocity and y velocity) are also initialised. This refers
		/// to how much the Shell moves in 1/10 of a frame.
		/// 
		/// Author John Santias October 2017
		/// </summary>
		/// <param name="x">The starting x position of the shell</param>
		/// <param name="y">The starting y position of the shell</param>
		/// <param name="angle">The angle for the shell fire at</param>
		/// <param name="power">The power/speed of the shell</param>
		/// <param name="gravity">Gravity for the shell to come back to the ground</param>
		/// <param name="explosion">Type of explosion</param>
		/// <param name="player">Which player is shooting the shell</param>
		public Shell(float x, float y, float angle, float power, float gravity, Blast explosion, Opponent player)
        {
            this.x = x;
            this.y = y;
            this.gravity = gravity;
            this.explosion = explosion;
            this.player = player;
            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;
            xVelocity = (float)Math.Cos(angleRadians) * magnitude;
            yVelocity = (float)Math.Sin(angleRadians) * -magnitude;
        }

		/// <summary>
		/// Moves the projectile (shell) according to its angle, power, gravity and wind. The process is 
		/// done 10 times for the projectile to fire from the tank, move according to its angle,
		/// velocity, gravity and hit the ground. 
		/// 
		/// Author John Santias October 2017
		/// </summary>
		public override void Process()
        {
			for (int step = 0; step < STEP_AMOUNT; step++)
			{
				x += xVelocity;
				y += yVelocity;
				x += x / i.GetWindSpeed() / 1000.0f;

                // Return and end effect if shell is out of screen
				if (x < 0 || x > Terrain.WIDTH || y < 0 || y > Terrain.HEIGHT)
				{
					i.EndEffect(this);
					return;
				}
                
                // Make effects if shell detects a collision
				else if (i.CheckHitTank(x, y))
				{
					player.ProjectileHitPos(x, y);
					explosion.Explode(x, y);
					i.AddWeaponEffect(explosion);
					i.EndEffect(this);
					return;
				}

				yVelocity += gravity;
			}
            
        }

		/// <summary>
		/// Draws the shell as a small white circle.
		/// 
		/// Author John Santias October 2017
		/// </summary>
		/// <param name="graphics">The looks of the shell</param>
		/// <param name="size">The size of the shell</param>
		public override void Draw(Graphics graphics, Size size)
        {
            float x = (float)this.x * size.Width / Terrain.WIDTH;
            float y = (float)this.y * size.Height / Terrain.HEIGHT;
            float s = size.Width / Terrain.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
