namespace UfoMovement
{
    partial class MainForm
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
            this.buttonLineMove = new System.Windows.Forms.Button();
            this.buttonAngelMove = new System.Windows.Forms.Button();
            this.canvasBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLineMove
            // 
            this.buttonLineMove.Location = new System.Drawing.Point(12, 415);
            this.buttonLineMove.Name = "buttonLineMove";
            this.buttonLineMove.Size = new System.Drawing.Size(239, 23);
            this.buttonLineMove.TabIndex = 0;
            this.buttonLineMove.Text = "Движение точки по уравнению прямой";
            this.buttonLineMove.UseVisualStyleBackColor = true;
            this.buttonLineMove.Click += new System.EventHandler(this.buttonLineMove_Click);
            // 
            // buttonAngelMove
            // 
            this.buttonAngelMove.Location = new System.Drawing.Point(257, 415);
            this.buttonAngelMove.Name = "buttonAngelMove";
            this.buttonAngelMove.Size = new System.Drawing.Size(239, 23);
            this.buttonAngelMove.TabIndex = 1;
            this.buttonAngelMove.Text = "Движение точки по углу";
            this.buttonAngelMove.UseVisualStyleBackColor = true;
            this.buttonAngelMove.Click += new System.EventHandler(this.buttonAngelMove_Click);
            // 
            // canvasBox
            // 
            this.canvasBox.BackColor = System.Drawing.Color.White;
            this.canvasBox.Location = new System.Drawing.Point(0, 0);
            this.canvasBox.Name = "canvasBox";
            this.canvasBox.Size = new System.Drawing.Size(800, 409);
            this.canvasBox.TabIndex = 2;
            this.canvasBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.canvasBox);
            this.Controls.Add(this.buttonAngelMove);
            this.Controls.Add(this.buttonLineMove);
            this.Name = "MainForm";
            this.Text = "GraphicLine";
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLineMove;
        private System.Windows.Forms.Button buttonAngelMove;
        private System.Windows.Forms.PictureBox canvasBox;
    }
}

