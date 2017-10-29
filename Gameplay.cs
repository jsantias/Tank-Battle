using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace TankBattle {
    public class Gameplay {
        //check if array / int
        private int[] numPlayers;
        private int numRounds;
        private int playerNum;
        private int currentPlayer;
        private List<WeaponEffect> WeaponsEffect;
        private int currentRound;
        private int opponent;
        private int x;
        private Terrain newTerrain;
        private Opponent[] TheOppo;
        private int Wind;
        private ControlledTank[] TheTank;
        private Random rnd = new Random();
        private float tankPositionX;
        private float tankPositionY;

        public Gameplay(int numPlayers, int numRounds) {
            this.numPlayers = new int[numPlayers];
            this.numRounds = numRounds;
            TheOppo = new Opponent[numPlayers];
            WeaponsEffect = new List<WeaponEffect>();
        }

        public int PlayerCount() {
            return numPlayers.Length;
        }

        public int GetRound() {
            return currentRound;
        }

        public int GetTotalRounds() {
            return numRounds;
        }

        public void SetPlayer(int playerNum, Opponent player) {
            TheOppo[playerNum - 1] = player;
        }

        public Opponent GetPlayerNumber(int playerNum) {
            return TheOppo[playerNum - 1];
        }

        public ControlledTank PlayerTank(int playerNum) {
            return TheTank[playerNum - 1];
        }

        public static Color GetTankColour(int playerNum) {
            Color[] TheColor = new Color[] { Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Red, Color.Violet, Color.Indigo, Color.Turquoise };
            return TheColor[playerNum - 1];
        }

        public static int[] CalculatePlayerPositions(int numPlayers) {
            int[] coords = new int[numPlayers];

            int terrainW = Terrain.WIDTH / numPlayers;
            int spaceBetweenTanks = terrainW / 2;

            // If there are 2 players
            if (numPlayers == 2) {
                coords[0] = spaceBetweenTanks;
                coords[1] = spaceBetweenTanks + terrainW;

                // If there are more than 2 players
            } else {
                for (int i = 1; i < numPlayers; i++) {
                    coords[0] = spaceBetweenTanks;
                    coords[i] = coords[i - 1] + terrainW;
                }
            }

            return coords;
        }

        public static void Shuffle(int[] array) {
            Random rndA = new Random();
            Random rndB = new Random();
            for (int i = array.Length; i >= 1; i--) {

                int idxA = rndA.Next(0, array.Length), idxB = rndB.Next(0, array.Length);

                while (idxA == idxB) {
                    idxB = rndB.Next(0, array.Length);
                }

                int tmp = array[idxA];
                array[idxA] = array[idxB];
                array[idxB] = tmp;
            }
        }

        public void NewGame() {
            currentRound = 1;
            opponent = 0;
            BeginRound();
        }

        public void BeginRound() {
            currentPlayer = opponent;
            newTerrain = new Terrain();
            int[] thepos = CalculatePlayerPositions(TheOppo.Length);

            for (int i = 0; i < TheOppo.Length - 1; i++) {
                TheOppo[i].StartRound();
            }

            Shuffle(thepos);
            TheTank = new ControlledTank[TheOppo.Length];
            for (int i = 0; i < TheTank.Length; i++) {
                TheTank[i] = new ControlledTank(TheOppo[i], thepos[i], newTerrain.TankYPosition(thepos[i]), this);
            }

            GetWindSpeed();

            GameplayForm newForm = new GameplayForm(this);
            newForm.Show();
        }

        public Terrain GetLevel() {
            return newTerrain;
        }

        public void DrawPlayers(Graphics graphics, Size displaySize) {
            for (int i = 0; i < TheTank.Length; i++) {
                if (TheTank[i].Exists()) {
                    TheTank[i].Draw(graphics, displaySize);
                }
            }
        }

        public ControlledTank GetCurrentGameplayTank() {
            return TheTank[currentPlayer];
        }

        public void AddWeaponEffect(WeaponEffect weaponEffect) {
            //for (int i = 0; i < WeaponsEffect.Count; i++)
            //         {
            //             if (WeaponsEffect[i] == null)
            //             {
            //                 WeaponsEffect[i] = weaponEffect;
            //             }
            //         }
            WeaponsEffect.Add(weaponEffect);
            weaponEffect.RecordCurrentGame(this);

        }

        public bool ProcessEffects() {
            bool weaponExist = false;
            //for (int i = 0; i < WeaponsEffect.Count; i++)
            //         {
            //             if (WeaponsEffect[i] != null) { weaponExist = true;}
            //             else { weaponExist = false; }
            //         }
            for (int i = 0; i < WeaponsEffect.Count; i++) {
                WeaponsEffect[i].Process();
                weaponExist = true;
            }
            return weaponExist;
        }

        public void RenderEffects(Graphics graphics, Size displaySize) {
            if (ProcessEffects()) {
                for (int i = 0; i < WeaponsEffect.Count; i++) {
                    WeaponsEffect[i].Draw(graphics, displaySize);
                }
            }
        }

        public void EndEffect(WeaponEffect weaponEffect) {
            if (WeaponsEffect.Contains(weaponEffect)) {
                WeaponsEffect.Remove(weaponEffect);
            }
        }

        public bool CheckHitTank(float projectileX, float projectileY) //double check
        {
            if (projectileX < 0 | projectileX > Terrain.WIDTH || projectileY < 0 || projectileY > Terrain.HEIGHT) { return false; }
            if (newTerrain.IsTileAt((int)projectileX, (int)projectileY)) {
                return true;
            }
            for (int i = 0; i < TheTank.Length; i++) {
                if (i == playerNum)   //prevent current tank hitting itself
                {
                    return false;
                }
                if (TheTank[i].Exists() && (TheTank[i].GetX() == projectileX && TheTank[i].GetYPos() == projectileY)) {
                    if (TheTank[i].Exists() && (projectileX <= TheTank[i].GetX() + TankModel.WIDTH && projectileY <= TheTank[i].GetYPos() + TankModel.HEIGHT)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void InflictDamage(float damageX, float damageY, float explosionDamage, float radius) //double check, incomplete
        {

            int damageDone = 0;
            float tempDamage = 0;

            for (int i = 0; i < TheTank.Length; i++) {
                if (TheTank[i].Exists()) {
                    float dist;
                    double tempDist;

                    tankPositionX = (TheTank[i].GetX() + (TankModel.WIDTH / 2));
                    tankPositionY = (TheTank[i].GetYPos() + (TankModel.HEIGHT / 2));

                    tempDist = Math.Sqrt(Math.Pow(damageX - tankPositionX, 2) + Math.Pow(damageY - tankPositionY, 2));
                    dist = (float)tempDist;

                    if (dist > radius) {
                        tempDamage = 0;
                    } else if (dist > radius && dist < radius / 2) {
                        tempDamage = (explosionDamage * (radius - dist) / radius);
                    } else if (dist < radius / 2) {
                        tempDamage = explosionDamage;
                    }

                    damageDone = (int)tempDamage;
                    TheTank[i].InflictDamage(damageDone);

                }
            }
        }

        public bool Gravity() //double check
        {
            bool anyMovement = false;
            newTerrain.Gravity();
            if (newTerrain.Gravity() == true) //if gravity applied to terrain, set bool to true
            {
                anyMovement = true;
            }
            for (int i = 0; i < TheTank.Length; i++) {
                newTerrain.Gravity();
                if (TheTank[i].Gravity() == true) {
                    anyMovement = true;
                }
            }
            return anyMovement;
        }

        public bool TurnOver() //double check
        {
            bool tanksExists = false;
            int howManyExists = 0;

            for (int i = 0; i < TheTank.Length; i++) {
                if (TheTank[i].Exists()) //checks to see how many tanks there are
                {
                    howManyExists++;
                }
            }

            if (howManyExists >= 2) //if there is two or more tanks, continue the round
            {
                currentPlayer++;
                if (currentPlayer == numPlayers.Length) { currentPlayer = 0; }
                if (!TheTank[currentPlayer].Exists()) {
                    currentPlayer++;
                }
                Wind += rnd.Next(-10, 10);
                if (Wind <= -100) { Wind = -100; } else if (Wind >= 100) { Wind = 100; }
                return true;
            } else if (howManyExists < 2) //if there is one tank left
              {
                RewardWinner();
                return false;
            }
            return tanksExists;
        }

        public void RewardWinner() //double check
        {
            for (int i = 0; i < TheTank.Length; i++) {
                if (TheTank[i].Exists()) {
                    TheOppo[currentPlayer].AddScore(); //correct tank? //option 1
                    // GetPlayerNumber(TheTank[i]).AddScore(); //option 2
                }
            }
        }

        public void NextRound() //double check
        {
            if (!TurnOver()) {
                if (currentRound <= numRounds) {
                    currentRound++;
                }
                currentPlayer++;
                if (currentPlayer > numPlayers.Length - 1) {
                    currentPlayer = 0;
                    BeginRound();
                } else if (currentRound > numRounds) {
                    IntroForm introform = new IntroForm();
                    introform.Show();
                }
            }
        }

        public int GetWindSpeed() {
            Random rnd = new Random();
            Wind = rnd.Next(-100, 101);
            return Wind;
        }
    }
}