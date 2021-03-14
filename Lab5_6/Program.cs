using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Lab5_6
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Encoding encoding = Encoding.UTF8;
            Dictionary<string, Encoding> encodingPairs = new Dictionary<string, Encoding>
            {
                { "866", Encoding.GetEncoding(866) },
                { "1251", Encoding.GetEncoding(1251) },
                { "utf8", Encoding.UTF8 },
                { "utf16", Encoding.GetEncoding(1200) }
            };
            string encodingName = "";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine(
                        "Програма для редагування вмісту таблиці." +
                        "a.exe [-? | -help] [-enc Encoding]" +
                        "де\n" +
                        "-? | -help    : отримання цієї справки\n" +
                        "-enc Encoding : вказати кодування файлу (866, 1251, UTF8, UTF16)\n");

                    return;
                }
                else if (args[i].ToLower() == "-enc")
                {
                    encodingName = setString(++i, args, "кодування");
                }
            }

            if (encodingPairs.ContainsKey(encodingName))
            {
                encoding = encodingPairs[encodingName];
            }
            else
            {
                Console.WriteLine("Введене кодування не підтримується, запустіть програму з ключем '-?' !");
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                Application.Run(new Window3(fileName, encoding));
            }
        }

        public static string setString(int i, string[] args, string par)
        {
            string ret = string.Empty;

            if (i < args.Length)
            {
                ret = args[i];
            }
            else
            {
                Console.WriteLine($"Значення для параметра {0} не задано!", par);
                Environment.Exit(2);
            }

            return ret;
        }
    }

    public class Field
    {
        public string name;
        public string def;
        public string value;

        public Field(string name, string def)
        {
            this.name = name;
            this.def = def;
            value = string.Empty;
        }
    }

    public class WindowFields : OkCancel
    {
        Dictionary<TextBox, Field> dictionaryFields = null;

        public WindowFields(string Title, Field[] fields) : base(Title)
        {
            dictionaryFields = new Dictionary<TextBox, Field>(); // создать список пар
            
            for (int i = fields.Length - 1; i >= 0; i--)
            {
                dictionaryFields.Add(addFld(i, fields[i]), fields[i]);            
            }

            okButton.Click += _KeyDown;
        }
        public TextBox addFld(int n, Field par) // метод добавляет в окно новое поле ввода
        {
            int h = 32;

            Panel p = new Panel(); // панель содержит метку и поле ввода
            p.Name = string.Format("p{0}", n);
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Size = new Size(1, h);
            p.AutoSize = true;
            p.Dock = DockStyle.Top;
            p.Padding = new Padding(0, 0, 0, 8);

            Panel p1 = new Panel(); // панель для высоты
            p1.BorderStyle = BorderStyle.FixedSingle;
            p1.Size = new Size(1, h);
            p1.Dock = DockStyle.Top; /// <--------
            p1.BackColor = Color.Blue;
            p.Controls.Add(p1);

            Label l1;
            l1 = new Label();
            l1.Name = string.Format("l{0}", n);
            l1.Size = new Size(172, h); ;
            l1.Text = par.name; // создать новую строку
            l1.Dock = DockStyle.Right;
            // Console.Error.WriteLine ("new field: ’{0}’/’{1}’/’{2}’"
            // , par.name, l1.Text, par.value);
            l1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            p.Controls.Add(l1);

            TextBox t1;
            t1 = new TextBox();
            t1.TabIndex = n;
            t1.Name = string.Format("t{0}", n); ;
            t1.Size = new Size(162, h);
            t1.Text = par.def;
            t1.Dock = DockStyle.Right;
            p.Controls.Add(t1);

            Controls.Add(p); /// готовое поле ввода

            return t1;
        }
        void _KeyDown(object sender, EventArgs e)
        {
            TextBox tb;
            Field f;
            foreach (KeyValuePair<TextBox, Field> Item in dictionaryFields)
            {
                tb = Item.Key;
                f = Item.Value;
                f.value = tb.Text;
                tb.Text = f.def; /// а теперь восстанавливается значение по умолчанию.
                Console.Error.WriteLine("keyDown: ’{0}’:’{1}’ ", f.name, f.value);
            }
        }

    }

    public class OkCancel : Form
    {
        protected Button okButton;
        protected Button cancleButton;

        public OkCancel(string text)
        {
            Text = text;
            int p = 10;
            Padding = new Padding(p, p, p, p);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            AutoScroll = false;
            AutoSize = true;
            Size = new Size(10, 10);

            Panel p0 = new Panel(); // эта панель задает высоту строки
            p0.Size = new Size(1, 32);
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Green;
            Controls.Add(p0);

            CreateOkButton();

            p0.Size = new Size(112, 32);
            p0.Dock = DockStyle.Right;
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Red;
            Controls.Add(p0);

            CreateCancleButton();

            DialogResult = DialogResult.Cancel;
            AcceptButton = okButton; // нажатие клавиши Enter как на ок
            CancelButton = cancleButton; // нажатие клавиши Esc КАК НА Cancel
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(112, 32);
            okButton.Dock = DockStyle.Right; // вправо
            okButton.Text = "Ok";
            okButton.DialogResult = DialogResult.OK;
            Controls.Add(okButton);
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(112, 32);
            cancleButton.Dock = DockStyle.Right;
            cancleButton.Text = "Cancel";
            cancleButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancleButton);
        }
    }

    public class Window : Form
    {
        protected Encoding encoding;
        protected string fileName;
        protected DataGridView dataGridView = new DataGridView();

        public Window(string fileName, Encoding encoding)
        {
            this.fileName = fileName;
            this.encoding = encoding;

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

            using (StreamReader fileStream = new StreamReader(path, encoding))
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
        protected ToolStrip toolStrip = new ToolStrip();
        protected StatusStrip statStrip;

        public Window2(string fileName, Encoding encoding) : base(fileName, encoding)
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

            toolStrip.ItemClicked += ToolStripButtonClick;
            toolStrip.Dock = DockStyle.Top;
            Controls.Add(toolStrip);
            Load += fLoad;
        }
        protected void fLoad(object sender, EventArgs args)
        {
            statStrip.Items[0].Text = string.Format("{0} records has been red", dataGridView.Rows.Count);
        }
        protected void ToolStripButtonClick(object sender, ToolStripItemClickedEventArgs args)
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

    public class Window3 : Window2
    {
        public Window3(string fnm, Encoding encoding) : base(fnm, encoding)
        {
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dataGridView.RowHeadersVisible = false;
            toolStrip.ItemClicked -= base.ToolStripButtonClick;
            toolStrip.ItemClicked += ToolBarButtonClick3;
            dataGridView.CellDoubleClick += dc2;
            dataGridView.PreviewKeyDown += _PreviewKeyDown;
            dataGridView.KeyDown += _KeyDown;
        }

        protected void _PreviewKeyDown(object sender, PreviewKeyDownEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                Console.WriteLine("CellDoubleClick: selrow / row: {0} ", dataGridView.CurrentRow.Index);
                doEdit();
            }
        }
        
        protected void _KeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                args.SuppressKeyPress = true;
            }
        }

        protected void dc2(object sender, DataGridViewCellEventArgs args)
        {
            Console.WriteLine("CellDoubleClick: selrow / row: {0}/{1} ", dataGridView.CurrentRow.Index, args.RowIndex);

            if (args.RowIndex >= 0)
            {
                doEdit();
                dataGridView.CurrentCell = dataGridView.Rows[args.RowIndex].Cells[0];
            }
        }

        protected void ToolBarButtonClick3(object sender, ToolStripItemClickedEventArgs args)
        {
            string buttonName = args.ClickedItem.Text;

            statStrip.Items[0].Text = string.Format("Ви натиснули {0} кнопку", buttonName);

            if (buttonName == "Exit")
            {
                Close();
            }
            else if (buttonName == "Insert")
            {
                doInsert();
            }
            else if (buttonName == "Edit")
            {
                doEdit();
            }
            else if (buttonName == "Delete")
            {
                doDelete();
            }
            else if (buttonName == "Save")
            {
                doSave();
            }
            else
            {
                base.ToolStripButtonClick(sender, args);
            }
        }

        public void doEdit()
        {
            string answer = "";

            if (dataGridView.RowCount > 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView.Rows[dataGridView.CurrentRow.Index];
                DataGridViewCellCollection row = dataGridViewRow.Cells;

                List<Field> fields = new List<Field>();
                Field field;

                for (int i = 0; i < dataGridView.ColumnCount; i++)
                {
                    field = new Field(dataGridView.Columns[i].Name, (row[i]).Value.ToString());
                    fields.Add(field);
                }

                if (fields.Count > 0)
                {
                    WindowFields w = new WindowFields("Змінити вибраний запис", fields.ToArray());

                    DialogResult rc = w.ShowDialog();

                    if (rc == DialogResult.OK)
                    {
                        for (int i = 0; i < fields.Count; i++)
                        {
                            dataGridViewRow.Cells[i].Value = fields[i].value;
                        }
                    }
                }
            }
            else
            {
                answer = "Нічого змінювати!";
            }
            
            Console.WriteLine("Змінено: '{0}'", answer);
            statStrip.Items[0].Text = answer;
        }

        public void doDelete()
        {
            string answer = "";

            if (dataGridView.RowCount > 0)
            {
                foreach (DataGridViewRow item in dataGridView.SelectedRows)
                {
                    dataGridView.Rows.RemoveAt(item.Index);
                }
            }
            else
            {
                answer = "Нічого видаляти!";
            }

            Console.WriteLine("Видалено: '{0}'", answer);
            statStrip.Items[0].Text = answer;
        }

        public void doSave()
        {
            string ss = csvExport(fileName, ';', dataGridView);
            Console.WriteLine("Зберегти: '{0}'", ss);
            statStrip.Items[0].Text = ss;
        }

        public void doInsert()
        {
            Field field;
            List<Field> fields = new List<Field>();
            
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                field = new Field(dataGridView.Columns[i].Name, "");
                fields.Add(field);
            }

            if (fields.Count > 0)
            {
                Form w = new WindowFields("Введіть новий запис", fields.ToArray());
                DialogResult dialogResult = w.ShowDialog();
                
                if (dialogResult == DialogResult.OK)
                {
                    Console.Error.WriteLine("doIns: кількість полів: '{0}' ", fields.Count);
                    statStrip.Items[0].Text = string.Format("Вставити після {0} запису", dataGridView.CurrentRow.Index);
                    string[] fs = new string[fields.Count];

                    for (int j = 0; j < fields.Count; j++)
                    {
                        fs[j] = fields[j].value;
                    }

                    dataGridView.Rows.Insert(dataGridView.CurrentRow.Index, fs);
                }
            }
            else
            {
                statStrip.Items[1].Text = string.Format("Нічого вставляти!");
            }
        }

        public virtual string csvExport(string nm, char sep_, DataGridView dgvw)
        {
            string rc = "Помилка";
            return rc;
        }
    }
}