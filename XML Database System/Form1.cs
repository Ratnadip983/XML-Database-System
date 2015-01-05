using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Microsoft.VisualBasic.PowerPacks;
using System.Xml;
using CreateXML;
using ValidityTest;

namespace XML_Database_System
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            string path = "Databases";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            home();
        }
   

        string pathOfDatabase = @"Databases\";
        string globalDatabaseName = "";
        
        //----------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------Home Display unit--------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------
        Panel tablePanel = new Panel();
        public void home()
        {
            contentPanel.Controls.Clear();
            this.Controls.Remove(homeButton);
            headerLabel.Text = "XML Database System [Home]";
            globalDatabaseName = "";
            globalTableName = "";

            UserControl homeUserControl = new UserControl();
            homeUserControl.Dock = DockStyle.Fill;
            homeUserControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            contentPanel.Controls.Add(homeUserControl);
            shapeContainer(homeUserControl);

            Button createButton = new Button();
            createButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) );
            createButton.Location = new System.Drawing.Point(21, 60);
            createButton.Name = "createButton";
            createButton.Size = new System.Drawing.Size(228, 44);
            createButton.Text = "Create New Database";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += new System.EventHandler(createButton_Click);

            homeUserControl.Controls.Add(createButton);

            Button importButton = new Button();
            importButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            importButton.Location = new System.Drawing.Point(21, 150);
            importButton.Name = "importButton";
            importButton.Size = new System.Drawing.Size(228, 44);
            importButton.Text = "Import Database";
            importButton.UseVisualStyleBackColor = true;
            importButton.Click += new System.EventHandler(importButton_Click);

            homeUserControl.Controls.Add(importButton);

            tablePanel.Controls.Clear();
            tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            tablePanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tablePanel.AutoScroll = true;
            tablePanel.Location = new System.Drawing.Point(262, 2);
            tablePanel.Name = "tablePanel";
            tablePanel.Size = new System.Drawing.Size(this.Width - 277, this.Height - 130);

            homeUserControl.Controls.Add(tablePanel);

            int i = Directory.GetDirectories(pathOfDatabase).Length;

            if (i > 0)
            {

                Label databaseHeaderLabel = new Label();
                databaseHeaderLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                databaseHeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                databaseHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                databaseHeaderLabel.ForeColor = System.Drawing.Color.Black;
                databaseHeaderLabel.Location = new System.Drawing.Point(45, 30);
                databaseHeaderLabel.Name = "databaseHeaderLabel";
                databaseHeaderLabel.Size = new System.Drawing.Size(230, 35);
                databaseHeaderLabel.Text = "Database Name";
                databaseHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(databaseHeaderLabel);


                Label tableNoHeaderLabel = new Label();
                tableNoHeaderLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                tableNoHeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                tableNoHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tableNoHeaderLabel.ForeColor = System.Drawing.Color.Black;
                tableNoHeaderLabel.Location = new System.Drawing.Point(277, 30);
                tableNoHeaderLabel.Name = "databaseHeaderLabel";
                tableNoHeaderLabel.Size = new System.Drawing.Size(100, 35);
                tableNoHeaderLabel.Text = "Tabel(s)";
                tableNoHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(tableNoHeaderLabel);

                Label deleteHeaderLabel = new Label();
                deleteHeaderLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                deleteHeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                deleteHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                deleteHeaderLabel.ForeColor = System.Drawing.Color.Black;
                deleteHeaderLabel.Location = new System.Drawing.Point(379, 30);
                deleteHeaderLabel.Name = "deleteHeaderLabel";
                deleteHeaderLabel.Size = new System.Drawing.Size(120, 35);
                deleteHeaderLabel.Text = "";
                deleteHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(deleteHeaderLabel);

                string[] databaseName = new string[i];
                int p = 0;
                int lastFile = 67;
                foreach (string name in Directory.GetDirectories(pathOfDatabase))
                {
                    databaseName[p] = Path.GetFileName(name);

                    LinkLabel databaseLinklabel = new LinkLabel();
                    databaseLinklabel.Size = new System.Drawing.Size(230, 32);
                    if (p % 2 == 0)
                        databaseLinklabel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        databaseLinklabel.BackColor = System.Drawing.Color.Gainsboro;
                    databaseLinklabel.Text = databaseName[p];
                    databaseLinklabel.TextAlign = ContentAlignment.MiddleLeft;
                    databaseLinklabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    databaseLinklabel.Location = new Point(45, lastFile);
                    databaseLinklabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
                    databaseLinklabel.LinkColor = Color.DarkSlateGray;
                    databaseLinklabel.LinkBehavior = LinkBehavior.HoverUnderline;
                    databaseLinklabel.Name = databaseName[p];
                    databaseLinklabel.Tag = p;
                    databaseLinklabel.LinkClicked += new LinkLabelLinkClickedEventHandler(databaseLinkLabel_Click);
                    tablePanel.Controls.Add(databaseLinklabel);

                    string pathOfTable = @"Databases\" + databaseName[p] + "\\";

                    Label tableNoLabel = new Label();
                    if (p % 2 == 0)
                        tableNoLabel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableNoLabel.BackColor = System.Drawing.Color.Gainsboro;
                    tableNoLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableNoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableNoLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
                    tableNoLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableNoLabel.Location = new System.Drawing.Point(277, lastFile);
                    tableNoLabel.Size = new System.Drawing.Size(100, 32);
                    tableNoLabel.Text = Directory.GetFiles(pathOfTable, "*_schema.xml").Length.ToString();
                    tableNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tablePanel.Controls.Add(tableNoLabel);

                    LinkLabel deleteLinkLabel = new LinkLabel();
                    if (p % 2 == 0)
                        deleteLinkLabel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        deleteLinkLabel.BackColor = System.Drawing.Color.Gainsboro;
                    deleteLinkLabel.Text = "[Delete]";
                    deleteLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
                    deleteLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    deleteLinkLabel.Size = new System.Drawing.Size(120, 32);
                    deleteLinkLabel.Location = new Point(379, lastFile);
                    deleteLinkLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    deleteLinkLabel.LinkColor = Color.DarkSlateGray;
                    deleteLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                    deleteLinkLabel.Name = databaseName[p];
                    deleteLinkLabel.Tag = p;
                    deleteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(deleteLinkLabel_Click);
                    tablePanel.Controls.Add(deleteLinkLabel);

                    lastFile += 34;
                    p++;

                }

                Panel BottomPanel = new Panel();
                BottomPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                BottomPanel.Size = new System.Drawing.Size(455, 35);
                BottomPanel.Location = new System.Drawing.Point(45, lastFile);
                tablePanel.Controls.Add(BottomPanel);
            }
            else
            {

                Label emptyDatabaseLabel = new Label();
                emptyDatabaseLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                emptyDatabaseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                emptyDatabaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                emptyDatabaseLabel.ForeColor = System.Drawing.Color.Black;
                emptyDatabaseLabel.Location = new System.Drawing.Point(45, 30);
                emptyDatabaseLabel.Name = "emptyDatabaseLabel";
                emptyDatabaseLabel.Size = new System.Drawing.Size(600, 35);
                emptyDatabaseLabel.Text = "No database exist in the system. Create new database.";
                emptyDatabaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(emptyDatabaseLabel);
            }
 
        }

        public void deleteLinkLabel_Click(object sender, EventArgs e)
        {
            
            LinkLabel ClickedLinkLabel = sender as LinkLabel;
            string databaseName = ClickedLinkLabel.Name;
            DialogResult dr = MessageBox.Show("Database["+databaseName+"] will no longer exist. Do you want to continue?","Database Deletion",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string path = @"Databases\" + databaseName;
                Directory.Delete(path, true);
                home();
            }
        }

        public void databaseLinkLabel_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lb = sender as LinkLabel;
            globalDatabaseName = lb.Name;
            tableView();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Title = "Import File";
            ofd.Filter = "NSTU database File(.ndbm)|*.ndbm|All Files(*.*)|*.*";

            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                
                string path = ofd.FileName;
                string name = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);

                if (extension == ".ndbm")
                {
                    if (!Directory.Exists(pathOfDatabase + "\\" + name))
                    {
                        try
                        {
                            ZipFile.ExtractToDirectory(path, pathOfDatabase + "\\" + name + "\\");
                            home();
                        }
                        catch
                        {
                            MessageBox.Show("Data of the seleceted file has been corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database with same name already exist.");
                    }
                }
                else
                {
                    MessageBox.Show("  Invalid File Format        ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
        private void createButton_Click(object sender, EventArgs e)
        {
            databaseCreation();
        }

        //----------------------------------------------------------------------------------------------------------------------
        //------------------------------------------Database creation Unit-------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------
        TextBox databaseNameTextBox;
        public void databaseCreation()
        {
            this.Controls.Add(homeButton);
            contentPanel.Controls.Clear();
            headerLabel.Text = "Create New Database";

            UserControl dbCreationUserControl = new UserControl();
            dbCreationUserControl.Dock = DockStyle.Fill;
            dbCreationUserControl.AutoScroll = true;
            dbCreationUserControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            contentPanel.Controls.Add(dbCreationUserControl);

            Panel databaseCreationPanel = new Panel();
            databaseCreationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            databaseCreationPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            databaseCreationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            databaseCreationPanel.Location = new System.Drawing.Point(270,200);
            databaseCreationPanel.Name = "databaseCreationPanel";
            databaseCreationPanel.Size = new System.Drawing.Size(700,150);
            dbCreationUserControl.Controls.Add(databaseCreationPanel);

            Label databaseNameLabel = new Label();
            //databaseNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            databaseNameLabel.AutoSize = true;
            databaseNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            databaseNameLabel.Location = new System.Drawing.Point(15,50);
            databaseNameLabel.Name = "databaseNameLabel";
            databaseNameLabel.Size = new System.Drawing.Size(159, 25);
            databaseNameLabel.Text = "Database Name:";
            databaseCreationPanel.Controls.Add(databaseNameLabel);

            databaseNameTextBox = new TextBox();
           // databaseNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            databaseNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            databaseNameTextBox.Location = new System.Drawing.Point(175, 50);
            databaseNameTextBox.Name = "databaseNameTextBox";
            databaseNameTextBox.Text = "";
            databaseNameTextBox.Size = new System.Drawing.Size(450, 25);
            databaseCreationPanel.Controls.Add(databaseNameTextBox);

            Button createdbButton = new Button();
            createdbButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            createdbButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            createdbButton.Location = new System.Drawing.Point(510, 90);
            createdbButton.Name = "createdbButton";
            createdbButton.Size = new System.Drawing.Size(115, 35);
            createdbButton.Text = "Create";
            createdbButton.UseVisualStyleBackColor =true;
            createdbButton.Click += new System.EventHandler(createdbButton_Click);
            databaseCreationPanel.Controls.Add(createdbButton);

        }

        public void createdbButton_Click(object sender, EventArgs e)
        {
            string databaseName =databaseNameTextBox.Text.Trim();

            if (databaseName != "")
            {
                if (ExpressionTest.NameString(databaseName))
                {
                    string path = @"Databases\" + databaseName;

                    if (Directory.Exists(path))
                    {
                        MessageBox.Show("Database with same name already exists.");
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        globalDatabaseName = databaseNameTextBox.Text;
                        tableView();
                    }
                }
                else
                {
                    MessageBox.Show("Database name inavlid.Name can not start with a number or none string data.");
                }
            }
            else
            {
                MessageBox.Show("Please, Give database name to create a new database.");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------table Display unit------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------
        int i;
        string[] tableName;
        CheckBox[] tCheckBox;
        TextBox tableNameTextBox;
        TextBox tableColumnTextBox;
        public void tableView()
        {
            this.Controls.Add(homeButton);
            contentPanel.Controls.Clear();
            headerLabel.Text = globalDatabaseName+"[Database]";
            globalTableName = "";

            UserControl tableViewUserControl = new UserControl();
            
            tablePanel.Controls.Clear();
            tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            tablePanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //tablePanel.AutoSize = true;
            tablePanel.AutoScroll = true;
            tablePanel.Location = new System.Drawing.Point(262, 2);
            tablePanel.Name = "tablePanel";
            tablePanel.Size = new System.Drawing.Size(this.Width-277,this.Height-130);
            tableViewUserControl.Controls.Add(tablePanel);

            tableViewUserControl.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            tableViewUserControl.Dock = DockStyle.Fill;
           // tableViewUserControl.Padding = new System.Windows.Forms.Padding(0,0,2,2);
            contentPanel.Controls.Add(tableViewUserControl);
            shapeContainer(tableViewUserControl);
            databaseComponent(tableViewUserControl);

            int lastPosition = 47;
            if (i > 0)
            {
                Label CheckboxLabel = new Label();
                CheckboxLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                CheckboxLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                CheckboxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CheckboxLabel.ForeColor = System.Drawing.Color.Black;
                CheckboxLabel.Location = new System.Drawing.Point(5, 15);
                CheckboxLabel.Name = "eCheckboxLabel";
                CheckboxLabel.Size = new System.Drawing.Size(38, 30);
                CheckboxLabel.Text = "";
                CheckboxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(CheckboxLabel);


                Label NameLabel = new Label();
                NameLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                NameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NameLabel.ForeColor = System.Drawing.Color.Black;
                NameLabel.Location = new System.Drawing.Point(45, 15);
                NameLabel.Name = "NameLabel";
                NameLabel.Size = new System.Drawing.Size(230, 30);
                NameLabel.Text = "Table Name";
                NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(NameLabel);

                Label RecordsLabel = new Label();
                RecordsLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                RecordsLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                RecordsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                RecordsLabel.ForeColor = System.Drawing.Color.Black;
                RecordsLabel.Location = new System.Drawing.Point(277, 15);
                RecordsLabel.Name = "recordsLabel";
                RecordsLabel.Size = new System.Drawing.Size(130, 30);
                RecordsLabel.Text = "Record(s)";
                RecordsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(RecordsLabel);

                Label sizeLabel = new Label();
                sizeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                sizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                sizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sizeLabel.ForeColor = System.Drawing.Color.Black;
                sizeLabel.Location = new System.Drawing.Point(409, 15);
                sizeLabel.Name = "sizeLabel";
                sizeLabel.Size = new System.Drawing.Size(200, 30);
                sizeLabel.Text = "Size(bytes)";
                sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(sizeLabel);

                Label createLabel = new Label();
                createLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                createLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                createLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                createLabel.ForeColor = System.Drawing.Color.Black;
                createLabel.Location = new System.Drawing.Point(611, 15);
                createLabel.Name = "createLabel";
                createLabel.Size = new System.Drawing.Size(190, 30);
                createLabel.Text = "Create date";
                createLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(createLabel);

                Label updateLabel = new Label();
                updateLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                updateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                updateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                updateLabel.ForeColor = System.Drawing.Color.Black;
                updateLabel.Location = new System.Drawing.Point(803, 15);
                updateLabel.Name = "updateLabel";
                updateLabel.Size = new System.Drawing.Size(190, 30);
                updateLabel.Text = "Last Update";
                updateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(updateLabel);

                Label[] tableNameLabel = new Label[i];
                Label[] tableRecordsLabel = new Label[i];
                Label[] tableSizeLabel = new Label[i];
                Label[] tableCreateDateLabel = new Label[i];
                Label[] tableUpdateLabel = new Label[i];
                tCheckBox = new CheckBox[i];

                
                for (int k = 0; k < i; k++)
                {

                    tCheckBox[k] = new CheckBox();
                    tCheckBox[k].Location = new System.Drawing.Point(13, 6);
                    tCheckBox[k].Name = "eCheckBox" + k.ToString();
                    tCheckBox[k].Size = new System.Drawing.Size(13, 13);
                    tCheckBox[k].Text = "";
                    tCheckBox[k].Tag = k;
                    tCheckBox[k].UseVisualStyleBackColor = true;

                    Panel checkboxPanel = new Panel();
                    if (k % 2 == 0)
                        checkboxPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        checkboxPanel.BackColor = System.Drawing.Color.Gainsboro;
                    checkboxPanel.Location = new System.Drawing.Point(5, lastPosition);
                    checkboxPanel.Size = new System.Drawing.Size(38, 25);
                    checkboxPanel.Controls.Add(tCheckBox[k]);
                    tablePanel.Controls.Add(checkboxPanel);


                    tableNameLabel[k] = new Label();
                    if (k % 2 == 0)
                        tableNameLabel[k].BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableNameLabel[k].BackColor = System.Drawing.Color.Gainsboro;
                    tableNameLabel[k].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableNameLabel[k].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableNameLabel[k].ForeColor = System.Drawing.Color.Black;
                    tableNameLabel[k].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableNameLabel[k].Location = new System.Drawing.Point(45, lastPosition);
                    tableNameLabel[k].Size = new System.Drawing.Size(230, 25);
                    tableNameLabel[k].Text = tableName[k];
                    tableNameLabel[k].Name = tableName[k];
                    tableNameLabel[k].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    tablePanel.Controls.Add(tableNameLabel[k]);


                    string tablePath = Application.StartupPath + "//Databases//" + globalDatabaseName + "//" + tableName[k] + ".xml";
                    string tableStructurePath = Application.StartupPath + "//Databases//" + globalDatabaseName + "//" + tableName[k] + "_schema.xml";
                    XmlDocument xmltable = new XmlDocument();
                    xmltable.Load(tablePath);
                    XmlNode currentNode = xmltable.SelectSingleNode("//@row");
                    tableRecordsLabel[k] = new Label();
                    if (k % 2 == 0)
                        tableRecordsLabel[k].BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableRecordsLabel[k].BackColor = System.Drawing.Color.Gainsboro;
                    tableRecordsLabel[k].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableRecordsLabel[k].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableRecordsLabel[k].ForeColor = System.Drawing.Color.Black;
                    tableRecordsLabel[k].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableRecordsLabel[k].Location = new System.Drawing.Point(277, lastPosition);
                    tableRecordsLabel[k].Size = new System.Drawing.Size(130, 25);
                    tableRecordsLabel[k].Text = currentNode.Value.ToString();
                    tableRecordsLabel[k].Name = tableName[k] + "Records";
                    tableRecordsLabel[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tablePanel.Controls.Add(tableRecordsLabel[k]);

                    FileInfo fi = new FileInfo(tablePath);
                    FileInfo fsi = new FileInfo(tableStructurePath);
                    tableSizeLabel[k] = new Label();
                    if (k % 2 == 0)
                        tableSizeLabel[k].BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableSizeLabel[k].BackColor = System.Drawing.Color.Gainsboro;
                    tableSizeLabel[k].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableSizeLabel[k].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableSizeLabel[k].ForeColor = System.Drawing.Color.Black;
                    tableSizeLabel[k].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableSizeLabel[k].Location = new System.Drawing.Point(409, lastPosition);
                    tableSizeLabel[k].Size = new System.Drawing.Size(200, 25);
                    tableSizeLabel[k].Text = (fi.Length + fsi.Length).ToString();
                    tableSizeLabel[k].Name = tableName[k] + "Size";
                    tableSizeLabel[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tablePanel.Controls.Add(tableSizeLabel[k]);

                    tableCreateDateLabel[k] = new Label();
                    if (k % 2 == 0)
                        tableCreateDateLabel[k].BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableCreateDateLabel[k].BackColor = System.Drawing.Color.Gainsboro;
                    tableCreateDateLabel[k].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableCreateDateLabel[k].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableCreateDateLabel[k].ForeColor = System.Drawing.Color.Black;
                    tableCreateDateLabel[k].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableCreateDateLabel[k].Location = new System.Drawing.Point(611, lastPosition);
                    tableCreateDateLabel[k].Size = new System.Drawing.Size(190, 25);
                    tableCreateDateLabel[k].Text = File.GetCreationTime(tablePath).ToShortDateString();
                    tableCreateDateLabel[k].Name = tableName[k] + "createdate";
                    tableCreateDateLabel[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tablePanel.Controls.Add(tableCreateDateLabel[k]);

                    tableUpdateLabel[k] = new Label();
                    if (k % 2 == 0)
                        tableUpdateLabel[k].BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        tableUpdateLabel[k].BackColor = System.Drawing.Color.Gainsboro;
                    tableUpdateLabel[k].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tableUpdateLabel[k].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tableUpdateLabel[k].ForeColor = System.Drawing.Color.Black;
                    tableUpdateLabel[k].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                    tableUpdateLabel[k].Location = new System.Drawing.Point(803, lastPosition);
                    tableUpdateLabel[k].Size = new System.Drawing.Size(190, 25);
                    tableUpdateLabel[k].Text = File.GetLastWriteTime(tablePath).ToShortDateString();
                    tableUpdateLabel[k].Name = tableName[k] + "update";
                    tableUpdateLabel[k].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    tablePanel.Controls.Add(tableUpdateLabel[k]);

                    //xmltable.Save(tablePath);
                    lastPosition += 27;

                }
                LinkLabel CheckLinkLabel = new LinkLabel();
                CheckLinkLabel.AutoSize = true;
                CheckLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                CheckLinkLabel.Location = new System.Drawing.Point(10, 9);
                CheckLinkLabel.Name = "checkLinkLabel";
                CheckLinkLabel.Size = new System.Drawing.Size(51, 13);
                CheckLinkLabel.TabStop = true;
                CheckLinkLabel.Text = "Check All";
                CheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(CheckLinkLabel_LinkClicked);


                Label SlashLabel = new Label();
                SlashLabel.AutoSize = true;
                SlashLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SlashLabel.Location = new System.Drawing.Point(78, 7);
                SlashLabel.Margin = new System.Windows.Forms.Padding(0);
                SlashLabel.Name = "slashLabel";
                SlashLabel.Size = new System.Drawing.Size(12, 17);
                SlashLabel.Text = "/";

                LinkLabel UncheckLinkLabel = new LinkLabel();
                UncheckLinkLabel.AutoSize = true;
                UncheckLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                UncheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                UncheckLinkLabel.Location = new System.Drawing.Point(90, 9);
                UncheckLinkLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                UncheckLinkLabel.Name = "uncheckLinlLabel";
                UncheckLinkLabel.Size = new System.Drawing.Size(63, 13);
                UncheckLinkLabel.TabStop = true;
                UncheckLinkLabel.Text = "Uncheck All";
                UncheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(UncheckLinkLabel_LinkClicked);

                Label ToLabel = new Label();
                ToLabel.AutoSize = true;
                ToLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ToLabel.Location = new System.Drawing.Point(180, 9);
                ToLabel.Margin = new System.Windows.Forms.Padding(0);
                ToLabel.Name = "toLabel";
                ToLabel.Size = new System.Drawing.Size(20, 13);
                ToLabel.Text = "to";

                LinkLabel DeleteLinkLabel = new LinkLabel();
                DeleteLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                DeleteLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                DeleteLinkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                DeleteLinkLabel.Location = new System.Drawing.Point(210, 3);
                DeleteLinkLabel.Margin = new System.Windows.Forms.Padding(3);
                DeleteLinkLabel.Name = "deleteLinkLabel";
                DeleteLinkLabel.Size = new System.Drawing.Size(65, 30);
                DeleteLinkLabel.TabStop = true;
                DeleteLinkLabel.Text = "Delete";
                DeleteLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                DeleteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(DeleteLinkLabel_LinkClicked);

                Panel ButtonPanel = new Panel();
                ButtonPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                ButtonPanel.Size = new System.Drawing.Size(988, 35);
                ButtonPanel.Location = new System.Drawing.Point(5, lastPosition);
                ButtonPanel.Controls.Add(CheckLinkLabel);
                ButtonPanel.Controls.Add(SlashLabel);
                ButtonPanel.Controls.Add(UncheckLinkLabel);
                ButtonPanel.Controls.Add(ToLabel);
                ButtonPanel.Controls.Add(DeleteLinkLabel);

                tablePanel.Controls.Add(ButtonPanel);
            }
            else
            {
                Label emptyLabel = new Label();
                emptyLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                emptyLabel.Location = new System.Drawing.Point(5, 10);
                emptyLabel.Size = new System.Drawing.Size(988, 35);
                emptyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                emptyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                emptyLabel.Text = "This database has no table. Create a new table.";
                emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tablePanel.Controls.Add(emptyLabel);
            }

            // 
            // tableNameLabel
            // 
            Label tableCreateNameLabel = new Label();
            tableCreateNameLabel.AutoSize = true;
            tableCreateNameLabel.Location = new System.Drawing.Point(11, 66);
            tableCreateNameLabel.Name = "tableNameLabel";
            tableCreateNameLabel.Size = new System.Drawing.Size(98, 20);
            tableCreateNameLabel.TabIndex = 0;
            tableCreateNameLabel.Text = "Table Name:";

            tableNameTextBox = new TextBox();
            tableNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            tableNameTextBox.Location = new System.Drawing.Point(113, 63);
            tableNameTextBox.Name = "tableNameTextBox";
            tableNameTextBox.Text = "";
            tableNameTextBox.Size = new System.Drawing.Size(350, 26);
            tableNameTextBox.TabIndex = 1;

            Label tableColumnLabel = new Label();
            tableColumnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            tableColumnLabel.AutoSize = true;
            tableColumnLabel.Location = new System.Drawing.Point(493, 66);
            tableColumnLabel.Name = "tableColumnLabel";
            tableColumnLabel.Size = new System.Drawing.Size(109, 20);
            tableColumnLabel.TabIndex = 2;
            tableColumnLabel.Text = "No of Column:";
            // 
            // tableColumnTextBox
            // 
            tableColumnTextBox = new TextBox();
            tableColumnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            tableColumnTextBox.Location = new System.Drawing.Point(610, 63);
            tableColumnTextBox.Name = "tableColumnTextBox";
            tableColumnTextBox.Text = "";
            tableColumnTextBox.Size = new System.Drawing.Size(164, 26);
            tableColumnTextBox.TabIndex = 3;


            Button createTableButton = new Button();
            createTableButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            createTableButton.Location = new System.Drawing.Point(683, 111);
            createTableButton.Name = "createTableButton";
            createTableButton.Size = new System.Drawing.Size(91, 31);
            createTableButton.TabIndex = 4;
            createTableButton.Text = "Next";
            createTableButton.UseVisualStyleBackColor = true;
            createTableButton.Click += new System.EventHandler(createTableButton_Click);

            GroupBox createTableGroupBox = new GroupBox();
            createTableGroupBox.Name = "createTableGroupBox";
            createTableGroupBox.Size = new System.Drawing.Size(988, 157);
            createTableGroupBox.TabIndex = 0;
            createTableGroupBox.TabStop = false;
            createTableGroupBox.Text = "Create new table on database " + globalDatabaseName;
            createTableGroupBox.Location = new System.Drawing.Point(5, lastPosition + 60);

            createTableGroupBox.Controls.Add(createTableButton);
            createTableGroupBox.Controls.Add(tableColumnTextBox);
            createTableGroupBox.Controls.Add(tableColumnLabel);
            createTableGroupBox.Controls.Add(tableCreateNameLabel);
            createTableGroupBox.Controls.Add(tableNameTextBox);
            tablePanel.Controls.Add(createTableGroupBox);


            Button exportdbButton = new Button();
            exportdbButton.BackColor = System.Drawing.Color.Transparent;
            exportdbButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            exportdbButton.ForeColor = System.Drawing.Color.Black;
            exportdbButton.Location = new System.Drawing.Point(44, 65);
            exportdbButton.Name = "exportdbButton";
            exportdbButton.Size = new System.Drawing.Size(107, 44);
            exportdbButton.TabIndex = 2;
            exportdbButton.Text = "Export";
            exportdbButton.UseVisualStyleBackColor = false;
            exportdbButton.Click += new System.EventHandler(exportdbButton_Click);

            Button deleteDatabaseButton = new Button();
            deleteDatabaseButton.BackColor = System.Drawing.Color.Transparent;
            deleteDatabaseButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            deleteDatabaseButton.ForeColor = System.Drawing.Color.Black;
            deleteDatabaseButton.Location = new System.Drawing.Point(468, 65);
            deleteDatabaseButton.Name = "deleteDatabaseButton";
            deleteDatabaseButton.Size = new System.Drawing.Size(107, 44);
            deleteDatabaseButton.TabIndex = 7;
            deleteDatabaseButton.Text = "Delete";
            deleteDatabaseButton.UseVisualStyleBackColor = false;
            deleteDatabaseButton.Click += new System.EventHandler(deleteDatabaseButton_Click);

            GroupBox exportDeleteGroupBox = new GroupBox();
            exportDeleteGroupBox.Name = "exportDeleteGroupBox";
            exportDeleteGroupBox.Size = new System.Drawing.Size(988, 140);
            exportDeleteGroupBox.TabIndex = 1;
            exportDeleteGroupBox.TabStop = false;
            exportDeleteGroupBox.Text = "Export/Delete";
            exportDeleteGroupBox.Location = new System.Drawing.Point(5, lastPosition + 240);
            exportDeleteGroupBox.Controls.Add(deleteDatabaseButton);
            exportDeleteGroupBox.Controls.Add(exportdbButton);
            tablePanel.Controls.Add(exportDeleteGroupBox);


            //
            //
            
        }

        private void exportdbButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    ZipFile.CreateFromDirectory(pathOfDatabase+"//"+globalDatabaseName, folderBrowserDialog1.SelectedPath + "\\" + globalDatabaseName + ".ndbm");
                    MessageBox.Show("Database successfully Exported.");
                }
                catch (IOException)
                {
                    DialogResult dr = MessageBox.Show("Database with same name already exist. Database will be saved as "+globalDatabaseName+"2."+" Do you want to continue?","Database Rename",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        ZipFile.CreateFromDirectory(pathOfDatabase + "//" + globalDatabaseName, folderBrowserDialog1.SelectedPath + "\\" + globalDatabaseName + "2.ndbm");
                        MessageBox.Show("Database successfully Exported.");
                    }
                }
            }
        }

        private void deleteDatabaseButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Database "+globalDatabaseName+" Will be completelly Deleted. Do you want to continue?","Delete Database",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string path = @"Databases\"+globalDatabaseName;
                Directory.Delete(path,true);
                home();
            }
        }

        static Int64 numberOfColumn;
        private void createTableButton_Click(object sender, EventArgs e)
        {

            string tableName = tableNameTextBox.Text;
            string databaseName = globalDatabaseName;
            string columnNumber = tableColumnTextBox.Text;

            if (tableName != "" && columnNumber != "")
            {
                if (ExpressionTest.NameString(tableName))
                {
                    if (DataTypeTest.IntegerType(columnNumber))
                    {
                        if (int.Parse(columnNumber) >= 2)
                        {
                            string path = Application.StartupPath + "\\Databases\\" + databaseName + "\\" + tableName + ".xml";
                            if (!File.Exists(path))
                            {
                                globalTableName = tableName;
                                numberOfColumn = Convert.ToInt64(columnNumber);
                                XmlandXmlSchema.createXmlandXmlSchema(databaseName, tableName);
                                tableDefinition();

                            }
                            else
                            {
                                MessageBox.Show("Table with same name already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Table must have at least 2 column","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Table number should be an integer/number.");
                    }
                }
                else
                {
                    MessageBox.Show("Table name invalid.Name can not be start with number.");
                }
            }
            else
            {
                MessageBox.Show("Both field should be filled up", "Blank box Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void CheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int p = 0; p < i; p++)
            {
                tCheckBox[p].Checked = true;
            }
        }

        public void UncheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int p = 0; p < i; p++)
            {
                tCheckBox[p].Checked = false;
            }
        }
        public void DeleteLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This process can not be undone. Do you want to continue?", "Table(s) Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                foreach (CheckBox ck in tCheckBox)
                {
                    if (ck.Checked)
                    {
                        int index = (int)ck.Tag;
                        string fileName = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + tableName[index] + ".xml";
                        string schemaFileName = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + tableName[index] + "_schema" + ".xml";
                        File.Delete(fileName);
                        File.Delete(schemaFileName);
                    }
                }
                tableView();
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------Table Creation unit--------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------

        Int64 size;
        TextBox[] ColumnTextbox;
        ComboBox[] TypeComboBox;
        ComboBox[] IndexComboBox;
        CheckBox[] NullCheckBox;
        CheckBox[] AutoIncrementCheckBox;

        public void tableDefinition()
        {
            size = numberOfColumn;
            this.Controls.Remove(homeButton);
            contentPanel.Controls.Clear();
            headerLabel.Text = globalDatabaseName+" :: " +globalTableName+ "[New Table]";

            UserControl tableDefinitionUserControl = new UserControl();

            tablePanel.Controls.Clear();
            //tablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            tablePanel.AutoScroll = true;
            tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            tablePanel.Location = new System.Drawing.Point(262, 2);
            tablePanel.Name = "tablePanel";
            tablePanel.Size = new System.Drawing.Size(this.Width-277, this.Height-130);
            tableDefinitionUserControl.Controls.Add(tablePanel);

            tableDefinitionUserControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            tableDefinitionUserControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(tableDefinitionUserControl);
            shapeContainer(tableDefinitionUserControl);
            //databaseComponent(tableDefinitionUserControl);

            GroupBox tableGroupBox = new GroupBox();
            tableGroupBox.AutoSize = true;
            tableGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableGroupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            tableGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tableGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            tableGroupBox.Location = new System.Drawing.Point(17, 8);
            tableGroupBox.Name = "tableGroupBox";
            tableGroupBox.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            tableGroupBox.Size = new System.Drawing.Size(6, 2);
            tableGroupBox.TabIndex = 5;
            tableGroupBox.TabStop = false;
            tablePanel.Controls.Add(tableGroupBox);
            tableGroupBox.Text = "New Table:(" + globalTableName + ")";


            Label NameLabel = new Label();
            NameLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            NameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            NameLabel.ForeColor = System.Drawing.Color.Black;
            NameLabel.Location = new System.Drawing.Point(5, 25);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new System.Drawing.Size(250, 30);
            NameLabel.Text = "Field";
            NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableGroupBox.Controls.Add(NameLabel);

            Label TypeLabel = new Label();
            TypeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            TypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            TypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TypeLabel.ForeColor = System.Drawing.Color.Black;
            TypeLabel.Location = new System.Drawing.Point(257, 25);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new System.Drawing.Size(230, 30);
            TypeLabel.Text = "Type";
            TypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableGroupBox.Controls.Add(TypeLabel);


            Label indexTypeLabel = new Label();
            indexTypeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            indexTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            indexTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            indexTypeLabel.ForeColor = System.Drawing.Color.Black;
            indexTypeLabel.Location = new System.Drawing.Point(489, 25);
            indexTypeLabel.Name = "Index";
            indexTypeLabel.Size = new System.Drawing.Size(220, 30);
            indexTypeLabel.Text = "Index";
            indexTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableGroupBox.Controls.Add(indexTypeLabel);


            Label NullLabel = new Label();
            NullLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            NullLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            NullLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            NullLabel.ForeColor = System.Drawing.Color.Black;
            NullLabel.Location = new System.Drawing.Point(711, 25);
            NullLabel.Name = "Null";
            NullLabel.Size = new System.Drawing.Size(90, 30);
            NullLabel.Text = "Null";
            NullLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableGroupBox.Controls.Add(NullLabel);


            Label AutoincrementLabel = new Label();
            AutoincrementLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            AutoincrementLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AutoincrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AutoincrementLabel.ForeColor = System.Drawing.Color.Black;
            AutoincrementLabel.Location = new System.Drawing.Point(803, 25);
            AutoincrementLabel.Name = "Autoincrement";
            AutoincrementLabel.Size = new System.Drawing.Size(90, 30);
            AutoincrementLabel.Text = "A.I";
            AutoincrementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableGroupBox.Controls.Add(AutoincrementLabel);
            //........................................................

            ColumnTextbox = new TextBox[numberOfColumn];
            TypeComboBox = new ComboBox[numberOfColumn];
            IndexComboBox = new ComboBox[numberOfColumn];
            NullCheckBox = new CheckBox[numberOfColumn];
            AutoIncrementCheckBox = new CheckBox[numberOfColumn];
            int lastPosition = 47 + 10;
            for (Int64 t = 0; t < size; t++)
            {
                ColumnTextbox[t] = new TextBox();
                ColumnTextbox[t].Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ColumnTextbox[t].Location = new Point(5, lastPosition);
                ColumnTextbox[t].Name = "ColumnTextbox" + t.ToString();
                ColumnTextbox[t].Size = new Size(250, 30);
                tableGroupBox.Controls.Add(ColumnTextbox[t]);

                TypeComboBox[t] = new ComboBox();
                TypeComboBox[t].Font = new Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TypeComboBox[t].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                TypeComboBox[t].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                TypeComboBox[t].FormattingEnabled = true;
                TypeComboBox[t].Items.AddRange(new object[] { "numeric", "string", "email" });
                TypeComboBox[t].Location = new Point(257, lastPosition);
                TypeComboBox[t].Name = "TypeComboBox" + t.ToString();
                TypeComboBox[t].Size = new Size(230, 30);
                tableGroupBox.Controls.Add(TypeComboBox[t]);

                //
                //

                IndexComboBox[t] = new ComboBox();
                IndexComboBox[t].Font = new Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                IndexComboBox[t].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                IndexComboBox[t].FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                IndexComboBox[t].FormattingEnabled = true;
                IndexComboBox[t].Items.AddRange(new object[] { "", "primary", "unique" });
                IndexComboBox[t].Location = new Point(489, lastPosition);
                IndexComboBox[t].Name = "IndexComboBox" + t.ToString();
                IndexComboBox[t].Size = new Size(220, 30);
                tableGroupBox.Controls.Add(IndexComboBox[t]);

                //
                //
                NullCheckBox[t] = new CheckBox();
                if (t % 2 == 0)
                    NullCheckBox[t].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    NullCheckBox[t].BackColor = System.Drawing.Color.Gainsboro;
                NullCheckBox[t].Appearance = System.Windows.Forms.Appearance.Normal;
                NullCheckBox[t].Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NullCheckBox[t].Location = new Point(750, lastPosition + 8);
                NullCheckBox[t].Size = new Size(13, 13);
                NullCheckBox[t].Name = "NullCheckBox" + t.ToString();
                NullCheckBox[t].TabStop = true;
                NullCheckBox[t].Text = "";
                NullCheckBox[t].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                NullCheckBox[t].UseVisualStyleBackColor = true;
                tableGroupBox.Controls.Add(NullCheckBox[t]);

                //
                //
                AutoIncrementCheckBox[t] = new CheckBox();

                AutoIncrementCheckBox[t].Appearance = System.Windows.Forms.Appearance.Normal;
                AutoIncrementCheckBox[t].Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                AutoIncrementCheckBox[t].Location = new Point(835, lastPosition + 8);
                AutoIncrementCheckBox[t].Size = new Size(13, 13);
                AutoIncrementCheckBox[t].Name = "NullCheckBox" + t.ToString();
                AutoIncrementCheckBox[t].TabStop = true;
                AutoIncrementCheckBox[t].Text = "";
                AutoIncrementCheckBox[t].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                AutoIncrementCheckBox[t].UseVisualStyleBackColor = true;
                tableGroupBox.Controls.Add(AutoIncrementCheckBox[t]);

                lastPosition += 29;

            }

            int ButtonPosition;

            if (lastPosition >= Screen.PrimaryScreen.Bounds.Height)
                ButtonPosition = lastPosition + 50;
            else
                ButtonPosition = Screen.PrimaryScreen.Bounds.Height - 225;
            
            Button tableCancleButton = new Button();
            //tableCancleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            tableCancleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tableCancleButton.Location = new System.Drawing.Point(19, ButtonPosition);
            tableCancleButton.Name = "cancelButton";
            tableCancleButton.Size = new System.Drawing.Size(121, 42);
            tableCancleButton.TabIndex = 4;
            tableCancleButton.Text = "Cancel";
            tableCancleButton.UseVisualStyleBackColor = true;
            tableCancleButton.Click += new System.EventHandler(tableCancleButton_Click);
            tablePanel.Controls.Add(tableCancleButton);
            // 
            // saveButton
            // 
            Button tableSaveButton = new Button();
            //tableSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            tableSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tableSaveButton.Location = new System.Drawing.Point(750, ButtonPosition);
            tableSaveButton.Name = "saveButton";
            tableSaveButton.Size = new System.Drawing.Size(121, 42);
            tableSaveButton.TabIndex = 2;
            tableSaveButton.Text = "Save";
            tableSaveButton.UseVisualStyleBackColor = true;
            tableSaveButton.Click += new System.EventHandler(tableSaveButton_Click);
            tablePanel.Controls.Add(tableSaveButton);



        }

        private void tableCancleButton_Click(object sender, EventArgs e)
        {

            DialogResult dr;
            dr = MessageBox.Show("Table will not exist. Do you want to continue? ", "Remove Table", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string fileName = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";
                string schemaFileName = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + "_schema" + ".xml";
                File.Delete(fileName);
                File.Delete(schemaFileName);
                tableView();
            }
        }

        private void tableSaveButton_Click(object sender, EventArgs e)
        {
            bool t = true;
            bool primaryKey = false;
            string error = "";
            int q = 1;
            for (int k = 0; k < size; k++)
            {
                for (int count = 0; count < k; count++)
                {
                    if (ColumnTextbox[count].Text.ToLower().Trim() == ColumnTextbox[k].Text.ToLower().Trim())
                    {
                        t = false;
                        error += "\nError " + q.ToString() + ": Two or more column name can not be same.";
                        q++;
                        break;
                    }
                }

                if (ColumnTextbox[k].Text == "" || !ExpressionTest.NameString(ColumnTextbox[k].Text))
                {
                    t = false;
                    if (ColumnTextbox[k].Text == "")
                        error += "\nError " + q.ToString() + ": Column " + (k + 1).ToString() + " name is not defined.";
                    else
                        error += "\nError " + q.ToString() + ": Column " + (k + 1).ToString() + " name is invlid(name can not start with number).";
                    q++;
                }

                if (TypeComboBox[k].Text == "")
                {
                    t = false;
                    error += "\nError " + q.ToString() + ": Column " + (k + 1).ToString() + " Type is not selected";
                    q++;
                }

                if (AutoIncrementCheckBox[k].Checked && TypeComboBox[k].Text != "numeric")
                {
                    t = false;
                    error += "\nError " + q.ToString() + ":In Column " + (k + 1).ToString() + " auto-increment value must be numeric type.";
                    q++;
                }

                if (IndexComboBox[k].Text == "primary" && NullCheckBox[k].Checked)
                {
                    t = false;
                    error += "\nError " + q.ToString() + ": Column " + (k + 1).ToString() + " Primary key can't be null";
                    q++;
                }

                if (AutoIncrementCheckBox[k].Checked)
                {
                    if (IndexComboBox[k].Text != "primary" && IndexComboBox[k].Text != "unique")
                    {
                        t = false;
                        error += "\nError " + q.ToString() + ": Column " + (k + 1).ToString() + " Auto incremented value must have to be primary or unique.";
                        q++;
                    }

                }


                if (IndexComboBox[k].Text == "primary")
                {
                    primaryKey = true;
                }

            }


            if (t == false || primaryKey == false)
            {
                if (primaryKey == false)
                    error += "\nError " + q.ToString() + ": Primary key not selected.";
                MessageBox.Show(error, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string xmlfile = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + "_schema" + ".xml";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlfile);

                XmlElement parentElement = null;
                parentElement = xmldoc.CreateElement("Columns");
                parentElement.SetAttribute("count", size.ToString());


                for (Int64 i = 0; i < size; i++)
                {
                    XmlElement element = null;
                    element = xmldoc.CreateElement("column");
                    element.SetAttribute("index", (i + 1).ToString());
                    element.SetAttribute("name", ColumnTextbox[i].Text);
                    element.SetAttribute("type", TypeComboBox[i].Text);

                    if (IndexComboBox[i].Text == "")
                        element.SetAttribute("indexType", "none");
                    else
                        element.SetAttribute("indexType", IndexComboBox[i].Text);

                    if (NullCheckBox[i].Checked)
                        element.SetAttribute("null", "true");
                    else
                        element.SetAttribute("null", "false");

                    if (AutoIncrementCheckBox[i].Checked)
                        element.SetAttribute("autoincrement", "true");
                    else
                        element.SetAttribute("autoincrement", "false");

                    parentElement.AppendChild(element);
                }
                xmldoc.DocumentElement.AppendChild(parentElement);
                xmldoc.Save(xmlfile);
                tableDataDisplay();

            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------Table Data Unit----------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------


        string[] eName;
        string[] eType;
        string[] eIndexType;
        string[] eNull;
        string[] eAutoIncrement;
        CheckBox[] eCheckBox;
        int slastPosition;
        Button addColumnButton;
        Panel elementPanel = new Panel();
        Panel insertPanel = new Panel();
        Panel browsePanel = new Panel();
        TextBox[] iValueTextBox;
        string[,] item;
        CheckBox[] bCheckBox;
        Button[] bEditButton;
        Label[,] itemLabel;
        TabControl tableViewTabControl = new TabControl();
        TabPage structureTabPage = new TabPage();
        TabPage browseTabPage = new TabPage();
        TabPage insertTabPage = new TabPage();
        public void tableDataDisplay()
        {
            contentPanel.Controls.Clear();
            headerLabel.Text = globalDatabaseName + " :: " + globalTableName + "[Table]";

            UserControl tableDataUserControl = new UserControl();

            tablePanel.Controls.Clear();
            tablePanel.AutoScroll = false;
            tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            tablePanel.Location = new System.Drawing.Point(262, 2);
            tablePanel.Name = "tablePanel";
            tablePanel.Size = new System.Drawing.Size(this.Width - 277, this.Height - 130);
            tableDataUserControl.Controls.Add(tablePanel);

            tableDataUserControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            tableDataUserControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(tableDataUserControl);
            shapeContainer(tableDataUserControl);
            databaseComponent(tableDataUserControl);

            
            tableViewTabControl.Dock= DockStyle.Fill;
            tableViewTabControl.Location = new System.Drawing.Point(0,0);
            tableViewTabControl.Name = "tableViewTabControl";
            tablePanel.Controls.Add(tableViewTabControl);

            tableViewTabControl.Controls.Clear();
            
            structureTabPage.Location = new Point(4,29);
            structureTabPage.Name = "structureTabPage";
            structureTabPage.Padding = new System.Windows.Forms.Padding(3);
            structureTabPage.Text = "Structure";
            structureTabPage.UseVisualStyleBackColor = false;
            tableViewTabControl.Controls.Add(structureTabPage);
            
            
            browseTabPage.Location = new Point(4, 29);
            browseTabPage.Name = "browseTabPage";
            browseTabPage.Padding = new System.Windows.Forms.Padding(3);
            browseTabPage.Text = "Browse";
            browseTabPage.UseVisualStyleBackColor = false;
            tableViewTabControl.Controls.Add(browseTabPage);

            
            insertTabPage.Location = new Point(4, 29);
            insertTabPage.Name = "insertTabPage";
            insertTabPage.Padding = new System.Windows.Forms.Padding(3);
            insertTabPage.Text = "Insert";
            insertTabPage.UseVisualStyleBackColor = false;
            tableViewTabControl.Controls.Add(insertTabPage);

            elementPanel.Controls.Clear();
            elementPanel.AutoScroll = true;
            elementPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            elementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            elementPanel.Location = new System.Drawing.Point(3, 3);
            elementPanel.Name = "elementPanel";
            elementPanel.Size = new System.Drawing.Size(714, 499);
            elementPanel.TabIndex = 2;
            structureTabPage.Controls.Add(elementPanel);

            //----------------------------------------------------------

            Label eCheckboxLabel = new Label();
            eCheckboxLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            eCheckboxLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            eCheckboxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eCheckboxLabel.ForeColor = System.Drawing.Color.Black;
            eCheckboxLabel.Location = new System.Drawing.Point(5, 15);
            eCheckboxLabel.Name = "eCheckboxLabel";
            eCheckboxLabel.Size = new System.Drawing.Size(38, 30);
            eCheckboxLabel.Text = "";
            eCheckboxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(eCheckboxLabel);



            Label eNameLabel = new Label();
            eNameLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            eNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            eNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eNameLabel.ForeColor = System.Drawing.Color.Black;
            eNameLabel.Location = new System.Drawing.Point(45, 15);
            eNameLabel.Name = "NameLabel";
            eNameLabel.Size = new System.Drawing.Size(230, 30);
            eNameLabel.Text = "Field";
            eNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(eNameLabel);

            Label eTypeLabel = new Label();
            eTypeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            eTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            eTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eTypeLabel.ForeColor = System.Drawing.Color.Black;
            eTypeLabel.Location = new System.Drawing.Point(277, 15);
            eTypeLabel.Name = "TypeLabel";
            eTypeLabel.Size = new System.Drawing.Size(200, 30);
            eTypeLabel.Text = "Type";
            eTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(eTypeLabel);

            Label eAutoincrementLabel = new Label();
            eAutoincrementLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            eAutoincrementLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            eAutoincrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eAutoincrementLabel.ForeColor = System.Drawing.Color.Black;
            eAutoincrementLabel.Location = new System.Drawing.Point(479, 15);
            eAutoincrementLabel.Name = "Autoincrement";
            eAutoincrementLabel.Size = new System.Drawing.Size(200, 30);
            eAutoincrementLabel.Text = "Auto Increment";
            eAutoincrementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(eAutoincrementLabel);

            Label eNullLabel = new Label();
            eNullLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            eNullLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            eNullLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eNullLabel.ForeColor = System.Drawing.Color.Black;
            eNullLabel.Location = new System.Drawing.Point(681, 15);
            eNullLabel.Name = "Null";
            eNullLabel.Size = new System.Drawing.Size(130, 30);
            eNullLabel.Text = "Null";
            eNullLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(eNullLabel);

            Label indexTypeLabel = new Label();
            indexTypeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            indexTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            indexTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            indexTypeLabel.ForeColor = System.Drawing.Color.Black;
            indexTypeLabel.Location = new System.Drawing.Point(813, 15);
            indexTypeLabel.Name = "Index";
            indexTypeLabel.Size = new System.Drawing.Size(190, 30);
            indexTypeLabel.Text = "Index";
            indexTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            elementPanel.Controls.Add(indexTypeLabel);


            string path = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + "_schema.xml";
            XmlTextReader xtr = new XmlTextReader(path);

            size = 0;
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Columns")
                {
                    size = Convert.ToInt64(xtr.GetAttribute("count"));
                    break;
                }
            }

            eName = new string[size];
            eType = new string[size];
            eIndexType = new string[size];
            eNull = new string[size];
            eAutoIncrement = new string[size];
            int i = 0;
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "column" && xtr.GetAttribute("index") == (i + 1).ToString())
                {
                    eName[i] = xtr.GetAttribute("name");
                    eType[i] = xtr.GetAttribute("type");
                    eIndexType[i] = xtr.GetAttribute("indexType");
                    eNull[i] = xtr.GetAttribute("null");
                    eAutoIncrement[i] = xtr.GetAttribute("autoincrement");
                    i++;
                    if (i == size)
                    {
                        xtr.Close();
                        break;
                    }
                }
            }


            Label[] elementNameLabel = new Label[size];
            Label[] elementTypeLabel = new Label[size];
            Label[] elementIndexTypeLabel = new Label[size];
            Label[] elementNullLabel = new Label[size];
            Label[] elementAutoIncrementLabel = new Label[size];
            LinkLabel[] elementEditDeleteLinkLabel = new LinkLabel[size];
            eCheckBox = new CheckBox[size];
            slastPosition = 47;

            for (int p = 0; p < size; p++)
            {
                eCheckBox[p] = new CheckBox();
                eCheckBox[p].Location = new System.Drawing.Point(13, 6);
                eCheckBox[p].Name = "eCheckBox" + p.ToString();
                eCheckBox[p].Size = new System.Drawing.Size(13, 13);
                eCheckBox[p].Text = "";
                eCheckBox[p].UseVisualStyleBackColor = true;

                Panel checkboxPanel = new Panel();
                if (p % 2 == 0)
                    checkboxPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    checkboxPanel.BackColor = System.Drawing.Color.Gainsboro;
                checkboxPanel.Location = new System.Drawing.Point(5, slastPosition);
                checkboxPanel.Size = new System.Drawing.Size(38, 25);
                checkboxPanel.Controls.Add(eCheckBox[p]);
                elementPanel.Controls.Add(checkboxPanel);



                elementNameLabel[p] = new Label();
                if (p % 2 == 0)
                    elementNameLabel[p].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    elementNameLabel[p].BackColor = System.Drawing.Color.Gainsboro;
                elementNameLabel[p].BorderStyle = System.Windows.Forms.BorderStyle.None;
                if (eIndexType[p] == "primary")
                    elementNameLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    elementNameLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                elementNameLabel[p].ForeColor = System.Drawing.Color.Black;
                elementNameLabel[p].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                elementNameLabel[p].Location = new System.Drawing.Point(45, slastPosition);
                elementNameLabel[p].Size = new System.Drawing.Size(230, 25);
                elementNameLabel[p].Text = eName[p];
                elementNameLabel[p].Name = eName[p];
                elementNameLabel[p].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                tableViewToolTip.SetToolTip(elementNameLabel[p], eName[p]);
                elementPanel.Controls.Add(elementNameLabel[p]);
                //
                //
                //
                elementTypeLabel[p] = new Label();
                if (p % 2 == 0)
                    elementTypeLabel[p].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    elementTypeLabel[p].BackColor = System.Drawing.Color.Gainsboro;
                elementTypeLabel[p].BorderStyle = System.Windows.Forms.BorderStyle.None;
                elementTypeLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                elementTypeLabel[p].ForeColor = System.Drawing.Color.Black;
                elementTypeLabel[p].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                elementTypeLabel[p].Location = new System.Drawing.Point(277, slastPosition);
                elementTypeLabel[p].Name = eType[p];
                elementTypeLabel[p].Size = new System.Drawing.Size(200, 25);
                elementTypeLabel[p].Text = eType[p]; ;
                elementTypeLabel[p].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableViewToolTip.SetToolTip(elementTypeLabel[p], eType[p]);
                elementPanel.Controls.Add(elementTypeLabel[p]);

                //
                //
                elementAutoIncrementLabel[p] = new Label();
                if (p % 2 == 0)
                    elementAutoIncrementLabel[p].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    elementAutoIncrementLabel[p].BackColor = System.Drawing.Color.Gainsboro;
                elementAutoIncrementLabel[p].BorderStyle = System.Windows.Forms.BorderStyle.None;
                elementAutoIncrementLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                elementAutoIncrementLabel[p].ForeColor = System.Drawing.Color.Black;
                elementAutoIncrementLabel[p].Location = new System.Drawing.Point(479, slastPosition);
                elementAutoIncrementLabel[p].Name = eAutoIncrement[p];
                elementAutoIncrementLabel[p].Size = new System.Drawing.Size(200, 25);

                if (eAutoIncrement[p] == "true")
                {
                    elementAutoIncrementLabel[p].Text = "auto-increment";
                    tableViewToolTip.SetToolTip(elementAutoIncrementLabel[p], "auto-increment");
                }
                else
                {
                    elementAutoIncrementLabel[p].Text = "";
                    tableViewToolTip.SetToolTip(elementAutoIncrementLabel[p], "");
                }
                elementAutoIncrementLabel[p].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                elementPanel.Controls.Add(elementAutoIncrementLabel[p]);

                //
                //
                //
                elementNullLabel[p] = new Label();
                if (p % 2 == 0)
                    elementNullLabel[p].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    elementNullLabel[p].BackColor = System.Drawing.Color.Gainsboro;
                elementNullLabel[p].BorderStyle = System.Windows.Forms.BorderStyle.None;
                elementNullLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                elementNullLabel[p].ForeColor = System.Drawing.Color.Black;
                elementNullLabel[p].Location = new System.Drawing.Point(681, slastPosition);
                elementNullLabel[p].Name = eNull[p];
                elementNullLabel[p].Size = new System.Drawing.Size(130, 25);
                if (eNull[p] == "true")
                {
                    elementNullLabel[p].Text = "null";
                    tableViewToolTip.SetToolTip(elementNullLabel[p], "null");
                }
                else
                {
                    elementNullLabel[p].Text = "";
                    tableViewToolTip.SetToolTip(elementNullLabel[p], "");
                }
                elementNullLabel[p].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                elementPanel.Controls.Add(elementNullLabel[p]);

                //
                //
                //
                elementIndexTypeLabel[p] = new Label();
                if (p % 2 == 0)
                    elementIndexTypeLabel[p].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    elementIndexTypeLabel[p].BackColor = System.Drawing.Color.Gainsboro;
                elementIndexTypeLabel[p].BorderStyle = System.Windows.Forms.BorderStyle.None;
                elementIndexTypeLabel[p].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                elementIndexTypeLabel[p].ForeColor = System.Drawing.Color.Black;
                elementIndexTypeLabel[p].Location = new System.Drawing.Point(813, slastPosition);
                elementIndexTypeLabel[p].Name = eIndexType[p] + p.ToString();
                elementIndexTypeLabel[p].Size = new System.Drawing.Size(190, 25);
                elementIndexTypeLabel[p].Text = eIndexType[p];
                elementIndexTypeLabel[p].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableViewToolTip.SetToolTip(elementIndexTypeLabel[p], eIndexType[p]);
                elementPanel.Controls.Add(elementIndexTypeLabel[p]);

                slastPosition += 27;
            }


            addColumnButton = new Button();
            addColumnButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            addColumnButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            addColumnButton.BackColor = System.Drawing.SystemColors.Control;
            addColumnButton.UseVisualStyleBackColor = true;
            addColumnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addColumnButton.Location = new System.Drawing.Point(10, 2);
            addColumnButton.Size = new System.Drawing.Size(150, 30);
            addColumnButton.Text = "Add new field";
            addColumnButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            addColumnButton.Name = "addColumnButton";
            tableViewToolTip.SetToolTip(addColumnButton, "Add new field");
            addColumnButton.Click += new EventHandler(addColumnButton_Click);

            //
            //
            LinkLabel sCheckLinkLabel = new LinkLabel();
            sCheckLinkLabel.AutoSize = true;
            sCheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            sCheckLinkLabel.Location = new System.Drawing.Point(170, 9);
            sCheckLinkLabel.Name = "checkLinkLabel";
            sCheckLinkLabel.Size = new System.Drawing.Size(51, 13);
            sCheckLinkLabel.TabStop = true;
            sCheckLinkLabel.Text = "Check All";
            tableViewToolTip.SetToolTip(sCheckLinkLabel, "Check all");
            sCheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(sCheckLinkLabel_LinkClicked);


            Label slashLabel = new Label();
            slashLabel.AutoSize = true;
            slashLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            slashLabel.Location = new System.Drawing.Point(241, 7);
            slashLabel.Margin = new System.Windows.Forms.Padding(0);
            slashLabel.Name = "slashLabel";
            slashLabel.Size = new System.Drawing.Size(12, 17);
            slashLabel.Text = "/";

            LinkLabel sUncheckLinkLabel = new LinkLabel();
            sUncheckLinkLabel.AutoSize = true;
            sUncheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            sUncheckLinkLabel.Location = new System.Drawing.Point(251, 9);
            sUncheckLinkLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            sUncheckLinkLabel.Name = "uncheckLinlLabel";
            sUncheckLinkLabel.Size = new System.Drawing.Size(63, 13);
            sUncheckLinkLabel.TabStop = true;
            sUncheckLinkLabel.Text = "Uncheck All";
            tableViewToolTip.SetToolTip(sUncheckLinkLabel, "Uncheck all");
            sUncheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(sUncheckLinkLabel_LinkClicked);

            Label toLabel = new Label();
            toLabel.AutoSize = true;
            toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toLabel.Location = new System.Drawing.Point(341, 7);
            toLabel.Margin = new System.Windows.Forms.Padding(0);
            toLabel.Name = "toLabel";
            toLabel.Size = new System.Drawing.Size(20, 13);
            toLabel.Text = "to";

            LinkLabel sDeleteLinkLabel = new LinkLabel();
            sDeleteLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            sDeleteLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            sDeleteLinkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sDeleteLinkLabel.Location = new System.Drawing.Point(371, 3);
            sDeleteLinkLabel.Margin = new System.Windows.Forms.Padding(3);
            sDeleteLinkLabel.Name = "deleteLinkLabel";
            sDeleteLinkLabel.Size = new System.Drawing.Size(65, 30);
            sDeleteLinkLabel.TabStop = true;
            sDeleteLinkLabel.Text = "Delete";
            sDeleteLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tableViewToolTip.SetToolTip(sDeleteLinkLabel, "Delete selected field(s)");
            sDeleteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(sDeleteLinkLabel_LinkClicked);

            //
            //
            Panel sbottomPanel = new Panel();
            sbottomPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            sbottomPanel.Size = new System.Drawing.Size(998, 35);
            sbottomPanel.Location = new System.Drawing.Point(5, slastPosition);
            sbottomPanel.Controls.Add(addColumnButton);
            sbottomPanel.Controls.Add(sCheckLinkLabel);
            sbottomPanel.Controls.Add(slashLabel);
            sbottomPanel.Controls.Add(sUncheckLinkLabel);
            sbottomPanel.Controls.Add(toLabel);
            sbottomPanel.Controls.Add(sDeleteLinkLabel);
            elementPanel.Controls.Add(sbottomPanel);
            

            //----------------insert part------------------------------------------

            insertPanel.Controls.Clear();
            insertPanel.AutoScroll = true;
            insertPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            insertPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            insertPanel.Location = new System.Drawing.Point(3, 3);
            insertPanel.Name = "elementPanel";
            insertPanel.Size = new System.Drawing.Size(714, 499);
            insertPanel.TabIndex = 2;
            insertTabPage.Controls.Add(insertPanel);

            Label iNameLabel = new Label();
            Label iTypeLabel = new Label();
            Label iAutoincrementLabel = new Label();
            Label iNullLabel = new Label();
            Label iValueLabel = new Label();


            iNameLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            iNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            iNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iNameLabel.ForeColor = System.Drawing.Color.Black;
            iNameLabel.Location = new System.Drawing.Point(5, 15);
            iNameLabel.Name = "NameLabel";
            iNameLabel.Size = new System.Drawing.Size(230, 30);
            iNameLabel.Text = "Field";
            iNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            insertPanel.Controls.Add(iNameLabel);

            iTypeLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            iTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            iTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iTypeLabel.ForeColor = System.Drawing.Color.Black;
            iTypeLabel.Location = new System.Drawing.Point(237, 15);
            iTypeLabel.Name = "TypeLabel";
            iTypeLabel.Size = new System.Drawing.Size(200, 30);
            iTypeLabel.Text = "Type";
            iTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            insertPanel.Controls.Add(iTypeLabel);

            iAutoincrementLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            iAutoincrementLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            iAutoincrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iAutoincrementLabel.ForeColor = System.Drawing.Color.Black;
            iAutoincrementLabel.Location = new System.Drawing.Point(439, 15);
            iAutoincrementLabel.Name = "Autoincrement";
            iAutoincrementLabel.Size = new System.Drawing.Size(210, 30);
            iAutoincrementLabel.Text = "Auto Increment";
            iAutoincrementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            insertPanel.Controls.Add(iAutoincrementLabel);

            iNullLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            iNullLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            iNullLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iNullLabel.ForeColor = System.Drawing.Color.Black;
            iNullLabel.Location = new System.Drawing.Point(651, 15);
            iNullLabel.Name = "Null";
            iNullLabel.Size = new System.Drawing.Size(130, 30);
            iNullLabel.Text = "Null";
            iNullLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            insertPanel.Controls.Add(iNullLabel);

            iValueLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            iValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            iValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            iValueLabel.ForeColor = System.Drawing.Color.Black;
            iValueLabel.Location = new System.Drawing.Point(783, 15);
            iValueLabel.Name = "Value";
            iValueLabel.Size = new System.Drawing.Size(220, 30);
            iValueLabel.Text = "Value";
            iValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            insertPanel.Controls.Add(iValueLabel);

            iValueTextBox = new TextBox[size];
            Label[] ielementNameLabel = new Label[size];
            Label[] ielementTypeLabel = new Label[size];
            Label[] ielementNullLabel = new Label[size];
            Label[] ielementAutoIncrementLabel = new Label[size];
            int iLastPosition = 47;

            for (int j = 0; j < size; j++)
            {

                ielementNameLabel[j] = new Label();
                if (j % 2 == 0)
                    ielementNameLabel[j].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    ielementNameLabel[j].BackColor = System.Drawing.Color.Gainsboro;
                ielementNameLabel[j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                if (eIndexType[j] == "primary")
                    ielementNameLabel[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    ielementNameLabel[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ielementNameLabel[j].ForeColor = System.Drawing.Color.Black;
                ielementNameLabel[j].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                ielementNameLabel[j].Location = new System.Drawing.Point(5, iLastPosition);
                ielementNameLabel[j].Size = new System.Drawing.Size(230, 25);
                ielementNameLabel[j].Text = eName[j];
                ielementNameLabel[j].Name = eName[j];
                ielementNameLabel[j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                insertPanel.Controls.Add(ielementNameLabel[j]);



                ielementTypeLabel[j] = new Label();
                if (j % 2 == 0)
                    ielementTypeLabel[j].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    ielementTypeLabel[j].BackColor = System.Drawing.Color.Gainsboro;
                ielementTypeLabel[j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                ielementTypeLabel[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ielementTypeLabel[j].ForeColor = System.Drawing.Color.Black;
                ielementTypeLabel[j].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                ielementTypeLabel[j].Location = new System.Drawing.Point(237, iLastPosition);
                ielementTypeLabel[j].Size = new System.Drawing.Size(200, 25);
                ielementTypeLabel[j].Text = eType[j];
                ielementTypeLabel[j].Name = eType[j];
                ielementTypeLabel[j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                insertPanel.Controls.Add(ielementTypeLabel[j]);


                ielementAutoIncrementLabel[j] = new Label();
                if (j % 2 == 0)
                    ielementAutoIncrementLabel[j].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    ielementAutoIncrementLabel[j].BackColor = System.Drawing.Color.Gainsboro;
                ielementAutoIncrementLabel[j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                ielementAutoIncrementLabel[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ielementAutoIncrementLabel[j].ForeColor = System.Drawing.Color.Black;
                ielementAutoIncrementLabel[j].Location = new System.Drawing.Point(439, iLastPosition);
                ielementAutoIncrementLabel[j].Name = eAutoIncrement[j];
                ielementAutoIncrementLabel[j].Size = new System.Drawing.Size(210, 25);

                if (eAutoIncrement[j] == "true")
                    ielementAutoIncrementLabel[j].Text = "auto-increment";
                else
                    ielementAutoIncrementLabel[j].Text = "";
                ielementAutoIncrementLabel[j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                insertPanel.Controls.Add(ielementAutoIncrementLabel[j]);


                ielementNullLabel[j] = new Label();
                if (j % 2 == 0)
                    ielementNullLabel[j].BackColor = System.Drawing.SystemColors.ScrollBar;
                else
                    ielementNullLabel[j].BackColor = System.Drawing.Color.Gainsboro;
                ielementNullLabel[j].BorderStyle = System.Windows.Forms.BorderStyle.None;
                ielementNullLabel[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ielementNullLabel[j].ForeColor = System.Drawing.Color.Black;
                ielementNullLabel[j].Location = new System.Drawing.Point(651, iLastPosition);
                ielementNullLabel[j].Name = eNull[j];
                ielementNullLabel[j].Size = new System.Drawing.Size(130, 25);
                if (eNull[j] == "true")
                    ielementNullLabel[j].Text = "null";
                else
                    ielementNullLabel[j].Text = "";
                ielementNullLabel[j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                insertPanel.Controls.Add(ielementNullLabel[j]);


                iValueTextBox[j] = new TextBox();
                iValueTextBox[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                iValueTextBox[j].Location = new System.Drawing.Point(783, iLastPosition);
                iValueTextBox[j].Size = new System.Drawing.Size(220, 30);
                iValueTextBox[j].Text = "";
                insertPanel.Controls.Add(iValueTextBox[j]);
                tableViewToolTip.SetToolTip(iValueTextBox[j], "Enter " + eName[j]);


                iLastPosition += 27;
            }

            Button goButton = new Button();
            goButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            goButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            goButton.BackColor = System.Drawing.SystemColors.Control;
            goButton.UseVisualStyleBackColor = true;
            goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            goButton.Location = new System.Drawing.Point(945, 2);
            goButton.Size = new System.Drawing.Size(50, 30);
            goButton.Text = "Go";
            goButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            goButton.Name = "goButton";
            tableViewToolTip.SetToolTip(goButton, "Click to insert data");
            goButton.Click += new EventHandler(goButton_Click);


            Panel ButtonPanel = new Panel();
            ButtonPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            ButtonPanel.Size = new System.Drawing.Size(998, 35);
            ButtonPanel.Location = new System.Drawing.Point(5, iLastPosition);
            ButtonPanel.Controls.Add(goButton);
            insertPanel.Controls.Add(ButtonPanel);


            //--------------------------------Browse part-------------------------------------


            browsePanel.Controls.Clear();
            browsePanel.AutoScroll = true;
            browsePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            browsePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            browsePanel.Location = new System.Drawing.Point(3, 3);
            browsePanel.Name = "elementPanel";
            browsePanel.Size = new System.Drawing.Size(714, 499);
            browsePanel.TabIndex = 2;
            browseTabPage.Controls.Add(browsePanel);



            string xmlfile = Application.StartupPath + "\\Databases\\" + globalDatabaseName+ "\\" + globalTableName + ".xml";
            XmlTextReader xmldata = new XmlTextReader(xmlfile);
            while (xmldata.Read())
            {
                if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name == "table")
                {
                    upsize = Convert.ToInt32(xmldata.GetAttribute("row"));
                    break;
                }
            }
            xmldata.Close();

            if (upsize > 0)
            {
                xmldata = new XmlTextReader(xmlfile);
                item = new string[upsize, size];
                int inner = 0;
                int outer = 0;
                while (xmldata.Read())
                {
                    if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name == eName[inner])
                    {
                        item[outer, inner] = xmldata.ReadString().Trim();
                        if ((inner + 1) == size)
                        {
                            inner = 0;
                            outer++;
                            if (outer == upsize)
                                break;
                        }
                        else
                        {
                            inner++;
                        }
                    }
                }
                xmldata.Close();

                Label CheckboxLabel = new Label();
                CheckboxLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                CheckboxLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                CheckboxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CheckboxLabel.ForeColor = System.Drawing.Color.Black;
                CheckboxLabel.Location = new System.Drawing.Point(5, 15);
                CheckboxLabel.Name = "checkLabel";
                CheckboxLabel.Size = new System.Drawing.Size(78, 30);
                CheckboxLabel.Text = "";
                browsePanel.Controls.Add(CheckboxLabel);

                Label[] bHeaderLabel = new Label[size];
                int bLastposition = 85;
                for (int m = 0; m < size; m++)
                {
                    bHeaderLabel[m] = new Label();
                    bHeaderLabel[m].BackColor = System.Drawing.SystemColors.InactiveCaption;
                    bHeaderLabel[m].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    bHeaderLabel[m].Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bHeaderLabel[m].ForeColor = System.Drawing.Color.Black;
                    bHeaderLabel[m].Location = new System.Drawing.Point(bLastposition, 15);
                    bHeaderLabel[m].Name = eName[m] + " HeaderLabel";
                    bHeaderLabel[m].Size = new System.Drawing.Size(230, 30);
                    bHeaderLabel[m].Text = eName[m];
                    bHeaderLabel[m].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    browsePanel.Controls.Add(bHeaderLabel[m]);

                    bLastposition += 232;
                }


                int upLastPosition = 47;
                int leftLastPosition = 0;
                itemLabel = new Label[upsize, size];
                bCheckBox = new CheckBox[upsize];
                bEditButton = new Button[upsize];

                for (int m = 0; m < upsize; m++)
                {

                    bCheckBox[m] = new CheckBox();
                    bCheckBox[m].Location = new System.Drawing.Point(13, 6);
                    bCheckBox[m].Name = "bCheckBox" + m.ToString();
                    bCheckBox[m].Size = new System.Drawing.Size(13, 13);
                    bCheckBox[m].Text = "";
                    bCheckBox[m].UseVisualStyleBackColor = true;
                    tableViewToolTip.SetToolTip(bCheckBox[m], "check");

                    Panel checkboxPanel = new Panel();
                    if (m % 2 == 0)
                        checkboxPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        checkboxPanel.BackColor = System.Drawing.Color.Gainsboro;
                    checkboxPanel.Location = new System.Drawing.Point(5, upLastPosition);
                    checkboxPanel.Size = new System.Drawing.Size(38, 25);
                    checkboxPanel.Controls.Add(bCheckBox[m]);
                    browsePanel.Controls.Add(checkboxPanel);

                    bEditButton[m] = new Button();
                    bEditButton[m].AutoSize = false;
                    bEditButton[m].FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    bEditButton[m].Image = global::XML_Database_System.Properties.Resources.bEdit;
                    bEditButton[m].Location = new System.Drawing.Point(7, 1);
                    bEditButton[m].Name = "editButton";
                    bEditButton[m].Size = new System.Drawing.Size(25, 23);
                    bEditButton[m].TabIndex = 0;
                    bEditButton[m].Tag = m + 1;
                    tableViewToolTip.SetToolTip(bEditButton[m], "Edit");
                    bEditButton[m].UseVisualStyleBackColor = true;
                    bEditButton[m].Click += new System.EventHandler(bEditButton_Click);

                    Panel editPanel = new Panel();
                    if (m % 2 == 0)
                        editPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
                    else
                        editPanel.BackColor = System.Drawing.Color.Gainsboro;
                    editPanel.Location = new System.Drawing.Point(45, upLastPosition);
                    editPanel.Size = new System.Drawing.Size(38, 25);
                    editPanel.Controls.Add(bEditButton[m]);
                    browsePanel.Controls.Add(editPanel);


                    leftLastPosition = 85;
                    for (int n = 0; n < size; n++)
                    {
                        itemLabel[m, n] = new Label();
                        if (m % 2 == 0)
                            itemLabel[m, n].BackColor = System.Drawing.SystemColors.ScrollBar;
                        else
                            itemLabel[m, n].BackColor = System.Drawing.Color.Gainsboro;
                        itemLabel[m, n].BorderStyle = System.Windows.Forms.BorderStyle.None;
                        itemLabel[m, n].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        itemLabel[m, n].ForeColor = System.Drawing.Color.Black;
                        itemLabel[m, n].Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
                        itemLabel[m, n].Location = new System.Drawing.Point(leftLastPosition, upLastPosition);
                        itemLabel[m, n].Size = new System.Drawing.Size(230, 25);
                        itemLabel[m, n].Text = item[m, n];
                        itemLabel[m, n].Name = item[m, n] + "label";
                        itemLabel[m, n].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        tableViewToolTip.SetToolTip(itemLabel[m, n], item[m, n]);
                        browsePanel.Controls.Add(itemLabel[m, n]);

                        leftLastPosition += 232;
                    }
                    upLastPosition += 27;
                }


                LinkLabel bCheckLinkLabel = new LinkLabel();
                bCheckLinkLabel.AutoSize = true;
                bCheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                bCheckLinkLabel.Location = new System.Drawing.Point(10, 9);
                bCheckLinkLabel.Name = "checkLinkLabel";
                bCheckLinkLabel.Size = new System.Drawing.Size(51, 13);
                bCheckLinkLabel.TabStop = true;
                bCheckLinkLabel.Text = "Check All";
                tableViewToolTip.SetToolTip(bCheckLinkLabel, "Check all");
                bCheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(bCheckLinkLabel_LinkClicked);


                Label bSlashLabel = new Label();
                bSlashLabel.AutoSize = true;
                bSlashLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bSlashLabel.Location = new System.Drawing.Point(80, 7);
                bSlashLabel.Margin = new System.Windows.Forms.Padding(0);
                bSlashLabel.Name = "slashLabel";
                bSlashLabel.Size = new System.Drawing.Size(12, 17);
                bSlashLabel.Text = "/";

                LinkLabel bUncheckLinkLabel = new LinkLabel();
                bUncheckLinkLabel.AutoSize = true;
                bUncheckLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                bUncheckLinkLabel.Location = new System.Drawing.Point(90, 9);
                bUncheckLinkLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                bUncheckLinkLabel.Name = "uncheckLinlLabel";
                bUncheckLinkLabel.Size = new System.Drawing.Size(63, 13);
                bUncheckLinkLabel.TabStop = true;
                bUncheckLinkLabel.Text = "Uncheck All";
                tableViewToolTip.SetToolTip(bUncheckLinkLabel, "Uncheck all");
                bUncheckLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(bUncheckLinkLabel_LinkClicked);

                Label bToLabel = new Label();
                bToLabel.AutoSize = true;
                bToLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bToLabel.Location = new System.Drawing.Point(180, 7);
                bToLabel.Margin = new System.Windows.Forms.Padding(0);
                bToLabel.Name = "toLabel";
                bToLabel.Size = new System.Drawing.Size(20, 13);
                bToLabel.Text = "to";

                LinkLabel bDeleteLinkLabel = new LinkLabel();
                bDeleteLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                bDeleteLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bDeleteLinkLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                bDeleteLinkLabel.Location = new System.Drawing.Point(210, 3);
                bDeleteLinkLabel.Margin = new System.Windows.Forms.Padding(3);
                bDeleteLinkLabel.Name = "deleteLinkLabel";
                bDeleteLinkLabel.Size = new System.Drawing.Size(65, 30);
                bDeleteLinkLabel.TabStop = true;
                bDeleteLinkLabel.Text = "Delete";
                bDeleteLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableViewToolTip.SetToolTip(bDeleteLinkLabel, "Delete selected field(s)");
                bDeleteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(bDeleteLinkLabel_LinkClicked);

                Panel bottomPanel = new Panel();
                bottomPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                bottomPanel.Size = new System.Drawing.Size(leftLastPosition - 8, 35);
                bottomPanel.Location = new System.Drawing.Point(5, upLastPosition);
                bottomPanel.Controls.Add(bCheckLinkLabel);
                bottomPanel.Controls.Add(bSlashLabel);
                bottomPanel.Controls.Add(bUncheckLinkLabel);
                bottomPanel.Controls.Add(bToLabel);
                bottomPanel.Controls.Add(bDeleteLinkLabel);
                browsePanel.Controls.Add(bottomPanel);
            }
            else
            {
                Label bEmptyLabel = new Label();
                bEmptyLabel.AutoSize = false;
                bEmptyLabel.BorderStyle = BorderStyle.FixedSingle;
                bEmptyLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
                bEmptyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bEmptyLabel.Location = new System.Drawing.Point(5, 10);
                bEmptyLabel.Margin = new System.Windows.Forms.Padding(0);
                bEmptyLabel.Name = "bEmptyLabel";
                bEmptyLabel.Size = new System.Drawing.Size(988, 35);
                bEmptyLabel.Text = "This table has no data yet.Insert data to the table.";
                bEmptyLabel.TextAlign = ContentAlignment.MiddleCenter;
                browsePanel.Controls.Add(bEmptyLabel);
            }
            
        }

        public void bCheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < upsize; i++)
            {
                bCheckBox[i].Checked = true;
            }
        }

        public void bUncheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < upsize; i++)
            {
                bCheckBox[i].Checked = false;
            }
        }

        public void bDeleteLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            bool checkTest = false;

            for (int i = 0; i < upsize; i++)
            {
                if (bCheckBox[i].Checked)
                    checkTest = true;
            }

            if (checkTest == true)
            {
                DialogResult dr = MessageBox.Show("This process can not be undone. Do you want to continue?", "Data delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string xmlFile = Application.StartupPath + "\\" + "Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";

                    XmlDocument xmlFiledoc = new XmlDocument();
                    xmlFiledoc.Load(xmlFile);
                    int p = 0;

                    XmlNode tableNode = xmlFiledoc.SelectSingleNode("//table");
                    foreach (XmlNode dataRowNode in xmlFiledoc.SelectNodes("//table//data_row"))
                    {
                        if (bCheckBox[p].Checked)
                        {
                            tableNode.RemoveChild(dataRowNode);
                        }
                        p++;
                    }

                    int id = 1;
                    foreach (XmlNode node in xmlFiledoc.SelectNodes("//@id"))
                    {
                        if (Int32.Parse(node.Value.Trim()) != id)
                            node.Value = id.ToString();
                        id++;
                    }

                    XmlNode rowNode = xmlFiledoc.SelectSingleNode("//@row");
                    rowNode.Value = (id - 1).ToString();

                    xmlFiledoc.Save(xmlFile);
                    tableDataDisplay();
                    tableViewTabControl.SelectedTab = browseTabPage;
                }
            }
            else
            {
                MessageBox.Show("Select at least one row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        Int32 id, upsize;

        TextBox[] eValueTextBox;
        Form editForm;
        int tagValue;
        public void bEditButton_Click(object sender, EventArgs e)
        {
            Button thisButton = sender as Button;
            tagValue = (int)thisButton.Tag;

            string xmlFile = Application.StartupPath + "//Databases//" + globalDatabaseName + "//" + globalTableName + ".xml";
            XmlDocument xmlFiledoc = new XmlDocument();
            xmlFiledoc.Load(xmlFile);


            Panel editBackPanel = new Panel();
            editBackPanel.BackColor = System.Drawing.Color.Transparent;
            editBackPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            editBackPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            Label editLabel = new Label();
            editLabel.BackColor = System.Drawing.Color.Transparent;
            editLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            editLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            editLabel.Size = new System.Drawing.Size(454, 80);
            editLabel.Location = new System.Drawing.Point(0, 0);
            editLabel.Name = "editLabel";
            editLabel.Text = "EDIT";
            editLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            editBackPanel.Controls.Add(editLabel);

            Panel editPanel = new Panel();
            editPanel.AutoScroll = true;
            editPanel.BackColor = System.Drawing.Color.White;
            editPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            editPanel.Location = new System.Drawing.Point(0, 80);


            int upLastPosition = 5;

            Label[] nameLanel = new Label[size];
            eValueTextBox = new TextBox[size];

            for (int i = 0; i < size; i++)
            {
                XmlNode currentNode = xmlFiledoc.SelectSingleNode("//table//data_row[@id='" + tagValue + "']//" + eName[i]);
                nameLanel[i] = new Label();
                nameLanel[i].BackColor = System.Drawing.Color.Transparent;
                nameLanel[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                nameLanel[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                nameLanel[i].Size = new System.Drawing.Size(200, 30);
                nameLanel[i].Location = new System.Drawing.Point(15, upLastPosition);
                nameLanel[i].Name = eName[i];
                nameLanel[i].Text = eName[i] + ":";
                nameLanel[i].TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                editPanel.Controls.Add(nameLanel[i]);

                eValueTextBox[i] = new TextBox();
                eValueTextBox[i].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                eValueTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                eValueTextBox[i].Location = new System.Drawing.Point(215, upLastPosition);
                eValueTextBox[i].Size = new System.Drawing.Size(220, 30);
                eValueTextBox[i].Text = currentNode.InnerText;
                editPanel.Controls.Add(eValueTextBox[i]);


                upLastPosition += 50;
            }

            int screenSize = Screen.PrimaryScreen.Bounds.Height - 130;
            if (upLastPosition < screenSize)
                screenSize = upLastPosition + 120;

            editPanel.Size = new System.Drawing.Size(455, screenSize - 130);
            editBackPanel.Controls.Add(editPanel);


            Button eCancelButton = new Button();
            eCancelButton.AutoSize = true;
            eCancelButton.Location = new System.Drawing.Point(25, screenSize - 40);
            eCancelButton.Name = "eCancelButton";
            eCancelButton.Size = new System.Drawing.Size(75, 25);
            eCancelButton.Text = "Cancel";
            eCancelButton.UseVisualStyleBackColor = true;
            eCancelButton.Click += new System.EventHandler(eCancelButton_Click);
            editBackPanel.Controls.Add(eCancelButton);

            Button eSaveButton = new Button();
            eSaveButton.AutoSize = true;
            eSaveButton.Location = new System.Drawing.Point(357, screenSize - 40);
            eSaveButton.Name = "eSaveButton";
            eSaveButton.Size = new System.Drawing.Size(75, 25);
            eSaveButton.Text = "Save";
            eSaveButton.UseVisualStyleBackColor = true;
            eSaveButton.Click += new EventHandler(eSaveButton_Click);
            editBackPanel.Controls.Add(eSaveButton);

            editForm = new Form();
            editForm.BackColor = System.Drawing.Color.Azure;
            editForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            editForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            editForm.StartPosition = FormStartPosition.CenterScreen;
            editForm.Size = new System.Drawing.Size(457, screenSize);
            editForm.Controls.Add(editBackPanel);

            editForm.ShowDialog();

        }


        public void eCancelButton_Click(object sender, EventArgs e)
        {
            editForm.Close();
        }

        public void eSaveButton_Click(object sender, EventArgs e)
        {
            string xmlFile = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";
            XmlDocument xmlFiledoc = new XmlDocument();
            xmlFiledoc.Load(xmlFile);

            XmlTextReader xmldata;
            bool t = true;
            for (int i = 0; i < size; i++)
            {
                XmlNode currentNode = xmlFiledoc.SelectSingleNode("//table//data_row[@id='" + tagValue + "']//" + eName[i]);
                if (eValueTextBox[i].Text.Trim() != "" && currentNode.InnerText.Trim().Equals(eValueTextBox[i].Text.Trim()))
                { }
                else
                {
                    if (eNull[i] == "false" && eValueTextBox[i].Text.Trim() == "")
                    {
                        t = false;
                        MessageBox.Show(eName[i].ToString() + " can not be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    else
                    {

                        if (eIndexType[i] == "unique")
                        {
                            xmldata = new XmlTextReader(xmlFile);
                            while (xmldata.Read())
                            {
                                if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name.Equals(eName[i].Trim()) && xmldata.ReadString().Trim().ToLower().Equals(eValueTextBox[i].Text.Trim().ToLower()) && eValueTextBox[i].Text.Trim() != "")
                                {
                                    t = false;
                                    MessageBox.Show("Value of " + eName[i].ToString() + " is already in the table ... unique value can not be repeated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            xmldata.Close();
                        }

                        if (eValueTextBox[i].Text.Trim() != "" && t == true)
                        {
                            if (eType[i] == "numeric" && DataTypeTest.IntegerType(eValueTextBox[i].Text))
                            {
                                currentNode.InnerText = eValueTextBox[i].Text.Trim();
                            }
                            else if (eType[i] == "string" && DataTypeTest.StringType(eValueTextBox[i].Text))
                            {
                                currentNode.InnerText = eValueTextBox[i].Text.Trim();
                            }
                            else if (eType[i] == "email" && DataTypeTest.EmailType(eValueTextBox[i].Text))
                            {
                                currentNode.InnerText = eValueTextBox[i].Text.Trim();
                            }
                            else
                            {
                                t = false;
                                MessageBox.Show(eName[i] + " Value data type not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (eValueTextBox[i].Text.Trim() == "" && t == true)
                        {
                            currentNode.InnerText = eValueTextBox[i].Text.Trim();
                        }
                    }

                }
            }

            for (int i = 0; i < upsize && t == true; i++)
            {
                if ((i + 1) != tagValue)
                {
                    string value1 = "";
                    string value2 = "";
                    for (int j = 0; j < size; j++)
                    {
                        if (eIndexType[j] == "primary")
                        {
                            XmlNode currentNode = xmlFiledoc.SelectSingleNode("//table//data_row[@id='" + (i + 1) + "']//" + eName[j]);
                            value1 += currentNode.InnerText.ToLower().ToString();

                            value2 += eValueTextBox[j].Text.Trim().ToLower();
                        }

                    }

                    if (value1.Equals(value2))
                    {
                        t = false;
                        MessageBox.Show("Primary key or Combination of primary keys can not be repeated.", "Primary Key Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

            }


            if (t == true)
            {
                editForm.Close();
                xmlFiledoc.Save(xmlFile);
               // tableDataDisplay();
                //tableViewTabControl.SelectedTab=browseTabPage;
                for (int i = 0; i < size; i++)
                {
                    itemLabel[tagValue - 1, i].Text = eValueTextBox[i].Text;
                    item[tagValue - 1, i] = eValueTextBox[i].Text.Trim();
                    tableViewToolTip.SetToolTip(itemLabel[tagValue-1, i], item[tagValue-1, i]);
                }


            }
        }


        //
        //..............................................goButton event handle..................................................
        //
        public void goButton_Click(object sender, EventArgs e)
        {

            string xmlfile = Application.StartupPath + "\\Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlfile);

            XmlTextReader xmldata = new XmlTextReader(xmlfile);

            while (xmldata.Read())
            {
                if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name == "table")
                {
                    id = Convert.ToInt32(xmldata.GetAttribute("row"));
                    break;
                }
            }
            xmldata.Close();
            bool t = true;
            XmlElement parentElement = null;
            parentElement = xmldoc.CreateElement("data_row");
            parentElement.SetAttribute("id", (id + 1).ToString());

            for (int i = 0; i < size && t == true; i++)
            {
                string value = "0";
                XmlElement childElement = null;

                childElement = xmldoc.CreateElement(eName[i]);

                if (eNull[i] == "false" && iValueTextBox[i].Text.Trim() == "" && eAutoIncrement[i] == "false")
                {
                    t = false;
                    MessageBox.Show(eName[i].ToString() + " can not be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                else
                {

                    if (eAutoIncrement[i] == "true" && iValueTextBox[i].Text.Trim() == "")
                    {
                        int temp = 0;
                        xmldata = new XmlTextReader(xmlfile);
                        while (xmldata.Read())
                        {

                            if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name.Equals(eName[i].ToString()))
                            {
                                value = xmldata.ReadString().Trim();
                                if (Int32.Parse(value) > temp)
                                    temp = Int32.Parse(value);
                            }
                        }
                        xmldata.Close();
                        iValueTextBox[i].Text = (temp + 1).ToString();
                    }



                    if (eIndexType[i] == "unique")
                    {
                        xmldata = new XmlTextReader(xmlfile);
                        while (xmldata.Read())
                        {
                            if (xmldata.NodeType == XmlNodeType.Element && xmldata.Name.Equals(eName[i].Trim()) && xmldata.ReadString().Trim().ToLower().Equals(iValueTextBox[i].Text.Trim().ToLower()) && iValueTextBox[i].Text.Trim() != "")
                            {
                                t = false;
                                MessageBox.Show("Value of " + eName[i].ToString() + " is already in the table ...unique value can not be repeated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                        xmldata.Close();

                    }


                    if (iValueTextBox[i].Text.Trim() != "" && t == true)
                    {
                        if (eType[i] == "numeric" && DataTypeTest.IntegerType(iValueTextBox[i].Text))
                        {
                            childElement.InnerText = iValueTextBox[i].Text.Trim();
                        }
                        else if (eType[i] == "string" && DataTypeTest.StringType(iValueTextBox[i].Text))
                        {
                            childElement.InnerText = iValueTextBox[i].Text.Trim();
                        }
                        else if (eType[i] == "email" && DataTypeTest.EmailType(iValueTextBox[i].Text))
                        {
                            childElement.InnerText = iValueTextBox[i].Text.Trim();
                        }
                        else
                        {
                            t = false;
                            MessageBox.Show(eName[i] + " Value data type not matched", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (iValueTextBox[i].Text.Trim() == "" && t == true)
                    {
                        childElement.InnerText = iValueTextBox[i].Text.Trim();
                    }

                }
               // xmldata.Dispose();//not required at all
                parentElement.AppendChild(childElement);
            }
            

            for (int i = 0; i < upsize && t==true; i++)
            {

                string value1 = "";
                string value2 = "";
                for (int j = 0; j < size; j++)
                {
                    if (eIndexType[j] == "primary")
                    {
                        XmlNode currentNode = xmldoc.SelectSingleNode("//table//data_row[@id='" + (i+1) + "']//"+eName[j]);
                        value1 += currentNode.InnerText.Trim().ToLower().ToString();

                        value2 += iValueTextBox[j].Text.Trim().ToLower();
                    }
                
                }

                if (value1.Equals(value2))
                {
                    t = false;
                    parentElement.RemoveAll();
                    MessageBox.Show("Primary key or Combination of primary keys can not be repeated.", "Primary Key Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                    
            }

            if (t == true)
            {
                xmldoc.DocumentElement.AppendChild(parentElement);
                xmldoc.DocumentElement.SetAttribute("row", (id + 1).ToString());
                xmldoc.Save(xmlfile);
                tableDataDisplay();
                tableViewTabControl.SelectedTab = insertTabPage;
                MessageBox.Show("Data successfully added.");
            }
        }


        public void sDeleteLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            string xmlSchemaFile = Application.StartupPath + "\\" + "Databases\\" + globalDatabaseName + "\\" + globalTableName + "_schema.xml";

            XmlDocument xmlSchemaFiledoc = new XmlDocument();
            xmlSchemaFiledoc.Load(xmlSchemaFile);
            int i = 0;
            bool t = false;
            bool checktest = false;
            foreach (string st in eName)
            {
                if (eCheckBox[i].Checked == false && eIndexType[i] == "primary")
                {
                    t = true;
                }

                if (eCheckBox[i].Checked)
                    checktest = true;

                i++;
            }


            if (checktest == true)
            {
                DialogResult dr = MessageBox.Show("If you delete any field then, corresponding data from data table will also be deleted. Do you want to delete?", "Delete Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {

                    if (t == true)
                    {
                        string xmlFile = Application.StartupPath + "\\" + "Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";

                        XmlDocument xmlFiledoc = new XmlDocument();
                        xmlFiledoc.Load(xmlFile);
                        i = 0;
                        foreach (string st in eName)
                        {
                            XmlNode ColumnsNode = xmlSchemaFiledoc.SelectSingleNode("//Columns");
                            if (eCheckBox[i].Checked)
                            {
                                XmlNode columnNode = xmlSchemaFiledoc.SelectSingleNode("//column[@name='" + eName[i] + "']");
                                ColumnsNode.RemoveChild(columnNode);

                                foreach (XmlNode dataNode in xmlFiledoc.SelectNodes("//table//data_row"))
                                {
                                    foreach (XmlNode childNode in dataNode.ChildNodes)
                                    {
                                        if (childNode.Name == eName[i])
                                        {
                                            dataNode.RemoveChild(childNode);
                                            break;
                                        }
                                    }
                                }

                            }
                            i++;
                            xmlFiledoc.Save(xmlFile);
                        }
                        int id = 1;
                        foreach (XmlNode node in xmlSchemaFiledoc.SelectNodes("//@index"))
                        {
                            if (Int32.Parse(node.Value.Trim()) != id)
                                node.Value = id.ToString();
                            id++;
                        }

                        XmlNode countNode = xmlSchemaFiledoc.SelectSingleNode("//@count");
                        countNode.Value = (id - 1).ToString();
                    }
                    else
                    {
                        MessageBox.Show("All primary key can not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    xmlSchemaFiledoc.Save(xmlSchemaFile);
                    if (t == true)
                    {
                        tableDataDisplay();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Field is selected to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        TextBox nameTextBox;
        ComboBox typeComboBox;
        ComboBox indexComboBox;
        CheckBox nullCheckBox;
        CheckBox autoincrementCheckBox;
        RadioButton endRadioButton;
        RadioButton beginRadioButton;
        RadioButton middleRadioButton;
        ComboBox middleComboBox;
        Panel addStructurePanel;
        public void addColumnButton_Click(object sender, EventArgs e)
        {

            Button thisButton = sender as Button;
            thisButton.Enabled = false;


            Label nameLabel = new Label();
            //nameLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nameLabel.ForeColor = System.Drawing.Color.Black;
            nameLabel.Location = new System.Drawing.Point(35, 30);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(200, 30);
            nameLabel.Text = "Field Name:";
            nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


            nameTextBox = new TextBox();
            nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nameTextBox.Location = new System.Drawing.Point(235, 30);
            nameTextBox.Size = new System.Drawing.Size(220, 30);
            tableViewToolTip.SetToolTip(nameTextBox, "Enter new Field name");


            Label typeLabel = new Label();
            // typeLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            typeLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            typeLabel.ForeColor = System.Drawing.Color.Black;
            typeLabel.Location = new System.Drawing.Point(35, 70);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new System.Drawing.Size(200, 30);
            typeLabel.Text = "Type:";
            typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            typeComboBox = new ComboBox();
            typeComboBox.Font = new Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            typeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            typeComboBox.FormattingEnabled = true;
            typeComboBox.Items.AddRange(new object[] { "numeric", "string", "email" });
            typeComboBox.Location = new Point(235, 70);
            typeComboBox.Name = "TypeComboBox";
            typeComboBox.Size = new Size(220, 30);
            tableViewToolTip.SetToolTip(typeComboBox, "Select data type");


            Label indexLabel = new Label();
            //indexLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            indexLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            indexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            indexLabel.ForeColor = System.Drawing.Color.Black;
            indexLabel.Location = new System.Drawing.Point(35, 110);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new System.Drawing.Size(200, 30);
            indexLabel.Text = "Index:";
            indexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


            indexComboBox = new ComboBox();
            indexComboBox.Font = new Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            indexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            indexComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            indexComboBox.FormattingEnabled = true;
            indexComboBox.Items.AddRange(new object[] { "", "primary", "unique" });
            indexComboBox.Location = new Point(235, 110);
            indexComboBox.Name = "indexComboBox";
            indexComboBox.Size = new Size(220, 30);
            tableViewToolTip.SetToolTip(indexComboBox, "Select index type");


            Label nullLabel = new Label();
            //nullLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            nullLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            nullLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nullLabel.ForeColor = System.Drawing.Color.Black;
            nullLabel.Location = new System.Drawing.Point(35, 150);
            nullLabel.Name = "nullLabel";
            nullLabel.Size = new System.Drawing.Size(200, 30);
            nullLabel.Text = "Null:";
            nullLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            nullCheckBox = new CheckBox();
            nullCheckBox.Appearance = System.Windows.Forms.Appearance.Normal;
            nullCheckBox.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nullCheckBox.Location = new Point(235, 160);
            nullCheckBox.Size = new Size(13, 13);
            nullCheckBox.Name = "NullCheckBox";
            nullCheckBox.TabStop = true;
            nullCheckBox.Text = "";
            nullCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            nullCheckBox.UseVisualStyleBackColor = true;
            tableViewToolTip.SetToolTip(nullCheckBox, "null");

            Label autoincrementLabel = new Label();
            //autoincrementLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            autoincrementLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            autoincrementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            autoincrementLabel.ForeColor = System.Drawing.Color.Black;
            autoincrementLabel.Location = new System.Drawing.Point(35, 190);
            autoincrementLabel.Name = "autoincrementLabel";
            autoincrementLabel.Size = new System.Drawing.Size(200, 30);
            autoincrementLabel.Text = "A.I:";
            autoincrementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            autoincrementCheckBox = new CheckBox();
            autoincrementCheckBox.Appearance = System.Windows.Forms.Appearance.Normal;
            autoincrementCheckBox.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            autoincrementCheckBox.Location = new Point(235, 195);
            autoincrementCheckBox.Size = new Size(13, 13);
            autoincrementCheckBox.Name = "autoincrementCheckBox";
            autoincrementCheckBox.TabStop = true;
            autoincrementCheckBox.Text = "";
            autoincrementCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            autoincrementCheckBox.UseVisualStyleBackColor = true;
            tableViewToolTip.SetToolTip(autoincrementCheckBox, "auto increment");


            Button addButton = new Button();
            addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addButton.Location = new System.Drawing.Point(880, 255);
            addButton.Name = "addButton";
            addButton.Size = new System.Drawing.Size(90, 30);
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            tableViewToolTip.SetToolTip(addButton, "Click to add field");
            addButton.Click += new EventHandler(addButton_Click);


            Button cancleButton = new Button();
            cancleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cancleButton.Location = new System.Drawing.Point(30, 255);
            cancleButton.Name = "cancleButton";
            cancleButton.Size = new System.Drawing.Size(90, 30);
            cancleButton.Text = "Cancle";
            cancleButton.UseVisualStyleBackColor = true;
            tableViewToolTip.SetToolTip(cancleButton, "Cancle");
            cancleButton.Click += new EventHandler(cancleButton_Click);


            Panel addPanel = new Panel();
            addPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
            addPanel.Location = new System.Drawing.Point(20, 20);
            addPanel.Size = new System.Drawing.Size(480, 230);
            addPanel.Controls.Add(nameLabel);
            addPanel.Controls.Add(nameTextBox);
            addPanel.Controls.Add(typeLabel);
            addPanel.Controls.Add(typeComboBox);
            addPanel.Controls.Add(indexLabel);
            addPanel.Controls.Add(indexComboBox);
            addPanel.Controls.Add(nullLabel);
            addPanel.Controls.Add(nullCheckBox);
            addPanel.Controls.Add(autoincrementLabel);
            addPanel.Controls.Add(autoincrementCheckBox);

            Label addLabel = new Label();
            addLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            addLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addLabel.ForeColor = System.Drawing.Color.Black;
            addLabel.Location = new System.Drawing.Point(30, 30);
            addLabel.Name = "addLabel";
            addLabel.Size = new System.Drawing.Size(150, 40);
            addLabel.Text = "Add Field at ";
            addLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            endRadioButton = new RadioButton();
            endRadioButton.AutoSize = true;
            endRadioButton.Checked = true;
            endRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            endRadioButton.Location = new System.Drawing.Point(85, 85);
            endRadioButton.Name = "endRadioButton";
            endRadioButton.Size = new System.Drawing.Size(86, 17);
            endRadioButton.TabIndex = 1;
            endRadioButton.TabStop = true;
            endRadioButton.Text = "End of the table";
            endRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            endRadioButton.UseVisualStyleBackColor = true;


            beginRadioButton = new RadioButton();
            beginRadioButton.AutoSize = true;
            beginRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            beginRadioButton.Location = new System.Drawing.Point(85, 115);
            beginRadioButton.Name = "beginRadioButton";
            beginRadioButton.Size = new System.Drawing.Size(86, 17);
            beginRadioButton.TabIndex = 2;
            beginRadioButton.TabStop = true;
            beginRadioButton.Text = "Beginning of the table";
            beginRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            beginRadioButton.UseVisualStyleBackColor = true;


            middleRadioButton = new RadioButton();
            middleRadioButton.AutoSize = true;
            middleRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            middleRadioButton.Location = new System.Drawing.Point(85, 145);
            middleRadioButton.Name = "middleRadioButton";
            middleRadioButton.Size = new System.Drawing.Size(86, 17);
            middleRadioButton.TabIndex = 2;
            middleRadioButton.TabStop = true;
            middleRadioButton.Text = "After the element ";
            middleRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            middleRadioButton.UseVisualStyleBackColor = true;
            middleRadioButton.CheckedChanged += new EventHandler(middleRadioButton_CheckedChanged);


            middleComboBox = new ComboBox();
            middleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            middleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            middleComboBox.Enabled = false;
            middleComboBox.FormattingEnabled = true;
            for (int it = 0; it < size; it++)
            {
                middleComboBox.Items.Add(eName[it]);
            }
            middleComboBox.SelectedItem = eName[0];
            middleComboBox.Location = new System.Drawing.Point(240, 142);
            middleComboBox.Name = "middleComboBox";
            middleComboBox.Size = new System.Drawing.Size(120, 20);
            tableViewToolTip.SetToolTip(middleComboBox, "Select field");


            Panel patternPanel = new Panel();
            patternPanel.BackColor = System.Drawing.SystemColors.ScrollBar;
            patternPanel.Location = new System.Drawing.Point(520, 20);
            patternPanel.Size = new System.Drawing.Size(460, 230);
            patternPanel.Controls.Add(addLabel);
            patternPanel.Controls.Add(endRadioButton);
            patternPanel.Controls.Add(beginRadioButton);
            patternPanel.Controls.Add(middleRadioButton);
            patternPanel.Controls.Add(middleComboBox);

            addStructurePanel = new Panel();
            addStructurePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            addStructurePanel.BorderStyle = BorderStyle.FixedSingle;
            addStructurePanel.Location = new System.Drawing.Point(5, slastPosition + 60);
            addStructurePanel.Size = new System.Drawing.Size(998, 290);
            addStructurePanel.Controls.Add(addPanel);
            addStructurePanel.Controls.Add(patternPanel);
            addStructurePanel.Controls.Add(addButton);
            addStructurePanel.Controls.Add(cancleButton);
            elementPanel.Controls.Add(addStructurePanel);
        }

        public void middleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (middleRadioButton.Checked)
            {
                middleComboBox.Enabled = true;
            }
            else
            {
                middleComboBox.Enabled = false;
            }
        }

        public void addButton_Click(object sender, EventArgs e)
        {
            bool t = true;
            string error = "";
            int q = 1;
            if (nameTextBox.Text == "" || !ExpressionTest.NameString(nameTextBox.Text))
            {
                t = false;
                if (nameTextBox.Text.Trim() == "")
                    error += "\nError " + q.ToString() + ": Column name is not defined.";
                else
                    error += "\nError " + q.ToString() + ": Column name is invlid(name can not start with number).";
                q++;
            }
            else
            {
                for (int p = 0; p < size; p++)
                {
                    if (nameTextBox.Text.Trim() == eName[p])
                    {
                        t = false;
                        error += "\nError " + q.ToString() + ": Column with same name already in the table.";
                        q++;
                        break;
                    }
                }
            }

            if (typeComboBox.Text == "")
            {
                t = false;
                error += "\nError " + q.ToString() + ": Column Type is not selected";
                q++;
            }

            if (autoincrementCheckBox.Checked && typeComboBox.Text != "numeric")
            {
                t = false;
                error += "\nError " + q.ToString() + ":In Column auto-increment value must be numeric type.";
                q++;
            }

            if (indexComboBox.Text == "primary" && nullCheckBox.Checked)
            {
                t = false;
                error += "\nError " + q.ToString() + ": Column Primary key can't be null";
                q++;
            }

            if (t == false)
            {
                MessageBox.Show(error, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                string xmlSchemaFile = Application.StartupPath + "\\" + "Databases\\" + globalDatabaseName + "\\" + globalTableName + "_schema.xml";

                XmlDocument xmlSchemaFiledoc = new XmlDocument();
                xmlSchemaFiledoc.Load(xmlSchemaFile);

                XmlNode columnNode = xmlSchemaFiledoc.SelectSingleNode("//@count");
                columnNode.Value = (size + 1).ToString();

                XmlNode elementNode = xmlSchemaFiledoc.SelectSingleNode("//Columns");

                XmlElement element = null;
                element = xmlSchemaFiledoc.CreateElement("column");

                if (beginRadioButton.Checked)
                    element.SetAttribute("index", "1");
                else if (middleRadioButton.Checked)
                    element.SetAttribute("index", (middleComboBox.SelectedIndex + 2).ToString());
                else
                    element.SetAttribute("index", (size + 1).ToString());
                element.SetAttribute("name", nameTextBox.Text);
                element.SetAttribute("type", typeComboBox.Text);

                if (indexComboBox.Text == "")
                    element.SetAttribute("indexType", "none");
                else
                    element.SetAttribute("indexType", indexComboBox.Text);

                if (nullCheckBox.Checked)
                    element.SetAttribute("null", "true");
                else
                    element.SetAttribute("null", "false");

                if (autoincrementCheckBox.Checked)
                    element.SetAttribute("autoincrement", "true");
                else
                    element.SetAttribute("autoincrement", "false");


                if (middleRadioButton.Checked)
                {
                    XmlNode middleNode = xmlSchemaFiledoc.SelectSingleNode("//column[@name='" + middleComboBox.Text.Trim() + "']");
                    elementNode.InsertAfter(element, middleNode);
                }
                else if (beginRadioButton.Checked)
                {
                    XmlNode beginNode = xmlSchemaFiledoc.SelectSingleNode("//column");
                    elementNode.InsertBefore(element, beginNode);
                }
                else
                {
                    elementNode.AppendChild(element);
                }

                if (beginRadioButton.Checked || middleRadioButton.Checked)
                {
                    int id = 1;
                    foreach (XmlNode node in xmlSchemaFiledoc.SelectNodes("//@index"))
                    {
                        if (Int32.Parse(node.Value.Trim()) != id)
                            node.Value = id.ToString();
                        id++;
                    }

                }

                xmlSchemaFiledoc.Save(xmlSchemaFile);

                //
                //
                string xmlFile = Application.StartupPath + "\\" + "Databases\\" + globalDatabaseName + "\\" + globalTableName + ".xml";

                XmlDocument xmlFiledoc = new XmlDocument();
                xmlFiledoc.Load(xmlFile);

                string xPath = "//table//data_row";
                foreach (XmlNode node in xmlFiledoc.SelectNodes(xPath))
                {
                    XmlElement dataElement = null;
                    dataElement = xmlFiledoc.CreateElement(nameTextBox.Text);
                    if (beginRadioButton.Checked)
                    {
                        node.InsertBefore(dataElement, node.FirstChild);

                    }
                    else if (middleRadioButton.Checked)
                    {
                        foreach (XmlNode middleNode in node.ChildNodes)
                        {
                            if (middleNode.Name == middleComboBox.Text.Trim())
                            {
                                node.InsertAfter(dataElement, middleNode);
                                break;
                            }
                        }
                    }
                    else
                    {
                        node.AppendChild(dataElement);
                    }
                }

                xmlFiledoc.Save(xmlFile);
                tableDataDisplay();
                tableViewTabControl.SelectedTab = structureTabPage;
            }


        }
        public void sCheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < size; i++)
            {
                eCheckBox[i].Checked = true;
            }
        }

        public void sUncheckLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < size; i++)
            {
                eCheckBox[i].Checked = false;
            }
        }

        public void cancleButton_Click(object sender, EventArgs e)
        {
            addStructurePanel.Dispose();
            addColumnButton.Enabled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------Common Database Component--------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------
        
        public void databaseComponent(UserControl us)
        {
            Label databaseLabel = new Label();
            databaseLabel.AutoSize = true;
            databaseLabel.Location = new System.Drawing.Point(101, 20);
            databaseLabel.Name = "databaseLabel";
            databaseLabel.Size = new System.Drawing.Size(79, 20);
            databaseLabel.Text = "Database";
            us.Controls.Add(databaseLabel);

            ComboBox databaseComboBox=new ComboBox();
            databaseComboBox.FormattingEnabled = true;
            databaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            databaseComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            databaseComboBox.Location = new System.Drawing.Point(19, 43);
            databaseComboBox.Name = "databaseComboBox";
            databaseComboBox.Size = new System.Drawing.Size(230, 28);

            foreach (string name in Directory.GetDirectories(pathOfDatabase))
            {
                databaseComboBox.Items.Add(Path.GetFileName(name));
            }
            databaseComboBox.SelectedItem = globalDatabaseName;
            databaseComboBox.SelectedIndexChanged += new EventHandler(databaseComboBox_SelectedIndexChanged);
            us.Controls.Add(databaseComboBox);

            LinkLabel databaseNameLinkLabel = new LinkLabel();
            databaseNameLinkLabel.AutoSize = true;
            databaseNameLinkLabel.Location = new System.Drawing.Point(14, 87);
            databaseNameLinkLabel.Name = "databaseNameLinkLabel";
            databaseNameLinkLabel.Size = new System.Drawing.Size(118, 20);
            databaseNameLinkLabel.TabStop = true;
            us.Controls.Add(databaseNameLinkLabel);

            Panel tableListPanel = new Panel();
            tableListPanel.Location = new System.Drawing.Point(19, 109);
            tableListPanel.Name = "tableListPanel";
            tableListPanel.Size = new System.Drawing.Size(255, 296);
            us.Controls.Add(tableListPanel);

            string pathOfTable = @"Databases\" + globalDatabaseName + "\\";

            i = Directory.GetFiles(pathOfTable, "*_schema.xml").Length;

            tableName = new string[i];
            int p = 0;
            int lastFile = 5;
            foreach (string name in Directory.GetFiles(pathOfTable, "*.xml"))
            {
                if (!name.EndsWith("_schema.xml"))
                {
                    tableName[p] = Path.GetFileNameWithoutExtension(name);

                    LinkLabel lb = new LinkLabel
                    {
                        AutoSize = true,
                        Text = tableName[p],
                        Location = new Point(30, lastFile),
                        LinkColor = Color.Black,
                        Name = tableName[p],
                        Tag = p,
                        LinkBehavior = LinkBehavior.HoverUnderline
                    };

                    lb.LinkClicked += new LinkLabelLinkClickedEventHandler(tableLinkLabel_Click);
                    tableListPanel.Controls.Add(lb);
                    lastFile += 20;
                    p++;
                }
            }

            databaseNameLinkLabel.Text = globalDatabaseName + "(" + i.ToString() + ")";

        }

        public void databaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cm = sender as ComboBox;
            globalDatabaseName = cm.Text;
            globalTableName = "";
            tableView();
        }
        string globalTableName;
        public void tableLinkLabel_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lb = sender as LinkLabel;
            globalTableName = lb.Name;
            tableDataDisplay();
        }
        //---------------------------------------------------------------------------------------------------------------------
        //------------------------------------------Common Shape Container-----------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        public void shapeContainer(UserControl us)
        {

            LineShape lineShape1 = new LineShape();
            lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            lineShape1.BorderColor = System.Drawing.Color.RoyalBlue;
            lineShape1.BorderWidth = 1;
            lineShape1.Name = "lineShape1";
            lineShape1.X1 = 260;
            lineShape1.X2 = 260;
            lineShape1.Y1 = 0;
            lineShape1.Y2 = 562;

            LineShape lineShape2 = new LineShape();
            lineShape2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            lineShape2.BorderColor = System.Drawing.Color.RoyalBlue;
            lineShape2.BorderWidth = 2;
            lineShape2.Name = "lineShape1";
            lineShape2.X1 = 0;
            lineShape2.X2 = 700;
            lineShape2.Y1 = 1;
            lineShape2.Y2 = 1;

            ShapeContainer shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            shapeContainer1.SuspendLayout();
            // 
            // shapeContainer1
            // 
            shapeContainer1.Location = new System.Drawing.Point(0, 0);
            shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            shapeContainer1.Name = "shapeContainer1";
            shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {lineShape1,lineShape2});
            shapeContainer1.Size = new System.Drawing.Size(281, 278);
            shapeContainer1.TabIndex = 0;
            shapeContainer1.TabStop = false;
           
            us.Controls.Add(shapeContainer1);
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            tablePanel.Size = new Size(this.Width-277,this.Height-130);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (globalTableName!="" && globalDatabaseName!="")
            {
                string tablepath = @"Databases\" + globalDatabaseName + "\\" + globalTableName + ".xml";
                string tableSchemaPath = @"Databases\" + globalDatabaseName + "\\" + globalTableName + "_schema.xml";
                XmlDocument xmlSchemaFiledoc = new XmlDocument();
                xmlSchemaFiledoc.Load(tableSchemaPath);

                XmlNode countNode = xmlSchemaFiledoc.SelectSingleNode("//@count");
                
                if (countNode==null)
                {
                    DialogResult tdr = MessageBox.Show("You have to define table completely. If you continue table will not exist. Do you want to continue?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (tdr == DialogResult.No)
                        e.Cancel = true;
                    else
                    {
                        File.Delete(tablepath);
                        File.Delete(tableSchemaPath);
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                        e.Cancel = true;
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            home();
        }
    }
}
