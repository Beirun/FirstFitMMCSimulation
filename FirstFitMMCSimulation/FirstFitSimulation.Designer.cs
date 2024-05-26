namespace FirstFitMMCSimulation
{
    partial class FirstFitSimulation
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            firstPanel = new Panel();
            errorProcess = new Label();
            errorMemory = new Label();
            nextButton = new Button();
            firstPanelLabel = new Label();
            memorySizePanel = new Panel();
            memorySizeTextBox = new TextBox();
            processNumberPanel = new Panel();
            processNumberTextBox = new TextBox();
            processNumberLabel = new Label();
            memorySizeLabel = new Label();
            secondPanel = new Panel();
            proceedButton = new Button();
            thirdPanel = new Panel();
            playButton = new RoundButton();
            waitingProcessPanel = new FlowLayoutPanel();
            timerLabel = new Label();
            memoryConfig = new Panel();
            firstPanel.SuspendLayout();
            memorySizePanel.SuspendLayout();
            processNumberPanel.SuspendLayout();
            secondPanel.SuspendLayout();
            thirdPanel.SuspendLayout();
            SuspendLayout();
            // 
            // firstPanel
            // 
            firstPanel.Controls.Add(errorProcess);
            firstPanel.Controls.Add(errorMemory);
            firstPanel.Controls.Add(nextButton);
            firstPanel.Controls.Add(firstPanelLabel);
            firstPanel.Controls.Add(memorySizePanel);
            firstPanel.Controls.Add(processNumberPanel);
            firstPanel.Controls.Add(processNumberLabel);
            firstPanel.Controls.Add(memorySizeLabel);
            firstPanel.Location = new Point(81, 44);
            firstPanel.Name = "firstPanel";
            firstPanel.Size = new Size(430, 311);
            firstPanel.TabIndex = 0;
            // 
            // errorProcess
            // 
            errorProcess.AutoSize = true;
            errorProcess.Location = new Point(307, 208);
            errorProcess.Name = "errorProcess";
            errorProcess.Size = new Size(50, 20);
            errorProcess.TabIndex = 10;
            errorProcess.Text = "label1";
            // 
            // errorMemory
            // 
            errorMemory.AutoSize = true;
            errorMemory.Location = new Point(307, 114);
            errorMemory.Name = "errorMemory";
            errorMemory.Size = new Size(50, 20);
            errorMemory.TabIndex = 8;
            errorMemory.Text = "label1";
            // 
            // nextButton
            // 
            nextButton.Location = new Point(160, 271);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(77, 24);
            nextButton.TabIndex = 7;
            nextButton.Text = "Next";
            nextButton.UseVisualStyleBackColor = true;
            // 
            // firstPanelLabel
            // 
            firstPanelLabel.AutoSize = true;
            firstPanelLabel.Location = new Point(192, 27);
            firstPanelLabel.Name = "firstPanelLabel";
            firstPanelLabel.Size = new Size(171, 20);
            firstPanelLabel.TabIndex = 6;
            firstPanelLabel.Text = "ENTER CONFIGURATION";
            // 
            // memorySizePanel
            // 
            memorySizePanel.Controls.Add(memorySizeTextBox);
            memorySizePanel.Location = new Point(128, 113);
            memorySizePanel.Name = "memorySizePanel";
            memorySizePanel.Size = new Size(161, 44);
            memorySizePanel.TabIndex = 5;
            // 
            // memorySizeTextBox
            // 
            memorySizeTextBox.BorderStyle = BorderStyle.None;
            memorySizeTextBox.Location = new Point(14, 3);
            memorySizeTextBox.Name = "memorySizeTextBox";
            memorySizeTextBox.Size = new Size(125, 20);
            memorySizeTextBox.TabIndex = 0;
            // 
            // processNumberPanel
            // 
            processNumberPanel.Controls.Add(processNumberTextBox);
            processNumberPanel.Location = new Point(128, 207);
            processNumberPanel.Name = "processNumberPanel";
            processNumberPanel.Size = new Size(161, 44);
            processNumberPanel.TabIndex = 4;
            // 
            // processNumberTextBox
            // 
            processNumberTextBox.BorderStyle = BorderStyle.None;
            processNumberTextBox.Location = new Point(14, 3);
            processNumberTextBox.Name = "processNumberTextBox";
            processNumberTextBox.Size = new Size(125, 20);
            processNumberTextBox.TabIndex = 1;
            // 
            // processNumberLabel
            // 
            processNumberLabel.AutoSize = true;
            processNumberLabel.Location = new Point(142, 174);
            processNumberLabel.Name = "processNumberLabel";
            processNumberLabel.Size = new Size(136, 20);
            processNumberLabel.TabIndex = 3;
            processNumberLabel.Text = "Number Of Process";
            // 
            // memorySizeLabel
            // 
            memorySizeLabel.AutoSize = true;
            memorySizeLabel.Location = new Point(142, 78);
            memorySizeLabel.Name = "memorySizeLabel";
            memorySizeLabel.Size = new Size(95, 20);
            memorySizeLabel.TabIndex = 2;
            memorySizeLabel.Text = "Memory Size";
            // 
            // secondPanel
            // 
            secondPanel.Controls.Add(proceedButton);
            secondPanel.Location = new Point(544, 241);
            secondPanel.Name = "secondPanel";
            secondPanel.Size = new Size(386, 306);
            secondPanel.TabIndex = 1;
            // 
            // proceedButton
            // 
            proceedButton.Location = new Point(140, 249);
            proceedButton.Name = "proceedButton";
            proceedButton.Size = new Size(94, 29);
            proceedButton.TabIndex = 0;
            proceedButton.Text = "Proceed";
            proceedButton.UseVisualStyleBackColor = true;
            proceedButton.Click += proceedButton_Click;
            // 
            // thirdPanel
            // 
            thirdPanel.Controls.Add(playButton);
            thirdPanel.Controls.Add(waitingProcessPanel);
            thirdPanel.Controls.Add(timerLabel);
            thirdPanel.Controls.Add(memoryConfig);
            thirdPanel.Location = new Point(1037, 71);
            thirdPanel.Name = "thirdPanel";
            thirdPanel.Size = new Size(311, 284);
            thirdPanel.TabIndex = 2;
            // 
            // playButton
            // 
            playButton.Location = new Point(14, 118);
            playButton.Name = "playButton";
            playButton.Size = new Size(100, 100);
            playButton.TabIndex = 4;
            playButton.UseVisualStyleBackColor = true;
            // 
            // waitingProcessPanel
            // 
            waitingProcessPanel.Anchor = AnchorStyles.None;
            waitingProcessPanel.Location = new Point(51, 229);
            waitingProcessPanel.Name = "waitingProcessPanel";
            waitingProcessPanel.Size = new Size(250, 31);
            waitingProcessPanel.TabIndex = 3;
            // 
            // timerLabel
            // 
            timerLabel.AutoSize = true;
            timerLabel.Location = new Point(142, 22);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(50, 20);
            timerLabel.TabIndex = 1;
            timerLabel.Text = "label1";
            // 
            // memoryConfig
            // 
            memoryConfig.Location = new Point(130, 99);
            memoryConfig.Name = "memoryConfig";
            memoryConfig.Size = new Size(80, 104);
            memoryConfig.TabIndex = 0;
            // 
            // FirstFitSimulation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 817);
            Controls.Add(thirdPanel);
            Controls.Add(secondPanel);
            Controls.Add(firstPanel);
            Name = "FirstFitSimulation";
            Text = "First Fit Simulation";
            firstPanel.ResumeLayout(false);
            firstPanel.PerformLayout();
            memorySizePanel.ResumeLayout(false);
            memorySizePanel.PerformLayout();
            processNumberPanel.ResumeLayout(false);
            processNumberPanel.PerformLayout();
            secondPanel.ResumeLayout(false);
            thirdPanel.ResumeLayout(false);
            thirdPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel firstPanel;
        private Panel secondPanel;
        private TextBox processNumberTextBox;
        private TextBox memorySizeTextBox;
        private Label processNumberLabel;
        private Label memorySizeLabel;
        private Panel processNumberPanel;
        private Panel memorySizePanel;
        private Button nextButton;
        private Label firstPanelLabel;
        private Button proceedButton;
        private Panel thirdPanel;
        private Label timerLabel;
        private Panel memoryConfig;
        private Label errorProcess;
        private Label errorMemory;
        private FlowLayoutPanel waitingProcessPanel;
        private RoundButton playButton;
    }
}
