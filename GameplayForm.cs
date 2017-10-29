using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TankBattle {
    public partial class GameplayForm : Form {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Gameplay currentGame;

        //added
        private ControlledTank currentControlledTank;
        private TankModel currentTankModel;
        private Opponent currentOpponent;

        private int windSpeed = 0;
        private int angleSet = 0;
        private int powerSet = 0;
        private int weaponSet = 0;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        public GameplayForm(Gameplay game) {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            string[] imageFilenames = { "Images\\background1.jpg",
                                        "Images\\background2.jpg",
                                        "Images\\background3.jpg",
                                        "Images\\background4.jpg"};

            Color[] landscapeColours = { Color.FromArgb(255, 0, 0, 0),
                                        Color.FromArgb(255, 73, 58, 47),
                                        Color.FromArgb(255, 148, 116, 93),
                                        Color.FromArgb(255, 133, 119, 109) };

            currentGame = game;

            int randNumber = rng.Next(4);

            //Set random background image and colour 
            backgroundImage = Image.FromFile(imageFilenames[randNumber]);
            landscapeColour = (landscapeColours[randNumber]);

            InitializeComponent();

            //Initialise graphic buffers
            backgroundGraphics = InitRenderBuffer();
            gameplayGraphics = InitRenderBuffer();

            DrawBackground();

            DrawGameplay();

            NewTurn();
        }

        public void EnableTankButtons() {
            controlPanel.Enabled = true;
        }

        public void SetAimingAngle(float angle) {
            angleSetter.Value = (int)angle;
        }

        public void SetPower(int power) {
			powerTrackBar.Value = power;
        }
        public void SetWeaponIndex(int weapon) {
            weaponComboBox.SelectedItem = weapon;
        }

        public void Attack() {
            currentControlledTank = currentGame.GetCurrentGameplayTank();
            currentControlledTank.Attack(); //Calls currentGame's GetCurrentGameplayTank() method to get a reference to the current player's
                                            //ControlledTank, then calls its Attack() method.
            controlPanel.Enabled = false;
            formTimer.Enabled = true;
		}

        private void DrawGameplay() {
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            currentGame.DrawPlayers(gameplayGraphics.Graphics, displayPanel.Size);
            currentGame.RenderEffects(gameplayGraphics.Graphics, displayPanel.Size);
        }

        private void NewTurn() {
            //First, get a reference to the current ControlledTank with currentGame.GetCurrentGameplayTank()
            currentControlledTank = currentGame.GetCurrentGameplayTank();

            //Likewise, get a reference to the current Opponent by calling the ControlledTank's GetPlayerNumber()
            currentOpponent = currentControlledTank.GetPlayerNumber();

            //Set the form caption to "Tank Battle - Round ? of ?", using methods in currentGame to get the current and
            //total rounds.
            this.Text = "Tank Battle - Round " + currentGame.GetRound() + " of " + currentGame.GetTotalRounds();

            //Set the BackColor property of controlPanel to the current Opponent's colour.
            controlPanel.BackColor = currentOpponent.GetColour();

            //Set the player name label to the current Opponent's name.
            playerLabel.Text = currentOpponent.Name();

            //Call SetAimingAngle() to set the current angle to the current ControlledTank's angle.
            currentControlledTank.SetAimingAngle(angleSet);

            //Call SetPower() to set the current turret power to the current ControlledTank's power.
            currentControlledTank.SetPower(powerSet);

            //Update the wind speed label to show the current wind speed, retrieved from currentGame.
            //Positive values should be shown as E winds, negative values as W winds.
            //For example, 50 would be displayed as "50 E" while -38 would be displayed as "38 W".
            windSpeed = currentGame.GetWindSpeed();

            if (windSpeed > 0) {
                windStatusLabel.Text = windSpeed + " E";
            } else if (windSpeed < 0) {
                windStatusLabel.Text = -windSpeed + " W";
            }

            //Clear the current weapon names from the ComboBox.
            weaponComboBox.Items.Clear();

            //Get a reference to the current TankModel with ControlledTank's GetTank() method, then get a list of
            //weapons available to that TankModel.
            currentTankModel = currentControlledTank.GetTank(); //not stored anywhere
            string[] weapons = currentTankModel.WeaponList();

            //Add each weapon name in the list to the ComboBox.
            for (int i = 0; i < weapons.Length; i++) {
                weaponComboBox.Items.Add(weapons[i]);
            }

            //Call SetWeaponIndex() to set the current weapon to the current ControlledTank's weapon.
            weaponSet = currentControlledTank.GetWeaponIndex();
            SetWeaponIndex(weaponSet);

            //Call the current Opponent's BeginTurn() method, passing in this and currentGame.
            currentOpponent.BeginTurn(this, currentGame);
        }


        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void DrawBackground() {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Terrain battlefield = currentGame.GetLevel();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Terrain.HEIGHT; y++) {
                for (int x = 0; x < Terrain.WIDTH; x++) {
                    if (battlefield.IsTileAt(x, y)) {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitRenderBuffer() {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void angleSetter_ValueChanged(object sender, EventArgs e) {
            angleSet = (int)angleSetter.Value;
            currentControlledTank.SetAimingAngle(angleSet);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void weaponComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            weaponSet = weaponComboBox.SelectedIndex;
            currentControlledTank.SetWeaponIndex(weaponSet);
        }

        private void formTimer_Tick(object sender, EventArgs e) {
			currentGame.ProcessEffects();
            if (currentGame.ProcessEffects() == false)
            {
                currentGame.Gravity();
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();
                if (currentGame.Gravity())
                {
                    return;
                }
                else
                {
                    formTimer.Enabled = false;
                    currentGame.TurnOver();
                    NewTurn();
                    if (!currentGame.TurnOver())
                    {
						if (currentGame.GetRound() < currentGame.GetTotalRounds())
						{
							currentGame.NextRound();
						}
						else
						{
							Dispose();
							Rankings ranks = new Rankings();
							ranks.Show();
						}
						
					}
                    currentGame.NextRound();
                    return;
                }
            }
            else
            {
                DrawGameplay();
                displayPanel.Invalidate();
                return;
            }
        }

		private void fireButton_Click(object sender, EventArgs e)
		{
			Attack();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
            powerSet = powerTrackBar.Value;
			currentControlledTank.SetPower(powerSet);
			DrawGameplay();
			displayPanel.Invalidate();
			powerLevelLabel.Text = powerTrackBar.Value.ToString();
		}
	}
}