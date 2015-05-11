namespace JimmyPresentation
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnDeserializeJson = new System.Windows.Forms.Button();
            this.btnSerializeJson = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSerializeStructArray = new System.Windows.Forms.Button();
            this.btnSerializeProtoBuf = new System.Windows.Forms.Button();
            this.btnSerializeBinaryFormatter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeserializeStructArray = new System.Windows.Forms.Button();
            this.btnDeserializeProtoBuf = new System.Windows.Forms.Button();
            this.btnDeserializeBinaryFormatter = new System.Windows.Forms.Button();
            this.btnSerializeSql = new System.Windows.Forms.Button();
            this.btnDeserializeSql = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Array of class";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Array of struct";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(437, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(405, 257);
            this.textBox1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(37, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 33);
            this.button3.TabIndex = 4;
            this.button3.Text = "LengthClass[]";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(37, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 31);
            this.button4.TabIndex = 3;
            this.button4.Text = "double[]";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(37, 188);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 33);
            this.button5.TabIndex = 5;
            this.button5.Text = "Length[]";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnDeserializeJson
            // 
            this.btnDeserializeJson.Location = new System.Drawing.Point(6, 64);
            this.btnDeserializeJson.Name = "btnDeserializeJson";
            this.btnDeserializeJson.Size = new System.Drawing.Size(139, 33);
            this.btnDeserializeJson.TabIndex = 7;
            this.btnDeserializeJson.Text = "JSON";
            this.btnDeserializeJson.UseVisualStyleBackColor = true;
            this.btnDeserializeJson.Click += new System.EventHandler(this.btnDeserializeJson_Click);
            // 
            // btnSerializeJson
            // 
            this.btnSerializeJson.Location = new System.Drawing.Point(6, 64);
            this.btnSerializeJson.Name = "btnSerializeJson";
            this.btnSerializeJson.Size = new System.Drawing.Size(139, 33);
            this.btnSerializeJson.TabIndex = 6;
            this.btnSerializeJson.Text = "JSON";
            this.btnSerializeJson.UseVisualStyleBackColor = true;
            this.btnSerializeJson.Click += new System.EventHandler(this.btnSerializeJson_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSerializeSql);
            this.groupBox1.Controls.Add(this.btnSerializeStructArray);
            this.groupBox1.Controls.Add(this.btnSerializeProtoBuf);
            this.groupBox1.Controls.Add(this.btnSerializeBinaryFormatter);
            this.groupBox1.Controls.Add(this.btnSerializeJson);
            this.groupBox1.Location = new System.Drawing.Point(37, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 220);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serialize";
            // 
            // btnSerializeStructArray
            // 
            this.btnSerializeStructArray.Location = new System.Drawing.Point(6, 181);
            this.btnSerializeStructArray.Name = "btnSerializeStructArray";
            this.btnSerializeStructArray.Size = new System.Drawing.Size(139, 33);
            this.btnSerializeStructArray.TabIndex = 9;
            this.btnSerializeStructArray.Text = "StructArray";
            this.btnSerializeStructArray.UseVisualStyleBackColor = true;
            this.btnSerializeStructArray.Click += new System.EventHandler(this.btnSerializeStructArray_Click);
            // 
            // btnSerializeProtoBuf
            // 
            this.btnSerializeProtoBuf.Location = new System.Drawing.Point(6, 142);
            this.btnSerializeProtoBuf.Name = "btnSerializeProtoBuf";
            this.btnSerializeProtoBuf.Size = new System.Drawing.Size(139, 33);
            this.btnSerializeProtoBuf.TabIndex = 8;
            this.btnSerializeProtoBuf.Text = "ProtoBuf";
            this.btnSerializeProtoBuf.UseVisualStyleBackColor = true;
            this.btnSerializeProtoBuf.Click += new System.EventHandler(this.btnSerializeProtoBuf_Click);
            // 
            // btnSerializeBinaryFormatter
            // 
            this.btnSerializeBinaryFormatter.Location = new System.Drawing.Point(6, 103);
            this.btnSerializeBinaryFormatter.Name = "btnSerializeBinaryFormatter";
            this.btnSerializeBinaryFormatter.Size = new System.Drawing.Size(139, 33);
            this.btnSerializeBinaryFormatter.TabIndex = 7;
            this.btnSerializeBinaryFormatter.Text = "BinaryFormatter";
            this.btnSerializeBinaryFormatter.UseVisualStyleBackColor = true;
            this.btnSerializeBinaryFormatter.Click += new System.EventHandler(this.btnSerializeBinaryFormatter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeserializeSql);
            this.groupBox2.Controls.Add(this.btnDeserializeStructArray);
            this.groupBox2.Controls.Add(this.btnDeserializeProtoBuf);
            this.groupBox2.Controls.Add(this.btnDeserializeBinaryFormatter);
            this.groupBox2.Controls.Add(this.btnDeserializeJson);
            this.groupBox2.Location = new System.Drawing.Point(197, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 220);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deserialize";
            // 
            // btnDeserializeStructArray
            // 
            this.btnDeserializeStructArray.Location = new System.Drawing.Point(6, 181);
            this.btnDeserializeStructArray.Name = "btnDeserializeStructArray";
            this.btnDeserializeStructArray.Size = new System.Drawing.Size(139, 33);
            this.btnDeserializeStructArray.TabIndex = 10;
            this.btnDeserializeStructArray.Text = "StructArray";
            this.btnDeserializeStructArray.UseVisualStyleBackColor = true;
            this.btnDeserializeStructArray.Click += new System.EventHandler(this.btnDeserializeStructArray_Click);
            // 
            // btnDeserializeProtoBuf
            // 
            this.btnDeserializeProtoBuf.Location = new System.Drawing.Point(6, 142);
            this.btnDeserializeProtoBuf.Name = "btnDeserializeProtoBuf";
            this.btnDeserializeProtoBuf.Size = new System.Drawing.Size(139, 33);
            this.btnDeserializeProtoBuf.TabIndex = 9;
            this.btnDeserializeProtoBuf.Text = "ProtoBuf";
            this.btnDeserializeProtoBuf.UseVisualStyleBackColor = true;
            this.btnDeserializeProtoBuf.Click += new System.EventHandler(this.btnDeserializeProtoBuf_Click);
            // 
            // btnDeserializeBinaryFormatter
            // 
            this.btnDeserializeBinaryFormatter.Location = new System.Drawing.Point(6, 103);
            this.btnDeserializeBinaryFormatter.Name = "btnDeserializeBinaryFormatter";
            this.btnDeserializeBinaryFormatter.Size = new System.Drawing.Size(139, 33);
            this.btnDeserializeBinaryFormatter.TabIndex = 8;
            this.btnDeserializeBinaryFormatter.Text = "BinaryFormatter";
            this.btnDeserializeBinaryFormatter.UseVisualStyleBackColor = true;
            this.btnDeserializeBinaryFormatter.Click += new System.EventHandler(this.btnDeserializeBinaryFormatter_Click);
            // 
            // btnSerializeSql
            // 
            this.btnSerializeSql.Location = new System.Drawing.Point(6, 25);
            this.btnSerializeSql.Name = "btnSerializeSql";
            this.btnSerializeSql.Size = new System.Drawing.Size(139, 33);
            this.btnSerializeSql.TabIndex = 10;
            this.btnSerializeSql.Text = "SQL";
            this.btnSerializeSql.UseVisualStyleBackColor = true;
            this.btnSerializeSql.Click += new System.EventHandler(this.btnSerializeSql_Click);
            // 
            // btnDeserializeSql
            // 
            this.btnDeserializeSql.Location = new System.Drawing.Point(6, 25);
            this.btnDeserializeSql.Name = "btnDeserializeSql";
            this.btnDeserializeSql.Size = new System.Drawing.Size(139, 33);
            this.btnDeserializeSql.TabIndex = 11;
            this.btnDeserializeSql.Text = "SQL";
            this.btnDeserializeSql.UseVisualStyleBackColor = true;
            this.btnDeserializeSql.Click += new System.EventHandler(this.btnDeserializeSql_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 503);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnDeserializeJson;
        private System.Windows.Forms.Button btnSerializeJson;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSerializeStructArray;
        private System.Windows.Forms.Button btnSerializeProtoBuf;
        private System.Windows.Forms.Button btnSerializeBinaryFormatter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeserializeStructArray;
        private System.Windows.Forms.Button btnDeserializeProtoBuf;
        private System.Windows.Forms.Button btnDeserializeBinaryFormatter;
        private System.Windows.Forms.Button btnSerializeSql;
        private System.Windows.Forms.Button btnDeserializeSql;
    }
}

