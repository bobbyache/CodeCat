namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    partial class PdfDocumentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdfDocumentControl));
            this.pdfControl = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.pdfControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pdfControl
            // 
            this.pdfControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfControl.Enabled = true;
            this.pdfControl.Location = new System.Drawing.Point(0, 0);
            this.pdfControl.Name = "pdfControl";
            this.pdfControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfControl.OcxState")));
            this.pdfControl.Size = new System.Drawing.Size(626, 529);
            this.pdfControl.TabIndex = 12;
            // 
            // PdfDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pdfControl);
            this.Name = "PdfDocumentControl";
            this.Size = new System.Drawing.Size(626, 529);
            this.Controls.SetChildIndex(this.pdfControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pdfControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private AxAcroPDFLib.AxAcroPDF pdfControl;
    }
}
