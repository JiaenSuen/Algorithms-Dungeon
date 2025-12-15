namespace Encrypt_Decrypt
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_val = new System.Windows.Forms.Label();
            this.text_val = new System.Windows.Forms.TextBox();
            this.text_Decrypt = new System.Windows.Forms.TextBox();
            this.label_decry = new System.Windows.Forms.Label();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.label_encry = new System.Windows.Forms.Label();
            this.EncryBtn = new System.Windows.Forms.Button();
            this.DecryBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Selection_Encrpto_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.Selection_Encrypto_List = new System.Windows.Forms.ListBox();
            this.btnAddToSequence = new System.Windows.Forms.Button();
            this.btnMoveUp_Click = new System.Windows.Forms.Button();
            this.btnMoveDown_Click = new System.Windows.Forms.Button();
            this.Encrypt_Procedure_Text = new System.Windows.Forms.TextBox();
            this.Decrypt_Procedure_Text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_val
            // 
            this.label_val.AutoSize = true;
            this.label_val.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_val.Location = new System.Drawing.Point(85, 73);
            this.label_val.Name = "label_val";
            this.label_val.Size = new System.Drawing.Size(69, 23);
            this.label_val.TabIndex = 0;
            this.label_val.Text = "Input  :";
            // 
            // text_val
            // 
            this.text_val.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_val.Location = new System.Drawing.Point(170, 73);
            this.text_val.Name = "text_val";
            this.text_val.Size = new System.Drawing.Size(302, 30);
            this.text_val.TabIndex = 1;
            // 
            // text_Decrypt
            // 
            this.text_Decrypt.BackColor = System.Drawing.Color.White;
            this.text_Decrypt.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_Decrypt.Location = new System.Drawing.Point(170, 369);
            this.text_Decrypt.Name = "text_Decrypt";
            this.text_Decrypt.ReadOnly = true;
            this.text_Decrypt.Size = new System.Drawing.Size(302, 30);
            this.text_Decrypt.TabIndex = 3;
            // 
            // label_decry
            // 
            this.label_decry.AutoSize = true;
            this.label_decry.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_decry.Location = new System.Drawing.Point(85, 369);
            this.label_decry.Name = "label_decry";
            this.label_decry.Size = new System.Drawing.Size(77, 23);
            this.label_decry.TabIndex = 2;
            this.label_decry.Text = "Decrypt";
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.BackColor = System.Drawing.Color.White;
            this.txtEncrypt.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtEncrypt.Location = new System.Drawing.Point(170, 213);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.ReadOnly = true;
            this.txtEncrypt.Size = new System.Drawing.Size(302, 30);
            this.txtEncrypt.TabIndex = 5;
            // 
            // label_encry
            // 
            this.label_encry.AutoSize = true;
            this.label_encry.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_encry.Location = new System.Drawing.Point(85, 215);
            this.label_encry.Name = "label_encry";
            this.label_encry.Size = new System.Drawing.Size(74, 23);
            this.label_encry.TabIndex = 4;
            this.label_encry.Text = "Encrypt";
            // 
            // EncryBtn
            // 
            this.EncryBtn.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EncryBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.EncryBtn.Location = new System.Drawing.Point(487, 73);
            this.EncryBtn.Name = "EncryBtn";
            this.EncryBtn.Size = new System.Drawing.Size(75, 32);
            this.EncryBtn.TabIndex = 6;
            this.EncryBtn.Text = "Encrypt";
            this.EncryBtn.UseVisualStyleBackColor = true;
            this.EncryBtn.Click += new System.EventHandler(this.EncryBtn_Click);
            // 
            // DecryBtn
            // 
            this.DecryBtn.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DecryBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DecryBtn.Location = new System.Drawing.Point(487, 213);
            this.DecryBtn.Name = "DecryBtn";
            this.DecryBtn.Size = new System.Drawing.Size(75, 32);
            this.DecryBtn.TabIndex = 7;
            this.DecryBtn.Text = "Decrypt";
            this.DecryBtn.UseVisualStyleBackColor = true;
            this.DecryBtn.Click += new System.EventHandler(this.DecryBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(639, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Selection ";
            // 
            // Selection_Encrpto_checkedListBox
            // 
            this.Selection_Encrpto_checkedListBox.BackColor = System.Drawing.Color.MintCream;
            this.Selection_Encrpto_checkedListBox.FormattingEnabled = true;
            this.Selection_Encrpto_checkedListBox.Items.AddRange(new object[] {
            "Excess-3",
            "Vigenere-KEY",
            "XOR-K",
            "XOR-64-K",
            "Feistel",
            "Playfair",
            "DES",
            "3DES",
            "3DES-CBC",
            "AES",
            "RC2",
            "RSA"});
            this.Selection_Encrpto_checkedListBox.Location = new System.Drawing.Point(618, 73);
            this.Selection_Encrpto_checkedListBox.Name = "Selection_Encrpto_checkedListBox";
            this.Selection_Encrpto_checkedListBox.Size = new System.Drawing.Size(165, 144);
            this.Selection_Encrpto_checkedListBox.TabIndex = 10;
            // 
            // Selection_Encrypto_List
            // 
            this.Selection_Encrypto_List.BackColor = System.Drawing.Color.MintCream;
            this.Selection_Encrypto_List.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Selection_Encrypto_List.FormattingEnabled = true;
            this.Selection_Encrypto_List.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Selection_Encrypto_List.ItemHeight = 15;
            this.Selection_Encrypto_List.Items.AddRange(new object[] {
            " "});
            this.Selection_Encrypto_List.Location = new System.Drawing.Point(618, 260);
            this.Selection_Encrypto_List.Name = "Selection_Encrypto_List";
            this.Selection_Encrypto_List.Size = new System.Drawing.Size(165, 109);
            this.Selection_Encrypto_List.TabIndex = 11;
            // 
            // btnAddToSequence
            // 
            this.btnAddToSequence.BackColor = System.Drawing.Color.MintCream;
            this.btnAddToSequence.Location = new System.Drawing.Point(618, 218);
            this.btnAddToSequence.Name = "btnAddToSequence";
            this.btnAddToSequence.Size = new System.Drawing.Size(165, 34);
            this.btnAddToSequence.TabIndex = 12;
            this.btnAddToSequence.Text = "Confirm";
            this.btnAddToSequence.UseVisualStyleBackColor = false;
            this.btnAddToSequence.Click += new System.EventHandler(this.btnAddToSequence_Click);
            // 
            // btnMoveUp_Click
            // 
            this.btnMoveUp_Click.Location = new System.Drawing.Point(618, 381);
            this.btnMoveUp_Click.Name = "btnMoveUp_Click";
            this.btnMoveUp_Click.Size = new System.Drawing.Size(75, 32);
            this.btnMoveUp_Click.TabIndex = 13;
            this.btnMoveUp_Click.Text = "Up";
            this.btnMoveUp_Click.UseVisualStyleBackColor = true;
            this.btnMoveUp_Click.Click += new System.EventHandler(this.btnMoveUp_Click_Click);
            // 
            // btnMoveDown_Click
            // 
            this.btnMoveDown_Click.Location = new System.Drawing.Point(708, 381);
            this.btnMoveDown_Click.Name = "btnMoveDown_Click";
            this.btnMoveDown_Click.Size = new System.Drawing.Size(75, 32);
            this.btnMoveDown_Click.TabIndex = 14;
            this.btnMoveDown_Click.Text = "Down";
            this.btnMoveDown_Click.UseVisualStyleBackColor = true;
            this.btnMoveDown_Click.Click += new System.EventHandler(this.btnMoveDown_Click_Click);
            // 
            // Encrypt_Procedure_Text
            // 
            this.Encrypt_Procedure_Text.BackColor = System.Drawing.Color.White;
            this.Encrypt_Procedure_Text.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Encrypt_Procedure_Text.Location = new System.Drawing.Point(170, 115);
            this.Encrypt_Procedure_Text.Multiline = true;
            this.Encrypt_Procedure_Text.Name = "Encrypt_Procedure_Text";
            this.Encrypt_Procedure_Text.ReadOnly = true;
            this.Encrypt_Procedure_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Encrypt_Procedure_Text.Size = new System.Drawing.Size(302, 89);
            this.Encrypt_Procedure_Text.TabIndex = 15;
            // 
            // Decrypt_Procedure_Text
            // 
            this.Decrypt_Procedure_Text.BackColor = System.Drawing.Color.White;
            this.Decrypt_Procedure_Text.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Decrypt_Procedure_Text.Location = new System.Drawing.Point(170, 264);
            this.Decrypt_Procedure_Text.Multiline = true;
            this.Decrypt_Procedure_Text.Name = "Decrypt_Procedure_Text";
            this.Decrypt_Procedure_Text.ReadOnly = true;
            this.Decrypt_Procedure_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Decrypt_Procedure_Text.Size = new System.Drawing.Size(302, 89);
            this.Decrypt_Procedure_Text.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(213, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Encrypt And Decrypt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(829, 510);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Decrypt_Procedure_Text);
            this.Controls.Add(this.Encrypt_Procedure_Text);
            this.Controls.Add(this.btnMoveDown_Click);
            this.Controls.Add(this.btnMoveUp_Click);
            this.Controls.Add(this.btnAddToSequence);
            this.Controls.Add(this.Selection_Encrypto_List);
            this.Controls.Add(this.Selection_Encrpto_checkedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecryBtn);
            this.Controls.Add(this.EncryBtn);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.label_encry);
            this.Controls.Add(this.text_Decrypt);
            this.Controls.Add(this.label_decry);
            this.Controls.Add(this.text_val);
            this.Controls.Add(this.label_val);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Encrypt & Decrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_val;
        private System.Windows.Forms.TextBox text_val;
        private System.Windows.Forms.TextBox text_Decrypt;
        private System.Windows.Forms.Label label_decry;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.Label label_encry;
        private System.Windows.Forms.Button EncryBtn;
        private System.Windows.Forms.Button DecryBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox Selection_Encrpto_checkedListBox;
        private System.Windows.Forms.ListBox Selection_Encrypto_List;
        private System.Windows.Forms.Button btnAddToSequence;
        private System.Windows.Forms.Button btnMoveUp_Click;
        private System.Windows.Forms.Button btnMoveDown_Click;
        private System.Windows.Forms.TextBox Encrypt_Procedure_Text;
        private System.Windows.Forms.TextBox Decrypt_Procedure_Text;
        private System.Windows.Forms.Label label2;
    }
}

