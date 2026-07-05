namespace Monopoly
{
    partial class StartScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            button1 = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            textBox1 = new TextBox();
            label2 = new Label();
            imageList_Tokens = new ImageList(components);
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.Font = new Font("Impact", 30F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(622, 578);
            button1.Name = "button1";
            button1.Size = new Size(163, 63);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.Red;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Arial Narrow", 72F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(464, 205);
            label1.Name = "label1";
            label1.Size = new Size(456, 112);
            label1.TabIndex = 1;
            label1.Text = "- - - - POLY";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(515, 48);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(311, 248);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Left;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(98, 85);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(360, 360);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Right;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(926, 85);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(360, 360);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(680, 385);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(155, 23);
            textBox1.TabIndex = 5;
            textBox1.Text = "player1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(515, 385);
            label2.Name = "label2";
            label2.Size = new Size(159, 25);
            label2.TabIndex = 6;
            label2.Text = "Enter your Name:";
            // 
            // imageList_Tokens
            // 
            imageList_Tokens.ColorDepth = ColorDepth.Depth32Bit;
            imageList_Tokens.ImageStream = (ImageListStreamer)resources.GetObject("imageList_Tokens.ImageStream");
            imageList_Tokens.TransparentColor = Color.Transparent;
            imageList_Tokens.Images.SetKeyName(0, "LionSquare.jpg");
            imageList_Tokens.Images.SetKeyName(1, "DogSquare.jpg");
            imageList_Tokens.Images.SetKeyName(2, "BullSquare.jpg");
            imageList_Tokens.Images.SetKeyName(3, "HippoSquare.jpg");
            imageList_Tokens.Images.SetKeyName(4, "FoxSquare.jpg");
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.LionIcon;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(538, 471);
            button2.Name = "button2";
            button2.Size = new Size(65, 65);
            button2.TabIndex = 7;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackgroundImage = Properties.Resources.BullIcon;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(622, 471);
            button3.Name = "button3";
            button3.Size = new Size(65, 65);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.DogIcon;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(703, 471);
            button4.Name = "button4";
            button4.Size = new Size(65, 65);
            button4.TabIndex = 9;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.HippoIcon;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(784, 471);
            button5.Name = "button5";
            button5.Size = new Size(65, 65);
            button5.TabIndex = 10;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Narrow", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(587, 434);
            label3.Name = "label3";
            label3.Size = new Size(198, 25);
            label3.TabIndex = 11;
            label3.Text = "Choose a player icon:";
            // 
            // StartScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PaleTurquoise;
            ClientSize = new Size(1348, 653);
            Controls.Add(label3);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            DoubleBuffered = true;
            Name = "StartScreen";
            Text = "StartScreen";
            Load += StartScreen_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private TextBox textBox1;
        private Label label2;
        private ImageList imageList_Tokens;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label3;
    }
}