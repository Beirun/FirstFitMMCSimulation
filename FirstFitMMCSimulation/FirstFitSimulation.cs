using System.Drawing.Drawing2D;

namespace FirstFitMMCSimulation
{
    public partial class FirstFitSimulation : Form
    {
        int processNumber = 0;
        int repaintedBorder = 0;
        int memorySize = 0;
        int secondsElapsed = 0;
        int longestCompletionTime = 0;
        int freeMemory = 0;
        int processStartLocation = 0;
        int processEndLocation = 0;
        int processIDEntered = 0;
        int[] memoryConfiguration;
        int[] memoryRequests;
        int[] allocationTimes;
        int[] completionTimes;
        bool isScrolled = false;
        bool isPaused = false;
        bool isBackHovered = false;
        bool[] isFinished;
        bool[] hasEntered;
        bool[] isBeingEntered;
        bool[] fromWaiting;
        bool[] isWaiting;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Panel memoryConfigPanel;
        Panel backButton;
        Label bottomLabel;
        TextBox[] memoryRequestTextBox;
        TextBox[] allocationTimeTextBox;
        TextBox[] completionTimeTextBox;
        Panel[] processConfigPanels;
        Label[] processConfigLabels;
        Label[] processMemorySizeLabels;
        Label[] tableLabels;
        Label[] processLabels;
        Label[] waitingProcessLabels;
        Label[] memorySizeLabels;
        FlowLayoutPanel eventList;
        Color foreColor = ColorTranslator.FromHtml("#fff6bc");
        Color backColor = ColorTranslator.FromHtml("#212121");
        Color memoryConfigColor = Color.DarkSeaGreen;
        Color waitingProcessColor = Color.FromArgb(128, ColorTranslator.FromHtml("#2f2f2f"));
        Color osProcessColor = Color.DarkSlateBlue;
        TableLayoutPanel processConfigTable;

        public FirstFitSimulation()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            firstSlide();
            ClientSize = this.Size;
            secondPanel.Location = thirdPanel.Location = new Point(Width, 0);
            this.DoubleBuffered = true;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        public void firstSlide()
        {
            firstPanel.BringToFront();
            firstPanel.BackColor = backColor;
            firstPanel.Location = new Point(0, 0);
            firstPanel.Size = new Size(this.Width, this.Height);

            firstPanelLabel.Font = new Font("Calida Code", 24, FontStyle.Bold);
            firstPanelLabel.ForeColor = foreColor;
            firstPanelLabel.BackColor = Color.FromArgb(0, ColorTranslator.FromHtml("#2e2e2e"));
            firstPanelLabel.Size = new Size(300, 200);
            firstPanelLabel.Location = new Point(firstPanel.Width / 2 - firstPanelLabel.Width / 2, firstPanel.Height / 2 - firstPanelLabel.Height / 2 - 200);

            memorySizePanel.Size = new Size(300, 60);
            memorySizePanel.BackColor = Color.White;
            memorySizePanel.Location = new Point(firstPanel.Width / 2 - memorySizePanel.Width / 2, firstPanel.Height / 2 - memorySizePanel.Height / 2 - 50);
            memorySizeTextBox.Font = new Font("Arial", 24, FontStyle.Regular);
            memorySizeTextBox.Size = new Size(memorySizePanel.Width - 10, 45);
            memorySizeTextBox.Location = new Point(memorySizePanel.Width / 2 - memorySizeTextBox.Width / 2, memorySizePanel.Height / 2 - memorySizeTextBox.Height / 2);
            memorySizeTextBox.TextChanged += (sender, e) => { errorMemory.Visible = false; errorMemory.Text = ""; };

            processNumberPanel.Size = new Size(300, 60);
            processNumberPanel.BackColor = Color.White;
            processNumberPanel.Location = new Point(firstPanel.Width / 2 - processNumberPanel.Width / 2, firstPanel.Height / 2 - processNumberPanel.Height / 2 + 100);

            processNumberTextBox.Font = new Font("Arial", 24, FontStyle.Regular);
            processNumberTextBox.Size = new Size(processNumberPanel.Width - 10, 45);
            processNumberTextBox.Location = new Point(processNumberPanel.Width / 2 - processNumberTextBox.Width / 2, processNumberPanel.Height / 2 - processNumberTextBox.Height / 2);
            processNumberTextBox.TextChanged += (sender, e) => { errorProcess.Visible = false; errorProcess.Text = ""; };

            memorySizeLabel.Font = new Font("Calida Code", 12, FontStyle.Bold);
            memorySizeLabel.ForeColor = foreColor;
            memorySizeLabel.BackColor = Color.FromArgb(0, ColorTranslator.FromHtml("#2e2e2e"));
            memorySizeLabel.Size = new Size(300, 200);
            memorySizeLabel.Location = new Point(memorySizePanel.Location.X, memorySizePanel.Location.Y - 35);

            processNumberLabel.Font = new Font("Calida Code", 12, FontStyle.Bold);
            processNumberLabel.ForeColor = foreColor;
            processNumberLabel.BackColor = Color.FromArgb(0, ColorTranslator.FromHtml("#2e2e2e"));
            processNumberLabel.Size = new Size(300, 200);
            processNumberLabel.Location = new Point(processNumberPanel.Location.X, processNumberPanel.Location.Y - 35);

            CustomButton customizeButton = new CustomButton();
            nextButton.Font = new Font("Calida Code", 14, FontStyle.Bold);
            nextButton.FlatStyle = FlatStyle.Flat;
            nextButton.ForeColor = ColorTranslator.FromHtml("#2f2f2f");
            nextButton.BackColor = ColorTranslator.FromHtml("#fff6bc");
            nextButton.FlatAppearance.MouseDownBackColor = backColor;
            nextButton.Size = new Size(150, 40);
            nextButton.Location = new Point(firstPanel.Width / 2 - nextButton.Width / 2, processNumberPanel.Location.Y + 125);
            nextButton.MouseEnter += customizeButton.Button_MouseEnter;
            nextButton.MouseLeave += customizeButton.Button_MouseLeave;
            nextButton.MouseClick += NextButton_MouseClick;
            firstPanel.Paint += FirstPanel_Paint;

            errorMemory.Font = new Font("Calida Code", 10, FontStyle.Regular);
            errorMemory.ForeColor = Color.FromArgb(180, 70, 70);
            errorMemory.BackColor = memorySizeLabel.BackColor;
            errorMemory.Location = new Point(memorySizePanel.Location.X, memorySizePanel.Location.Y + memorySizePanel.Height + 10);
            errorMemory.Visible = false;

            errorProcess.Font = new Font("Calida Code", 10, FontStyle.Regular);
            errorProcess.ForeColor = Color.FromArgb(180, 70, 70);
            errorProcess.BackColor = memorySizeLabel.BackColor;
            errorProcess.Location = new Point(processNumberPanel.Location.X, processNumberPanel.Location.Y +memorySizePanel.Height + 10);
            errorProcess.Visible = false;
        }

        private void NextButton_MouseClick(object? sender, MouseEventArgs e)
        {
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(memorySizeTextBox.Text, "/^[0-9]*$/"))
            {
                if (memorySizeTextBox.Text.Trim() == "") errorMemory.Text = "× Must not be empty!";
                try
                {
                    if (memorySizeTextBox.Text.Trim() != "")
                    {
                        memorySize = Convert.ToInt32(memorySizeTextBox.Text);
                        if (memorySize <= 100) errorMemory.Text = "× Must be greater than 100!";
                    }

                }
                catch (Exception)
                {
                    errorMemory.Text = "× Must be a numerical character!";
                }
                if(errorMemory.Text != "") errorMemory.Visible = true;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(processNumberTextBox.Text, "/^[0-9]*$/"))
            {

                if (processNumberTextBox.Text.Trim() == "") errorProcess.Text = "× Must not be empty!";
                try
                {
                    if (processNumberTextBox.Text.Trim() != "")
                    {
                        processNumber = Convert.ToInt32(processNumberTextBox.Text);
                        if (processNumber < 1) errorProcess.Text = "× Must not be a zero or negative number!";
                    }
                }
                catch (Exception)
                {
                    errorProcess.Text = "× Must be a numerical character!";

                }
                if(errorProcess.Text != "") errorProcess.Visible = true;
            }
            if (errorProcess.Visible || errorMemory.Visible) return;

            secondSlide();
        }

        private void IncorrectPassword_Paint(object? sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics graphics = e.Graphics;

            using (Pen pen = new Pen(Color.Red, 4.0f))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(2, 2, panel.Width - 4, panel.Height - 4));
            }

        }

        private void FirstPanel_Paint(object? sender, PaintEventArgs e)
        {
            int width = memorySizePanel.Width + 400;
            int height = nextButton.Location.Y + nextButton.Height - memorySizeLabel.Location.Y + 200;
            Graphics graphics = e.Graphics;
            Rectangle rect = new Rectangle(memorySizePanel.Location.X - 200, memorySizeLabel.Location.Y - 150, width, height);
            int radius = 15 * 2;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, ColorTranslator.FromHtml("#2e2e2e"))))
            {
                graphics.FillPath(brush, path);
            }
        }


        public void secondSlide()
        {
            secondPanel.BringToFront();
            secondPanel.BackColor = backColor;
            secondPanel.Location = new Point(0, 0);
            secondPanel.Dock = DockStyle.None;
            secondPanel.Size = new Size(firstPanel.Width, firstPanel.Height);

            Panel backButton = new Panel();
            backButton.Paint += BackButton_Paint;
            backButton.BackColor = secondPanel.BackColor;
            backButton.Size = new Size(100, 100);
            backButton.Location = new Point(10, 10);
            backButton.Click += SecondBackButton_Click;
            backButton.MouseEnter += (sender, e) => { isBackHovered = true; backButton.Refresh(); };
            backButton.MouseLeave += (sender, e) => { isBackHovered = false; backButton.Refresh(); };
            secondPanel.Controls.Add(backButton);

            processConfigTable = new TableLayoutPanel();
            processConfigTable.RowCount = processNumber + 1;
            processConfigTable.ColumnCount = 4;
            processConfigTable.Size = new Size(1250, firstPanel.Height - 250);
            processConfigTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            processConfigTable.Location = new Point(secondPanel.Width / 2 - processConfigTable.Width / 2, secondPanel.Height / 2 - processConfigTable.Height / 2 - 50);
            processConfigTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            processConfigTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
            processConfigTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350));
            processConfigTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350));

            memoryRequestTextBox = new TextBox[processNumber];
            allocationTimeTextBox = new TextBox[processNumber];
            completionTimeTextBox = new TextBox[processNumber];
            tableLabels = new Label[4];
            processLabels = new Label[processNumber];

            string[] labelNames = { "Process ID", "Memory Request", "Allocation Time", "Completion Time" };
            Font font = new Font("Arial", 64, FontStyle.Bold);
            int fontSize = 64;

            while (true)
            {
                if (TextRenderer.MeasureText("Test", font).Height < (processConfigTable.Height / (processNumber + 1)) - (processConfigTable.Height / (processNumber + 1) / 3)) break;
                else font = new Font("Arial", fontSize -= 2, FontStyle.Bold);
            }

            for (int i = 0; i < processNumber; i++)
            {
                processConfigTable.RowStyles.Add(new RowStyle(SizeType.Absolute, processConfigTable.Height / (processNumber + 1)));
                processLabels[i] = new Label();
                processLabels[i].Font = font;
                processLabels[i].Text = "P" + (i + 1);
                processLabels[i].Size = new Size(TextRenderer.MeasureText(processLabels[i].Text, font).Width, TextRenderer.MeasureText(processLabels[i].Text, font).Height);
                processLabels[i].ForeColor = foreColor;
                processLabels[i].Anchor = AnchorStyles.None;
                processConfigTable.Controls.Add(processLabels[i], 0, i + 1);
                
                memoryRequestTextBox[i] = new TextBox();
                memoryRequestTextBox[i].Multiline = false;
                memoryRequestTextBox[i].BorderStyle = BorderStyle.None;
                memoryRequestTextBox[i].TextAlign = HorizontalAlignment.Center;
                memoryRequestTextBox[i].Font = font;
                memoryRequestTextBox[i].BackColor = secondPanel.BackColor;
                memoryRequestTextBox[i].Size = new Size(290, 300);
                memoryRequestTextBox[i].ForeColor = foreColor;
                memoryRequestTextBox[i].Anchor = AnchorStyles.None;
                memoryRequestTextBox[i].Tag = i;
                memoryRequestTextBox[i].Name = "MemoryRequest";
                memoryRequestTextBox[i].Enter += AddKB_Enter;
                memoryRequestTextBox[i].Leave += AddKB_Leave;
                memoryRequestTextBox[i].KeyPress += EnterKey_Press;
                processConfigTable.Controls.Add(memoryRequestTextBox[i], 1, i + 1);
                
                allocationTimeTextBox[i] = new TextBox();
                allocationTimeTextBox[i].Multiline = false;
                allocationTimeTextBox[i].BorderStyle = BorderStyle.None;
                allocationTimeTextBox[i].TextAlign = HorizontalAlignment.Center;
                allocationTimeTextBox[i].Font = font;
                allocationTimeTextBox[i].BackColor = secondPanel.BackColor;
                allocationTimeTextBox[i].Size = new Size(340, 350);
                allocationTimeTextBox[i].ForeColor = foreColor;
                allocationTimeTextBox[i].Anchor = AnchorStyles.None;
                allocationTimeTextBox[i].Tag = i;
                allocationTimeTextBox[i].Name = "AllocationTime";
                allocationTimeTextBox[i].KeyPress += EnterKey_Press;
                allocationTimeTextBox[i].Enter += AddMSEC_Enter;
                allocationTimeTextBox[i].Leave += AddMSEC_Leave;
                processConfigTable.Controls.Add(allocationTimeTextBox[i], 2, i + 1);
                
                completionTimeTextBox[i] = new TextBox();
                completionTimeTextBox[i].Multiline = false;
                completionTimeTextBox[i].BorderStyle = BorderStyle.None;
                completionTimeTextBox[i].TextAlign = HorizontalAlignment.Center;
                completionTimeTextBox[i].Font = font;
                completionTimeTextBox[i].BackColor = secondPanel.BackColor;
                completionTimeTextBox[i].Size = new Size(340, 350);
                completionTimeTextBox[i].ForeColor = foreColor;
                completionTimeTextBox[i].Anchor = AnchorStyles.None;
                completionTimeTextBox[i].Tag = i;
                completionTimeTextBox[i].Name = "CompletionTime";
                completionTimeTextBox[i].KeyPress += EnterKey_Press;
                completionTimeTextBox[i].Enter += AddMSEC_Enter;
                completionTimeTextBox[i].Leave += AddMSEC_Leave;
                processConfigTable.Controls.Add(completionTimeTextBox[i], 3, i + 1);
            }
            fontSize = 64;
            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    if (TextRenderer.MeasureText(labelNames[i], font).Width < processConfigTable.GetColumnWidths()[i] && 
                        TextRenderer.MeasureText(labelNames[i], font).Height < processConfigTable.GetRowHeights()[0]) break;
                    else font = new Font("Arial", fontSize-=2, FontStyle.Bold);
                }
                tableLabels[i] = new Label();
                tableLabels[i].Font = font;
                tableLabels[i].Text = labelNames[i];
                tableLabels[i].Size = TextRenderer.MeasureText(tableLabels[i].Text, font);
                tableLabels[i].ForeColor = foreColor;
                tableLabels[i].Anchor = AnchorStyles.None;
                processConfigTable.Controls.Add(tableLabels[i], i, 0);
            }
            for(int i = 0; i < processNumber; i++)
            {
                memoryRequestTextBox[i].Font = setFont(memorySize + " KB");
                allocationTimeTextBox[i].Font = setFont("50 msec");
                completionTimeTextBox[i].Font = setFont("50 msec");
            }
            secondPanel.Controls.Add(processConfigTable);
            CustomButton customizeButton = new CustomButton();
            proceedButton.Font = new Font("Calida Code", 12, FontStyle.Bold);
            proceedButton.FlatStyle = FlatStyle.Flat;
            proceedButton.ForeColor = ColorTranslator.FromHtml("#2f2f2f");
            proceedButton.BackColor = ColorTranslator.FromHtml("#fff6bc");
            proceedButton.FlatAppearance.MouseDownBackColor = backColor;
            proceedButton.Size = new Size(175, 50);
            proceedButton.Location = new Point(secondPanel.Width / 2 - proceedButton.Width / 2, processConfigTable.Location.Y + processConfigTable.Height + 40);
            proceedButton.MouseEnter += customizeButton.Button_MouseEnter;
            proceedButton.MouseLeave += customizeButton.Button_MouseLeave;
            processConfigTable.BringToFront();
        }

        private void SecondBackButton_Click(object? sender, EventArgs e)
        {
            firstSlide();
        }

        private void EnterKey_Press(object? sender, KeyPressEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            int index = (int)textBox.Tag;
            string textBoxName = textBox.Name;

            if (e.KeyChar == (char)Keys.Enter && !textBox.Text.Equals(""))
            {
                if (textBoxName.Equals("MemoryRequest"))
                {
                    if (index == memoryRequestTextBox.Length - 1) allocationTimeTextBox[0].Focus();
                    else memoryRequestTextBox[index + 1].Focus();
                }
                else if (textBoxName.Equals("AllocationTime"))
                {
                    if (index == allocationTimeTextBox.Length - 1) completionTimeTextBox[0].Focus();
                    else allocationTimeTextBox[index + 1].Focus();
                }
                else
                {
                    if (index == completionTimeTextBox.Length - 1)
                    {
                        AddMSEC_Leave(sender, e);
                        proceedButton.PerformClick();
                    }
                    else completionTimeTextBox[index + 1].Focus();
                }
            }
        }
        public Font setFont(string text)
        {
            int fontSize = 48;
            Font font = new Font("Arial", fontSize, FontStyle.Bold);
            while (true)
            {
                if (TextRenderer.MeasureText(text, font).Height < (processConfigTable.Height / (processNumber + 1)) - (processConfigTable.Height / (processNumber + 1) / 3) &&
                    TextRenderer.MeasureText(text, font).Width < (processConfigTable.Width / (processNumber + 1)) - (processConfigTable.Height / (processNumber + 1) / 3)) break;
                else font = new Font("Arial", fontSize -= 2, FontStyle.Bold);
            }
            return font;
        }

        private void AddMSEC_Leave(object? sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            int i = (int)textBox.Tag;
            textBox.Text = textBox.Text.Trim();
            try { 
                int number = Convert.ToInt32(textBox.Text);
                if(number < 0) textBox.ForeColor = Color.Red;
                if (textBox == completionTimeTextBox[i])
                {
                    if (Convert.ToInt32(allocationTimeTextBox[i].Text.Substring(0, allocationTimeTextBox[i].TextLength - 5).Trim()) > 
                        Convert.ToInt32(completionTimeTextBox[i].Text)) completionTimeTextBox[i].ForeColor = Color.Red;
                }
            }
            catch { textBox.ForeColor = Color.Red; }
            
            if (!textBox.Text.Contains(" msec") && textBox.Text.Trim() != "") textBox.Text += " msec";
            textBox.Text = textBox.Text.Trim();
        }

        private void AddMSEC_Enter(object? sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            textBox.ForeColor = foreColor;
            textBox.Text = textBox.Text.Trim();
            if (textBox.Text.Contains(" msec")) textBox.Text = textBox.Text.Substring(0, textBox.TextLength - 5);
            textBox.Text = textBox.Text.Trim();
        }


        private void AddKB_Leave(object? sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
            try { int number = Convert.ToInt32(textBox.Text); if (number < 1) textBox.ForeColor = Color.Red;
            }
            catch { textBox.ForeColor  = Color.Red; }
            if (!textBox.Text.Contains(" KB") && textBox.Text.Trim() != "") textBox.Text += " KB";
            textBox.Text = textBox.Text.Trim();
        }

        private void AddKB_Enter(object? sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
            textBox.ForeColor = foreColor;
            if (textBox.Text.Contains(" KB")) textBox.Text = textBox.Text.Substring(0, textBox.TextLength - 3);
            textBox.Text = textBox.Text.Trim();
        }
        private void proceedButton_Click(object sender, EventArgs e)
        {
            thirdPanel.Size = new Size(firstPanel.Width, firstPanel.Height);
            playButton.MouseClick += PlayButton_MouseClick;
            playButton.Paint += PlayButton_Paint;
            playButton.Size = new Size(75, 75);
            playButton.FlatStyle = FlatStyle.Flat;
            playButton.Location = new Point(thirdPanel.Width / 2 - playButton.Width / 2, thirdPanel.Height - playButton.Height - 10);
            playButton.BackColor = foreColor;

            backButton = new Panel();
            backButton.Paint += BackButton_Paint;
            backButton.BackColor = secondPanel.BackColor;
            backButton.Size = new Size(100, 100);
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            backButton.MouseEnter += (sender, e) => { isBackHovered = true; backButton.Refresh(); };
            backButton.MouseLeave += (sender, e) => { isBackHovered = false; backButton.Refresh(); };

            isPaused = false;
            memoryRequests = new int[processNumber];
            allocationTimes = new int[processNumber];
            completionTimes = new int[processNumber];
            memoryConfiguration = new int[memorySize];
            isFinished = new bool[processNumber + 1]; 
            isWaiting = new bool[processNumber + 1]; 
            hasEntered = new bool[processNumber + 1]; 
            fromWaiting = new bool[processNumber + 1]; 
            isBeingEntered = new bool[processNumber + 1];
            memorySizeLabels = new Label[processNumber + 1];
            waitingProcessLabels = new Label[processNumber];
            for (int i = 0; i < 100; i++) memoryConfiguration[i] = -1;
            for (int i = 0; i < processNumber; i++)
            {
                isFinished[i] = false;
                fromWaiting[i] = false;
                hasEntered[i] = false;
                try
                {
                    memoryRequests[i] = Convert.ToInt32(memoryRequestTextBox[i].Text.Substring(0, memoryRequestTextBox[i].TextLength - 3).Trim());
                    allocationTimes[i] = Convert.ToInt32(allocationTimeTextBox[i].Text.Substring(0, allocationTimeTextBox[i].TextLength - 5).Trim());
                    completionTimes[i] = Convert.ToInt32(completionTimeTextBox[i].Text.Substring(0, completionTimeTextBox[i].TextLength - 5).Trim());
                }catch { return; }
            }
            for (int i = 0; i < processNumber; i++) if (longestCompletionTime < completionTimes[i]) longestCompletionTime = completionTimes[i];
            thirdSlide();
        }
        public void thirdSlide()
        {
            secondsElapsed = 0;
            thirdPanel.BringToFront();
            thirdPanel.Controls.Clear();
            memoryConfig.Controls.Clear();
            waitingProcessPanel.Controls.Clear();
            if (memoryConfigPanel != null) memoryConfigPanel.Controls.Clear();
            thirdPanel.Controls.Add(playButton);
            thirdPanel.Controls.Add(timerLabel);
            thirdPanel.Controls.Add(memoryConfig);
            thirdPanel.Controls.Add(waitingProcessPanel);
            thirdPanel.BackColor = backColor;
            thirdPanel.Location = new Point(0, 0);
            thirdPanel.Dock = DockStyle.None;

            thirdPanel.Controls.Add(backButton);

            memoryConfig.BackColor = thirdPanel.BackColor;
            memoryConfig.Size = new Size(500, 700);
            memoryConfig.Location = new Point(thirdPanel.Width / 2 - memoryConfig.Width / 2, thirdPanel.Height / 2 - memoryConfig.Height / 2 );

            memoryConfigPanel = new Panel();
            memoryConfigPanel.BackColor = memoryConfigColor;
            memoryConfigPanel.Paint += PaintBorderConfig;
            memoryConfigPanel.Size = new Size(300, 650);
            memoryConfigPanel.Location = new Point(memoryConfig.Width / 2 - memoryConfigPanel.Width / 2, memoryConfig.Height / 2 - memoryConfigPanel.Height / 2);
            memoryConfig.Controls.Add(memoryConfigPanel);

            timerLabel.ForeColor = foreColor;
            timerLabel.Text = "0 msec";
            timerLabel.Font = new Font("Calida Code", 14, FontStyle.Bold);
            timerLabel.Location = new Point(thirdPanel.Width / 2 - timerLabel.Width / 2, 15);

            
            waitingProcessPanel.BackColor = waitingProcessColor;
            waitingProcessPanel.Size = new Size(100,memoryConfigPanel.Height);
            waitingProcessPanel.Location = new Point(memoryConfig.Location.X - 100, memoryConfig.Location.Y + (memoryConfigPanel.Location.Y));

            Label waitinglabel = new Label();
            waitinglabel.Text = "Waiting List";
            waitinglabel.Font = new Font("Calida Code", 14, FontStyle.Bold);
            waitinglabel.ForeColor = foreColor;
            waitinglabel.Size = TextRenderer.MeasureText(waitinglabel.Text, waitinglabel.Font);
            waitinglabel.Location = new Point(waitingProcessPanel.Left + waitingProcessPanel.Width / 2 - waitinglabel.Width / 2, waitingProcessPanel.Top - waitinglabel.Height - 5);
            thirdPanel.Controls.Add(waitinglabel);
            waitinglabel.BringToFront();

            Label ramLabel = new Label();
            ramLabel.Text = "MEMORY";
            ramLabel.Font = new Font("Calida Code", 14, FontStyle.Bold);
            ramLabel.ForeColor = foreColor;
            ramLabel.Size = TextRenderer.MeasureText(ramLabel.Text, ramLabel.Font);
            ramLabel.Location = new Point(thirdPanel.Width/2 - ramLabel.Width/2, waitinglabel.Top);
            thirdPanel.Controls.Add(ramLabel);
            ramLabel.BringToFront();

            eventList = new FlowLayoutPanel();
            eventList.Size = new Size(memoryConfig.Width - 150, 0);
            eventList.BackColor = waitingProcessColor;
            eventList.FlowDirection = FlowDirection.TopDown;
            eventList.WrapContents = false;
            eventList.Location = new Point(memoryConfig.Left + memoryConfig.Width + 75, 150);
            thirdPanel.Controls.Add(eventList);


            processConfigPanels = new Panel[processNumber + 1];
            processConfigLabels = new Label[processNumber + 1];
            processMemorySizeLabels = new Label[processNumber + 2];

            processConfigPanels[processNumber] = new Panel();
            processConfigPanels[processNumber].Tag = processNumber;
            processConfigPanels[processNumber].Size = new Size(memoryConfigPanel.Width, (memoryConfigPanel.Height * 100) / memorySize);
            processConfigPanels[processNumber].Location = new Point(0, 0);
            processConfigPanels[processNumber].BackColor = osProcessColor;
            processConfigPanels[processNumber].Paint += PaintBorder;
            memoryConfigPanel.Controls.Add(processConfigPanels[processNumber]);

            processConfigLabels[processNumber] = new Label();
            processConfigLabels[processNumber].Text = "OS";

            memorySizeLabels[processNumber] = new Label();
            memorySizeLabels[processNumber].Text = "(100 KB)";
            Font font = new Font("Arial", 32, FontStyle.Bold);
            int fontSize = 48;

            while (true)
            {
                if (TextRenderer.MeasureText("OS", font).Height + TextRenderer.MeasureText(memorySizeLabels[processNumber].Text, 
                    new Font("Arial", fontSize - (fontSize / 2 + fontSize / 5), FontStyle.Bold)).Height 
                    < processConfigPanels[processNumber].Height - (processConfigPanels[processNumber].Height / 8)) break;
                else font = new Font("Arial", fontSize -= 2, FontStyle.Bold);
            }
            processConfigLabels[processNumber].Font = font;
            processConfigLabels[processNumber].ForeColor = foreColor;
            processConfigLabels[processNumber].Size = TextRenderer.MeasureText("OS", font);
            processConfigLabels[processNumber].Location = new Point(processConfigPanels[processNumber].Width / 2 - processConfigLabels[processNumber].Width / 2, 
                processConfigPanels[processNumber].Height / 2 - processConfigLabels[processNumber].Height / 2);
            processConfigLabels[processNumber].BackColor = osProcessColor;
            processConfigPanels[processNumber].Controls.Add(processConfigLabels[processNumber]);
            processConfigLabels[processNumber].Top  -= processConfigLabels[processNumber].Top/2;

            memorySizeLabels[processNumber].Font = new Font("Arial", fontSize - (fontSize/2 + fontSize / 5), FontStyle.Bold);
            memorySizeLabels[processNumber].ForeColor = foreColor;
            memorySizeLabels[processNumber].Size = TextRenderer.MeasureText(memorySizeLabels[processNumber].Text, memorySizeLabels[processNumber].Font);
            memorySizeLabels[processNumber].Location = new Point(processConfigPanels[processNumber].Width / 2 - memorySizeLabels[processNumber].Width / 2, 
                processConfigLabels[processNumber].Top + processConfigLabels[processNumber].Height - processConfigLabels[processNumber].Height/15);
            processConfigPanels[processNumber].Controls.Add(memorySizeLabels[processNumber]);
            memorySizeLabels[processNumber].BringToFront();

            processMemorySizeLabels[0] = new Label();
            processMemorySizeLabels[0].Text = "0 KB";
            processMemorySizeLabels[0].Font = new Font("Calida Code", 10, FontStyle.Bold);
            processMemorySizeLabels[0].ForeColor = foreColor;
            processMemorySizeLabels[0].Size = TextRenderer.MeasureText(processMemorySizeLabels[0].Text, processMemorySizeLabels[0].Font);
            processMemorySizeLabels[0].Location = new Point(memoryConfigPanel.Location.X + memoryConfigPanel.Width + 10, memoryConfigPanel.Location.Y - 
                processMemorySizeLabels[0].Height / 2);
            processMemorySizeLabels[0].BackColor = backColor;
            memoryConfig.Controls.Add(processMemorySizeLabels[0]);

            processMemorySizeLabels[1] = new Label();
            processMemorySizeLabels[1].Text = "100 KB";
            processMemorySizeLabels[1].Font = new Font("Calida Code", 10, FontStyle.Bold);
            processMemorySizeLabels[1].ForeColor = foreColor;
            processMemorySizeLabels[1].Size = TextRenderer.MeasureText(processMemorySizeLabels[1].Text, processMemorySizeLabels[1].Font);
            processMemorySizeLabels[1].Location = new Point(memoryConfigPanel.Location.X + memoryConfigPanel.Width + 10, memoryConfigPanel.Location.Y + 
                processConfigPanels[processNumber].Height - processMemorySizeLabels[1].Height / 2);
            processMemorySizeLabels[1].BackColor = backColor;
            memoryConfig.Controls.Add(processMemorySizeLabels[1]);

            processMemorySizeLabels[processMemorySizeLabels.Length - 1] = new Label();
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].Text = memorySize + " KB";
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].Font = new Font("Calida Code", 10, FontStyle.Bold);
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].ForeColor = foreColor;
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].Size = TextRenderer.MeasureText(processMemorySizeLabels[processMemorySizeLabels.Length - 1].Text,
                processMemorySizeLabels[processMemorySizeLabels.Length - 1].Font);
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].Location = new Point(memoryConfigPanel.Location.X + memoryConfigPanel.Width + 10, memoryConfigPanel.Location.Y 
                + memoryConfigPanel.Height - processMemorySizeLabels[processMemorySizeLabels.Length - 1].Height / 2);
            processMemorySizeLabels[processMemorySizeLabels.Length - 1].BackColor = backColor;
           memoryConfig.Controls.Add(processMemorySizeLabels[processMemorySizeLabels.Length - 1]);
            timer.Start();
        }

        private void PlayButton_Paint(object? sender, PaintEventArgs e)
        {
            if (isPaused || secondsElapsed == longestCompletionTime) e.Graphics.DrawImage(Image.FromFile("C:\\Users\\Beirun\\source\\repos\\FirstFitMMCSimulation\\FirstFitMMCSimulation\\play.png"), 15, 13, 50, 50);
            else e.Graphics.DrawImage(Image.FromFile("C:\\Users\\Beirun\\source\\repos\\FirstFitMMCSimulation\\FirstFitMMCSimulation\\pause.png"), 18, 18, 40, 40);
        }

        private void BackButton_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (!isBackHovered) g.DrawImage(Image.FromFile("C:\\Users\\Beirun\\source\\repos\\FirstFitMMCSimulation\\FirstFitMMCSimulation\\left.png"), 0, 0, 100, 100);
            else g.DrawImage(Image.FromFile("C:\\Users\\Beirun\\source\\repos\\FirstFitMMCSimulation\\FirstFitMMCSimulation\\hover.png"), 0, 0, 100, 100);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            secondSlide();
        }

        private void PlayButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (secondsElapsed == longestCompletionTime + 1)
            {
                secondsElapsed = 0;
                isPaused = false;
                memoryConfiguration = new int[memorySize];
                isFinished = new bool[processNumber + 1];
                isWaiting = new bool[processNumber + 1];
                hasEntered = new bool[processNumber + 1];
                fromWaiting = new bool[processNumber + 1];
                isBeingEntered = new bool[processNumber + 1];
                memorySizeLabels = new Label[processNumber + 1];
                waitingProcessLabels = new Label[processNumber];
                for (int i = 0; i < 100; i++) memoryConfiguration[i] = -1;
                for (int i = 0; i < processNumber; i++)
                {
                    isFinished[i] = false;
                    fromWaiting[i] = false;
                    hasEntered[i] = false;
                }
                thirdSlide();
                return;
            }
            if (timer.Enabled)
            {

                if (secondsElapsed < longestCompletionTime)
                {
                    isPaused = true;
                    timer.Stop();
                    playButton.Refresh();
                }
            }
            else
            {
                if(secondsElapsed < longestCompletionTime)
                {
                    timer.Start();
                    isPaused = false;
                    playButton.Refresh();
                }
            }
            

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            timerLabel.Text = secondsElapsed + " msec";
            for (int i = 0; i < processNumber; i++)
            {
                for(int m = 0; m < processNumber; m++)
                {
                    if (processConfigPanels[m] != null && secondsElapsed == completionTimes[m] && 
                        completionTimes[m] != allocationTimes[m] && !isWaiting[m] && !isFinished[m])
                    {

                        processConfigLabels[m].Hide();
                        memorySizeLabels[m].Hide();
                        processConfigPanels[m].BackColor = memoryConfigPanel.BackColor;
                        addEvent(m, "Completed");

                        isFinished[m] = true;
                    }
                }
                for(int m = 0; m < processNumber; m++)
                {
                    if (isWaiting[m])
                    {
                        int process = Convert.ToInt32(waitingProcessLabels[m].Text.Substring(1)) - 1;

                        freeMemory = getFreeMemory(process);

                        if (freeMemory >= memoryRequests[process])
                        {
                            isWaiting[m] = false;
                            fromWaiting[process] = true;
                            i = process;
                            break;
                        }
                    }
                }
                if (secondsElapsed == allocationTimes[i] || (fromWaiting[i] && secondsElapsed < completionTimes[i]))
                {
                    freeMemory = getFreeMemory(i);

                    if (freeMemory < memoryRequests[i])
                    {
                        if (isWaiting[i]) continue;
                        waitingProcessLabels[i] = new Label();
                        waitingProcessLabels[i].Text = "P" + (i + 1);
                        waitingProcessLabels[i].Margin = new Padding(5, 5, 0, 0);
                        waitingProcessLabels[i].Size = new Size(waitingProcessPanel.Width-10 , waitingProcessPanel.Width-10);
                        Font font = new Font("Arial", 25, FontStyle.Bold);
                        int fontSize = 25;

                        while (true)
                        {
                            if (TextRenderer.MeasureText(waitingProcessLabels[i].Text, font).Width < waitingProcessLabels[i].Width - (waitingProcessLabels[i].Width / 8)) break;
                            else font = new Font("Arial", fontSize --, FontStyle.Bold);
                        }
                        waitingProcessLabels[i].Font = font;
                        waitingProcessLabels[i].ForeColor = foreColor;
                        waitingProcessLabels[i].TextAlign = ContentAlignment.MiddleCenter;
                        waitingProcessLabels[i].BackColor = ColorTranslator.FromHtml("#3f3f3f");
                        addEvent(i, "Queued");
                        isWaiting[i] = true;
                        waitingProcessPanel.Controls.Add(waitingProcessLabels[i]);
                    }
                    else if (!hasEntered[i]) {
                        
                        hasEntered[i] = true;
                        if (processIDEntered != 0) isBeingEntered[processIDEntered - 1] = true;
                        processStartLocation = processEndLocation - freeMemory;
                        addEvent(i, "Allocated");

                        processConfigPanels[i] = new Panel();
                        processConfigPanels[i].Tag = i;
                        processConfigPanels[i].Size = new Size(memoryConfigPanel.Width, (memoryConfigPanel.Height * (memoryRequests[i]+2)) / memorySize);
                        if (processIDEntered == 0) processConfigPanels[i].Location = new Point(0, memoryConfigPanel.Height * processStartLocation / memorySize - 3);
                        else processConfigPanels[i].Location = new Point(0, memoryConfigPanel.Height * 0 / memorySize);
                        processConfigPanels[i].BackColor = osProcessColor;
                        processConfigPanels[i].Paint += PaintBorder;
                        if (processIDEntered == 0) memoryConfigPanel.Controls.Add(processConfigPanels[i]);
                        else processConfigPanels[processIDEntered - 1].Controls.Add(processConfigPanels[i]);
                        
                        processConfigLabels[i] = new Label();
                        processConfigLabels[i].Text = "P" + (i + 1);

                        memorySizeLabels[i] = new Label();
                        memorySizeLabels[i].Text = "(" + memoryRequests[i] + " KB)";
                        Font font = new Font("Arial", 32, FontStyle.Bold);
                        int fontSize = 48;

                        while (true)
                        {
                            if (TextRenderer.MeasureText(processConfigLabels[i].Text, font).Height + 
                                TextRenderer.MeasureText(memorySizeLabels[i].Text, new Font("Arial", fontSize - (fontSize / 2 + fontSize / 5), FontStyle.Bold)).Height < 
                                processConfigPanels[i].Height - (processConfigPanels[i].Height / 8)) break;
                            else font = new Font("Arial", fontSize --, FontStyle.Bold);
                        }
                        processConfigLabels[i].Font = font;
                        processConfigLabels[i].ForeColor = foreColor;
                        processConfigLabels[i].Size = TextRenderer.MeasureText(processConfigLabels[i].Text, font);
                        processConfigLabels[i].Location = new Point(processConfigPanels[i].Width / 2 - processConfigLabels[i].Width / 2, 
                            processConfigPanels[i].Height / 2 - processConfigLabels[i].Height / 2);
                        processConfigLabels[i].BackColor = osProcessColor;
                        processConfigPanels[i].Controls.Add(processConfigLabels[i]);
                        processConfigLabels[i].Top -= processConfigLabels[i].Top / 2;


                        memorySizeLabels[i].Font = new Font("Arial", fontSize - (fontSize/2 + fontSize / 5), FontStyle.Bold);
                        memorySizeLabels[i].ForeColor = foreColor;
                        memorySizeLabels[i].Size = TextRenderer.MeasureText(memorySizeLabels[i].Text, memorySizeLabels[i].Font);
                        memorySizeLabels[i].Location = new Point(processConfigPanels[i].Width / 2 - memorySizeLabels[i].Width / 2, processConfigLabels[i].Top 
                            + processConfigLabels[i].Height - processConfigLabels[i].Height/15);
                        processConfigPanels[i].Controls.Add(memorySizeLabels[i]);
                        memorySizeLabels[i].BringToFront();

                        if (processEndLocation != memorySize)
                        {
                            processMemorySizeLabels[i] = new Label();
                            processMemorySizeLabels[i].Text = processEndLocation + " KB";
                            processMemorySizeLabels[i].Font = new Font("Calida Code", 10, FontStyle.Bold);
                            processMemorySizeLabels[i].ForeColor = foreColor;
                            processMemorySizeLabels[i].Size = TextRenderer.MeasureText(processMemorySizeLabels[i].Text, processMemorySizeLabels[i].Font);
                            if (processIDEntered == 0) processMemorySizeLabels[i].Location = new Point(memoryConfigPanel.Location.X + memoryConfigPanel.Width + 10,
                                memoryConfigPanel.Location.Y + (processConfigPanels[i].Location.Y + processConfigPanels[i].Height) - processMemorySizeLabels[i].Height / 2);
                            else processMemorySizeLabels[i].Location = new Point(memoryConfigPanel.Location.X + memoryConfigPanel.Width + 10,
                                memoryConfigPanel.Location.Y + (memoryConfigPanel.Height * processEndLocation / memorySize) - processMemorySizeLabels[i].Height / 2);

                            processMemorySizeLabels[i].BackColor = backColor;
                            memoryConfig.Controls.Add(processMemorySizeLabels[i]);
                        }
                        memoryConfigPanel.Refresh();
                        for (int j = processStartLocation; j < processEndLocation; j++) memoryConfiguration[j] = i + 1;
                        if (fromWaiting[i])
                        {
                            waitingProcessPanel.Controls.Remove(waitingProcessLabels[i]);
                            fromWaiting[i] = false;
                            i = 0;
                        }

                    }
                }
                if (secondsElapsed == completionTimes[i] && !isWaiting[i])
                {
                    processConfigLabels[i].Hide();
                    memorySizeLabels[i].Hide();
                    processConfigPanels[i].BackColor = memoryConfigPanel.BackColor;
                    isFinished[i] = true;
                }
            }
            if (secondsElapsed >= longestCompletionTime)
            {

                timer.Stop();
                for(int i = 0; i < processNumber; i++)
                {
                    if (isWaiting[i])
                    {
                        addEvent(i , "Terminated");
                        waitingProcessPanel.Controls.Remove(waitingProcessLabels[i]);
                    }
                }
                Label externalLabel = new Label();
                externalLabel.Text = "Total External Fragmentation";
                externalLabel.BackColor = backColor;
                externalLabel.ForeColor = foreColor;
                externalLabel.Font = new Font("Calida Code", 14, FontStyle.Bold);
                externalLabel.Size = TextRenderer.MeasureText(externalLabel.Text, externalLabel.Font);
                externalLabel.Location = new Point(eventList.Left + eventList.Width/2 - externalLabel.Width/2, eventList.Top + eventList.Height + 25);
                thirdPanel.Controls.Add(externalLabel);

                Label externalFragLabel = new Label();
                int externalFrag = 0;
                for(int i  = 100; i < memoryConfiguration.Length; i++) for(int j= 0; j < processNumber; j++) if (isBeingEntered[j] && j + 1 == memoryConfiguration[i]) externalFrag++;
                
                externalFragLabel.Text = externalFrag + " KB";
                externalFragLabel.BackColor = backColor;
                externalFragLabel.ForeColor = Color.DarkOrange;
                externalFragLabel.Font = new Font("Arial", 28, FontStyle.Bold);
                externalFragLabel.Size = TextRenderer.MeasureText(externalFragLabel.Text, externalFragLabel.Font);
                externalFragLabel.Location = new Point(externalLabel.Left + externalLabel.Width / 2 - externalFragLabel.Width / 2, externalLabel.Top + externalLabel.Height + 10);
                thirdPanel.Controls.Add(externalFragLabel);
                isPaused = true;
                playButton.Refresh();
            }
            secondsElapsed++;
        }

        public int getFreeMemory(int process)
        {
            int freeMemory = 0;
            for (int j = 100; j < memorySize; j++)
            {
                if (memoryConfiguration[j] == 0)
                {
                    if (processIDEntered != 0) freeMemory = 0;
                    freeMemory++;
                    processIDEntered = 0;

                    /*if (memoryConfiguration[j - 1] == 0)
                    {
                        freeMemory++;
                        processIDEntered = 0;
                    }
                    else if ((j + 1 == memorySize || memoryConfiguration[j + 1] != processIDEntered) && memoryConfiguration[j-1] == processIDEntered) freeMemory++;
                    else freeMemory = 0;*/
                }
                else
                {
                    for (int k = 1; k <= processNumber; k++)
                    {
                        if (memoryConfiguration[j] == k && isFinished[k - 1])
                        {
                            if (!isBeingEntered[k - 1])
                            {
                                if (processIDEntered != k) freeMemory = 0;
                                freeMemory++;
                                processIDEntered = k;
                            }
                            else
                            {
                                freeMemory = 0;
                                break;
                            }
                            /*if (memoryConfiguration[j - 1] == k && isFinished[k - 1] && !isBeingEntered[k - 1])
                            {
                                freeMemory++;
                                processIDEntered = k;
                            }
                            else if ((j + 1 == memorySize || memoryConfiguration[j + 1] != processIDEntered) && memoryConfiguration[j - 1] == processIDEntered) freeMemory++;
                            else freeMemory = 0;*/

                        }
                    }
                }
                if (freeMemory == memoryRequests[process])
                {
                    processEndLocation = j+1;
                    break;
                }
            }
            return freeMemory;
        }

        public void addEvent(int i, string eventString)
        {
            if(eventList.Controls.Count == 0) eventList.Height += 10;
            if (bottomLabel != null) eventList.Controls.Remove(bottomLabel);
            Label eventLabel = new Label();
            eventLabel.Text = "P" + (i + 1) + " " + eventString + " at " + secondsElapsed + " msec";
            eventLabel.ForeColor = foreColor;
            eventLabel.Font = new Font("Calida Code", 14, FontStyle.Bold);
            eventLabel.Margin = new Padding(5, 10, 0, 0);
            eventLabel.Size = new Size(350 - 10, TextRenderer.MeasureText(eventLabel.Text, eventLabel.Font).Height + 10);
            eventLabel.TextAlign = ContentAlignment.MiddleLeft;
            eventList.Controls.Add(eventLabel);
            if(eventList.Height < 500) eventList.Height += eventLabel.Height + 10;
            else if(eventList.Controls.Count > 9 && !isScrolled)
            {
                isScrolled = true;
                eventList.Width += 25;
                eventList.AutoScroll = true;
                bottomLabel = new Label();
            }
            else
            {
                bottomLabel.Size = new Size(350 - 10, 0);
                bottomLabel.Margin = new Padding(5, 10, 0, 0);
                bottomLabel.BackColor = eventList.BackColor;
                eventList.Controls.Add(bottomLabel);
            }
            if (eventList.Controls.Count == 1)
            {
                Label label = new Label();
                label.Text = "Event List";
                label.Font = new Font("Calida Code", 18, FontStyle.Bold);
                label.ForeColor = foreColor;
                label.Size = TextRenderer.MeasureText(label.Text, label.Font);
                label.Location = new Point(eventList.Left + eventList.Width / 2 - label.Width / 2, eventList.Top - label.Height - 10);
                thirdPanel.Controls.Add(label);
            }
        }
        private void PaintBorder(object? sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            int index = (int)panel.Tag;
            Graphics graphics = e.Graphics;
            Pen pen1 = new Pen(panel.BackColor, 2);

            using (Pen pen = new Pen(ColorTranslator.FromHtml("#353535"), 5))
            {
                if (index != processNumber)
                {
                    using (SolidBrush brush = new SolidBrush(Color.DarkOrange)) if (isFinished[index] && isBeingEntered[index]) 
                            graphics.FillRectangle(brush, new Rectangle(0, -1, panel.Width, panel.Height));
                    using (SolidBrush brush = new SolidBrush(memoryConfigPanel.BackColor)) if (isFinished[index] && !isBeingEntered[index]) 
                            graphics.FillRectangle(brush, new Rectangle(0, -1, panel.Width, panel.Height));
                }
                if (panel.Location.Y == 0 && processIDEntered == 0 && repaintedBorder == 0) e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                else if (repaintedBorder != 0) e.Graphics.DrawRectangle(pen, new Rectangle(0, -3, panel.Width - 1, panel.Height + 2));
                if (index == processNumber) e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                repaintedBorder++;
            }
        }
        private void PaintBorderConfig(object? sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics graphics = e.Graphics;
            Pen pen1 = new Pen(panel.BackColor, 2);
            using (SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#353535")))
            {
                int end = 0;
                for (int i = 0; i < memorySize; i++) if (memoryConfiguration[i] != 0) end++;
                graphics.FillRectangle(brush, new Rectangle(0, -1, panel.Width - 1, (memoryConfigPanel.Height * end) / memorySize - 3));
            }
            using (Pen pen = new Pen(ColorTranslator.FromHtml("#353535"), 5))
            {
                if (panel.Location.Y == 0)  e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1));
                else e.Graphics.DrawRectangle(pen, new Rectangle(0, -3, panel.Width - 1, panel.Height + 2));
            }
            processConfigPanels[processNumber].BackColor = osProcessColor;
        }
    }
}
