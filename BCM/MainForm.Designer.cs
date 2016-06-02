namespace BCM
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.载入对象ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存对象ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.载入配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.配置对象类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置设备类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置监测点类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.配置RMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置RIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.打开对象表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开配置表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Project130ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.初始化基础数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.编辑mstationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑mtrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑mdeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.授权ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.授权管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.objectTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.nodeEditor1 = new BCM.NodeEditor();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnDelNode = new System.Windows.Forms.Button();
            this.btnUpdNode = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.settingList = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.settingEditor1 = new BCM.SettingEditor();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnDelSetting = new System.Windows.Forms.Button();
            this.btnUpdSetting = new System.Windows.Forms.Button();
            this.btnAddSetting = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingList)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML文件|*.xml";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML文件|*.xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.数据库ToolStripMenuItem,
            this.Project130ToolStripMenuItem,
            this.授权ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.载入对象ToolStripMenuItem,
            this.保存对象ToolStripMenuItem,
            this.toolStripSeparator1,
            this.载入配置ToolStripMenuItem,
            this.保存配置ToolStripMenuItem,
            this.toolStripSeparator2,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 载入对象ToolStripMenuItem
            // 
            this.载入对象ToolStripMenuItem.Name = "载入对象ToolStripMenuItem";
            this.载入对象ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.载入对象ToolStripMenuItem.Text = "载入对象文件";
            this.载入对象ToolStripMenuItem.Click += new System.EventHandler(this.载入对象ToolStripMenuItem_Click);
            // 
            // 保存对象ToolStripMenuItem
            // 
            this.保存对象ToolStripMenuItem.Name = "保存对象ToolStripMenuItem";
            this.保存对象ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存对象ToolStripMenuItem.Text = "保存对象文件";
            this.保存对象ToolStripMenuItem.Click += new System.EventHandler(this.保存对象ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 载入配置ToolStripMenuItem
            // 
            this.载入配置ToolStripMenuItem.Name = "载入配置ToolStripMenuItem";
            this.载入配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.载入配置ToolStripMenuItem.Text = "载入配置文件";
            this.载入配置ToolStripMenuItem.Click += new System.EventHandler(this.载入配置ToolStripMenuItem_Click);
            // 
            // 保存配置ToolStripMenuItem
            // 
            this.保存配置ToolStripMenuItem.Name = "保存配置ToolStripMenuItem";
            this.保存配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存配置ToolStripMenuItem.Text = "保存配置文件";
            this.保存配置ToolStripMenuItem.Click += new System.EventHandler(this.保存配置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 数据库ToolStripMenuItem
            // 
            this.数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接配置ToolStripMenuItem,
            this.toolStripSeparator7,
            this.配置对象类型ToolStripMenuItem,
            this.配置设备类型ToolStripMenuItem,
            this.配置监测点类型ToolStripMenuItem,
            this.toolStripSeparator6,
            this.配置RMToolStripMenuItem,
            this.配置RIToolStripMenuItem,
            this.toolStripSeparator5,
            this.打开对象表ToolStripMenuItem,
            this.打开配置表ToolStripMenuItem});
            this.数据库ToolStripMenuItem.Name = "数据库ToolStripMenuItem";
            this.数据库ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.数据库ToolStripMenuItem.Text = "数据库";
            // 
            // 连接配置ToolStripMenuItem
            // 
            this.连接配置ToolStripMenuItem.Name = "连接配置ToolStripMenuItem";
            this.连接配置ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.连接配置ToolStripMenuItem.Text = "连接配置";
            this.连接配置ToolStripMenuItem.Click += new System.EventHandler(this.连接配置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(151, 6);
            // 
            // 配置对象类型ToolStripMenuItem
            // 
            this.配置对象类型ToolStripMenuItem.Name = "配置对象类型ToolStripMenuItem";
            this.配置对象类型ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置对象类型ToolStripMenuItem.Text = "配置对象类型";
            this.配置对象类型ToolStripMenuItem.Click += new System.EventHandler(this.配置对象类型ToolStripMenuItem_Click);
            // 
            // 配置设备类型ToolStripMenuItem
            // 
            this.配置设备类型ToolStripMenuItem.Name = "配置设备类型ToolStripMenuItem";
            this.配置设备类型ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置设备类型ToolStripMenuItem.Text = "配置设备类型";
            this.配置设备类型ToolStripMenuItem.Click += new System.EventHandler(this.配置设备类型ToolStripMenuItem_Click);
            // 
            // 配置监测点类型ToolStripMenuItem
            // 
            this.配置监测点类型ToolStripMenuItem.Name = "配置监测点类型ToolStripMenuItem";
            this.配置监测点类型ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置监测点类型ToolStripMenuItem.Text = "配置监测点类型";
            this.配置监测点类型ToolStripMenuItem.Click += new System.EventHandler(this.配置监测点类型ToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(151, 6);
            // 
            // 配置RMToolStripMenuItem
            // 
            this.配置RMToolStripMenuItem.Name = "配置RMToolStripMenuItem";
            this.配置RMToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置RMToolStripMenuItem.Text = "配置RM";
            this.配置RMToolStripMenuItem.Click += new System.EventHandler(this.配置RMToolStripMenuItem_Click);
            // 
            // 配置RIToolStripMenuItem
            // 
            this.配置RIToolStripMenuItem.Name = "配置RIToolStripMenuItem";
            this.配置RIToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置RIToolStripMenuItem.Text = "配置RI";
            this.配置RIToolStripMenuItem.Click += new System.EventHandler(this.配置RIToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // 打开对象表ToolStripMenuItem
            // 
            this.打开对象表ToolStripMenuItem.Name = "打开对象表ToolStripMenuItem";
            this.打开对象表ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.打开对象表ToolStripMenuItem.Text = "打开对象表";
            this.打开对象表ToolStripMenuItem.Click += new System.EventHandler(this.打开对象表ToolStripMenuItem_Click);
            // 
            // 打开配置表ToolStripMenuItem
            // 
            this.打开配置表ToolStripMenuItem.Name = "打开配置表ToolStripMenuItem";
            this.打开配置表ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.打开配置表ToolStripMenuItem.Text = "打开配置表";
            this.打开配置表ToolStripMenuItem.Click += new System.EventHandler(this.打开配置表ToolStripMenuItem_Click);
            // 
            // Project130ToolStripMenuItem
            // 
            this.Project130ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.初始化基础数据ToolStripMenuItem,
            this.toolStripSeparator3,
            this.编辑mstationToolStripMenuItem,
            this.编辑mtrainToolStripMenuItem,
            this.编辑mdeviceToolStripMenuItem});
            this.Project130ToolStripMenuItem.Name = "Project130ToolStripMenuItem";
            this.Project130ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.Project130ToolStripMenuItem.Text = "130项目";
            // 
            // 初始化基础数据ToolStripMenuItem
            // 
            this.初始化基础数据ToolStripMenuItem.Enabled = false;
            this.初始化基础数据ToolStripMenuItem.Name = "初始化基础数据ToolStripMenuItem";
            this.初始化基础数据ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.初始化基础数据ToolStripMenuItem.Text = "初始化基础数据";
            this.初始化基础数据ToolStripMenuItem.Click += new System.EventHandler(this.初始化基础数据ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // 编辑mstationToolStripMenuItem
            // 
            this.编辑mstationToolStripMenuItem.Name = "编辑mstationToolStripMenuItem";
            this.编辑mstationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.编辑mstationToolStripMenuItem.Text = "编辑监测站";
            this.编辑mstationToolStripMenuItem.Click += new System.EventHandler(this.编辑mstationToolStripMenuItem_Click);
            // 
            // 编辑mtrainToolStripMenuItem
            // 
            this.编辑mtrainToolStripMenuItem.Name = "编辑mtrainToolStripMenuItem";
            this.编辑mtrainToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.编辑mtrainToolStripMenuItem.Text = "编辑列车";
            this.编辑mtrainToolStripMenuItem.Click += new System.EventHandler(this.编辑mtrainToolStripMenuItem_Click);
            // 
            // 编辑mdeviceToolStripMenuItem
            // 
            this.编辑mdeviceToolStripMenuItem.Name = "编辑mdeviceToolStripMenuItem";
            this.编辑mdeviceToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.编辑mdeviceToolStripMenuItem.Text = "编辑设备";
            this.编辑mdeviceToolStripMenuItem.Click += new System.EventHandler(this.编辑mdeviceToolStripMenuItem_Click);
            // 
            // 授权ToolStripMenuItem
            // 
            this.授权ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.授权管理ToolStripMenuItem});
            this.授权ToolStripMenuItem.Name = "授权ToolStripMenuItem";
            this.授权ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.授权ToolStripMenuItem.Text = "授权";
            // 
            // 授权管理ToolStripMenuItem
            // 
            this.授权管理ToolStripMenuItem.Name = "授权管理ToolStripMenuItem";
            this.授权管理ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.授权管理ToolStripMenuItem.Text = "授权管理";
            this.授权管理ToolStripMenuItem.Click += new System.EventHandler(this.授权管理ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 554);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(980, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel2.Text = "  ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(125, 17);
            this.toolStripStatusLabel3.Text = "尚未打开文件或数据库";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 530);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(980, 530);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(972, 505);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "对象管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.objectTree);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(654, 499);
            this.panel3.TabIndex = 1;
            // 
            // objectTree
            // 
            this.objectTree.ContextMenuStrip = this.contextMenuStrip1;
            this.objectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTree.Location = new System.Drawing.Point(0, 0);
            this.objectTree.Name = "objectTree";
            this.objectTree.Size = new System.Drawing.Size(654, 499);
            this.objectTree.TabIndex = 0;
            this.objectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.objectTree_AfterSelect);
            this.objectTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectTree_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem,
            this.toolStripSeparator4,
            this.刷新ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(91, 6);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(657, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 499);
            this.panel2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.nodeEditor1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(312, 449);
            this.panel7.TabIndex = 1;
            // 
            // nodeEditor1
            // 
            this.nodeEditor1.AutoScroll = true;
            this.nodeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeEditor1.Location = new System.Drawing.Point(0, 0);
            this.nodeEditor1.MinimumSize = new System.Drawing.Size(180, 55);
            this.nodeEditor1.Name = "nodeEditor1";
            this.nodeEditor1.Size = new System.Drawing.Size(312, 449);
            this.nodeEditor1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnDelNode);
            this.panel6.Controls.Add(this.btnUpdNode);
            this.panel6.Controls.Add(this.btnAddNode);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 449);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(312, 50);
            this.panel6.TabIndex = 0;
            this.panel6.Visible = false;
            // 
            // btnDelNode
            // 
            this.btnDelNode.Image = global::BCM.Properties.Resources.delete;
            this.btnDelNode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelNode.Location = new System.Drawing.Point(222, 5);
            this.btnDelNode.Name = "btnDelNode";
            this.btnDelNode.Size = new System.Drawing.Size(75, 40);
            this.btnDelNode.TabIndex = 2;
            this.btnDelNode.Text = "删除";
            this.btnDelNode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnDelNode, "从数据库中删除对象");
            this.btnDelNode.UseVisualStyleBackColor = true;
            this.btnDelNode.Click += new System.EventHandler(this.btnDelNode_Click);
            // 
            // btnUpdNode
            // 
            this.btnUpdNode.Image = global::BCM.Properties.Resources.update;
            this.btnUpdNode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdNode.Location = new System.Drawing.Point(119, 5);
            this.btnUpdNode.Name = "btnUpdNode";
            this.btnUpdNode.Size = new System.Drawing.Size(75, 40);
            this.btnUpdNode.TabIndex = 1;
            this.btnUpdNode.Text = "修改";
            this.btnUpdNode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnUpdNode, "更新对象到数据库");
            this.btnUpdNode.UseVisualStyleBackColor = true;
            this.btnUpdNode.Click += new System.EventHandler(this.btnUpdNode_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Image = global::BCM.Properties.Resources.insert;
            this.btnAddNode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNode.Location = new System.Drawing.Point(15, 5);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(75, 40);
            this.btnAddNode.TabIndex = 0;
            this.btnAddNode.Text = "添加";
            this.btnAddNode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnAddNode, "添加对象到数据库");
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(972, 505);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.settingList);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(654, 499);
            this.panel5.TabIndex = 1;
            // 
            // settingList
            // 
            this.settingList.AllowUserToAddRows = false;
            this.settingList.AllowUserToDeleteRows = false;
            this.settingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.settingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingList.Location = new System.Drawing.Point(0, 0);
            this.settingList.Name = "settingList";
            this.settingList.ReadOnly = true;
            this.settingList.RowTemplate.Height = 23;
            this.settingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.settingList.Size = new System.Drawing.Size(654, 499);
            this.settingList.TabIndex = 0;
            this.settingList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.settingList_CellClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(657, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(312, 499);
            this.panel4.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.settingEditor1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(312, 449);
            this.panel8.TabIndex = 3;
            // 
            // settingEditor1
            // 
            this.settingEditor1.AutoScroll = true;
            this.settingEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingEditor1.Location = new System.Drawing.Point(0, 0);
            this.settingEditor1.MinimumSize = new System.Drawing.Size(180, 55);
            this.settingEditor1.Name = "settingEditor1";
            this.settingEditor1.Size = new System.Drawing.Size(312, 449);
            this.settingEditor1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnDelSetting);
            this.panel9.Controls.Add(this.btnUpdSetting);
            this.panel9.Controls.Add(this.btnAddSetting);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 449);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(312, 50);
            this.panel9.TabIndex = 2;
            // 
            // btnDelSetting
            // 
            this.btnDelSetting.Image = global::BCM.Properties.Resources.delete;
            this.btnDelSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelSetting.Location = new System.Drawing.Point(222, 4);
            this.btnDelSetting.Name = "btnDelSetting";
            this.btnDelSetting.Size = new System.Drawing.Size(75, 40);
            this.btnDelSetting.TabIndex = 2;
            this.btnDelSetting.Text = "删除";
            this.btnDelSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnDelSetting, "从数据库中删除配置");
            this.btnDelSetting.UseVisualStyleBackColor = true;
            this.btnDelSetting.Click += new System.EventHandler(this.btnDelSetting_Click);
            // 
            // btnUpdSetting
            // 
            this.btnUpdSetting.Image = global::BCM.Properties.Resources.update;
            this.btnUpdSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdSetting.Location = new System.Drawing.Point(119, 5);
            this.btnUpdSetting.Name = "btnUpdSetting";
            this.btnUpdSetting.Size = new System.Drawing.Size(75, 40);
            this.btnUpdSetting.TabIndex = 1;
            this.btnUpdSetting.Text = "修改";
            this.btnUpdSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnUpdSetting, "更新配置到数据库");
            this.btnUpdSetting.UseVisualStyleBackColor = true;
            this.btnUpdSetting.Click += new System.EventHandler(this.btnUpdSetting_Click);
            // 
            // btnAddSetting
            // 
            this.btnAddSetting.Image = global::BCM.Properties.Resources.insert;
            this.btnAddSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSetting.Location = new System.Drawing.Point(16, 5);
            this.btnAddSetting.Name = "btnAddSetting";
            this.btnAddSetting.Size = new System.Drawing.Size(75, 40);
            this.btnAddSetting.TabIndex = 0;
            this.btnAddSetting.Text = "添加";
            this.btnAddSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnAddSetting, "添加配置到数据库");
            this.btnAddSetting.UseVisualStyleBackColor = true;
            this.btnAddSetting.Click += new System.EventHandler(this.btnAddSetting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 576);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(660, 580);
            this.Name = "MainForm";
            this.Text = "系统基础配置管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 载入对象ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存对象ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连接配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开对象表ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView objectTree;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripMenuItem 授权ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 授权管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 载入配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开配置表ToolStripMenuItem;
        private System.Windows.Forms.DataGridView settingList;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnDelNode;
        private System.Windows.Forms.Button btnUpdNode;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnDelSetting;
        private System.Windows.Forms.Button btnUpdSetting;
        private System.Windows.Forms.Button btnAddSetting;
        private NodeEditor nodeEditor1;
        private SettingEditor settingEditor1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 配置对象类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置设备类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置监测点类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 配置RMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置RIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem Project130ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 初始化基础数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 编辑mstationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑mtrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑mdeviceToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;

    }
}