using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_5
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                Application.Run(new Window2(fileName));
            }
        }
    }

    public class Window : Form
    {
        private string fileName;
        protected DataGridView dataGridView = new DataGridView();
        
        public Window(string fileName)
        {
            this.fileName = fileName;

            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
            dataGridViewColumn.Name = "Col 1";
            
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            Load += fLoad;

            Controls.Add(dataGridView);
        }

        private void fLoad(object sender, EventArgs a)
        {
            Console.WriteLine("Load");
            ReadFile(fileName);
        }

        public void ReadFile(string path)
        {
            string str;
            string[] fields;
            bool firsttime = true;
            
            using (StreamReader fileStream = new StreamReader(path))
            {
                while ((str = fileStream.ReadLine()) != null)
                {
                    fields = str.Split(';');

                    if (fields.Length <= 0)
                    {
                        continue;
                    }
                    
                    if (firsttime)
                    {
                        dataGridView.ColumnCount = fields.Length;
                        
                        for (int i = 0; i < fields.Length; i++)
                        {
                            dataGridView.Columns[i].Name = String.Format("Pole{0}", i + 1);
                        }
                    
                        firsttime = false;
                    }

                    dataGridView.Rows.Add(fields);
                }
            }
        }
    }

    public class Window2 : Window
    {
        private ToolStrip toolStrip = new ToolStrip();
        private StatusStrip statStrip;

        public Window2(string fileName) : base(fileName)
        {
            statStrip = new StatusStrip();

            ToolStripStatusLabel statLabel = new ToolStripStatusLabel();
            statLabel.Text = "Вікно готово";
            statStrip.Items.Add(statLabel);
            Controls.Add(statStrip);

            toolStrip.Size = new Size((int)(200 / 3), (int)(40));
            
            ToolStripButton tlbExit = new ToolStripButton("Exit");
            tlbExit.ToolTipText = "Закрити вікно таблиці";
            
            ToolStripButton tIns = new ToolStripButton("Insert");
            tIns.ToolTipText = "Додати запис";
            
            ToolStripButton tEdit = new ToolStripButton("Edit");
            tEdit.ToolTipText = "Редагувати запис";
            
            ToolStripButton tDel = new ToolStripButton("Delete");
            tDel.ToolTipText = "Видалити запис";
            
            ToolStripButton tSave = new ToolStripButton("Save");
            tSave.ToolTipText = "Зберегти зміни";
            
            ToolStripButton tExp = new ToolStripButton("Export");
            tExp.ToolTipText = "Экспортувати таблицю";
            
            Padding = new Padding(2);
            toolStrip.Items.AddRange(new ToolStripButton[] { 
                tIns,
                tEdit,
                tDel,
                tSave,
                tExp,
                tlbExit
            });
            
            toolStrip.ItemClicked += new ToolStripItemClickedEventHandler(ToolStripButtonClick);
            toolStrip.Dock = DockStyle.Top;
            Controls.Add(toolStrip);
            Load += fLoad;
        }
        private void fLoad(object sender, EventArgs args)
        {
            statStrip.Items[0].Text = string.Format("{0} records has been red", dataGridView.Rows.Count);
        }

        private void ToolStripButtonClick(object sender, ToolStripItemClickedEventArgs args)
        {
            string buttonText = args.ClickedItem.Text;
            statStrip.Items[0].Text = string.Format("You've pressed {0} button", buttonText);

            if (buttonText == "Exit")
            {
                Close();
            }
            else if (buttonText == "Open")
            {
                ;
            }
            else
            {
                MessageBox.Show("Action not ready!", "Warning");
            }
        }
    }
}
