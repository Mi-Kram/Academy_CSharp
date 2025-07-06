namespace Main
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.resultPanel = new System.Windows.Forms.Panel();
            this.historyTextBox = new System.Windows.Forms.TextBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.memoryPanel = new System.Windows.Forms.Panel();
            this.MMinusButton = new System.Windows.Forms.Button();
            this.MPlusButton = new System.Windows.Forms.Button();
            this.MRButton = new System.Windows.Forms.Button();
            this.MCButton = new System.Windows.Forms.Button();
            this.textPanel = new System.Windows.Forms.Panel();
            this.op2Panel = new System.Windows.Forms.Panel();
            this.multiplyButton = new System.Windows.Forms.Button();
            this.minusButton = new System.Windows.Forms.Button();
            this.plusButton = new System.Windows.Forms.Button();
            this.equalButton = new System.Windows.Forms.Button();
            this.numsPanel = new System.Windows.Forms.Panel();
            this.sevenButton = new System.Windows.Forms.Button();
            this.sixButton = new System.Windows.Forms.Button();
            this.comaButton = new System.Windows.Forms.Button();
            this.twoButton = new System.Windows.Forms.Button();
            this.ChangeSignButton = new System.Windows.Forms.Button();
            this.nineButton = new System.Windows.Forms.Button();
            this.threeButton = new System.Windows.Forms.Button();
            this.fourButton = new System.Windows.Forms.Button();
            this.fiveButton = new System.Windows.Forms.Button();
            this.eightButton = new System.Windows.Forms.Button();
            this.zeroButton = new System.Windows.Forms.Button();
            this.oneButton = new System.Windows.Forms.Button();
            this.op1Panel = new System.Windows.Forms.Panel();
            this.PercentButton = new System.Windows.Forms.Button();
            this.one_div_xButton = new System.Windows.Forms.Button();
            this.CEButton = new System.Windows.Forms.Button();
            this.x_multiply_xButton = new System.Windows.Forms.Button();
            this.divisionButton = new System.Windows.Forms.Button();
            this.CButton = new System.Windows.Forms.Button();
            this.SqrtButton = new System.Windows.Forms.Button();
            this.BackSpaceButton = new System.Windows.Forms.Button();
            this.resultPanel.SuspendLayout();
            this.memoryPanel.SuspendLayout();
            this.textPanel.SuspendLayout();
            this.op2Panel.SuspendLayout();
            this.numsPanel.SuspendLayout();
            this.op1Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultPanel
            // 
            this.resultPanel.BackColor = System.Drawing.SystemColors.GrayText;
            this.resultPanel.Controls.Add(this.historyTextBox);
            this.resultPanel.Controls.Add(this.resultTextBox);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.resultPanel.Location = new System.Drawing.Point(0, 0);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(363, 87);
            this.resultPanel.TabIndex = 0;
            // 
            // historyTextBox
            // 
            this.historyTextBox.BackColor = System.Drawing.SystemColors.GrayText;
            this.historyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.historyTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.historyTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.historyTextBox.Location = new System.Drawing.Point(12, 11);
            this.historyTextBox.Name = "historyTextBox";
            this.historyTextBox.ReadOnly = true;
            this.historyTextBox.Size = new System.Drawing.Size(343, 15);
            this.historyTextBox.TabIndex = 1;
            this.historyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // resultTextBox
            // 
            this.resultTextBox.BackColor = System.Drawing.SystemColors.GrayText;
            this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.resultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.resultTextBox.Location = new System.Drawing.Point(12, 36);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.Size = new System.Drawing.Size(343, 48);
            this.resultTextBox.TabIndex = 0;
            this.resultTextBox.Text = "0";
            this.resultTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // memoryPanel
            // 
            this.memoryPanel.BackColor = System.Drawing.SystemColors.GrayText;
            this.memoryPanel.Controls.Add(this.MMinusButton);
            this.memoryPanel.Controls.Add(this.MPlusButton);
            this.memoryPanel.Controls.Add(this.MRButton);
            this.memoryPanel.Controls.Add(this.MCButton);
            this.memoryPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.memoryPanel.Location = new System.Drawing.Point(0, 87);
            this.memoryPanel.Name = "memoryPanel";
            this.memoryPanel.Size = new System.Drawing.Size(363, 40);
            this.memoryPanel.TabIndex = 1;
            // 
            // MMinusButton
            // 
            this.MMinusButton.BackColor = System.Drawing.SystemColors.GrayText;
            this.MMinusButton.FlatAppearance.BorderSize = 0;
            this.MMinusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MMinusButton.ForeColor = System.Drawing.SystemColors.Window;
            this.MMinusButton.Location = new System.Drawing.Point(276, 10);
            this.MMinusButton.Name = "MMinusButton";
            this.MMinusButton.Size = new System.Drawing.Size(85, 30);
            this.MMinusButton.TabIndex = 3;
            this.MMinusButton.Text = "M-";
            this.MMinusButton.UseVisualStyleBackColor = false;
            this.MMinusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MMinusButton_MouseUp);
            // 
            // MPlusButton
            // 
            this.MPlusButton.BackColor = System.Drawing.SystemColors.GrayText;
            this.MPlusButton.FlatAppearance.BorderSize = 0;
            this.MPlusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MPlusButton.ForeColor = System.Drawing.SystemColors.Window;
            this.MPlusButton.Location = new System.Drawing.Point(185, 10);
            this.MPlusButton.Name = "MPlusButton";
            this.MPlusButton.Size = new System.Drawing.Size(85, 30);
            this.MPlusButton.TabIndex = 2;
            this.MPlusButton.Text = "M+";
            this.MPlusButton.UseVisualStyleBackColor = false;
            this.MPlusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MPlusButton_MouseUp);
            // 
            // MRButton
            // 
            this.MRButton.BackColor = System.Drawing.SystemColors.GrayText;
            this.MRButton.FlatAppearance.BorderSize = 0;
            this.MRButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MRButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MRButton.ForeColor = System.Drawing.SystemColors.Window;
            this.MRButton.Location = new System.Drawing.Point(94, 10);
            this.MRButton.Name = "MRButton";
            this.MRButton.Size = new System.Drawing.Size(85, 30);
            this.MRButton.TabIndex = 1;
            this.MRButton.Text = "MR";
            this.MRButton.UseVisualStyleBackColor = false;
            this.MRButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MRButton_MouseUp);
            // 
            // MCButton
            // 
            this.MCButton.BackColor = System.Drawing.SystemColors.GrayText;
            this.MCButton.FlatAppearance.BorderSize = 0;
            this.MCButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MCButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MCButton.ForeColor = System.Drawing.SystemColors.Window;
            this.MCButton.Location = new System.Drawing.Point(3, 10);
            this.MCButton.Name = "MCButton";
            this.MCButton.Size = new System.Drawing.Size(85, 30);
            this.MCButton.TabIndex = 0;
            this.MCButton.Text = "MC";
            this.MCButton.UseVisualStyleBackColor = false;
            this.MCButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MCButton_MouseUp);
            // 
            // textPanel
            // 
            this.textPanel.BackColor = System.Drawing.SystemColors.GrayText;
            this.textPanel.Controls.Add(this.op2Panel);
            this.textPanel.Controls.Add(this.numsPanel);
            this.textPanel.Controls.Add(this.op1Panel);
            this.textPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textPanel.Location = new System.Drawing.Point(0, 127);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new System.Drawing.Size(363, 337);
            this.textPanel.TabIndex = 3;
            // 
            // op2Panel
            // 
            this.op2Panel.Controls.Add(this.multiplyButton);
            this.op2Panel.Controls.Add(this.minusButton);
            this.op2Panel.Controls.Add(this.plusButton);
            this.op2Panel.Controls.Add(this.equalButton);
            this.op2Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.op2Panel.Location = new System.Drawing.Point(270, 117);
            this.op2Panel.Name = "op2Panel";
            this.op2Panel.Size = new System.Drawing.Size(93, 220);
            this.op2Panel.TabIndex = 10;
            // 
            // multiplyButton
            // 
            this.multiplyButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.multiplyButton.FlatAppearance.BorderSize = 0;
            this.multiplyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.multiplyButton.ForeColor = System.Drawing.SystemColors.Window;
            this.multiplyButton.Location = new System.Drawing.Point(6, 0);
            this.multiplyButton.Name = "multiplyButton";
            this.multiplyButton.Size = new System.Drawing.Size(85, 50);
            this.multiplyButton.TabIndex = 0;
            this.multiplyButton.Text = "×";
            this.multiplyButton.UseVisualStyleBackColor = false;
            this.multiplyButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.multiplyButton_MouseUp);
            // 
            // minusButton
            // 
            this.minusButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.minusButton.FlatAppearance.BorderSize = 0;
            this.minusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minusButton.ForeColor = System.Drawing.SystemColors.Window;
            this.minusButton.Location = new System.Drawing.Point(6, 56);
            this.minusButton.Name = "minusButton";
            this.minusButton.Size = new System.Drawing.Size(85, 50);
            this.minusButton.TabIndex = 1;
            this.minusButton.Text = "-";
            this.minusButton.UseVisualStyleBackColor = false;
            this.minusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.minusButton_MouseUp);
            // 
            // plusButton
            // 
            this.plusButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.plusButton.FlatAppearance.BorderSize = 0;
            this.plusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plusButton.ForeColor = System.Drawing.SystemColors.Window;
            this.plusButton.Location = new System.Drawing.Point(6, 112);
            this.plusButton.Name = "plusButton";
            this.plusButton.Size = new System.Drawing.Size(85, 50);
            this.plusButton.TabIndex = 2;
            this.plusButton.Text = "+";
            this.plusButton.UseVisualStyleBackColor = false;
            this.plusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plusButton_MouseUp);
            // 
            // equalButton
            // 
            this.equalButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.equalButton.FlatAppearance.BorderSize = 0;
            this.equalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.equalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.equalButton.ForeColor = System.Drawing.SystemColors.Window;
            this.equalButton.Location = new System.Drawing.Point(6, 168);
            this.equalButton.Name = "equalButton";
            this.equalButton.Size = new System.Drawing.Size(85, 50);
            this.equalButton.TabIndex = 3;
            this.equalButton.Text = "=";
            this.equalButton.UseVisualStyleBackColor = false;
            this.equalButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.equalButton_MouseUp);
            // 
            // numsPanel
            // 
            this.numsPanel.Controls.Add(this.sevenButton);
            this.numsPanel.Controls.Add(this.sixButton);
            this.numsPanel.Controls.Add(this.comaButton);
            this.numsPanel.Controls.Add(this.twoButton);
            this.numsPanel.Controls.Add(this.ChangeSignButton);
            this.numsPanel.Controls.Add(this.nineButton);
            this.numsPanel.Controls.Add(this.threeButton);
            this.numsPanel.Controls.Add(this.fourButton);
            this.numsPanel.Controls.Add(this.fiveButton);
            this.numsPanel.Controls.Add(this.eightButton);
            this.numsPanel.Controls.Add(this.zeroButton);
            this.numsPanel.Controls.Add(this.oneButton);
            this.numsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.numsPanel.Location = new System.Drawing.Point(0, 117);
            this.numsPanel.Name = "numsPanel";
            this.numsPanel.Size = new System.Drawing.Size(270, 220);
            this.numsPanel.TabIndex = 9;
            // 
            // sevenButton
            // 
            this.sevenButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.sevenButton.FlatAppearance.BorderSize = 0;
            this.sevenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sevenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sevenButton.ForeColor = System.Drawing.SystemColors.Window;
            this.sevenButton.Location = new System.Drawing.Point(3, 0);
            this.sevenButton.Name = "sevenButton";
            this.sevenButton.Size = new System.Drawing.Size(85, 50);
            this.sevenButton.TabIndex = 1;
            this.sevenButton.Text = "7";
            this.sevenButton.UseVisualStyleBackColor = false;
            this.sevenButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sevenButton_MouseUp);
            // 
            // sixButton
            // 
            this.sixButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.sixButton.FlatAppearance.BorderSize = 0;
            this.sixButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sixButton.ForeColor = System.Drawing.SystemColors.Window;
            this.sixButton.Location = new System.Drawing.Point(185, 56);
            this.sixButton.Name = "sixButton";
            this.sixButton.Size = new System.Drawing.Size(85, 50);
            this.sixButton.TabIndex = 1;
            this.sixButton.Text = "6";
            this.sixButton.UseVisualStyleBackColor = false;
            this.sixButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sixButton_MouseUp);
            // 
            // comaButton
            // 
            this.comaButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.comaButton.FlatAppearance.BorderSize = 0;
            this.comaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comaButton.ForeColor = System.Drawing.SystemColors.Window;
            this.comaButton.Location = new System.Drawing.Point(185, 168);
            this.comaButton.Name = "comaButton";
            this.comaButton.Size = new System.Drawing.Size(85, 50);
            this.comaButton.TabIndex = 1;
            this.comaButton.Text = ",";
            this.comaButton.UseVisualStyleBackColor = false;
            this.comaButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.comaButton_MouseUp);
            // 
            // twoButton
            // 
            this.twoButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.twoButton.FlatAppearance.BorderSize = 0;
            this.twoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.twoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.twoButton.ForeColor = System.Drawing.SystemColors.Window;
            this.twoButton.Location = new System.Drawing.Point(94, 111);
            this.twoButton.Name = "twoButton";
            this.twoButton.Size = new System.Drawing.Size(85, 50);
            this.twoButton.TabIndex = 1;
            this.twoButton.Text = "2";
            this.twoButton.UseVisualStyleBackColor = false;
            this.twoButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.twoButton_MouseUp);
            // 
            // ChangeSignButton
            // 
            this.ChangeSignButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.ChangeSignButton.FlatAppearance.BorderSize = 0;
            this.ChangeSignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeSignButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChangeSignButton.ForeColor = System.Drawing.SystemColors.Window;
            this.ChangeSignButton.Location = new System.Drawing.Point(3, 168);
            this.ChangeSignButton.Name = "ChangeSignButton";
            this.ChangeSignButton.Size = new System.Drawing.Size(85, 50);
            this.ChangeSignButton.TabIndex = 1;
            this.ChangeSignButton.Text = "+/-";
            this.ChangeSignButton.UseVisualStyleBackColor = false;
            this.ChangeSignButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChangeSignButton_MouseUp);
            // 
            // nineButton
            // 
            this.nineButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.nineButton.FlatAppearance.BorderSize = 0;
            this.nineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nineButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nineButton.ForeColor = System.Drawing.SystemColors.Window;
            this.nineButton.Location = new System.Drawing.Point(185, 0);
            this.nineButton.Name = "nineButton";
            this.nineButton.Size = new System.Drawing.Size(85, 50);
            this.nineButton.TabIndex = 1;
            this.nineButton.Text = "9";
            this.nineButton.UseVisualStyleBackColor = false;
            this.nineButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.nineButton_MouseUp);
            // 
            // threeButton
            // 
            this.threeButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.threeButton.FlatAppearance.BorderSize = 0;
            this.threeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.threeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.threeButton.ForeColor = System.Drawing.SystemColors.Window;
            this.threeButton.Location = new System.Drawing.Point(185, 111);
            this.threeButton.Name = "threeButton";
            this.threeButton.Size = new System.Drawing.Size(85, 50);
            this.threeButton.TabIndex = 1;
            this.threeButton.Text = "3";
            this.threeButton.UseVisualStyleBackColor = false;
            this.threeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.threeButton_MouseUp);
            // 
            // fourButton
            // 
            this.fourButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.fourButton.FlatAppearance.BorderSize = 0;
            this.fourButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fourButton.ForeColor = System.Drawing.SystemColors.Window;
            this.fourButton.Location = new System.Drawing.Point(3, 56);
            this.fourButton.Name = "fourButton";
            this.fourButton.Size = new System.Drawing.Size(85, 50);
            this.fourButton.TabIndex = 1;
            this.fourButton.Text = "4";
            this.fourButton.UseVisualStyleBackColor = false;
            this.fourButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fourButton_MouseUp);
            // 
            // fiveButton
            // 
            this.fiveButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.fiveButton.FlatAppearance.BorderSize = 0;
            this.fiveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fiveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fiveButton.ForeColor = System.Drawing.SystemColors.Window;
            this.fiveButton.Location = new System.Drawing.Point(94, 56);
            this.fiveButton.Name = "fiveButton";
            this.fiveButton.Size = new System.Drawing.Size(85, 50);
            this.fiveButton.TabIndex = 1;
            this.fiveButton.Text = "5";
            this.fiveButton.UseVisualStyleBackColor = false;
            this.fiveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fiveButton_MouseUp);
            // 
            // eightButton
            // 
            this.eightButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.eightButton.FlatAppearance.BorderSize = 0;
            this.eightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.eightButton.ForeColor = System.Drawing.SystemColors.Window;
            this.eightButton.Location = new System.Drawing.Point(94, 0);
            this.eightButton.Name = "eightButton";
            this.eightButton.Size = new System.Drawing.Size(85, 50);
            this.eightButton.TabIndex = 1;
            this.eightButton.Text = "8";
            this.eightButton.UseVisualStyleBackColor = false;
            this.eightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.eightButton_MouseUp);
            // 
            // zeroButton
            // 
            this.zeroButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.zeroButton.FlatAppearance.BorderSize = 0;
            this.zeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zeroButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zeroButton.ForeColor = System.Drawing.SystemColors.Window;
            this.zeroButton.Location = new System.Drawing.Point(94, 168);
            this.zeroButton.Name = "zeroButton";
            this.zeroButton.Size = new System.Drawing.Size(85, 50);
            this.zeroButton.TabIndex = 1;
            this.zeroButton.Text = "0";
            this.zeroButton.UseVisualStyleBackColor = false;
            this.zeroButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zeroButton_MouseUp);
            // 
            // oneButton
            // 
            this.oneButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.oneButton.FlatAppearance.BorderSize = 0;
            this.oneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oneButton.ForeColor = System.Drawing.SystemColors.Window;
            this.oneButton.Location = new System.Drawing.Point(3, 111);
            this.oneButton.Name = "oneButton";
            this.oneButton.Size = new System.Drawing.Size(85, 50);
            this.oneButton.TabIndex = 1;
            this.oneButton.Text = "1";
            this.oneButton.UseVisualStyleBackColor = false;
            this.oneButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.oneButton_MouseUp);
            // 
            // op1Panel
            // 
            this.op1Panel.Controls.Add(this.PercentButton);
            this.op1Panel.Controls.Add(this.one_div_xButton);
            this.op1Panel.Controls.Add(this.CEButton);
            this.op1Panel.Controls.Add(this.x_multiply_xButton);
            this.op1Panel.Controls.Add(this.divisionButton);
            this.op1Panel.Controls.Add(this.CButton);
            this.op1Panel.Controls.Add(this.SqrtButton);
            this.op1Panel.Controls.Add(this.BackSpaceButton);
            this.op1Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.op1Panel.Location = new System.Drawing.Point(0, 0);
            this.op1Panel.Name = "op1Panel";
            this.op1Panel.Size = new System.Drawing.Size(363, 117);
            this.op1Panel.TabIndex = 8;
            // 
            // PercentButton
            // 
            this.PercentButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.PercentButton.FlatAppearance.BorderSize = 0;
            this.PercentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PercentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PercentButton.ForeColor = System.Drawing.SystemColors.Window;
            this.PercentButton.Location = new System.Drawing.Point(3, 6);
            this.PercentButton.Name = "PercentButton";
            this.PercentButton.Size = new System.Drawing.Size(85, 50);
            this.PercentButton.TabIndex = 0;
            this.PercentButton.Text = "%";
            this.PercentButton.UseVisualStyleBackColor = false;
            this.PercentButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PercentButton_MouseUp);
            // 
            // one_div_xButton
            // 
            this.one_div_xButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.one_div_xButton.FlatAppearance.BorderSize = 0;
            this.one_div_xButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.one_div_xButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.one_div_xButton.ForeColor = System.Drawing.SystemColors.Window;
            this.one_div_xButton.Location = new System.Drawing.Point(3, 62);
            this.one_div_xButton.Name = "one_div_xButton";
            this.one_div_xButton.Size = new System.Drawing.Size(85, 50);
            this.one_div_xButton.TabIndex = 1;
            this.one_div_xButton.Text = "1/x";
            this.one_div_xButton.UseVisualStyleBackColor = false;
            this.one_div_xButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.one_div_xButton_MouseUp);
            // 
            // CEButton
            // 
            this.CEButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.CEButton.FlatAppearance.BorderSize = 0;
            this.CEButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CEButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CEButton.ForeColor = System.Drawing.SystemColors.Window;
            this.CEButton.Location = new System.Drawing.Point(94, 6);
            this.CEButton.Name = "CEButton";
            this.CEButton.Size = new System.Drawing.Size(85, 50);
            this.CEButton.TabIndex = 2;
            this.CEButton.Text = "CE";
            this.CEButton.UseVisualStyleBackColor = false;
            this.CEButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CEButton_MouseUp);
            // 
            // x_multiply_xButton
            // 
            this.x_multiply_xButton.BackColor = System.Drawing.SystemColors.MenuText;
            this.x_multiply_xButton.FlatAppearance.BorderSize = 0;
            this.x_multiply_xButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.x_multiply_xButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x_multiply_xButton.ForeColor = System.Drawing.SystemColors.Window;
            this.x_multiply_xButton.Location = new System.Drawing.Point(94, 62);
            this.x_multiply_xButton.Name = "x_multiply_xButton";
            this.x_multiply_xButton.Size = new System.Drawing.Size(85, 50);
            this.x_multiply_xButton.TabIndex = 3;
            this.x_multiply_xButton.Text = " x²";
            this.x_multiply_xButton.UseVisualStyleBackColor = false;
            this.x_multiply_xButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.x_multiply_xButton_MouseUp);
            // 
            // divisionButton
            // 
            this.divisionButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.divisionButton.FlatAppearance.BorderSize = 0;
            this.divisionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.divisionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divisionButton.ForeColor = System.Drawing.SystemColors.Window;
            this.divisionButton.Location = new System.Drawing.Point(276, 62);
            this.divisionButton.Name = "divisionButton";
            this.divisionButton.Size = new System.Drawing.Size(85, 50);
            this.divisionButton.TabIndex = 7;
            this.divisionButton.Text = "÷";
            this.divisionButton.UseVisualStyleBackColor = false;
            this.divisionButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.divisionButton_MouseUp);
            // 
            // CButton
            // 
            this.CButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.CButton.FlatAppearance.BorderSize = 0;
            this.CButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CButton.ForeColor = System.Drawing.SystemColors.Window;
            this.CButton.Location = new System.Drawing.Point(185, 6);
            this.CButton.Name = "CButton";
            this.CButton.Size = new System.Drawing.Size(85, 50);
            this.CButton.TabIndex = 4;
            this.CButton.Text = "C";
            this.CButton.UseVisualStyleBackColor = false;
            this.CButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CButton_MouseUp);
            // 
            // SqrtButton
            // 
            this.SqrtButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.SqrtButton.FlatAppearance.BorderSize = 0;
            this.SqrtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SqrtButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SqrtButton.ForeColor = System.Drawing.SystemColors.Window;
            this.SqrtButton.Location = new System.Drawing.Point(185, 62);
            this.SqrtButton.Name = "SqrtButton";
            this.SqrtButton.Size = new System.Drawing.Size(85, 50);
            this.SqrtButton.TabIndex = 5;
            this.SqrtButton.Tag = "";
            this.SqrtButton.Text = "√x";
            this.SqrtButton.UseVisualStyleBackColor = false;
            this.SqrtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SqrtButton_MouseUp);
            // 
            // BackSpaceButton
            // 
            this.BackSpaceButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.BackSpaceButton.FlatAppearance.BorderSize = 0;
            this.BackSpaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackSpaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackSpaceButton.ForeColor = System.Drawing.SystemColors.Window;
            this.BackSpaceButton.Location = new System.Drawing.Point(276, 6);
            this.BackSpaceButton.Name = "BackSpaceButton";
            this.BackSpaceButton.Size = new System.Drawing.Size(85, 50);
            this.BackSpaceButton.TabIndex = 6;
            this.BackSpaceButton.Text = "<=";
            this.BackSpaceButton.UseVisualStyleBackColor = false;
            this.BackSpaceButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BackSpaceButton_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(363, 464);
            this.Controls.Add(this.textPanel);
            this.Controls.Add(this.memoryPanel);
            this.Controls.Add(this.resultPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.memoryPanel.ResumeLayout(false);
            this.textPanel.ResumeLayout(false);
            this.op2Panel.ResumeLayout(false);
            this.numsPanel.ResumeLayout(false);
            this.op1Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Panel memoryPanel;
        private System.Windows.Forms.Panel textPanel;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button MMinusButton;
        private System.Windows.Forms.Button MPlusButton;
        private System.Windows.Forms.Button MRButton;
        private System.Windows.Forms.Button MCButton;
        private System.Windows.Forms.Button divisionButton;
        private System.Windows.Forms.Button BackSpaceButton;
        private System.Windows.Forms.Button SqrtButton;
        private System.Windows.Forms.Button CButton;
        private System.Windows.Forms.Button x_multiply_xButton;
        private System.Windows.Forms.Button CEButton;
        private System.Windows.Forms.Button one_div_xButton;
        private System.Windows.Forms.Button PercentButton;
        private System.Windows.Forms.Button equalButton;
        private System.Windows.Forms.Button plusButton;
        private System.Windows.Forms.Button minusButton;
        private System.Windows.Forms.Button multiplyButton;
        private System.Windows.Forms.TextBox historyTextBox;
        private System.Windows.Forms.Button comaButton;
        private System.Windows.Forms.Button zeroButton;
        private System.Windows.Forms.Button oneButton;
        private System.Windows.Forms.Button nineButton;
        private System.Windows.Forms.Button twoButton;
        private System.Windows.Forms.Button eightButton;
        private System.Windows.Forms.Button threeButton;
        private System.Windows.Forms.Button sixButton;
        private System.Windows.Forms.Button ChangeSignButton;
        private System.Windows.Forms.Button fiveButton;
        private System.Windows.Forms.Button sevenButton;
        private System.Windows.Forms.Button fourButton;
        private System.Windows.Forms.Panel op1Panel;
        private System.Windows.Forms.Panel op2Panel;
        private System.Windows.Forms.Panel numsPanel;
    }
}

