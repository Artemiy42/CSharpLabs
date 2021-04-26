using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
            AllocConsole();
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
                        ReturnWindow returnWindow = new ReturnWindow(mainWindow.ReturnValue);
                        DialogResult res = returnWindow.ShowDialog();
                        break;
                    case DialogResult.Cancel:
                        Console.WriteLine("Canceled!");
                        break;
                    default:
                        Console.WriteLine("Default result");
                        break;
                }
            }

            Console.WriteLine("End");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }

    public struct ProductInfo
    {
        public int id;
        public string name;
        public int cost;
        
        public ProductInfo(int id, string name, int cost)
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
        }

        public string[] ToListStrings()
        {
            string[] strs = new string[3];

            strs[0] = id.ToString();
            strs[1] = name;
            strs[2] = cost.ToString();

            return strs;
        }

        public override string ToString()
        {
            return $"{id} {name} {cost}";
        }
    }

    public class ReturnWindow : Form
    {
        private DataGridView dataGridView;

        public ReturnWindow(ProductInfo productInfo)
        {
            Text = "Вибраний рядок";
            Padding = new Padding(10, 10, 10, 10);
            Size = new Size(300, 150);

            dataGridView = new DataGridView();
            dataGridView.Name = "Col 2";
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(dataGridView);

            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "Код";
            dataGridView.Columns[1].Name = "Назва";
            dataGridView.Columns[2].Name = "Ціна";
            dataGridView.Rows.Add(productInfo.ToListStrings());
            Console.WriteLine(productInfo.ToString());

            DialogResult = DialogResult.Cancel;
            StartPosition = FormStartPosition.CenterScreen;
        }
    }

    public class MainWindow : Form
    {
        public ProductInfo ReturnValue
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
            Text = "Пошук у таблиці";
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
            dataGridView.Rows[0].Selected = true;
            dataGridView.CellClick += DataDricView_OnCellClick;

            okButton.Click += _KeyDown;
        }

        private void DataDricView_OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView.SelectedCells[0].OwningRow.Index;
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex].Selected = true;
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(110, 30);
            okButton.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            okButton.Location = new Point(20, 420);
            okButton.Text = "Ок";
            okButton.DialogResult = DialogResult.OK;
            Controls.Add(okButton);
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(110, 30);
            cancleButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            cancleButton.Location = new Point(360, 420);
            cancleButton.Text = "Відміна";
            cancleButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancleButton);
        }

        public void CreateSearchBox()
        {
            searchBox = new TextBox();
            searchBox.Size = new Size(110, 30);
            searchBox.Anchor = AnchorStyles.Top;
            searchBox.Location = new Point(20, 20);
            searchBox.Text = "Шукати тут";
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
            if (dataGridView.RowCount > 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView.Rows[dataGridView.CurrentRow.Index];
                DataGridViewCellCollection row = dataGridViewRow.Cells;

                ReturnValue = new ProductInfo(int.Parse((string) row[0].Value),
                    (string) row[1].Value,
                    int.Parse((string) row[2].Value));
            }
            else
            {
                Console.WriteLine("Таблиця пуста!");
            }
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
