using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer

        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        decimal[,] vertices;
        decimal[,] screenPoints;
        decimal[,] ctrans = new decimal[4, 4];  //your main transformation matrix
        private ImageList tbimages;
        private ToolBar toolBar1;
        private ToolBarButton transleftbtn;
        private ToolBarButton transrightbtn;
        private ToolBarButton transupbtn;
        private ToolBarButton transdownbtn;
        private ToolBarButton toolBarButton1;
        private ToolBarButton scaleupbtn;
        private ToolBarButton scaledownbtn;
        private ToolBarButton toolBarButton2;
        private ToolBarButton rotxby1btn;
        private ToolBarButton rotyby1btn;
        private ToolBarButton rotzby1btn;
        private ToolBarButton toolBarButton3;
        private ToolBarButton rotxbtn;
        private ToolBarButton rotybtn;
        private ToolBarButton rotzbtn;
        private ToolBarButton toolBarButton4;
        private ToolBarButton shearrightbtn;
        private ToolBarButton shearleftbtn;
        private ToolBarButton toolBarButton5;
        private ToolBarButton resetbtn;
        private ToolBarButton exitbtn;
        private Timer rotateTimer;
        int[,] lines;
        decimal[] returnVals;
        decimal[] toZeroArray;
        decimal[,] rotateXMatrix = new decimal[,]  {
                {1,0,0,0 },
                {0,Convert.ToDecimal(Math.Cos(0.5)), Convert.ToDecimal(-Math.Sin(0.5)) ,0 },
                {0,Convert.ToDecimal(Math.Sin(0.5)), Convert.ToDecimal(Math.Cos(0.5)),0 },
                {0,0,0,1 },
            };

        decimal[,] rotateYMatrix = new decimal[,] {
                {Convert.ToDecimal(Math.Cos(0.05)), 0, Convert.ToDecimal(Math.Sin(0.05)),0 },
                {0,1,0,0 },
                {Convert.ToDecimal(-Math.Sin(0.05)), 0, Convert.ToDecimal(Math.Cos(0.05)),0 },
                {0,0,0,1 },
            };

        decimal[,] rotateZMatrix = new decimal[,] {
                {Convert.ToDecimal( Math.Cos(0.05)),Convert.ToDecimal(-Math.Sin(0.05)),0,0 },
                {Convert.ToDecimal(Math.Sin(0.05)),Convert.ToDecimal(Math.Cos(0.05)),0,0 },
                {0,0,1,0 },
                {0,0,0,1 },
            };

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (200830) (Your Name Here)";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));

            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));

            MenuItem miDash = new MenuItem("-");

            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });

            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));

            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            decimal temp;
            int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans
                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        screenPoints[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen,
                        (int)screenPoints[lines[i, 0], 0],
                        (int)screenPoints[lines[i, 0], 1],
                        (int)screenPoints[lines[i, 1], 0],
                        (int)screenPoints[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage()
        {
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }

            setIdentity(ref ctrans);  //initialize transformation matrix to identity

            Translate(-vertices[0, 0], -vertices[0, 1], -vertices[0, 2]);

            decimal scaleFactor = Height / 2 / shapeHeight(vertices);

            decimal centerX = Width / 2;
            decimal centerY = Height / 2;

            Scale(scaleFactor);
            Reflect(ref ctrans, 1, -1);

            Translate(centerX, centerY);

            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            vertices = new decimal[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = decimal.Parse(text[0]);
                if (vertices[numpts, 0] < 0) break;
                vertices[numpts, 1] = decimal.Parse(text[1]);
                vertices[numpts, 2] = decimal.Parse(text[2]);
                vertices[numpts, 3] = 1;
                numpts++;
            }
            screenPoints = (decimal[,])vertices.Clone();
            Debug.WriteLine(screenPoints.GetLength(0));
            Debug.WriteLine(screenPoints.GetLength(1));

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(ref decimal[,] m)
        {
            Debug.WriteLine(Width + " " + Height);
            m = new decimal[,]
             {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };
        }// end of setIdentity


        private void Transformer_Load(object sender, System.EventArgs e)
        {
        }

        public void Reflect(ref decimal[,] m, decimal x = 1, decimal y = 1, decimal z = 1)
        {
            decimal[,] reflect = new decimal[,] {
                { x, 0, 0, 0},
                { 0, y, 0, 0},
                { 0, 0, z, 0},
                { 0, 0, 0, 1},
            };

            multiplyMatrices(ref m, reflect);
        }

        public decimal shapeHeight(decimal[,] points)
        {
            decimal low = vertices[0, 1];
            decimal high = low;
            int rows = points.GetLength(0);

            for (int y = 0; y < rows; ++y)
            {
                if (points[y, 1] < low)
                    low = points[y, 1];
                else if (points[y, 1] > high)
                    high = points[y, 1];
            }
            return high - low;
        }

        public void Translate(decimal x = 0, decimal y = 0, decimal z = 0)
        {
            decimal[,] translateMatrix =
            {
                {1,0,0,0 },
                {0,1,0,0 },
                {0,0,1,0 },
                {x,y,z,1 },
            };

            printMatrix(translateMatrix);

            multiplyMatrices(ref ctrans, translateMatrix);
        }

        public void Scale(decimal scaleAmount)
        {
            // moveToZero();
            decimal[,] scaleMatrix =
            {
                {scaleAmount,0,0,0 },
                {0,scaleAmount,0,0 },
                {0,0,scaleAmount,0 },
                {0,0,0,1 },
            };
            multiplyMatrices(ref ctrans, scaleMatrix);
            // moveBack();
        }

        public void moveToZero()
        {
            Console.WriteLine(screenPoints[0, 0]);
            returnVals = new decimal[] { screenPoints[0, 0], screenPoints[0, 1], screenPoints[0, 2] };
            Console.WriteLine("ZERO : " + -returnVals[0] + " " + -returnVals[1] + " " + -returnVals[2]);
            Translate(-returnVals[0], -returnVals[1], -returnVals[2]);
        }

        public void moveBack()
        {
            Console.WriteLine("BACK : " + returnVals[0] + " " + returnVals[1] + " " + returnVals[2]);
            Translate(returnVals[0], returnVals[1], returnVals[2]);
        }
        
        // MARK:: INTEGRATE WITH OTHER MULTIPLICATION
        private decimal[,] multMatrices(ref decimal[,] temparr)
        {
            decimal value = 0;
            decimal[,] c = new decimal[4, 4];

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    value = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        value += ctrans[row, k] * temparr[k, col];
                    }
                    c[row, col] = value;
                }
            }
            return c;
        }

        public void RotateX(object sender, EventArgs e)
        {
            RotateX();

            Refresh();
        }

        public void RotateX()
        {
            /*
            Console.WriteLine("SCREEN POINTS BEFORE : ");
            printMatrix(screenPoints);
            moveToZero();
            multiplyMatrices(ref ctrans, rotateXMatrix);
            moveBack();
            Refresh();
            Console.WriteLine("SCREEN POINTS AFTER : ");
            printMatrix(screenPoints); */
            moveToZero();
          
            ctrans = multMatrices(ref rotateXMatrix);

            moveBack();
        }

        public void RotateY()
        {
            moveToZero();
            multiplyMatrices(ref ctrans, rotateYMatrix);
            moveBack();
        }

        public void RotateZ()
        {
            moveToZero();
            multiplyMatrices(ref ctrans, rotateZMatrix);
            moveBack();
        }

        public void Shear(decimal factor = 0)
        {
            moveToZero();
            decimal[,] shearMatrix =
            {
                {1,0,0,0 },
                {factor,1,0,0 },
                {0,0,1,0 },
                {0,0,0,1 },
            };
            multiplyMatrices(ref ctrans, shearMatrix);
            moveBack();

        }
        public void Reset()
        {
            setIdentity(ref ctrans);
            Refresh();
        }

        /// <summary>
        /// multiplies 2 matrices, and puts values into matrix 1
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        public void multiplyMatrices(ref decimal[,] m1, decimal[,] m2)
        {
            decimal temp;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp = 0;
                    for (int k = 0; k < 4; k++)
                        temp += m1[i, k] * m2[k, j];
                    m1[i, j] = temp;
                }
            }

            /*
            decimal value = 0;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    value = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        value += ctrans[row, k] * m2[k, col];
                    }
                    m1[row, col] = value;
                }
            } */

        }

        public void printMatrix(decimal[,] m)
        {
            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = 0; j < m.GetLength(1); ++j)
                {
                    Console.Write(m[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (rotateTimer != null)
            {
                Console.WriteLine("STOPZ");
                rotateTimer.Stop();
            }
            if (e.Button == transleftbtn)
            {
                Translate(-75);
                Refresh();
            }
            else if (e.Button == transrightbtn)
            {
                Translate(75, 0, 0);
                Refresh();
            }
            else if (e.Button == transupbtn)
            {
                Translate(0, -35, 0);
                Refresh();
            }

            else if (e.Button == transdownbtn)
            {
                Translate(0, 35, 0);
                Refresh();
            }
            else if (e.Button == scaleupbtn)
            {
                moveToZero();
                decimal s = 1.1M;
                Scale(s);
                moveBack();
                Refresh();
            }
            else if (e.Button == scaledownbtn)
            {
                moveToZero();
                decimal s = 0.9M;
                Scale(s);
                moveBack();
                Refresh();
            }
            else if (e.Button == rotxby1btn)
            {
                Console.WriteLine();
                // printMatrix(ctrans);
                // printMatrix(screenPoints);
                Console.WriteLine();
                RotateX();
                // printMatrix(ctrans);
                // printMatrix(screenPoints);
                Refresh();
            }
            else if (e.Button == rotyby1btn)
            {
                RotateY();
                Refresh();
            }
            else if (e.Button == rotzby1btn)
            {
                RotateZ();
                Refresh();
            }

            else if (e.Button == rotxbtn)
            {
                // returnVals = new decimal[] { screenPoints[0, 0], screenPoints[0, 1], screenPoints[0, 2] };
                // Translate(-returnVals[0], -returnVals[1], -returnVals[2]);
                // Refresh();
                rotateTimer = new Timer();
                rotateTimer.Tick += new EventHandler(RotateX);
                rotateTimer.Interval = 2; // in miliseconds
                rotateTimer.Start();
            }
            else if (e.Button == rotybtn)
            {
                RotateY();
                Refresh();
            }

            else if (e.Button == rotzbtn)
            {
                RotateZ();
                Refresh();
            }

            else if (e.Button == shearleftbtn)
            {
                decimal s = -.1M;
                Shear(s);
                Refresh();
            }

            else if (e.Button == shearrightbtn)
            {
                decimal s = .1M;
                Shear(s);
                Refresh();
            }

            else if (e.Button == resetbtn)
            {
                Reset();
                RestoreInitialImage();
            }
            else if (e.Button == exitbtn)
            {
                Close();
            }
        }

    }
}
