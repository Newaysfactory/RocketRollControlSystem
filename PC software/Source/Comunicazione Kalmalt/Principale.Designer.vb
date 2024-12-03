Imports System.Globalization      'Per il cambio lingua
Imports System.Threading          'Per il cambio lingua

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Principale
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principale))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripConnesso = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusXY = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApriToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalvaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EsportaDatiGrezziToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EsciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StrumentiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetMemoriaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TryFloatingPointNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AiutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformazioniToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerStart = New System.Windows.Forms.Timer(Me.components)
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TimerStreaming = New System.Windows.Forms.Timer(Me.components)
        Me.DlgSalva = New System.Windows.Forms.SaveFileDialog()
        Me.DlgApri = New System.Windows.Forms.OpenFileDialog()
        Me.DlgEsporta = New System.Windows.Forms.SaveFileDialog()
        Me.TabStreaming = New System.Windows.Forms.TabPage()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.LabVarianza = New System.Windows.Forms.Label()
        Me.LabDevStand = New System.Windows.Forms.Label()
        Me.LabMedia = New System.Windows.Forms.Label()
        Me.LabNCampioni = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.PauseStreaming = New System.Windows.Forms.Button()
        Me.StopStreaming = New System.Windows.Forms.Button()
        Me.PlayStreaming = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ComboBoxCanale = New System.Windows.Forms.ComboBox()
        Me.GraficoStreaming = New ZedGraph.ZedGraphControl()
        Me.TabGrafico = New System.Windows.Forms.TabPage()
        Me.Button_reset_y = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tb_titolo = New System.Windows.Forms.TextBox()
        Me.lbl_gyro_z_zero = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lbl_gyro_x_zero = New System.Windows.Forms.Label()
        Me.lbl_gyro_y_zero = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LabelFileName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.CBox_pos_servo = New System.Windows.Forms.CheckBox()
        Me.CBox_acc_angolare = New System.Windows.Forms.CheckBox()
        Me.CBox_roll = New System.Windows.Forms.CheckBox()
        Me.CBox_vel_angolare = New System.Windows.Forms.CheckBox()
        Me.CBox_yaw = New System.Windows.Forms.CheckBox()
        Me.CBox_pos_angolare = New System.Windows.Forms.CheckBox()
        Me.CBox_pitch = New System.Windows.Forms.CheckBox()
        Me.CBox_accelerazione = New System.Windows.Forms.CheckBox()
        Me.CBox_analog_aux = New System.Windows.Forms.CheckBox()
        Me.CBox_velocità = New System.Windows.Forms.CheckBox()
        Me.CBox_barometro = New System.Windows.Forms.CheckBox()
        Me.CBox_quota = New System.Windows.Forms.CheckBox()
        Me.CBox_accelerometro = New System.Windows.Forms.CheckBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GraficoDati = New ZedGraph.ZedGraphControl()
        Me.lbl_baro_zero = New System.Windows.Forms.Label()
        Me.lbl_acc_zero = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.BTNContrai = New System.Windows.Forms.Button()
        Me.BTNEspandi = New System.Windows.Forms.Button()
        Me.TabRiassunto = New System.Windows.Forms.TabPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label_conversione = New System.Windows.Forms.Label()
        Me.GroupBoxServo = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_disattiva_servo = New System.Windows.Forms.Button()
        Me.Lbl_servo_state = New System.Windows.Forms.Label()
        Me.Button_attiva_servo = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.LblValoreFramLettoHex = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.LblValoreFramLettoDec = New System.Windows.Forms.Label()
        Me.TextBox_indirizzo_lettura = New System.Windows.Forms.TextBox()
        Me.Button_leggi_FRAM = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.barometro = New System.Windows.Forms.Label()
        Me.accelerometro = New System.Windows.Forms.Label()
        Me.batteria = New System.Windows.Forms.Label()
        Me.analog_aux = New System.Windows.Forms.Label()
        Me.R = New System.Windows.Forms.Label()
        Me.P = New System.Windows.Forms.Label()
        Me.Q = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox_Kd = New System.Windows.Forms.TextBox()
        Me.TextBox_Ki = New System.Windows.Forms.TextBox()
        Me.TextBox_Kp = New System.Windows.Forms.TextBox()
        Me.TextBox_rotazione_per_programma_1 = New System.Windows.Forms.TextBox()
        Me.TextBox_trasferenza_rad_ms = New System.Windows.Forms.TextBox()
        Me.TextBox_servo_max = New System.Windows.Forms.TextBox()
        Me.TextBox_programma = New System.Windows.Forms.TextBox()
        Me.TextBox_servo_min = New System.Windows.Forms.TextBox()
        Me.low_battery_value = New System.Windows.Forms.Label()
        Me.TextBox_servo_zero = New System.Windows.Forms.TextBox()
        Me.TextBox_low_battery_value = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.programma = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.P0 = New System.Windows.Forms.Label()
        Me.TextBox_Q0 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.TextBox_P0 = New System.Windows.Forms.TextBox()
        Me.TextBox_R0 = New System.Windows.Forms.TextBox()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TabStreaming.SuspendLayout()
        Me.TabGrafico.SuspendLayout()
        Me.TabRiassunto.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxServo.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripConnesso, Me.ToolStripInfo, Me.ToolStripStatusXY})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 531)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(922, 28)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripConnesso
        '
        Me.ToolStripConnesso.AutoSize = False
        Me.ToolStripConnesso.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripConnesso.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripConnesso.Name = "ToolStripConnesso"
        Me.ToolStripConnesso.Size = New System.Drawing.Size(84, 23)
        Me.ToolStripConnesso.Text = "Disconnected"
        Me.ToolStripConnesso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripInfo
        '
        Me.ToolStripInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripInfo.Name = "ToolStripInfo"
        Me.ToolStripInfo.Size = New System.Drawing.Size(270, 23)
        Me.ToolStripInfo.Text = "Plug in USB cable and turn on the flight computer"
        '
        'ToolStripStatusXY
        '
        Me.ToolStripStatusXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusXY.Name = "ToolStripStatusXY"
        Me.ToolStripStatusXY.Size = New System.Drawing.Size(553, 23)
        Me.ToolStripStatusXY.Spring = True
        Me.ToolStripStatusXY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.StrumentiToolStripMenuItem, Me.AiutoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.Size = New System.Drawing.Size(922, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApriToolStripMenuItem, Me.SalvaToolStripMenuItem, Me.EsportaDatiGrezziToolStripMenuItem, Me.EsciToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ApriToolStripMenuItem
        '
        Me.ApriToolStripMenuItem.Name = "ApriToolStripMenuItem"
        Me.ApriToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.ApriToolStripMenuItem.Text = "Open flight..."
        '
        'SalvaToolStripMenuItem
        '
        Me.SalvaToolStripMenuItem.Name = "SalvaToolStripMenuItem"
        Me.SalvaToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.SalvaToolStripMenuItem.Text = "Save flight..."
        '
        'EsportaDatiGrezziToolStripMenuItem
        '
        Me.EsportaDatiGrezziToolStripMenuItem.Name = "EsportaDatiGrezziToolStripMenuItem"
        Me.EsportaDatiGrezziToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.EsportaDatiGrezziToolStripMenuItem.Text = "Export data"
        '
        'EsciToolStripMenuItem
        '
        Me.EsciToolStripMenuItem.Name = "EsciToolStripMenuItem"
        Me.EsciToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.EsciToolStripMenuItem.Text = "Exit"
        '
        'StrumentiToolStripMenuItem
        '
        Me.StrumentiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetMemoriaToolStripMenuItem, Me.TryFloatingPointNumberToolStripMenuItem})
        Me.StrumentiToolStripMenuItem.Name = "StrumentiToolStripMenuItem"
        Me.StrumentiToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.StrumentiToolStripMenuItem.Text = "Tools"
        '
        'ResetMemoriaToolStripMenuItem
        '
        Me.ResetMemoriaToolStripMenuItem.Name = "ResetMemoriaToolStripMenuItem"
        Me.ResetMemoriaToolStripMenuItem.Size = New System.Drawing.Size(314, 22)
        Me.ResetMemoriaToolStripMenuItem.Text = "Clear memory (delete all flights)"
        '
        'TryFloatingPointNumberToolStripMenuItem
        '
        Me.TryFloatingPointNumberToolStripMenuItem.Name = "TryFloatingPointNumberToolStripMenuItem"
        Me.TryFloatingPointNumberToolStripMenuItem.Size = New System.Drawing.Size(314, 22)
        Me.TryFloatingPointNumberToolStripMenuItem.Text = "Receive the floating point number 12345,6789"
        '
        'AiutoToolStripMenuItem
        '
        Me.AiutoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformazioniToolStripMenuItem})
        Me.AiutoToolStripMenuItem.Name = "AiutoToolStripMenuItem"
        Me.AiutoToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.AiutoToolStripMenuItem.Text = "Help"
        '
        'InformazioniToolStripMenuItem
        '
        Me.InformazioniToolStripMenuItem.Name = "InformazioniToolStripMenuItem"
        Me.InformazioniToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.InformazioniToolStripMenuItem.Text = "About"
        '
        'TimerStart
        '
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(0, 0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(200, 100)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        '
        'TimerStreaming
        '
        Me.TimerStreaming.Interval = 70
        '
        'DlgSalva
        '
        Me.DlgSalva.DefaultExt = "rcs"
        Me.DlgSalva.Filter = "KalmAlt File (*.rcs)|*.rcs"
        '
        'DlgApri
        '
        Me.DlgApri.Filter = "KalmAlt File (*.rcs)|*.rcs|Tutti i file (*.*)|*.*"
        '
        'DlgEsporta
        '
        Me.DlgEsporta.DefaultExt = "txt"
        Me.DlgEsporta.Filter = "Text file (*.txt)|*.txt|Tutti i file (*.*)|*.*"
        '
        'TabStreaming
        '
        Me.TabStreaming.BackColor = System.Drawing.SystemColors.Control
        Me.TabStreaming.Controls.Add(Me.Label50)
        Me.TabStreaming.Controls.Add(Me.LabVarianza)
        Me.TabStreaming.Controls.Add(Me.LabDevStand)
        Me.TabStreaming.Controls.Add(Me.LabMedia)
        Me.TabStreaming.Controls.Add(Me.LabNCampioni)
        Me.TabStreaming.Controls.Add(Me.Label17)
        Me.TabStreaming.Controls.Add(Me.Label16)
        Me.TabStreaming.Controls.Add(Me.Label15)
        Me.TabStreaming.Controls.Add(Me.Label14)
        Me.TabStreaming.Controls.Add(Me.PauseStreaming)
        Me.TabStreaming.Controls.Add(Me.StopStreaming)
        Me.TabStreaming.Controls.Add(Me.PlayStreaming)
        Me.TabStreaming.Controls.Add(Me.Label12)
        Me.TabStreaming.Controls.Add(Me.ComboBoxCanale)
        Me.TabStreaming.Controls.Add(Me.GraficoStreaming)
        Me.TabStreaming.Location = New System.Drawing.Point(4, 22)
        Me.TabStreaming.Name = "TabStreaming"
        Me.TabStreaming.Padding = New System.Windows.Forms.Padding(3)
        Me.TabStreaming.Size = New System.Drawing.Size(914, 481)
        Me.TabStreaming.TabIndex = 2
        Me.TabStreaming.Text = "Sensor streaming"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(485, 11)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(404, 91)
        Me.Label50.TabIndex = 16
        Me.Label50.Text = resources.GetString("Label50.Text")
        '
        'LabVarianza
        '
        Me.LabVarianza.AutoSize = True
        Me.LabVarianza.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabVarianza.Location = New System.Drawing.Point(405, 79)
        Me.LabVarianza.Name = "LabVarianza"
        Me.LabVarianza.Size = New System.Drawing.Size(78, 13)
        Me.LabVarianza.TabIndex = 15
        Me.LabVarianza.Text = "N° di campioni:"
        '
        'LabDevStand
        '
        Me.LabDevStand.AutoSize = True
        Me.LabDevStand.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabDevStand.Location = New System.Drawing.Point(405, 61)
        Me.LabDevStand.Name = "LabDevStand"
        Me.LabDevStand.Size = New System.Drawing.Size(78, 13)
        Me.LabDevStand.TabIndex = 14
        Me.LabDevStand.Text = "N° di campioni:"
        '
        'LabMedia
        '
        Me.LabMedia.AutoSize = True
        Me.LabMedia.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabMedia.Location = New System.Drawing.Point(405, 43)
        Me.LabMedia.Name = "LabMedia"
        Me.LabMedia.Size = New System.Drawing.Size(78, 13)
        Me.LabMedia.TabIndex = 13
        Me.LabMedia.Text = "N° di campioni:"
        '
        'LabNCampioni
        '
        Me.LabNCampioni.AutoSize = True
        Me.LabNCampioni.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabNCampioni.Location = New System.Drawing.Point(405, 26)
        Me.LabNCampioni.Name = "LabNCampioni"
        Me.LabNCampioni.Size = New System.Drawing.Size(78, 13)
        Me.LabNCampioni.TabIndex = 12
        Me.LabNCampioni.Text = "N° di campioni:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label17.Location = New System.Drawing.Point(352, 79)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 13)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Variance:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(305, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(99, 13)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Standard deviation:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(367, 43)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "Mean:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(321, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Sample number:"
        '
        'PauseStreaming
        '
        Me.PauseStreaming.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.PauseStreaming.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PauseStreaming.Location = New System.Drawing.Point(216, 49)
        Me.PauseStreaming.Name = "PauseStreaming"
        Me.PauseStreaming.Size = New System.Drawing.Size(56, 21)
        Me.PauseStreaming.TabIndex = 5
        Me.PauseStreaming.Text = "Pause"
        Me.PauseStreaming.UseVisualStyleBackColor = True
        '
        'StopStreaming
        '
        Me.StopStreaming.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.StopStreaming.Location = New System.Drawing.Point(216, 76)
        Me.StopStreaming.Name = "StopStreaming"
        Me.StopStreaming.Size = New System.Drawing.Size(56, 21)
        Me.StopStreaming.TabIndex = 4
        Me.StopStreaming.Text = "Stop"
        Me.StopStreaming.UseVisualStyleBackColor = True
        '
        'PlayStreaming
        '
        Me.PlayStreaming.Location = New System.Drawing.Point(216, 22)
        Me.PlayStreaming.Name = "PlayStreaming"
        Me.PlayStreaming.Size = New System.Drawing.Size(56, 21)
        Me.PlayStreaming.TabIndex = 3
        Me.PlayStreaming.Text = "Play"
        Me.PlayStreaming.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Channel:"
        '
        'ComboBoxCanale
        '
        Me.ComboBoxCanale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCanale.FormattingEnabled = True
        Me.ComboBoxCanale.Items.AddRange(New Object() {"Accelerometer [ADC]", "Accelerometer [m/s^2]", "Pressure sensor [ADC]", "Pressure sensor [m]", "Analog Aux [ADC]", "Gyro X ( - yaw) [ADC]", "Gyro X ( - yaw) [rad/s]", "Gyro X ( - yaw) [deg/s]", "Gyro Y (pitch) [ADC]", "Gyro Y (pitch) [rad/s]", "Gyro Y (pitch) [deg/s]", "Gyro Z (roll) [ADC]", "Gyro Z (roll) [rad/s]", "Gyro Z (roll) [deg/s]", "Battery [ADC]"})
        Me.ComboBoxCanale.Location = New System.Drawing.Point(65, 22)
        Me.ComboBoxCanale.Name = "ComboBoxCanale"
        Me.ComboBoxCanale.Size = New System.Drawing.Size(145, 21)
        Me.ComboBoxCanale.TabIndex = 1
        '
        'GraficoStreaming
        '
        Me.GraficoStreaming.IsAutoScrollRange = True
        Me.GraficoStreaming.Location = New System.Drawing.Point(5, 115)
        Me.GraficoStreaming.Name = "GraficoStreaming"
        Me.GraficoStreaming.PanButtons = System.Windows.Forms.MouseButtons.None
        Me.GraficoStreaming.PanButtons2 = System.Windows.Forms.MouseButtons.Left
        Me.GraficoStreaming.ScrollGrace = 0.0R
        Me.GraficoStreaming.ScrollMaxX = 0.0R
        Me.GraficoStreaming.ScrollMaxY = 0.0R
        Me.GraficoStreaming.ScrollMaxY2 = 0.0R
        Me.GraficoStreaming.ScrollMinX = 0.0R
        Me.GraficoStreaming.ScrollMinY = 0.0R
        Me.GraficoStreaming.ScrollMinY2 = 0.0R
        Me.GraficoStreaming.Size = New System.Drawing.Size(903, 360)
        Me.GraficoStreaming.TabIndex = 0
        Me.GraficoStreaming.ZoomButtons = System.Windows.Forms.MouseButtons.Middle
        '
        'TabGrafico
        '
        Me.TabGrafico.BackColor = System.Drawing.SystemColors.Control
        Me.TabGrafico.Controls.Add(Me.Button_reset_y)
        Me.TabGrafico.Controls.Add(Me.Label5)
        Me.TabGrafico.Controls.Add(Me.tb_titolo)
        Me.TabGrafico.Controls.Add(Me.lbl_gyro_z_zero)
        Me.TabGrafico.Controls.Add(Me.Label10)
        Me.TabGrafico.Controls.Add(Me.lbl_gyro_x_zero)
        Me.TabGrafico.Controls.Add(Me.lbl_gyro_y_zero)
        Me.TabGrafico.Controls.Add(Me.Label7)
        Me.TabGrafico.Controls.Add(Me.Label8)
        Me.TabGrafico.Controls.Add(Me.LabelFileName)
        Me.TabGrafico.Controls.Add(Me.Label4)
        Me.TabGrafico.Controls.Add(Me.Button5)
        Me.TabGrafico.Controls.Add(Me.CBox_pos_servo)
        Me.TabGrafico.Controls.Add(Me.CBox_acc_angolare)
        Me.TabGrafico.Controls.Add(Me.CBox_roll)
        Me.TabGrafico.Controls.Add(Me.CBox_vel_angolare)
        Me.TabGrafico.Controls.Add(Me.CBox_yaw)
        Me.TabGrafico.Controls.Add(Me.CBox_pos_angolare)
        Me.TabGrafico.Controls.Add(Me.CBox_pitch)
        Me.TabGrafico.Controls.Add(Me.CBox_accelerazione)
        Me.TabGrafico.Controls.Add(Me.CBox_analog_aux)
        Me.TabGrafico.Controls.Add(Me.CBox_velocità)
        Me.TabGrafico.Controls.Add(Me.CBox_barometro)
        Me.TabGrafico.Controls.Add(Me.CBox_quota)
        Me.TabGrafico.Controls.Add(Me.CBox_accelerometro)
        Me.TabGrafico.Controls.Add(Me.Button3)
        Me.TabGrafico.Controls.Add(Me.GraficoDati)
        Me.TabGrafico.Controls.Add(Me.lbl_baro_zero)
        Me.TabGrafico.Controls.Add(Me.lbl_acc_zero)
        Me.TabGrafico.Controls.Add(Me.Label13)
        Me.TabGrafico.Controls.Add(Me.Label26)
        Me.TabGrafico.Controls.Add(Me.Label27)
        Me.TabGrafico.Controls.Add(Me.BTNContrai)
        Me.TabGrafico.Controls.Add(Me.BTNEspandi)
        Me.TabGrafico.Location = New System.Drawing.Point(4, 22)
        Me.TabGrafico.Name = "TabGrafico"
        Me.TabGrafico.Padding = New System.Windows.Forms.Padding(3)
        Me.TabGrafico.Size = New System.Drawing.Size(914, 481)
        Me.TabGrafico.TabIndex = 1
        Me.TabGrafico.Text = "Current flight"
        '
        'Button_reset_y
        '
        Me.Button_reset_y.Location = New System.Drawing.Point(716, 104)
        Me.Button_reset_y.Name = "Button_reset_y"
        Me.Button_reset_y.Size = New System.Drawing.Size(90, 35)
        Me.Button_reset_y.TabIndex = 89
        Me.Button_reset_y.Text = "Reset Y axis"
        Me.Button_reset_y.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(568, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 15)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Graph title:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_titolo
        '
        Me.tb_titolo.Location = New System.Drawing.Point(638, 55)
        Me.tb_titolo.Name = "tb_titolo"
        Me.tb_titolo.Size = New System.Drawing.Size(255, 20)
        Me.tb_titolo.TabIndex = 87
        '
        'lbl_gyro_z_zero
        '
        Me.lbl_gyro_z_zero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lbl_gyro_z_zero.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_gyro_z_zero.Location = New System.Drawing.Point(500, 126)
        Me.lbl_gyro_z_zero.Name = "lbl_gyro_z_zero"
        Me.lbl_gyro_z_zero.Size = New System.Drawing.Size(90, 15)
        Me.lbl_gyro_z_zero.TabIndex = 84
        Me.lbl_gyro_z_zero.Text = "Label32"
        Me.lbl_gyro_z_zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(363, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(135, 15)
        Me.Label10.TabIndex = 83
        Me.Label10.Text = "Gyro Z zero (roll) (ADC):"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_gyro_x_zero
        '
        Me.lbl_gyro_x_zero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lbl_gyro_x_zero.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_gyro_x_zero.Location = New System.Drawing.Point(500, 91)
        Me.lbl_gyro_x_zero.Name = "lbl_gyro_x_zero"
        Me.lbl_gyro_x_zero.Size = New System.Drawing.Size(90, 15)
        Me.lbl_gyro_x_zero.TabIndex = 82
        Me.lbl_gyro_x_zero.Text = "Label29"
        Me.lbl_gyro_x_zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_gyro_y_zero
        '
        Me.lbl_gyro_y_zero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lbl_gyro_y_zero.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_gyro_y_zero.Location = New System.Drawing.Point(500, 108)
        Me.lbl_gyro_y_zero.Name = "lbl_gyro_y_zero"
        Me.lbl_gyro_y_zero.Size = New System.Drawing.Size(90, 15)
        Me.lbl_gyro_y_zero.TabIndex = 81
        Me.lbl_gyro_y_zero.Text = "Label32"
        Me.lbl_gyro_y_zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(354, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(144, 15)
        Me.Label7.TabIndex = 80
        Me.Label7.Text = "Gyro Y zero (pitch) (ADC):"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(354, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(144, 15)
        Me.Label8.TabIndex = 79
        Me.Label8.Text = "Gyro X zero (-yaw) (ADC):"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelFileName
        '
        Me.LabelFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.LabelFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelFileName.Location = New System.Drawing.Point(470, 9)
        Me.LabelFileName.Name = "LabelFileName"
        Me.LabelFileName.Size = New System.Drawing.Size(349, 39)
        Me.LabelFileName.TabIndex = 78
        Me.LabelFileName.Text = "no file opened"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(368, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 15)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "File name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(145, 101)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(77, 34)
        Me.Button5.TabIndex = 76
        Me.Button5.Text = "Redraw graph"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'CBox_pos_servo
        '
        Me.CBox_pos_servo.AutoSize = True
        Me.CBox_pos_servo.Checked = True
        Me.CBox_pos_servo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_pos_servo.Location = New System.Drawing.Point(145, 75)
        Me.CBox_pos_servo.Name = "CBox_pos_servo"
        Me.CBox_pos_servo.Size = New System.Drawing.Size(116, 17)
        Me.CBox_pos_servo.TabIndex = 75
        Me.CBox_pos_servo.Text = "Servo rotation (rad)"
        Me.CBox_pos_servo.UseVisualStyleBackColor = True
        '
        'CBox_acc_angolare
        '
        Me.CBox_acc_angolare.AutoSize = True
        Me.CBox_acc_angolare.Checked = True
        Me.CBox_acc_angolare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_acc_angolare.Location = New System.Drawing.Point(145, 58)
        Me.CBox_acc_angolare.Name = "CBox_acc_angolare"
        Me.CBox_acc_angolare.Size = New System.Drawing.Size(152, 17)
        Me.CBox_acc_angolare.TabIndex = 75
        Me.CBox_acc_angolare.Text = "Ang acceleration (rad/s^2)"
        Me.CBox_acc_angolare.UseVisualStyleBackColor = True
        '
        'CBox_roll
        '
        Me.CBox_roll.AutoSize = True
        Me.CBox_roll.Checked = True
        Me.CBox_roll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_roll.Location = New System.Drawing.Point(14, 92)
        Me.CBox_roll.Name = "CBox_roll"
        Me.CBox_roll.Size = New System.Drawing.Size(111, 17)
        Me.CBox_roll.TabIndex = 75
        Me.CBox_roll.Text = "Gyro Z (roll) (ADC)"
        Me.CBox_roll.UseVisualStyleBackColor = True
        '
        'CBox_vel_angolare
        '
        Me.CBox_vel_angolare.AutoSize = True
        Me.CBox_vel_angolare.Checked = True
        Me.CBox_vel_angolare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_vel_angolare.Location = New System.Drawing.Point(145, 41)
        Me.CBox_vel_angolare.Name = "CBox_vel_angolare"
        Me.CBox_vel_angolare.Size = New System.Drawing.Size(118, 17)
        Me.CBox_vel_angolare.TabIndex = 75
        Me.CBox_vel_angolare.Text = "Ang velocity (rad/s)"
        Me.CBox_vel_angolare.UseVisualStyleBackColor = True
        '
        'CBox_yaw
        '
        Me.CBox_yaw.AutoSize = True
        Me.CBox_yaw.Checked = True
        Me.CBox_yaw.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_yaw.Location = New System.Drawing.Point(14, 75)
        Me.CBox_yaw.Name = "CBox_yaw"
        Me.CBox_yaw.Size = New System.Drawing.Size(122, 17)
        Me.CBox_yaw.TabIndex = 75
        Me.CBox_yaw.Text = "Gyro Y (Pitch) (ADC)"
        Me.CBox_yaw.UseVisualStyleBackColor = True
        '
        'CBox_pos_angolare
        '
        Me.CBox_pos_angolare.AutoSize = True
        Me.CBox_pos_angolare.Checked = True
        Me.CBox_pos_angolare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_pos_angolare.Location = New System.Drawing.Point(145, 24)
        Me.CBox_pos_angolare.Name = "CBox_pos_angolare"
        Me.CBox_pos_angolare.Size = New System.Drawing.Size(108, 17)
        Me.CBox_pos_angolare.TabIndex = 75
        Me.CBox_pos_angolare.Text = "Ang position (rad)"
        Me.CBox_pos_angolare.UseVisualStyleBackColor = True
        '
        'CBox_pitch
        '
        Me.CBox_pitch.AutoSize = True
        Me.CBox_pitch.Checked = True
        Me.CBox_pitch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_pitch.Location = New System.Drawing.Point(14, 58)
        Me.CBox_pitch.Name = "CBox_pitch"
        Me.CBox_pitch.Size = New System.Drawing.Size(123, 17)
        Me.CBox_pitch.TabIndex = 75
        Me.CBox_pitch.Text = "Gyro X (- yaw) (ADC)"
        Me.CBox_pitch.UseVisualStyleBackColor = True
        '
        'CBox_accelerazione
        '
        Me.CBox_accelerazione.AutoSize = True
        Me.CBox_accelerazione.Checked = True
        Me.CBox_accelerazione.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_accelerazione.Location = New System.Drawing.Point(145, 7)
        Me.CBox_accelerazione.Name = "CBox_accelerazione"
        Me.CBox_accelerazione.Size = New System.Drawing.Size(124, 17)
        Me.CBox_accelerazione.TabIndex = 75
        Me.CBox_accelerazione.Text = "Acceleration (m/s^2)"
        Me.CBox_accelerazione.UseVisualStyleBackColor = True
        '
        'CBox_analog_aux
        '
        Me.CBox_analog_aux.AutoSize = True
        Me.CBox_analog_aux.Checked = True
        Me.CBox_analog_aux.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_analog_aux.Location = New System.Drawing.Point(14, 41)
        Me.CBox_analog_aux.Name = "CBox_analog_aux"
        Me.CBox_analog_aux.Size = New System.Drawing.Size(110, 17)
        Me.CBox_analog_aux.TabIndex = 75
        Me.CBox_analog_aux.Text = "Analog aux (ADC)"
        Me.CBox_analog_aux.UseVisualStyleBackColor = True
        '
        'CBox_velocità
        '
        Me.CBox_velocità.AutoSize = True
        Me.CBox_velocità.Checked = True
        Me.CBox_velocità.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_velocità.Location = New System.Drawing.Point(14, 126)
        Me.CBox_velocità.Name = "CBox_velocità"
        Me.CBox_velocità.Size = New System.Drawing.Size(90, 17)
        Me.CBox_velocità.TabIndex = 75
        Me.CBox_velocità.Text = "Velocity (m/s)"
        Me.CBox_velocità.UseVisualStyleBackColor = True
        '
        'CBox_barometro
        '
        Me.CBox_barometro.AutoSize = True
        Me.CBox_barometro.Checked = True
        Me.CBox_barometro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_barometro.Location = New System.Drawing.Point(14, 24)
        Me.CBox_barometro.Name = "CBox_barometro"
        Me.CBox_barometro.Size = New System.Drawing.Size(105, 17)
        Me.CBox_barometro.TabIndex = 75
        Me.CBox_barometro.Text = "Barometer (ADC)"
        Me.CBox_barometro.UseVisualStyleBackColor = True
        '
        'CBox_quota
        '
        Me.CBox_quota.AutoSize = True
        Me.CBox_quota.Checked = True
        Me.CBox_quota.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_quota.Location = New System.Drawing.Point(14, 109)
        Me.CBox_quota.Name = "CBox_quota"
        Me.CBox_quota.Size = New System.Drawing.Size(78, 17)
        Me.CBox_quota.TabIndex = 75
        Me.CBox_quota.Text = "Altitude (m)"
        Me.CBox_quota.UseVisualStyleBackColor = True
        '
        'CBox_accelerometro
        '
        Me.CBox_accelerometro.AutoSize = True
        Me.CBox_accelerometro.Checked = True
        Me.CBox_accelerometro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBox_accelerometro.Location = New System.Drawing.Point(14, 7)
        Me.CBox_accelerometro.Name = "CBox_accelerometro"
        Me.CBox_accelerometro.Size = New System.Drawing.Size(125, 17)
        Me.CBox_accelerometro.TabIndex = 75
        Me.CBox_accelerometro.Text = "Accelerometer (ADC)"
        Me.CBox_accelerometro.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(228, 101)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(77, 34)
        Me.Button3.TabIndex = 74
        Me.Button3.Text = "Download" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "flight data"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GraficoDati
        '
        Me.GraficoDati.IsAutoScrollRange = True
        Me.GraficoDati.Location = New System.Drawing.Point(6, 153)
        Me.GraficoDati.Name = "GraficoDati"
        Me.GraficoDati.PanButtons = System.Windows.Forms.MouseButtons.Middle
        Me.GraficoDati.PanButtons2 = System.Windows.Forms.MouseButtons.Left
        Me.GraficoDati.ScrollGrace = 0.0R
        Me.GraficoDati.ScrollMaxX = 0.0R
        Me.GraficoDati.ScrollMaxY = 0.0R
        Me.GraficoDati.ScrollMaxY2 = 0.0R
        Me.GraficoDati.ScrollMinX = 0.0R
        Me.GraficoDati.ScrollMinY = 0.0R
        Me.GraficoDati.ScrollMinY2 = 0.0R
        Me.GraficoDati.Size = New System.Drawing.Size(908, 332)
        Me.GraficoDati.TabIndex = 0
        Me.GraficoDati.ZoomButtons = System.Windows.Forms.MouseButtons.Middle
        '
        'lbl_baro_zero
        '
        Me.lbl_baro_zero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lbl_baro_zero.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_baro_zero.Location = New System.Drawing.Point(500, 56)
        Me.lbl_baro_zero.Name = "lbl_baro_zero"
        Me.lbl_baro_zero.Size = New System.Drawing.Size(90, 15)
        Me.lbl_baro_zero.TabIndex = 60
        Me.lbl_baro_zero.Text = "Label29"
        Me.lbl_baro_zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_acc_zero
        '
        Me.lbl_acc_zero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lbl_acc_zero.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbl_acc_zero.Location = New System.Drawing.Point(500, 73)
        Me.lbl_acc_zero.Name = "lbl_acc_zero"
        Me.lbl_acc_zero.Size = New System.Drawing.Size(90, 15)
        Me.lbl_acc_zero.TabIndex = 57
        Me.lbl_acc_zero.Text = "Label32"
        Me.lbl_acc_zero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(368, 41)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 15)
        Me.Label13.TabIndex = 50
        Me.Label13.Text = "Calibration info"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label26.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label26.Location = New System.Drawing.Point(347, 73)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(151, 15)
        Me.Label26.TabIndex = 47
        Me.Label26.Text = "Accelerometer zero (ADC):"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label27.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label27.Location = New System.Drawing.Point(368, 56)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(130, 15)
        Me.Label27.TabIndex = 46
        Me.Label27.Text = "Barometer zero (ADC):"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BTNContrai
        '
        Me.BTNContrai.AutoSize = True
        Me.BTNContrai.Image = CType(resources.GetObject("BTNContrai.Image"), System.Drawing.Image)
        Me.BTNContrai.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BTNContrai.Location = New System.Drawing.Point(855, 104)
        Me.BTNContrai.Name = "BTNContrai"
        Me.BTNContrai.Size = New System.Drawing.Size(36, 36)
        Me.BTNContrai.TabIndex = 73
        Me.BTNContrai.UseVisualStyleBackColor = True
        '
        'BTNEspandi
        '
        Me.BTNEspandi.AutoSize = True
        Me.BTNEspandi.Image = CType(resources.GetObject("BTNEspandi.Image"), System.Drawing.Image)
        Me.BTNEspandi.Location = New System.Drawing.Point(813, 104)
        Me.BTNEspandi.Name = "BTNEspandi"
        Me.BTNEspandi.Size = New System.Drawing.Size(36, 36)
        Me.BTNEspandi.TabIndex = 72
        Me.BTNEspandi.UseVisualStyleBackColor = True
        '
        'TabRiassunto
        '
        Me.TabRiassunto.BackColor = System.Drawing.SystemColors.Control
        Me.TabRiassunto.Controls.Add(Me.PictureBox1)
        Me.TabRiassunto.Controls.Add(Me.Label_conversione)
        Me.TabRiassunto.Controls.Add(Me.GroupBoxServo)
        Me.TabRiassunto.Controls.Add(Me.GroupBox6)
        Me.TabRiassunto.Controls.Add(Me.Button2)
        Me.TabRiassunto.Controls.Add(Me.Label34)
        Me.TabRiassunto.Controls.Add(Me.GroupBox5)
        Me.TabRiassunto.Controls.Add(Me.GroupBox4)
        Me.TabRiassunto.Controls.Add(Me.P0)
        Me.TabRiassunto.Controls.Add(Me.TextBox_Q0)
        Me.TabRiassunto.Controls.Add(Me.Label2)
        Me.TabRiassunto.Controls.Add(Me.Label35)
        Me.TabRiassunto.Controls.Add(Me.TextBox_P0)
        Me.TabRiassunto.Controls.Add(Me.TextBox_R0)
        Me.TabRiassunto.Location = New System.Drawing.Point(4, 22)
        Me.TabRiassunto.Name = "TabRiassunto"
        Me.TabRiassunto.Padding = New System.Windows.Forms.Padding(3)
        Me.TabRiassunto.Size = New System.Drawing.Size(914, 481)
        Me.TabRiassunto.TabIndex = 3
        Me.TabRiassunto.Text = " Summary"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Software_Tesi_Magistrale.My.Resources.Resources.PID_equation
        Me.PictureBox1.Image = Global.Software_Tesi_Magistrale.My.Resources.Resources.PID_equation
        Me.PictureBox1.Location = New System.Drawing.Point(448, 124)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(233, 44)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 49
        Me.PictureBox1.TabStop = False
        '
        'Label_conversione
        '
        Me.Label_conversione.AutoSize = True
        Me.Label_conversione.Location = New System.Drawing.Point(445, 182)
        Me.Label_conversione.Name = "Label_conversione"
        Me.Label_conversione.Size = New System.Drawing.Size(147, 195)
        Me.Label_conversione.TabIndex = 48
        Me.Label_conversione.Text = resources.GetString("Label_conversione.Text")
        '
        'GroupBoxServo
        '
        Me.GroupBoxServo.Controls.Add(Me.Label3)
        Me.GroupBoxServo.Controls.Add(Me.Label1)
        Me.GroupBoxServo.Controls.Add(Me.Button_disattiva_servo)
        Me.GroupBoxServo.Controls.Add(Me.Lbl_servo_state)
        Me.GroupBoxServo.Controls.Add(Me.Button_attiva_servo)
        Me.GroupBoxServo.Location = New System.Drawing.Point(631, 319)
        Me.GroupBoxServo.Name = "GroupBoxServo"
        Me.GroupBoxServo.Size = New System.Drawing.Size(283, 156)
        Me.GroupBoxServo.TabIndex = 47
        Me.GroupBoxServo.TabStop = False
        Me.GroupBoxServo.Text = "Servo"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(31, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(231, 48)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "There are some oscillations in servo position because also USB communication uses" & _
    " interrupts"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Servo state:"
        '
        'Button_disattiva_servo
        '
        Me.Button_disattiva_servo.Location = New System.Drawing.Point(165, 25)
        Me.Button_disattiva_servo.Name = "Button_disattiva_servo"
        Me.Button_disattiva_servo.Size = New System.Drawing.Size(87, 39)
        Me.Button_disattiva_servo.TabIndex = 49
        Me.Button_disattiva_servo.Text = "Stop servo"
        Me.Button_disattiva_servo.UseVisualStyleBackColor = True
        '
        'Lbl_servo_state
        '
        Me.Lbl_servo_state.AutoSize = True
        Me.Lbl_servo_state.Location = New System.Drawing.Point(94, 73)
        Me.Lbl_servo_state.Name = "Lbl_servo_state"
        Me.Lbl_servo_state.Size = New System.Drawing.Size(27, 13)
        Me.Lbl_servo_state.TabIndex = 50
        Me.Lbl_servo_state.Text = "OFF"
        '
        'Button_attiva_servo
        '
        Me.Button_attiva_servo.Location = New System.Drawing.Point(33, 25)
        Me.Button_attiva_servo.Name = "Button_attiva_servo"
        Me.Button_attiva_servo.Size = New System.Drawing.Size(87, 39)
        Me.Button_attiva_servo.TabIndex = 48
        Me.Button_attiva_servo.Text = "Start servo"
        Me.Button_attiva_servo.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label28)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.LblValoreFramLettoHex)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.LblValoreFramLettoDec)
        Me.GroupBox6.Controls.Add(Me.TextBox_indirizzo_lettura)
        Me.GroupBox6.Controls.Add(Me.Button_leggi_FRAM)
        Me.GroupBox6.Location = New System.Drawing.Point(448, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(284, 99)
        Me.GroupBox6.TabIndex = 46
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Read FRAM address"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(175, 50)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(29, 13)
        Me.Label28.TabIndex = 49
        Me.Label28.Text = "Hex:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(174, 30)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(30, 13)
        Me.Label24.TabIndex = 48
        Me.Label24.Text = "Dec:"
        '
        'LblValoreFramLettoHex
        '
        Me.LblValoreFramLettoHex.AutoSize = True
        Me.LblValoreFramLettoHex.Location = New System.Drawing.Point(202, 50)
        Me.LblValoreFramLettoHex.Name = "LblValoreFramLettoHex"
        Me.LblValoreFramLettoHex.Size = New System.Drawing.Size(60, 13)
        Me.LblValoreFramLettoHex.TabIndex = 47
        Me.LblValoreFramLettoHex.Text = "Valore letto"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(27, 50)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(132, 26)
        Me.Label25.TabIndex = 46
        Me.Label25.Text = "Insert an address between" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0 and 131071"
        '
        'LblValoreFramLettoDec
        '
        Me.LblValoreFramLettoDec.AutoSize = True
        Me.LblValoreFramLettoDec.Location = New System.Drawing.Point(202, 30)
        Me.LblValoreFramLettoDec.Name = "LblValoreFramLettoDec"
        Me.LblValoreFramLettoDec.Size = New System.Drawing.Size(60, 13)
        Me.LblValoreFramLettoDec.TabIndex = 45
        Me.LblValoreFramLettoDec.Text = "Valore letto"
        '
        'TextBox_indirizzo_lettura
        '
        Me.TextBox_indirizzo_lettura.Location = New System.Drawing.Point(30, 27)
        Me.TextBox_indirizzo_lettura.MaxLength = 10
        Me.TextBox_indirizzo_lettura.Name = "TextBox_indirizzo_lettura"
        Me.TextBox_indirizzo_lettura.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_indirizzo_lettura.TabIndex = 45
        '
        'Button_leggi_FRAM
        '
        Me.Button_leggi_FRAM.Location = New System.Drawing.Point(102, 25)
        Me.Button_leggi_FRAM.Name = "Button_leggi_FRAM"
        Me.Button_leggi_FRAM.Size = New System.Drawing.Size(66, 22)
        Me.Button_leggi_FRAM.TabIndex = 35
        Me.Button_leggi_FRAM.Text = "Read"
        Me.Button_leggi_FRAM.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(295, 384)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(87, 39)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Update" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "readings"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(445, 434)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(21, 13)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "R0"
        Me.Label34.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.barometro)
        Me.GroupBox5.Controls.Add(Me.accelerometro)
        Me.GroupBox5.Controls.Add(Me.batteria)
        Me.GroupBox5.Controls.Add(Me.analog_aux)
        Me.GroupBox5.Controls.Add(Me.R)
        Me.GroupBox5.Controls.Add(Me.P)
        Me.GroupBox5.Controls.Add(Me.Q)
        Me.GroupBox5.Controls.Add(Me.Label44)
        Me.GroupBox5.Controls.Add(Me.Label43)
        Me.GroupBox5.Controls.Add(Me.Label49)
        Me.GroupBox5.Controls.Add(Me.Label45)
        Me.GroupBox5.Controls.Add(Me.Label48)
        Me.GroupBox5.Controls.Add(Me.Label46)
        Me.GroupBox5.Controls.Add(Me.Label47)
        Me.GroupBox5.Location = New System.Drawing.Point(18, 286)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(415, 179)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Current readings"
        '
        'barometro
        '
        Me.barometro.AutoSize = True
        Me.barometro.Location = New System.Drawing.Point(149, 45)
        Me.barometro.Name = "barometro"
        Me.barometro.Size = New System.Drawing.Size(82, 13)
        Me.barometro.TabIndex = 18
        Me.barometro.Text = "Pressure sensor"
        '
        'accelerometro
        '
        Me.accelerometro.AutoSize = True
        Me.accelerometro.Location = New System.Drawing.Point(149, 24)
        Me.accelerometro.Name = "accelerometro"
        Me.accelerometro.Size = New System.Drawing.Size(75, 13)
        Me.accelerometro.TabIndex = 17
        Me.accelerometro.Text = "Accelerometer"
        '
        'batteria
        '
        Me.batteria.AutoSize = True
        Me.batteria.Location = New System.Drawing.Point(149, 150)
        Me.batteria.Name = "batteria"
        Me.batteria.Size = New System.Drawing.Size(78, 13)
        Me.batteria.TabIndex = 23
        Me.batteria.Text = "Battery voltage"
        '
        'analog_aux
        '
        Me.analog_aux.AutoSize = True
        Me.analog_aux.Location = New System.Drawing.Point(149, 66)
        Me.analog_aux.Name = "analog_aux"
        Me.analog_aux.Size = New System.Drawing.Size(60, 13)
        Me.analog_aux.TabIndex = 19
        Me.analog_aux.Text = "Analog aux"
        '
        'R
        '
        Me.R.AutoSize = True
        Me.R.Location = New System.Drawing.Point(149, 129)
        Me.R.Name = "R"
        Me.R.Size = New System.Drawing.Size(40, 13)
        Me.R.TabIndex = 22
        Me.R.Text = "Gyro R"
        '
        'P
        '
        Me.P.AutoSize = True
        Me.P.Location = New System.Drawing.Point(149, 87)
        Me.P.Name = "P"
        Me.P.Size = New System.Drawing.Size(39, 13)
        Me.P.TabIndex = 20
        Me.P.Text = "Gyro P"
        '
        'Q
        '
        Me.Q.AutoSize = True
        Me.Q.Location = New System.Drawing.Point(149, 108)
        Me.Q.Name = "Q"
        Me.Q.Size = New System.Drawing.Size(40, 13)
        Me.Q.TabIndex = 21
        Me.Q.Text = "Gyro Q"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(68, 45)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(82, 13)
        Me.Label44.TabIndex = 11
        Me.Label44.Text = "Pressure sensor"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(75, 24)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(75, 13)
        Me.Label43.TabIndex = 10
        Me.Label43.Text = "Accelerometer"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(72, 150)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(78, 13)
        Me.Label49.TabIndex = 16
        Me.Label49.Text = "Battery voltage"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(90, 66)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(60, 13)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "Analog aux"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(46, 129)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(105, 13)
        Me.Label48.TabIndex = 15
        Me.Label48.Text = "Gyro Z velocity (Roll)"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(35, 87)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(115, 13)
        Me.Label46.TabIndex = 13
        Me.Label46.Text = "Gyro X velocity ( - yaw)"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(40, 108)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(110, 13)
        Me.Label47.TabIndex = 14
        Me.Label47.Text = "Gyro Y velocity (pitch)"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Controls.Add(Me.TextBox_Kd)
        Me.GroupBox4.Controls.Add(Me.TextBox_Ki)
        Me.GroupBox4.Controls.Add(Me.TextBox_Kp)
        Me.GroupBox4.Controls.Add(Me.TextBox_rotazione_per_programma_1)
        Me.GroupBox4.Controls.Add(Me.TextBox_trasferenza_rad_ms)
        Me.GroupBox4.Controls.Add(Me.TextBox_servo_max)
        Me.GroupBox4.Controls.Add(Me.TextBox_programma)
        Me.GroupBox4.Controls.Add(Me.TextBox_servo_min)
        Me.GroupBox4.Controls.Add(Me.low_battery_value)
        Me.GroupBox4.Controls.Add(Me.TextBox_servo_zero)
        Me.GroupBox4.Controls.Add(Me.TextBox_low_battery_value)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Controls.Add(Me.Label39)
        Me.GroupBox4.Controls.Add(Me.Label38)
        Me.GroupBox4.Controls.Add(Me.programma)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.Label36)
        Me.GroupBox4.Controls.Add(Me.Label42)
        Me.GroupBox4.Controls.Add(Me.Label41)
        Me.GroupBox4.Location = New System.Drawing.Point(18, 10)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(415, 270)
        Me.GroupBox4.TabIndex = 17
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "EEPROM memory content"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(181, 243)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 22)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Send"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox_Kd
        '
        Me.TextBox_Kd.Location = New System.Drawing.Point(181, 172)
        Me.TextBox_Kd.MaxLength = 10
        Me.TextBox_Kd.Name = "TextBox_Kd"
        Me.TextBox_Kd.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_Kd.TabIndex = 44
        '
        'TextBox_Ki
        '
        Me.TextBox_Ki.Location = New System.Drawing.Point(181, 152)
        Me.TextBox_Ki.MaxLength = 10
        Me.TextBox_Ki.Name = "TextBox_Ki"
        Me.TextBox_Ki.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_Ki.TabIndex = 44
        '
        'TextBox_Kp
        '
        Me.TextBox_Kp.Location = New System.Drawing.Point(181, 132)
        Me.TextBox_Kp.MaxLength = 10
        Me.TextBox_Kp.Name = "TextBox_Kp"
        Me.TextBox_Kp.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_Kp.TabIndex = 44
        '
        'TextBox_rotazione_per_programma_1
        '
        Me.TextBox_rotazione_per_programma_1.Location = New System.Drawing.Point(181, 221)
        Me.TextBox_rotazione_per_programma_1.MaxLength = 10
        Me.TextBox_rotazione_per_programma_1.Name = "TextBox_rotazione_per_programma_1"
        Me.TextBox_rotazione_per_programma_1.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_rotazione_per_programma_1.TabIndex = 44
        '
        'TextBox_trasferenza_rad_ms
        '
        Me.TextBox_trasferenza_rad_ms.Location = New System.Drawing.Point(181, 111)
        Me.TextBox_trasferenza_rad_ms.MaxLength = 10
        Me.TextBox_trasferenza_rad_ms.Name = "TextBox_trasferenza_rad_ms"
        Me.TextBox_trasferenza_rad_ms.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_trasferenza_rad_ms.TabIndex = 42
        '
        'TextBox_servo_max
        '
        Me.TextBox_servo_max.Location = New System.Drawing.Point(181, 90)
        Me.TextBox_servo_max.MaxLength = 10
        Me.TextBox_servo_max.Name = "TextBox_servo_max"
        Me.TextBox_servo_max.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_servo_max.TabIndex = 41
        '
        'TextBox_programma
        '
        Me.TextBox_programma.Location = New System.Drawing.Point(181, 200)
        Me.TextBox_programma.MaxLength = 10
        Me.TextBox_programma.Name = "TextBox_programma"
        Me.TextBox_programma.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_programma.TabIndex = 43
        '
        'TextBox_servo_min
        '
        Me.TextBox_servo_min.Location = New System.Drawing.Point(181, 69)
        Me.TextBox_servo_min.MaxLength = 10
        Me.TextBox_servo_min.Name = "TextBox_servo_min"
        Me.TextBox_servo_min.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_servo_min.TabIndex = 40
        '
        'low_battery_value
        '
        Me.low_battery_value.AutoSize = True
        Me.low_battery_value.Location = New System.Drawing.Point(253, 30)
        Me.low_battery_value.Name = "low_battery_value"
        Me.low_battery_value.Size = New System.Drawing.Size(109, 13)
        Me.low_battery_value.TabIndex = 15
        Me.low_battery_value.Text = "Limite batteria scarica"
        '
        'TextBox_servo_zero
        '
        Me.TextBox_servo_zero.Location = New System.Drawing.Point(181, 48)
        Me.TextBox_servo_zero.MaxLength = 10
        Me.TextBox_servo_zero.Name = "TextBox_servo_zero"
        Me.TextBox_servo_zero.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_servo_zero.TabIndex = 39
        '
        'TextBox_low_battery_value
        '
        Me.TextBox_low_battery_value.Location = New System.Drawing.Point(181, 27)
        Me.TextBox_low_battery_value.MaxLength = 10
        Me.TextBox_low_battery_value.Name = "TextBox_low_battery_value"
        Me.TextBox_low_battery_value.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_low_battery_value.TabIndex = 38
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(157, 175)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Kd"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(158, 155)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(19, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "K i"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(157, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Kp"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(54, 114)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(123, 13)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "rad-ms conversion factor"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(96, 93)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(81, 13)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "Servo max [rad]"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(99, 72)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(78, 13)
        Me.Label38.TabIndex = 5
        Me.Label38.Text = "Servo min [rad]"
        '
        'programma
        '
        Me.programma.AutoSize = True
        Me.programma.Location = New System.Drawing.Point(253, 199)
        Me.programma.Name = "programma"
        Me.programma.Size = New System.Drawing.Size(150, 65)
        Me.programma.TabIndex = 20
        Me.programma.Text = "0) Normale controllo di rollio" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1) Volo con alette ruotate" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2) Demo senza kalman" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3) Demo con kalman" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4) Volo con manovra a 55 m/s"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(97, 51)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(80, 13)
        Me.Label37.TabIndex = 4
        Me.Label37.Text = "Servo zero [ms]"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(95, 30)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(82, 13)
        Me.Label36.TabIndex = 3
        Me.Label36.Text = "Low battery limit"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(3, 223)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(174, 13)
        Me.Label42.TabIndex = 9
        Me.Label42.Text = "Fins angle for program 1 and 4 [rad]"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(89, 202)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(88, 13)
        Me.Label41.TabIndex = 8
        Me.Label41.Text = "Program to follow"
        '
        'P0
        '
        Me.P0.AutoSize = True
        Me.P0.Location = New System.Drawing.Point(543, 413)
        Me.P0.Name = "P0"
        Me.P0.Size = New System.Drawing.Size(151, 13)
        Me.P0.TabIndex = 12
        Me.P0.Text = "Valori di taratura del giroscopio"
        Me.P0.Visible = False
        '
        'TextBox_Q0
        '
        Me.TextBox_Q0.Location = New System.Drawing.Point(471, 410)
        Me.TextBox_Q0.MaxLength = 10
        Me.TextBox_Q0.Name = "TextBox_Q0"
        Me.TextBox_Q0.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_Q0.TabIndex = 36
        Me.TextBox_Q0.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(445, 392)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "P0"
        Me.Label2.Visible = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(444, 413)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(21, 13)
        Me.Label35.TabIndex = 2
        Me.Label35.Text = "Q0"
        Me.Label35.Visible = False
        '
        'TextBox_P0
        '
        Me.TextBox_P0.Location = New System.Drawing.Point(471, 389)
        Me.TextBox_P0.MaxLength = 10
        Me.TextBox_P0.Name = "TextBox_P0"
        Me.TextBox_P0.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_P0.TabIndex = 35
        Me.TextBox_P0.Visible = False
        '
        'TextBox_R0
        '
        Me.TextBox_R0.Location = New System.Drawing.Point(471, 431)
        Me.TextBox_R0.MaxLength = 10
        Me.TextBox_R0.Name = "TextBox_R0"
        Me.TextBox_R0.Size = New System.Drawing.Size(66, 20)
        Me.TextBox_R0.TabIndex = 37
        Me.TextBox_R0.Visible = False
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabRiassunto)
        Me.TabControl.Controls.Add(Me.TabGrafico)
        Me.TabControl.Controls.Add(Me.TabStreaming)
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 24)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.ShowToolTips = True
        Me.TabControl.Size = New System.Drawing.Size(922, 507)
        Me.TabControl.TabIndex = 58
        '
        'Principale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 559)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "Principale"
        Me.Text = "Roll control system inteface"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabStreaming.ResumeLayout(False)
        Me.TabStreaming.PerformLayout()
        Me.TabGrafico.ResumeLayout(False)
        Me.TabGrafico.PerformLayout()
        Me.TabRiassunto.ResumeLayout(False)
        Me.TabRiassunto.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxServo.ResumeLayout(False)
        Me.GroupBoxServo.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripConnesso As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusXY As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StrumentiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResetMemoriaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerStart As System.Windows.Forms.Timer
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TimerStreaming As System.Windows.Forms.Timer
    Friend WithEvents DlgSalva As System.Windows.Forms.SaveFileDialog
    Friend WithEvents DlgApri As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ApriToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalvaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EsciToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EsportaDatiGrezziToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DlgEsporta As System.Windows.Forms.SaveFileDialog
    Friend WithEvents AiutoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InformazioniToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TryFloatingPointNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabStreaming As System.Windows.Forms.TabPage
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents LabVarianza As System.Windows.Forms.Label
    Friend WithEvents LabDevStand As System.Windows.Forms.Label
    Friend WithEvents LabMedia As System.Windows.Forms.Label
    Friend WithEvents LabNCampioni As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PauseStreaming As System.Windows.Forms.Button
    Friend WithEvents StopStreaming As System.Windows.Forms.Button
    Friend WithEvents PlayStreaming As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCanale As System.Windows.Forms.ComboBox
    Friend WithEvents GraficoStreaming As ZedGraph.ZedGraphControl
    Friend WithEvents TabGrafico As System.Windows.Forms.TabPage
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents CBox_pos_servo As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_acc_angolare As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_roll As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_vel_angolare As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_yaw As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_pos_angolare As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_pitch As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_accelerazione As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_analog_aux As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_velocità As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_barometro As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_quota As System.Windows.Forms.CheckBox
    Friend WithEvents CBox_accelerometro As System.Windows.Forms.CheckBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents BTNContrai As System.Windows.Forms.Button
    Friend WithEvents BTNEspandi As System.Windows.Forms.Button
    Friend WithEvents GraficoDati As ZedGraph.ZedGraphControl
    Friend WithEvents lbl_baro_zero As System.Windows.Forms.Label
    Friend WithEvents lbl_acc_zero As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TabRiassunto As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents LblValoreFramLettoHex As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents LblValoreFramLettoDec As System.Windows.Forms.Label
    Friend WithEvents TextBox_indirizzo_lettura As System.Windows.Forms.TextBox
    Friend WithEvents Button_leggi_FRAM As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents barometro As System.Windows.Forms.Label
    Friend WithEvents accelerometro As System.Windows.Forms.Label
    Friend WithEvents batteria As System.Windows.Forms.Label
    Friend WithEvents analog_aux As System.Windows.Forms.Label
    Friend WithEvents R As System.Windows.Forms.Label
    Friend WithEvents P As System.Windows.Forms.Label
    Friend WithEvents Q As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox_rotazione_per_programma_1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_programma As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TextBox_trasferenza_rad_ms As System.Windows.Forms.TextBox
    Friend WithEvents P0 As System.Windows.Forms.Label
    Friend WithEvents TextBox_servo_max As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_servo_min As System.Windows.Forms.TextBox
    Friend WithEvents low_battery_value As System.Windows.Forms.Label
    Friend WithEvents TextBox_servo_zero As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_low_battery_value As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_R0 As System.Windows.Forms.TextBox
    Friend WithEvents programma As System.Windows.Forms.Label
    Friend WithEvents TextBox_Q0 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_P0 As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents GroupBoxServo As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button_disattiva_servo As System.Windows.Forms.Button
    Friend WithEvents Lbl_servo_state As System.Windows.Forms.Label
    Friend WithEvents Button_attiva_servo As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LabelFileName As System.Windows.Forms.Label
    Friend WithEvents lbl_gyro_z_zero As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lbl_gyro_x_zero As System.Windows.Forms.Label
    Friend WithEvents lbl_gyro_y_zero As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tb_titolo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button_reset_y As System.Windows.Forms.Button
    Friend WithEvents Label_conversione As System.Windows.Forms.Label
    Friend WithEvents TextBox_Kd As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Ki As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Kp As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
