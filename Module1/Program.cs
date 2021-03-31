using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module1
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                MainWindow mainWindow = new MainWindow(fileName);
                DialogResult rc = mainWindow.ShowDialog();

                switch (rc)
                {
                    case DialogResult.OK:
                        Console.WriteLine(mainWindow.ReturnValue);
                        break;
                    case DialogResult.Cancel:
                        Console.WriteLine("Canceled!");
                        break;
                    default:
                        Console.WriteLine("Default result");
                        break;
                }

                DialogResult res = MessageBox.Show(mainWindow.ReturnValue, "Hide column", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Console.WriteLine("End");
        }
    }

    public class MainWindow : Form
    {
        public string ReturnValue
        {
            get;
            private set;
        }

        protected Button okButton;
        protected Button cancleButton;
        protected TextBox searchBox;
        protected DataGridView dataGridView;

        public MainWindow(string filePath)
        {
            Text = "Search in grid";
            Padding = new Padding(10, 10, 10, 10);
            Size = new Size(500, 500);

            CreateSearchBox();
            CreateDataGrid();
            CreateOkButton();
            CreateCancleButton();
            ReadFile(filePath);

            DialogResult = DialogResult.Cancel;
            AcceptButton = okButton; // нажатие клавиши Enter как на ок
            CancelButton = cancleButton; // нажатие клавиши Esc КАК НА Cancel
            StartPosition = FormStartPosition.CenterScreen;

            okButton.Click += _KeyDown;
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(110, 30);
            okButton.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            okButton.Location = new Point(20, 420);
            okButton.Text = "Ok";
            okButton.DialogResult = DialogResult.OK;
            Controls.Add(okButton);
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(110, 30);
            cancleButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            cancleButton.Location = new Point(360, 420);
            cancleButton.Text = "Cancel";
            cancleButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancleButton);
        }

        public void CreateSearchBox()
        {
            searchBox = new TextBox();
            searchBox.Size = new Size(110, 30);
            searchBox.Anchor = AnchorStyles.Top;
            searchBox.Location = new Point(20, 20);
            searchBox.Text = "Search here";
            searchBox.TextChanged += UpdateActiveCell;
            Controls.Add(searchBox);
        }

        public void CreateDataGrid()
        {
            dataGridView = new DataGridView();
            dataGridView.Name = "Col 1";
            dataGridView.ReadOnly = true;
            dataGridView.Size = new Size(440, 200);
            dataGridView.Location = new Point(20, 100);
            dataGridView.Anchor = AnchorStyles.Top;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(dataGridView);
        }
        private void UpdateActiveCell(object sender, EventArgs e)
        {
            if (Int32.TryParse(searchBox.Text, out int searcingId))
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    if (Int32.TryParse((string) dataGridView[0, i].Value, out int productId))
                    {
                        if (productId == searcingId)
                        {
                            dataGridView.CurrentCell = dataGridView[0, i];
                        }
                    }
                }
            }
        }

        private void _KeyDown(object sender, EventArgs e)
        {
            string answer = "";

            if (dataGridView.RowCount > 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView.Rows[dataGridView.CurrentRow.Index];
                DataGridViewCellCollection row = dataGridViewRow.Cells;

                answer = (string) row[row.Count - 1].Value;
            }
            else
            {
                answer = "Таблиця пуста!";
            }

            ReturnValue = answer;
        }

        public void ReadFile(string path)
        {
            string str;
            string[] fields;
            bool firsttime = true;

            using (StreamReader fileStream = new StreamReader(path, Encoding.UTF8))
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
                            dataGridView.Columns[i].Name = fields[i];
                        }

                        firsttime = false;

                        continue;
                    }

                    dataGridView.Rows.Add(fields);
                    dataGridView.Columns[dataGridView.ColumnCount - 1].Visible = false; 
                }
            }
        }
    }
}
