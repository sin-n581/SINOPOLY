using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class Form2 : Form
    {

        Random rnd = new Random();
        List<Panel> panels;
        List<System.Windows.Forms.Label> OwnerLabels;
        bool playersTurn = true;
        int stepsRemaining = 0;
        bool rollDoubles = false;
        // Dictionary to store property information
        Dictionary<int, Property> properties = new Dictionary<int, Property>();
        int playerBalance = 1000;
        int opponentBalance = 1000;
        private string GameName;
        private string PlayerName;
        bool isMoving = false; // Flag to prevent multiple moves simultaneously
        bool awaitingPlayerDecision = false; // Flag to pause for player input
        int consecutiveDoubles = 0;
        int tokenID ;
        

        public Form2(string playerName, int imageIndex)
        {
            InitializeComponent();
            GameName = "SINOPOLY";
            PlayerName = playerName;

            tokenID = imageIndex;


            if (tokenID == 0) pictureBox1.Image = Properties.Resources.LionIcon;
            else if (tokenID == 1) pictureBox1.Image = Properties.Resources.BullIcon;
            else if (tokenID == 2) pictureBox1.Image = Properties.Resources.DogIcon;
            else if (tokenID == 3) pictureBox1.Image = Properties.Resources.HippoIcon;


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            panels = new List<Panel>
             {
                panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8, panel9, panel10,
                panel11, panel12, panel13, panel14, panel15, panel16, panel17, panel18, panel19, panel20,
                panel21, panel22, panel23, panel24, panel25, panel26

             };
            OwnerLabels = new List<System.Windows.Forms.Label>
            {
              LBowner0, LBowner1, LBowner2, LBowner3, LBowner4, LBowner5, LBowner6, LBowner7, LBowner8, LBowner9, LBowner10,
              LBowner11, LBowner12, LBowner13, LBowner14, LBowner15, LBowner16, LBowner17, LBowner18, LBowner19, LBowner20,
              LBowner21, LBowner22, LBowner23, LBowner24, LBowner25
            };

            LBplayerBalance.Text = playerBalance.ToString() + "M$";
            LBopponentBalance.Text = opponentBalance.ToString() + "M$";


            timer1.Interval = 200; // milliseconds (speed)
            pictureBox2.BringToFront();
            pictureBox1.BringToFront();
            

            label71.Text = PlayerName + " Balance:";
            label77.Text = GameName;
            WhoseTurn();
            InitializeProperties();


        }

        private void InitializeProperties()
        {
            // Define properties at specific panel indices
            properties[1] = new Property("Mediterranean Avenue", 60, 60);
            properties[2] = new Property("Baltic Avenue", 60, 60);
            properties[3] = new Property("Reading railroad", 200, 250, PropertyType.Railroad);
            properties[4] = new Property("Oriental Avenue", 100, 150);
            properties[5] = new Property("Vermont Avenue", 100, 150);
            properties[6] = new Property("Connecticut Avenue", 120, 150);
            properties[8] = new Property("St.Charles Place", 140, 170);
            properties[9] = new Property("States Avenue", 140, 170);

            properties[10] = new Property("Virginia Avenue", 160, 190);
            properties[11] = new Property("St. James Place", 180, 200);
            properties[12] = new Property("Tenesse Avenue", 200, 220);
            properties[14] = new Property("Kentucky Avenue", 220, 240);
            properties[15] = new Property("Indiana Avenue", 220, 260);
            properties[16] = new Property("Illinois Avenue", 240, 270);
            properties[17] = new Property("B and O railroad", 200, 200, PropertyType.Railroad);
            properties[18] = new Property("Atlantic Avenue", 260, 300);
            properties[19] = new Property("Ventor Avenue", 260, 280);

            properties[21] = new Property("Pacific Avenue", 300, 350);
            properties[22] = new Property("North Carolina Avenue", 140, 170);
            properties[23] = new Property("Pennsylvenia Avenue", 320, 340);
            properties[24] = new Property("Park Place", 350, 400);
            properties[25] = new Property("Boardwalk", 400, 4500);


            properties[0] = new Property("GO", 0, 0, PropertyType.Special);
            properties[13] = new Property("Free Parking", 0, 0, PropertyType.Special);
            properties[20] = new Property("Go to Jail", 0, 0, PropertyType.Special);
            properties[7] = new Property("Jail", 0, 0, PropertyType.Special);

        }


        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (stepsRemaining <= 0)
            {
                timer1.Stop();
                isMoving = false;


                // Handle landing action
                PictureBox currentPlayer = playersTurn ? pictureBox1 : pictureBox2;
                HandleLanding(currentPlayer);
                WhoseTurn();

                if (!rollDoubles)
                {

                    playersTurn = !playersTurn;
                }
                // If it's now the bot's turn, schedule the bot's roll
                if (!playersTurn && !awaitingPlayerDecision)
                {

                    botTimer.Interval = 1500; // Wait 2 seconds before bot rolls
                    botTimer.Tick += (s, ev) =>
                    {
                        botTimer.Stop();
                        botTimer.Dispose();
                        BotRoll();
                    };


                    botTimer.Start();

                }
                // StartBotTurn();



                // Reset doubles flag
                rollDoubles = false;

                button1.Enabled = true;
                WhoseTurn();

                return;
            }
            else
            {


                PictureBox currentPlayer = playersTurn ? pictureBox1 : pictureBox2;
                string currentPlayerName = playersTurn ? PlayerName : "Opponent";

                int currentIndex = -1;

                for (int i = 0; i < panels.Count; i++)
                {
                    if (panels[i].Controls.Contains(currentPlayer))
                    {
                        currentIndex = i;
                        break;
                    }
                }

                int nextIndex = (currentIndex + 1) % panels.Count;
                panels[nextIndex].Controls.Add(currentPlayer);



                Property property = properties[nextIndex];

                if (stepsRemaining <= 1 && property.Type != PropertyType.Special)
                {
                    WriteToGameLog($"{currentPlayerName} Landed on {property.Name}");
                }


                stepsRemaining--;
                PassedGO(currentIndex, nextIndex);


                pictureBox1.BringToFront();
                pictureBox2.BringToFront();




            }



        }


        private void PassedGO(int currentIndex, int nextIndex)
        {

            if (currentIndex > nextIndex)
            {

                if (playersTurn)
                {
                    playerBalance += 200;
                    LBplayerBalance.Text = playerBalance.ToString() + "M$";
                    MessageBox.Show("You passed GO! Collect 200M$");

                }
                else
                {
                    opponentBalance += 200;
                    LBopponentBalance.Text = opponentBalance.ToString() + "M$";
                    MessageBox.Show("Opponent passed GO! Collected 200M$");
                }

            }
        }

        private void HandleLanding(PictureBox player)
        {
            // Find which panel the player landed on
            int landedPanelIndex = -1;

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].Controls.Contains(player))
                {
                    landedPanelIndex = i;
                    break;
                }
            }

            int balance = 0;
            if (player == pictureBox2)
            {
                balance = opponentBalance;
                LBopponentBalance.Text = balance.ToString() + "M$";
            }
            else
            {
                balance = playerBalance;
                LBplayerBalance.Text = balance.ToString() + "M$";
            }

            if (landedPanelIndex == -1) return;

            // Check if there's a property at this location
            if (properties.ContainsKey(landedPanelIndex))
            {
                Property property = properties[landedPanelIndex];

                switch (property.Type)
                {
                    case PropertyType.Street:
                        HandleStreetLanding(property, player);
                        break;

                    case PropertyType.Railroad:
                        HandleStreetLanding(property, player);
                        break;

                    case PropertyType.Special:
                        HandleSpecialTileLanding(property, landedPanelIndex, player, balance);
                        break;



                }
            }

        }

        private void HandleStreetLanding(Property property, PictureBox player)
        {
            int balance;
            string currentPlayerName = playersTurn ? PlayerName : "Opponent";

            if (player == pictureBox2)
                balance = opponentBalance;
            else
                balance = playerBalance;


            if (property.Owner == null)
            {
                if (playersTurn)
                {
                    awaitingPlayerDecision = true;
                    ShowPurchaseDialog(property, player, balance);
                    awaitingPlayerDecision = false;
                }
                else
                {
                    BotDecidePurchase(property, player, balance);
                }
            }

            else if (property.Owner != currentPlayerName)
            {
                PayRent(property, player, balance);
            }

        }

        private void ShowPurchaseDialog(Property property, PictureBox player, int balance)
        {
            string playerName = playersTurn ? PlayerName : "Opponent";



            DialogResult result = MessageBox.Show(
                $"{playerName} landed on {property.Name}!\n\n" +
                $"Property Price: ${property.Price}\n" +
                $"Current Balance: ${balance}\n\n" +
                $"Do you want to buy this property?",
                "Property Purchase",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes && balance >= property.Price)
            {
                // Purchase property
                balance -= property.Price;
                property.Owner = playerName;
                property.IsPurchased = true;

                MessageBox.Show($"You successfully purchased {property.Name}! \n New Balance: ${balance}");

                if (player == pictureBox1)
                {
                    playerBalance = balance;
                }
                else
                {
                    opponentBalance = balance;
                }
                WriteToGameLog($"{playerName} purchased {property.Name}");

                UpdatePropertyDisplay(property, playerName);
            }
            else if (result == DialogResult.Yes && balance < property.Price)
            {
                MessageBox.Show("You don't have enough money to buy this property!");
            }

            if (player == pictureBox1)
            {
                playerBalance = balance;
                LBplayerBalance.Text = balance.ToString() + "M$";
            }
            else
            {
                opponentBalance = balance;
                LBopponentBalance.Text = balance.ToString() + "M$";
            }




            IsGameOver();
        }

        private void PayRent(Property property, PictureBox player, int balance)
        {
            string playerName = playersTurn ? PlayerName : "Opponent";
            int rentAmount = property.Rent;

            balance -= rentAmount;

            if (property.Owner == PlayerName)
            {
                playerBalance += rentAmount;
                LBplayerBalance.Text = playerBalance.ToString() + "M$";
            }
            else if (property.Owner == "Player 2" || property.Owner == "Opponent")
            {
                opponentBalance += rentAmount;
                LBopponentBalance.Text = opponentBalance.ToString() + "M$";
            }

            MessageBox.Show(
                $"{playerName} landed on {property.Name}, owned by {property.Owner}.\n" +
                $"Paid ${rentAmount} rent.");

            // Update payer balance
            if (player == pictureBox1)
            {
                playerBalance = balance;
                LBplayerBalance.Text = balance.ToString() + "M$";
            }
            else
            {
                opponentBalance = balance;
                LBopponentBalance.Text = balance.ToString() + "M$";
            }

            WriteToGameLog($"{playerName} paid ${rentAmount} to {property.Owner}");

            IsGameOver();
        }



        private void HandleSpecialTileLanding(Property property, int panelIndex, PictureBox player, int balance)
        {
            string playerName = playersTurn ? PlayerName : "Opponent";

            switch (property.Name)
            {
                case "GO":
                    MessageBox.Show((playersTurn ? "You " : "Opponent") + " landed on GO");
                    WriteToGameLog($"{playerName} landed on GO .");
                    break;

                case "Free Parking":
                    MessageBox.Show((playersTurn ? "You are" : "Opponent is") + " on Free Parking. Nothing happens.");
                    WriteToGameLog($"{playerName} landed on Free Parking.");
                    break;

                case "Go to Jail":
                    MessageBox.Show((playersTurn ? "You" : "Opponent") + " went to Jail, -100M$ Penalty!");
                    panels[7].Controls.Add(player);
                    balance -= 100;

                    WriteToGameLog($"{playerName} went to Jail and Paid up");
                    break;

                case "Jail":
                    MessageBox.Show((playersTurn ? "You are" : "Opponent is") + " in the jail, nothing happens!");
                    WriteToGameLog($"{playerName} landed on Jail but he is free to go");
                    break;
            }

            if (player == pictureBox1)
            {
                playerBalance = balance;
                LBplayerBalance.Text = balance.ToString() + "M$";
            }
            else
            {
                opponentBalance = balance;
                LBopponentBalance.Text = balance.ToString() + "M$";
            }
            pictureBox1.BringToFront();
            pictureBox2.BringToFront();

        }


        private void UpdatePropertyDisplay(Property property, string playerName)
        {


            // Find the panel index for this property and change the text of the label with the same index
            foreach (var kvp in properties)
            {
                if (kvp.Value == property)
                {


                    OwnerLabels[kvp.Key].Text = playerName;
                    break;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (isMoving || !playersTurn)
            {
                return; // Prevent player from rolling during movement or bot's turn
            }

            if (IsGameOver()) { return; }
            button1.Enabled = false;

            int firstDie = rnd.Next(1, 5); // random number between 1-4 because 1-6 would be too much for our board
            int secondDie = rnd.Next(1, 5);
            string currentPlayer = playersTurn ? PlayerName : "Opponent";
            WriteToGameLog($"{currentPlayer} rolled: {firstDie} and {secondDie}");


            stepsRemaining = firstDie + secondDie;

            LBfirstDie.Text = firstDie.ToString();
            LBsecondDie.Text = secondDie.ToString();

            rollDoubles = (firstDie == secondDie);

            if (HandleDoubles(rollDoubles, PlayerName, true))
            {
                return; // stop turn if penalty triggered
            }

            timer1.Start();

            WhoseTurn();

        }


        private void BotRoll()
        {
            if (playersTurn || isMoving)
            {
                return; // Safety check
            }

            int firstDie = rnd.Next(1, 5);
            int secondDie = rnd.Next(1, 5);
            stepsRemaining = firstDie + secondDie;

            LBfirstDie.Text = firstDie.ToString();
            LBsecondDie.Text = secondDie.ToString();

            rollDoubles = (firstDie == secondDie);



            isMoving = true;

            WriteToGameLog($"Opponent rolled: {firstDie} and {secondDie}");
            if (HandleDoubles(rollDoubles, "Opponent", false))
            {
                return;
            }
            timer1.Start();

            WhoseTurn();
        }

        private void BotDecidePurchase(Property property, PictureBox player, int balance)
        {
            // Bot purchasing logic
            bool shouldBuy = BotPurchasingStrategy(property, balance);

            if (shouldBuy && balance >= property.Price && property.Owner == null)
            {
                // Purchase property
                balance -= property.Price;
                property.Owner = "Opponent";
                property.IsPurchased = true;

                MessageBox.Show($"Opponent purchased {property.Name}! Opponent's New Balance: ${balance}");
                WriteToGameLog($"Opponent purchased {property.Name}");
                opponentBalance = balance;
                LBopponentBalance.Text = balance.ToString() + "M$";

                // Update UI to show property is owned
                UpdatePropertyDisplay(property, "Opponent");

            }
            else if (!shouldBuy || balance < property.Price)
            {
                MessageBox.Show($"Opponent declined to purchase {property.Name}.");

            }
            WhoseTurn();


        }

        private bool BotPurchasingStrategy(Property property, int balance)
        {
            int safetyMoney = 200;

            if (balance - property.Price >= safetyMoney)
            {
                return true;
            }

            return false;
        }


        private void WriteToGameLog(string action)
        {
            richTextBox1.AppendText(Environment.NewLine + action + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }

        private void WhoseTurn()
        {
            if (playersTurn)
            {
                LBplayerTurn.Text = PlayerName;
                LBplayerTurn.ForeColor = Color.Blue;
            }
            else
            {
                LBplayerTurn.Text = "Opponent";
                LBplayerTurn.ForeColor = Color.Crimson;
            }


        }

        private bool HandleDoubles(bool isDouble, string currentPlayerName, bool isPlayer)
        {
            if (isDouble)
            {
                consecutiveDoubles++;
            }
            else
            {
                consecutiveDoubles = 0;
            }


            if (consecutiveDoubles == 3)
            {
                MessageBox.Show($"{currentPlayerName} rolled 3 doubles in a row! Penalty applied.");

                int penalty = 100;

                if (isPlayer)
                {
                    playerBalance -= penalty;
                    LBplayerBalance.Text = playerBalance + "M$";
                    panels[7].Controls.Add(pictureBox1);
                    pictureBox1.BringToFront();
                }
                else
                {
                    opponentBalance -= penalty;
                    LBopponentBalance.Text = opponentBalance + "M$";
                    panels[7].Controls.Add(pictureBox2);
                    pictureBox2.BringToFront();
                }

                WriteToGameLog($"{currentPlayerName} rolled 3 doubles and paid ${penalty} penalty in Jail!");

                consecutiveDoubles = 0;


                playersTurn = !playersTurn;
                WhoseTurn();

                if (!playersTurn)
                {
                    BotRoll();
                }

                return true; // penalty triggered → stop turn
            }

            return false; // continue normally
        }
        private bool IsGameOver()
        {
            if (playerBalance <= 0)
            {
                EndGame("opponent");
                return true;
            }
            else if (opponentBalance <= 0)
            {
                EndGame(PlayerName);
                return true;
            }
            return false;
        }


        private void EndGame(string winner)
        {
            timer1.Stop();
            button1.Enabled = false;  // Disable the roll button

            MessageBox.Show(
                $"GAME OVER!\n\n" +
                $"{winner} WINS! \n\n" +
                $"{PlayerName} Balance: ${playerBalance}\n" +
                $"Opponent Balance: ${opponentBalance}",
                "Game Over",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            WriteToGameLog($"{winner} Won!");

            DialogResult resetResult = MessageBox.Show(
                "Do you want to play again?",
                "Play Again?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resetResult == DialogResult.Yes)
            {
                ResetGame();
            }
            else
            {
                this.Close();
            }
        }

        private void ResetGame()
        {
            playerBalance = 1000;
            opponentBalance = 1000;
            LBplayerBalance.Text = playerBalance.ToString() + "M$";
            LBopponentBalance.Text = opponentBalance.ToString() + "M$";

            panels[0].Controls.Add(pictureBox1);
            panels[0].Controls.Add(pictureBox2);
            pictureBox1.Location = new Point((panels[0].Width - pictureBox1.Width) / 2, (panels[0].Height - pictureBox1.Height) / 2 - 20);
            pictureBox2.Location = new Point((panels[0].Width - pictureBox2.Width) / 2, (panels[0].Height - pictureBox2.Height) / 2 + 20);

            LBfirstDie.Text = "0";
            LBsecondDie.Text = "0";

            playersTurn = true;
            WhoseTurn();
            stepsRemaining = 0;
            rollDoubles = false;

            foreach (var property in properties.Values)
            {
                property.Owner = null;
                property.IsPurchased = false;
            }

            foreach (var label in OwnerLabels)
            {
                label.Text = "";
            }

            button1.Enabled = true;

            richTextBox1.Clear();

            pictureBox1.BringToFront();
            pictureBox2.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void BTNhelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            " HOW TO PLAY\n\n" +
             "• You and the opponent start with 1000M$.\n" +
             "• Roll the dice to move around the board.\n" +
             "• Buy properties when you land on them.\n" +
             "• Pay rent if you land on opponent's property.\n\n" +

             " DOUBLES\n" +
            "• Rolling doubles gives you another turn.\n" +
            "• Rolling doubles 3 times = -100M$ penalty and move to jail.\n\n" +

            " SPECIAL TILES\n" +
             "• Passing GO: +200M$\n" +
             "• Go to Jail: -100M$ and move to Jail\n" +
             "• Free Parking / Jail: nothing happens\n\n" +

            "🏆 WIN\n" +
             "• If a player reaches 0M$, they lose.\n" +
            "• The other player wins the game.\n\n" +

            "Good luck and have fun!",
            "How to Play",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
            );
        }
    }

    public class Property
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Rent { get; set; }
        public string Owner { get; set; }
        public bool IsPurchased { get; set; }
        public PropertyType Type { get; set; }

        public Property(string name, int price, int rent, PropertyType type = PropertyType.Street)
        {
            Name = name;
            Price = price;
            Rent = rent;
            Owner = null;
            IsPurchased = false;
            Type = type;
        }
    }

    public enum PropertyType
    {
        Street,
        Railroad,
        Utility,
        Special
    }


}

