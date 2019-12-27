namespace AudioTool
{
    partial class FrmAudioTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAudioTool));
            this.ucMicrophone1 = new AudioTool.UCMicrophone();
            this.ucSpeaker1 = new AudioTool.UCSpeaker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSound = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucMicrophone1
            // 
            this.ucMicrophone1.BackColor = System.Drawing.Color.Black;
            this.ucMicrophone1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMicrophone1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMicrophone1.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.ucMicrophone1.Location = new System.Drawing.Point(11, 12);
            this.ucMicrophone1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ucMicrophone1.Name = "ucMicrophone1";
            this.ucMicrophone1.Size = new System.Drawing.Size(50, 300);
            this.ucMicrophone1.TabIndex = 0;
            this.ucMicrophone1.UsageColor = System.Drawing.Color.Red;
            // 
            // ucSpeaker1
            // 
            this.ucSpeaker1.BackColor = System.Drawing.Color.Black;
            this.ucSpeaker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucSpeaker1.Location = new System.Drawing.Point(79, 12);
            this.ucSpeaker1.Name = "ucSpeaker1";
            this.ucSpeaker1.Size = new System.Drawing.Size(50, 300);
            this.ucSpeaker1.TabIndex = 1;
            this.ucSpeaker1.UsageColor = System.Drawing.Color.Red;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mic";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(77, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "LS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSound
            // 
            this.btnSound.Location = new System.Drawing.Point(12, 354);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(115, 35);
            this.btnSound.TabIndex = 4;
            this.btnSound.Text = "Sound...";
            this.btnSound.UseVisualStyleBackColor = true;
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // FrmAudioTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(139, 404);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucSpeaker1);
            this.Controls.Add(this.ucMicrophone1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAudioTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Tool";
            this.Load += new System.EventHandler(this.FrmAudioTool_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCMicrophone ucMicrophone1;
        private UCSpeaker ucSpeaker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSound;
    }
}

