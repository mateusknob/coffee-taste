namespace CoffeeTaste
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnAvaliacoes = new System.Windows.Forms.Button();
            this.btnRotulos = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAvaliacoes
            // 
            this.btnAvaliacoes.Location = new System.Drawing.Point(14, 12);
            this.btnAvaliacoes.Name = "btnAvaliacoes";
            this.btnAvaliacoes.Size = new System.Drawing.Size(165, 23);
            this.btnAvaliacoes.TabIndex = 1;
            this.btnAvaliacoes.Text = "Avaliações";
            this.btnAvaliacoes.UseVisualStyleBackColor = true;
            this.btnAvaliacoes.Click += new System.EventHandler(this.btnAvaliacoes_Click);
            // 
            // btnRotulos
            // 
            this.btnRotulos.Location = new System.Drawing.Point(13, 41);
            this.btnRotulos.Name = "btnRotulos";
            this.btnRotulos.Size = new System.Drawing.Size(165, 23);
            this.btnRotulos.TabIndex = 3;
            this.btnRotulos.Text = "Rótulos";
            this.btnRotulos.UseVisualStyleBackColor = true;
            this.btnRotulos.Click += new System.EventHandler(this.btnRotulos_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(14, 156);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(165, 23);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(13, 70);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(165, 23);
            this.btnBackup.TabIndex = 5;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 191);
            this.ControlBox = false;
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnRotulos);
            this.Controls.Add(this.btnAvaliacoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coffee Taste";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAvaliacoes;
        private System.Windows.Forms.Button btnRotulos;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnBackup;
    }
}