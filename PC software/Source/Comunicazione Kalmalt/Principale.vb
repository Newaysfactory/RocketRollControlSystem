'ATTENZIONE!!!! RICORDARSI TRA LE PROPRIETA' DEL PROGETTO->AVANZATE DI COMPILARE PER PROCESSORI X86 ALTRIMENTI DARA' 
'ERRORE NEI COMPUTER CON PROCESSORE A 64 BIT PERCHE' CERCHERA' DI ESEGUIRE IL PROGRAMMA A 64 BIT SENZA RIUSCIRCI.

Imports ZedGraph
Imports System.IO.File            'Importa le istruzioniu per leggere e scrivere file
Imports System.Globalization      'Per il cambio lingua
Imports System.Threading          'Per il cambio lingua
Imports System



Public Class Principale

    ' vendor and product IDs
    Public Const VendorID As Short = &H4D8    'Replace with your device's...
    Public Const ProductID As Short = &H40    '...product and vendor IDs
    Public Const VersioneSoftware As String = "1.0"

    ' read and write buffers
    Private Const BufferInSize As Short = 65   'Size of the data buffer coming IN to the PC
    Private Const BufferOutSize As Short = 65  'Size of the data buffer going OUT from the PC
    Public BufferIn(BufferInSize) As Byte      'Received data will be stored here - the first byte in the array is unused
    Public BufferOut(BufferOutSize) As Byte    'Transmitted data is stored here - the first item in the array must be 0

    'Altre variabili
    'Dim dati_raw() As Byte                'Array con i dati grezzi in Big Indian
    Dim DatiInteger() As Double            'Array con i dati in interi.
    Dim InfoVoloCorrente(49) As Byte       'Array contenente il registro memoria del volo in corrente elaborazione
    Dim jj As Integer = 0
    Dim pagineAttese, pacchettiAttesi As Integer
    Dim pacchetto As Integer = 0         'Variabile per la conta dei pacchetti dati in arrivo 
    Dim Playing As Boolean = False       'Flag per indicare che lo streaming è in play
    Public Vsensore, Vpyro, Vcomputer, Vmain, Vdrogue As Double            'Variabili per i calcoli dei voltaggi che hanno bisogno della virgola

    'Per il calcolo di varianza e compagnia bella
    Dim NCampioni As Double = 0
    Dim Sommatoria As Double = 0
    Dim SommatoriaQuadrati As Double = 0
    Dim Media As Double = 0
    Dim DeviazioneStandard As Double = 0
    Dim Varianza As Double = 0

    'Array contententi le informazioni scaricate
    Public MemoriaInterna(12), Tensioni(9) As Byte   'Pubbliche perchè utilizzate nel GlowDurability test
    Dim Volo0(50), Volo1(50), Volo2(50) As Byte  'Volo0, ecc. sono i registri memoria dei vari voli con accodato il byte "next bank"

    '############################################################################################################
    'Variabili nuovo programma
    Dim dati_raw() As Byte                        'Array con i dati grezzi
    Dim indirizzoDiScrittura As Integer = 0       'Contatore dei byte per il trasferimento
    Dim FlagRicezione As Boolean = False          'Se true il computer sta ricevendo dati.

    Public Structure StruEEPROM
        'Dim p0 As Short
        'Dim q0 As Short
        'Dim r0 As Short
        Dim low_battery_value As Short
        Dim servo_zero As Double
        Dim servo_min As Double
        Dim servo_max As Double
        Dim trasferenza_rad_ms As Double
        Dim programma As Byte
        Dim rotazione_per_programma_1 As Double
        Dim Kp As Double
        Dim Ki As Double
        Dim Kd As Double
    End Structure
    Dim EEPROM As StruEEPROM

    Public Structure StruSensori
        Dim accelerometro As Integer
        Dim barometro As Integer
        Dim analog_aux As Integer
        Dim p As Integer
        Dim q As Integer
        Dim r As Integer
        Dim batteria As Integer
    End Structure
    Dim sensori As StruSensori

    Public Structure StruRegistroMemoria
        Dim used As Byte
        Dim primo_indirizzo As Integer
        Dim ultimo_indirizzo As Integer
        Dim baro_zero As Integer
        Dim acc_zero As Integer
    End Structure
    Dim registro_memoria As StruRegistroMemoria

    Public Structure StruDati
        Dim accelerometro As Double
        Dim barometro As Double
        Dim analog_aux As Double
        Dim pitch As Double
        Dim yaw As Double
        Dim roll As Double
    End Structure
    Dim dati As StruDati

    <Serializable()> Public Structure StruDatiScaricati
        Dim baro_zero As Double
        Dim acc_zero As Double
        Dim gyro_x_zero As Double
        Dim gyro_y_zero As Double
        Dim gyro_z_zero As Double
        Dim accelerometro() As Double
        Dim barometro() As Double
        Dim analog_aux() As Double
        Dim pitch() As Double
        Dim yaw() As Double
        Dim roll() As Double
        Dim quota() As Double
        Dim velocità() As Double
        Dim accelerazione() As Double
        Dim pos_angolare() As Double
        Dim vel_angolare() As Double
        Dim acc_angolare() As Double
        Dim pos_servo() As Double
        Dim tempo() As Double
    End Structure
    Dim dati_scaricati As StruDatiScaricati


    ' ****************************************************************
    ' when the form loads, connect to the HID controller - pass
    ' the form window handle so that you can receive notification
    ' events...
    '*****************************************************************
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' do not remove!
        ConnectToHID(Me)

        GroupBoxServo.Visible = False
        ComboBoxCanale.SelectedIndex = 0    'seleziona il primo elemento della combobox per lo streaming
        'DateTimePicker1.Value = Now         'imposta la data del datetimepicker

        'Settaggio di ZedGraph
        GraficoDati.IsShowHScrollBar = True        'Visualizza gli scrollbar per i grafici
        GraficoDati.GraphPane.Chart.Fill = New Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)        'Colora lo sfondo giallino con gradiente
        GraficoDati.GraphPane.XAxis.MajorGrid.IsVisible = True
        GraficoDati.GraphPane.YAxis.MajorGrid.IsVisible = True
        GraficoDati.GraphPane.XAxis.Scale.LabelGap = 0.015
        GraficoDati.GraphPane.YAxis.Scale.LabelGap = 0.015
        GraficoDati.GraphPane.XAxis.Title.Gap = 0.015
        GraficoDati.GraphPane.YAxis.Title.Gap = 0.015
        GraficoDati.GraphPane.Margin.Left = 10
        GraficoDati.GraphPane.Margin.Right = 10
        GraficoDati.GraphPane.Margin.Top = 10
        GraficoDati.GraphPane.TitleGap = 0.03
        GraficoDati.GraphPane.Legend.Gap = 0.03
        GraficoStreaming.GraphPane.Chart.Fill = New Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)
        GraficoDati.GraphPane.XAxis.Title.Text = "T(s)"
        GraficoDati.GraphPane.YAxis.Title.Text = ""

        GraficoStreaming.IsShowHScrollBar = True
        GraficoStreaming.GraphPane.Chart.Fill = New Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)        'Colora lo sfondo giallino con gradiente
        GraficoStreaming.GraphPane.XAxis.MajorGrid.IsVisible = True
        GraficoStreaming.GraphPane.YAxis.MajorGrid.IsVisible = True
        GraficoStreaming.GraphPane.XAxis.Scale.LabelGap = 0.015
        GraficoStreaming.GraphPane.YAxis.Scale.LabelGap = 0.015
        GraficoStreaming.GraphPane.XAxis.Title.Gap = 0.015
        GraficoStreaming.GraphPane.YAxis.Title.Gap = 0.015
        GraficoStreaming.GraphPane.Margin.Left = 10
        GraficoStreaming.GraphPane.Margin.Right = 10
        GraficoStreaming.GraphPane.Margin.Top = 20
        GraficoStreaming.GraphPane.TitleGap = 0.03
        GraficoStreaming.GraphPane.Legend.Gap = 0.03
        GraficoStreaming.GraphPane.Chart.Fill = New Fill(Color.White, Color.LightGoldenrodYellow, 45.0F)
        GraficoStreaming.GraphPane.Title.Text = ""
        GraficoStreaming.GraphPane.XAxis.Title.Text = ""
        GraficoStreaming.GraphPane.YAxis.Title.Text = ""

        SetSize()                                             'Dimensiona per bene i grafici

        'CancellaLabelSommario()    'Cancella tutte le label della prima tab
        CancellaLabelGraficoDati() 'Cancella tutte le label della seconda tab
        CancellaLabelStreaming()   'Cancella tutte le label della terza tab
        AbilitaTutto(False)        'Disabilita tutti i controlli

    End Sub

    '*****************************************************************
    ' disconnect from the HID controller...
    '*****************************************************************
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DisconnectFromHID()
    End Sub

    '*****************************************************************
    ' a HID device has been plugged in...
    '*****************************************************************
    Public Sub OnPlugged(ByVal pHandle As Integer)
        If hidGetVendorID(pHandle) = VendorID And hidGetProductID(pHandle) = ProductID Then
            ' ** YOUR CODE HERE **
            ToolStripInfo.Text = ""                 'Scrive che è connesso ed elimina le istruzioni per la connessione
            ToolStripConnesso.Text = "Connected"
            TimerStart.Enabled = True                           'Devo usare un timer per richiedere le info perchè serve qualche istante al pic per avviarsi
            TimerStart.Interval = 800                           'Per inizializzare il tutto. Quando va in overflow viene
            AbilitaTutto(True)                                  'Abilita tutti i controlli
            TimerStart.Start()                                  'chiamata la routine RecuperaDati dopo qualche istante

        End If
    End Sub

    '*****************************************************************
    ' a HID device has been unplugged...
    '*****************************************************************
    Public Sub OnUnplugged(ByVal pHandle As Integer)
        If hidGetVendorID(pHandle) = VendorID And hidGetProductID(pHandle) = ProductID Then
            hidSetReadNotify(hidGetHandle(VendorID, ProductID), False)
            GC.Collect()                                 'Non so a che servano queste 4 righe, ma senza non funziona la disconnessione
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.KeepAlive(Me.Handle.ToInt32)
            ' ** YOUR CODE HERE **
            ToolStripConnesso.Text = "Disconnected"      'Mostra la label
            AbilitaTutto(False)                          'Disabilita tutti i controllo

        End If
    End Sub

    '*****************************************************************
    ' controller changed notification - called
    ' after ALL HID devices are plugged or unplugged
    '*****************************************************************
    Public Sub OnChanged()
        ' get the handle of the device we are interested in, then set
        ' its read notify flag to true - this ensures you get a read
        ' notification message when there is some data to read...
        Dim pHandle As Integer
        pHandle = hidGetHandle(VendorID, ProductID)
        hidSetReadNotify(hidGetHandle(VendorID, ProductID), True)
    End Sub

    'Task da eseguire alla disconnessione
    Public Sub Disconnesso()
        ToolStripConnesso.Text = "Non Connesso"
        ToolStripInfo.Text = "Per connettere l'altimetro collegarlo alla porta USB e accenderlo solo dopo"
        AbilitaTutto(False)
        'CancellaLabelSommario()
    End Sub



    'FUNZIONI E SUB PER IL TAB SOMMARIO E PER LA COMUNICAZIONE

    '*****************************************************************
    ' on read event...
    '*****************************************************************
    Public Sub OnRead(ByVal pHandle As Integer)
        ' read the data (don't forget, pass the whole array)...
        If hidRead(pHandle, BufferIn(0)) Then
            ' ** YOUR CODE HERE **
            ' first byte is the report ID, e.g. BufferIn(0)
            ' the other bytes are the data from the microcontroller...

            'If pacchetto >= 1 And pacchetto <= pacchettiAttesi Then            'Se pacchetto è diverso da 0, il che si verifica solo quando c'è un comando di lettura dati (133)
            '    Array.Copy(BufferIn, 1, dati_raw, 64 * (pacchetto - 1), 64)        'esegui la copia array
            '    pacchetto = pacchetto + 1
            '    If pacchetto = pacchettiAttesi + 1 Then                            'Se ha copiato tutti i pacchetti
            '        pacchetto = 0                                              'Resetta la variabile pacchetti
            '    End If
            '    Exit Sub
            'End If

            If FlagRicezione = True Then                                        'Se il programma è in modalità ricezione dati
                Array.Copy(BufferIn, 1, dati_raw, indirizzoDiScrittura, 64)         'esegui la copia array
                indirizzoDiScrittura = indirizzoDiScrittura + 64             'Il prossimo byte da cui cominciare a copiare sarà questo
                If indirizzoDiScrittura >= 131071 Then                       'Se ha copiato tutti i byte
                    indirizzoDiScrittura = 0                                 'Resetta per futuro uso
                    FlagRicezione = False                                    'Esce dalla modalità ricezione dati
                    dati_scaricati = ElaboraVolo(dati_raw)                   'Chiama la routine di elaborazione del volo
                    DisegnaDati(dati_scaricati, True)
                    'For Each element As Integer In dati_raw                     'Stampa a video tutti i dati
                    'TextBox2.AppendText(dati_raw(element) & " ")                
                    'Next

                    ''BLOCCO DI VERIFICA 
                    'Dim contatore As Integer = 0
                    'For kkk = 100 To 99999
                    '    If dati_raw(kkk) <> contatore Then Stop
                    '    contatore = contatore + 1
                    '    If contatore = 256 Then contatore = 0
                    'Next
                    'TextBox2.AppendText("   Tutto a posto!")

                End If
                Exit Sub
            End If

            Select Case BufferIn(1)                   'A seconda del comando in entrata fa cose diverse

                'Comando 129 (recupera contenuto EEPROM interna)
                Case 129
                    If BufferIn(2) = 0 Then
                        MessageBox.Show("Problems with EEPROM data download")
                        Exit Sub
                    End If
                    'EEPROM.p0 = (CShort(BufferIn(3)) << 8) Or BufferIn(4)
                    'EEPROM.q0 = (CShort(BufferIn(5)) << 8) Or BufferIn(6)
                    'EEPROM.r0 = (CShort(BufferIn(7)) << 8) Or BufferIn(8)
                    EEPROM.low_battery_value = (CShort(BufferIn(9)) << 8) Or BufferIn(10)
                    EEPROM.servo_zero = (CShort(BufferIn(11)) << 8) Or BufferIn(12)
                    EEPROM.servo_min = (CShort(BufferIn(13)) << 8) Or BufferIn(14)
                    EEPROM.servo_max = (CShort(BufferIn(15)) << 8) Or BufferIn(16)
                    EEPROM.trasferenza_rad_ms = (CShort(BufferIn(17)) << 8) Or BufferIn(18)
                    EEPROM.programma = BufferIn(19)
                    EEPROM.rotazione_per_programma_1 = (CShort(BufferIn(20)) << 8) Or BufferIn(21)
                    EEPROM.Kp = (CShort(BufferIn(22)) << 8) Or BufferIn(23)
                    EEPROM.Ki = (CShort(BufferIn(24)) << 8) Or BufferIn(25)
                    EEPROM.Kd = (CShort(BufferIn(26)) << 8) Or BufferIn(27)

                    BufferOut(1) = 132                                'Comando recupero dati sensori
                    hidWriteEx(VendorID, ProductID, BufferOut(0))     'Invia
                    Exit Sub

                    'Comando 130 (Scrittura dati EEPROM)
                Case 130
                    If BufferIn(2) = 1 Then                              'Se è tutto OK lo dice con un messaggio
                        MessageBox.Show("EEPROM data correctly written")
                        RecuperaDati()                                   'Aggiorna i dati
                    ElseIf BufferIn(2) = 0 Then
                        MessageBox.Show("SOMETHING WENT WRONG!!! WRITTEN FAILED")
                    Else
                        MessageBox.Show("Il valore di ritorno non è nè 0 nè 1. C'è qualche problema strano")
                    End If
                    Exit Sub

                    'Ricezione conferma eliminazione dati
                Case 131
                    MessageBox.Show("Data memory cleared")

                    'Comando 132 (recupera dati sensori). Se è attivo lo streaming aggiunge il punto
                    'al grafico streaming
                    'ATTENZIONE CHE L'INDIANITA' DI QUESTI DATI E' INVERTITA!!!
                Case 132
                    sensori.accelerometro = (CShort(BufferIn(4)) << 8) Or BufferIn(3)
                    sensori.barometro = (CShort(BufferIn(6)) << 8) Or BufferIn(5)
                    sensori.analog_aux = (CShort(BufferIn(8)) << 8) Or BufferIn(7)
                    sensori.p = (CShort(BufferIn(10)) << 8) Or BufferIn(9)
                    sensori.q = (CShort(BufferIn(12)) << 8) Or BufferIn(11)
                    sensori.r = (CShort(BufferIn(14)) << 8) Or BufferIn(13)
                    sensori.batteria = (CShort(BufferIn(16)) << 8) Or BufferIn(15)

                    If Playing = True Then    'Se è attivo lo streaming dati
                        Select Case ComboBoxCanale.SelectedIndex
                            Case 0
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.accelerometro)) 'invia in valore ADC
                            Case 1
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.accelerometro) * 0.2993774414) 'invia in m/s^2 (non calibrato rispetto allo zero)
                            Case 2
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.barometro)) 'invia alla sub il valore in ADC
                            Case 3
                                AggiungiPuntoAlGrafico(Convert.ToDouble(44345.9 - 44345.9 * ((389.12 + sensori.barometro) / (389.12 + 3388)) ^ 0.18092998)) 'invia alla sub il valore in metri riferito rispetto a una quota zero più o meno veritiera
                            Case 4
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.analog_aux)) 'invia alla sub il valore letto
                            Case 5
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.p)) 'invia alla sub il valore in ADC
                            Case 6
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.p) * 0.00030543) 'invia alla sub il valore in rad/s (La conversione presuppone un fondoscala di 500dps)
                            Case 7
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.p) * 0.0175) 'invia alla sub il valore in deg/s (La conversione presuppone un fondoscala di 500dps)
                            Case 8
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.q)) 'invia alla sub il valore letto
                            Case 9
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.q) * 0.00030543)
                            Case 10
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.q) * 0.0175) 'invia alla sub il valore in deg/s (La conversione presuppone un fondoscala di 500dps)
                            Case 11
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.r)) 'invia alla sub il valore letto
                            Case 12
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.r) * 0.00030543)
                            Case 13
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.r) * 0.0175) 'invia alla sub il valore in deg/s (La conversione presuppone un fondoscala di 500dps)
                            Case 14
                                AggiungiPuntoAlGrafico(Convert.ToDouble(sensori.batteria)) 'invia alla sub il valore letto
                        End Select
                        Exit Sub                                         'Esci prima dalla sub senza visualizzare i dati nell'altra scheda
                    End If

                    VisualizzaDati()         'Chiama la routine di visualizzazione dati
                    Exit Sub


                    'Comando 133: Comando di recupero dati registrati
                Case 133
                    If BufferIn(2) = 0 Then
                        'Se il banco è pieno
                        dati_raw = Nothing                    'Cancella tutti i dati precedenti
                        ReDim dati_raw(131071)
                        FlagRicezione = True                'Pone il computer in modalità ricezione dati
                        indirizzoDiScrittura = 0            'Il primo indirizzo di scrittura è zero
                        Exit Sub
                    Else   'Se il banco è vuoto
                        MessageBox.Show("Memory empty. No data to download")
                        Exit Sub
                    End If

                    'Prova di ricezione di valori floating point a 32bit ricevuti in 4 blocchi da 1 byte
                Case 134
                    Dim numero_con_virgola As Double  'In realta ciò che ricevo, una volta riunito, è un numero single, non un double.
                    numero_con_virgola = Unisci_4_byte(BufferIn(3), BufferIn(4), BufferIn(5), BufferIn(6))
                    MessageBox.Show((numero_con_virgola))

                    'Riceve il byte della FRAM e lo scrive nella label
                Case 136
                    LblValoreFramLettoDec.Text = (BufferIn(2))
                    LblValoreFramLettoHex.Text = Conversion.Hex(BufferIn(2))

                    'Se non viene ricevuto nessuno dei programmi noti

                    'Riceve lo stato del servo e lo visualizza nella label
                Case 137
                    If BufferIn(2) = 1 Then
                        Lbl_servo_state.Text = "ON"
                        Button_attiva_servo.Text = "Update position"
                    Else
                        Lbl_servo_state.Text = "OFF"
                        Button_attiva_servo.Text = "Start servo"
                    End If

                    'Se non viene ricevuto nessuno dei programmi noti

                Case Else                                                 'Se riceve un comando strano dice che non lo riconosce
                    MessageBox.Show("Command not recognized")

            End Select
        End If
    End Sub

    Public Sub VisualizzaDati()
        'Visualizza a video tutti i dati appena recuperati 
        'Dim QuotaMain, SensoreADC As Integer                                'Dichiara le variabili per i calcoli

        'TextBox_P0.Text = EEPROM.p0                                                 'Scrive nelle label
        'TextBox_Q0.Text = EEPROM.q0
        'TextBox_R0.Text = EEPROM.r0
        TextBox_low_battery_value.Text = EEPROM.low_battery_value
        TextBox_servo_zero.Text = EEPROM.servo_zero / 10000
        TextBox_servo_min.Text = EEPROM.servo_min / 10000
        TextBox_servo_max.Text = EEPROM.servo_max / 10000
        TextBox_trasferenza_rad_ms.Text = EEPROM.trasferenza_rad_ms / 10000
        TextBox_programma.Text = EEPROM.programma
        TextBox_rotazione_per_programma_1.Text = EEPROM.rotazione_per_programma_1 / 10000
        TextBox_Kp.Text = EEPROM.Kp / 10000
        TextBox_Ki.Text = EEPROM.Ki / 10000
        TextBox_Kd.Text = EEPROM.Kd / 10000

        accelerometro.Text = sensori.accelerometro
        barometro.Text = sensori.barometro
        analog_aux.Text = sensori.analog_aux
        P.Text = sensori.p
        Q.Text = sensori.q
        R.Text = sensori.r
        batteria.Text = sensori.batteria

    End Sub

    Public Sub RecuperaDati()
        'Recupera tutte le informazioni dal circuito:
        'Siccome non so come fare applico un metodo sporchissimo per eludere i continui interrupt di ricezione:
        'Chiamo il primo comando, gli altri saranno chiamati in cascata dagli eventi di lettura
        BufferOut(1) = 129                                'Comando Recupero EEPROM interna
        'inutile per questo circuito  BufferOut(2) = 0                                  'Per esattezza il volo del bank 0
        hidWriteEx(VendorID, ProductID, BufferOut(0))     'Invia
    End Sub

    'Quando, dopo l'avvio del programma, il timer va in overflow, vengono aggiornati i dati 
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerStart.Tick
        TimerStart.Enabled = False
        RecuperaDati()
    End Sub










    'IN QUESTA SEZIONE CI SONO SOLO FUNZIONI CHE RIGUARDANO L'ELABORAZIONE E IL GRAFICO DATI

    'Riceve un array dati costituito dai dati grezzi in valori ADC. Ricostruisce i valori e li sistema
    'in una struttura contenente array.
    Public Function ElaboraVolo(ByVal DatiGrezzi() As Byte) As StruDatiScaricati
        Dim iii As Integer  'variabile locale di ciclo
        Dim destinazione_dati As StruDatiScaricati

        'Calcola i valori di taratura
        destinazione_dati.baro_zero = ((CUShort(DatiGrezzi(7)) << 8) Or DatiGrezzi(8)) / 10 'Valore di taratura del barometro. Divido per 10 perchè era stato memorizzato moltiplicato per 10
        destinazione_dati.acc_zero = ((CUShort(DatiGrezzi(9)) << 8) Or DatiGrezzi(10)) / 10 'Valore di taratura dell'accelerometro. Divido per 10 perchè era stato memorizzato moltiplicato per 10
        destinazione_dati.gyro_x_zero = ((CShort(DatiGrezzi(11)) << 8) Or DatiGrezzi(12)) / 10 'Valore di taratura del giroscopio (asse x). Divido per 10 perchè era stato memorizzato moltiplicato per 10
        destinazione_dati.gyro_y_zero = ((CShort(DatiGrezzi(13)) << 8) Or DatiGrezzi(14)) / 10 'Valore di taratura del giroscopio (asse y). Divido per 10 perchè era stato memorizzato moltiplicato per 10
        destinazione_dati.gyro_z_zero = ((CShort(DatiGrezzi(15)) << 8) Or DatiGrezzi(16)) / 10 'Valore di taratura del giroscopio (asse z). Divido per 10 perchè era stato memorizzato moltiplicato per 10

        ReDim destinazione_dati.accelerometro(3269)             'dimensiona tutti gli array dati
        ReDim destinazione_dati.barometro(3269)
        ReDim destinazione_dati.analog_aux(3269)
        ReDim destinazione_dati.pitch(3269)
        ReDim destinazione_dati.yaw(3269)
        ReDim destinazione_dati.roll(3269)
        ReDim destinazione_dati.quota(3269)
        ReDim destinazione_dati.velocità(3269)
        ReDim destinazione_dati.accelerazione(3269)
        ReDim destinazione_dati.pos_angolare(3269)
        ReDim destinazione_dati.vel_angolare(3269)
        ReDim destinazione_dati.acc_angolare(3269)
        ReDim destinazione_dati.pos_servo(3269)
        ReDim destinazione_dati.tempo(3269)                                   'dimensiona l'array del tempo

        'Riempie la struttura dati dati_scaricati. I vary byte vanno uniti insieme. Alcuni valori sono interi (2byte), altri float (4byte)
        For iii = 0 To 3269
            destinazione_dati.accelerometro(iii) = (CShort(DatiGrezzi(iii * 40 + 101)) << 8) Or DatiGrezzi(iii * 40 + 100)
            destinazione_dati.barometro(iii) = (CShort(DatiGrezzi(iii * 40 + 103)) << 8) Or DatiGrezzi(iii * 40 + 102)
            destinazione_dati.analog_aux(iii) = (CShort(DatiGrezzi(iii * 40 + 105)) << 8) Or DatiGrezzi(iii * 40 + 104)
            destinazione_dati.pitch(iii) = (CShort(DatiGrezzi(iii * 40 + 107)) << 8) Or DatiGrezzi(iii * 40 + 106)
            destinazione_dati.yaw(iii) = (CShort(DatiGrezzi(iii * 40 + 109)) << 8) Or DatiGrezzi(iii * 40 + 108)
            destinazione_dati.roll(iii) = (CShort(DatiGrezzi(iii * 40 + 111)) << 8) Or DatiGrezzi(iii * 40 + 110)
            destinazione_dati.quota(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 115), DatiGrezzi(iii * 40 + 114), DatiGrezzi(iii * 40 + 113), DatiGrezzi(iii * 40 + 112))
            destinazione_dati.velocità(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 119), DatiGrezzi(iii * 40 + 118), DatiGrezzi(iii * 40 + 117), DatiGrezzi(iii * 40 + 116))
            destinazione_dati.accelerazione(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 123), DatiGrezzi(iii * 40 + 122), DatiGrezzi(iii * 40 + 121), DatiGrezzi(iii * 40 + 120))
            destinazione_dati.pos_angolare(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 127), DatiGrezzi(iii * 40 + 126), DatiGrezzi(iii * 40 + 125), DatiGrezzi(iii * 40 + 124))
            destinazione_dati.vel_angolare(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 131), DatiGrezzi(iii * 40 + 130), DatiGrezzi(iii * 40 + 129), DatiGrezzi(iii * 40 + 128))
            destinazione_dati.acc_angolare(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 136), DatiGrezzi(iii * 40 + 135), DatiGrezzi(iii * 40 + 133), DatiGrezzi(iii * 40 + 132))
            destinazione_dati.pos_servo(iii) = Unisci_4_byte(DatiGrezzi(iii * 40 + 139), DatiGrezzi(iii * 40 + 138), DatiGrezzi(iii * 40 + 137), DatiGrezzi(iii * 40 + 136))
            destinazione_dati.tempo(iii) = iii / 100              'Riempie con interavalli di tempo da 1/100esimo di secondo
        Next

        Return (destinazione_dati)

    End Function


    Public Sub DisegnaDati(ByVal dati_da_disegnare As StruDatiScaricati, ByVal CancellaInfoModello As Boolean)
        'Se in input ho deciso "SI" si cancella le informazioni sul modello poste popra il grafico
        If CancellaInfoModello = True Then
            'TBIDVolo.Text = ""
            'DateTimePicker1.Value = Now
            ' TBNomeModello.Text = ""
            'TBMotoreUtilizzato.Text = ""
            'TBSitoDiLancio.Text = ""
        End If

        lbl_baro_zero.Text = (dati_da_disegnare.baro_zero)
        lbl_acc_zero.Text = (dati_da_disegnare.acc_zero)
        lbl_gyro_x_zero.Text = (dati_da_disegnare.gyro_x_zero)
        lbl_gyro_y_zero.Text = (dati_da_disegnare.gyro_y_zero)
        lbl_gyro_z_zero.Text = (dati_da_disegnare.gyro_z_zero)

        GraficoDati.GraphPane.CurveList.Clear()                              'Pulisce dalle curve precedenti
        GraficoDati.GraphPane.GraphObjList.Clear()                           'Pulisce gli oggetti grafici precedenti (le linee verticali)

        CambiaTitolo(GraficoDati)                                            'Cambia il titolo

        If CBox_accelerometro.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Accelerometer (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.accelerometro, Color.Red, SymbolType.None)     'Disegna la curva dei dati dell'accelerometro
        End If
        If CBox_barometro.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Barometer (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.barometro, Color.LightSkyBlue, SymbolType.None)     'Disegna la curva dei dati del barometro
        End If
        If CBox_analog_aux.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Analog aux (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.analog_aux, Color.Gray, SymbolType.None)     'Disegna la curva dei dati del canale analogico accessorio
        End If
        If CBox_pitch.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Rocket pitch (gyro X axis) (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.pitch, Color.IndianRed, SymbolType.None)     'Disegna la curva del pitch del giroscopio (asse x)
        End If
        If CBox_yaw.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Rocket yaw (gyro Y axis) (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.yaw, Color.Violet, SymbolType.None)     'Disegna la curva del yaw del giroscopio (asse y)
        End If
        If CBox_roll.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Rocket roll (gyro z axis) (ADC)", dati_da_disegnare.tempo, dati_da_disegnare.roll, Color.Orange, SymbolType.None)     'Disegna la curva del roll del giroscopio (asse z)
        End If
        If CBox_quota.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Altitude (m)", dati_da_disegnare.tempo, dati_da_disegnare.quota, Color.DarkBlue, SymbolType.None)     'Disegna la curva della quota elaborata a bordo dal kalman
        End If
        If CBox_velocità.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Velocity (m/s)", dati_da_disegnare.tempo, dati_da_disegnare.velocità, Color.Green, SymbolType.None)     'Disegna la curva della velocità elaborata a bordo dal kalman
        End If
        If CBox_accelerazione.Checked = True Then
            GraficoDati.GraphPane.AddCurve("acceleration (m/s^2)", dati_da_disegnare.tempo, dati_da_disegnare.accelerazione, Color.Red, SymbolType.None)     'Disegna la curva dell'accelerazione elaborata a bordo dal kalman
        End If
        If CBox_pos_angolare.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Angular position (rad)", dati_da_disegnare.tempo, dati_da_disegnare.pos_angolare, Color.Cyan, SymbolType.None)     'Disegna la curva della posizione angolare elaborata a bordo dal kalman
        End If
        If CBox_vel_angolare.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Angular velocity (rad/s)", dati_da_disegnare.tempo, dati_da_disegnare.vel_angolare, Color.ForestGreen, SymbolType.None)     'Disegna la curva della velocità angolare elaborata a bordo dal kalman
        End If
        If CBox_acc_angolare.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Angular acceleration (rad/s^2)", dati_da_disegnare.tempo, dati_da_disegnare.acc_angolare, Color.Coral, SymbolType.None)     'Disegna la curva dell'accelerazione angolare elaborata a bordo dal kalman
        End If
        If CBox_pos_servo.Checked = True Then
            GraficoDati.GraphPane.AddCurve("Servo rotation (rad)", dati_da_disegnare.tempo, dati_da_disegnare.pos_servo, Color.Black, SymbolType.None)     'Disegna la curva della rotazione del servo elaborata a bordo dal kalman
        End If

        GraficoDati.AxisChange()            'Aggiusta la scala di visualizzazione

        TabGrafico.Enabled = True
        TabControl.SelectedTab = TabGrafico     'Passa al tab appena creato
    End Sub


    'Espande l'asse del tempo
    Private Sub BTNEspandi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNEspandi.Click
        Dim xScale As Scale = GraficoDati.GraphPane.XAxis.Scale
        'Ridimensiona l'asse x
        xScale.Max = xScale.Max - 2     'a colpi di 5 secondi
        xScale.Min = xScale.Min + 2
        'Ridisegna il grafico
        GraficoDati.AxisChange()
        ' Force a redraw
        GraficoDati.Invalidate()
        GraficoDati.Focus()             'Restituisce il focus al grafico per potere zoomare con la rotella senza ricliccare il grafico
    End Sub

    'Contrae l'asse del tempo 
    Private Sub BTNContrai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNContrai.Click
        Dim xScale As Scale = GraficoDati.GraphPane.XAxis.Scale
        'Ridimensiona l'asse x
        xScale.Max = xScale.Max + 2    'a colpi di 5 secondi
        xScale.Min = xScale.Min - 2
        'Ridisegna il grafico
        GraficoDati.AxisChange()
        ' Force a redraw
        GraficoDati.Invalidate()
        GraficoDati.Focus()            'Restituisce il focus al grafico per potere zoomare con la rotella senza ricliccare il grafico
    End Sub







    'DA QUI IN POI C'è IL CODICE PER LO STREAMING DATI

    'Quando ridimensiono il form ridimensiona anche i grafici dello streaming e dei dati
    Private Sub TabControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.Resize
        SetSize()
    End Sub

    ' Set the size and location of the ZedGraphControl GraficoStreaming e GraficoDati
    Private Sub SetSize()
        ' Lascia sopra 100 pixel per le informazioni
        GraficoStreaming.Location = New Point(0, 120)
        GraficoDati.Location = New Point(0, 150)
        ' Lascia un po' di spazio in basso per non coprire la StatusBar
        GraficoStreaming.Size = New Size(ClientRectangle.Width - 10, ClientRectangle.Height - 199)
        GraficoDati.Size = New Size(ClientRectangle.Width - 10, ClientRectangle.Height - 229)

    End Sub

    'Tasto di start streaming
    Private Sub PlayStreaming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayStreaming.Click

        Playing = True       'Abilita il flag per il play dei dati

        'Scrive sull'asse y il canale che sis sta leggendo
        GraficoStreaming.GraphPane.YAxis.Title.Text = ComboBoxCanale.SelectedItem

        'Se la combobox del canale è abilitata significa che sto avviando un nuovo grafico e che quindi devo pulire il grafico
        'e azzerare i dati statistici.
        'se invece è disabilitata significa che sono in pausa e che quindi i dati dovranno essere lasciati al loro posto
        If ComboBoxCanale.Enabled = True Then
            GraficoStreaming.GraphPane.CurveList.Clear()      'Pulisce dalle curve precedenti e...
            ' Save 5000 points.
            ' The RollingPointPairList is an efficient storage class that always
            ' keeps a rolling set of point data without needing to shift any data values
            Dim list As New RollingPointPairList(5000)
            ' Initially, a curve is added with no data points (list is empty)
            ' Color is blue, and there will be no symbols
            Dim curve As LineItem = GraficoStreaming.GraphPane.AddCurve("Streaming Data", list, Color.Blue, SymbolType.None)
            'Non visualizzare la curva nella legenda
            curve.Label.IsVisible = False

            'Inizializzazione dei dati per il calcolo di varianza e compagnia bella
            NCampioni = 0                 'Azzera tutto
            Sommatoria = 0
            SommatoriaQuadrati = 0
            Media = 0
            DeviazioneStandard = 0
            Varianza = 0

        End If

        ' Just manually control the X axis range so it scrolls continuously
        ' instead of discrete step-sized jumps
        GraficoStreaming.GraphPane.XAxis.Scale.Min = 0
        GraficoStreaming.GraphPane.XAxis.Scale.Max = 300
        GraficoStreaming.GraphPane.XAxis.Scale.MinorStep = 10
        GraficoStreaming.GraphPane.XAxis.Scale.MajorStep = 50

        'Non so che cosa faccia ma corregge il problema di axischange e cioè che a volte non avviene il resize dell'asse Y
        GraficoStreaming.GraphPane.YAxis.Scale.MinAuto = True
        GraficoStreaming.GraphPane.YAxis.Scale.MaxAuto = True

        ' Scale the axes
        GraficoStreaming.AxisChange()
        ' Force a redraw
        GraficoStreaming.Invalidate()

        ComboBoxCanale.Enabled = False             'Disabilita la combobox del canale
        PlayStreaming.Enabled = False              'Disabilita il tasto start
        PauseStreaming.Enabled = True              'Abilita il pulsante di pausa

        ' Campiona alla velocità scelta nella combobox
        TimerStreaming.Interval = 50
        TimerStreaming.Enabled = True
        TimerStreaming.Start()

    End Sub

    'Tasto di stop streaming
    Private Sub StopStreaming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopStreaming.Click
        TimerStreaming.Stop()                     'Ferma il timer
        TimerStreaming.Enabled = False            'Disabilita il timer
        ComboBoxCanale.Enabled = True             'Riabilita la combobox
        PlayStreaming.Enabled = True              'Riabilita il tasto start
        Playing = False                           'Non è più in play
    End Sub

    'Tasto di pausa streaming
    Private Sub PauseStreaming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseStreaming.Click
        PausaStreaming()
    End Sub

    'Routine di pausa streaming
    Sub PausaStreaming()
        TimerStreaming.Stop()                   'Ferma semplicemente il timer senza azzerare la variabile tickstart
        PlayStreaming.Enabled = True            'Riabilita il pulsante di start
        PauseStreaming.Enabled = False          'disabilita il pulsante di pausa
    End Sub

    'Interrupt del timer per lo streaming dati
    Public Sub TimerStreaming_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerStreaming.Tick
        NCampioni = NCampioni + 1                            'Incrementa il numero di campioni
        'TickStart = TickStart + TimerStreaming.Interval     'Incrementa TickStart per sapere quanto è passato dallo start
        BufferOut(1) = 132                                   'Comando lettura analogica
        'BufferOut(2) = System.Convert.ToByte(ComboBoxCanale.SelectedIndex)         'Canale da leggere
        hidWriteEx(VendorID, ProductID, BufferOut(0))        'Invia
    End Sub

    'Aggiunge il punto al grafico dello streaming
    Private Sub AggiungiPuntoAlGrafico(ByVal Lettura As Double)
        'Dim Lettura As Double = 256 * LetturaMSB + LetturaLSB      'Ricostruisce il valore. Uso un double perchè zedgraph accetta solo dati double

        ' Make sure that the curvelist has at least one curve
        If GraficoStreaming.GraphPane.CurveList.Count <= 0 Then Return

        ' Get the first CurveItem in the graph
        Dim curve As LineItem = GraficoStreaming.GraphPane.CurveList(0)
        If curve Is Nothing Then Return

        ' Get the PointPairList
        Dim list As IPointListEdit = curve.Points
        ' If this is null, it means the reference at curve.Points does not
        ' support IPointListEdit, so we won't be able to modify it
        If list Is Nothing Then Return

        ' Add the point
        list.Add(NCampioni, Lettura)

        ' Keep the X scale at a rolling interval, with one
        ' major step between the max X value and the end of the axis
        Dim xScale As Scale = GraficoStreaming.GraphPane.XAxis.Scale
        If NCampioni > xScale.Max - xScale.MajorStep Then
            xScale.Max = NCampioni + xScale.MajorStep
            xScale.Min = xScale.Max - 300.0
        End If

        ' Make sure the Y axis is rescaled to accommodate actual data
        GraficoStreaming.AxisChange()
        ' Force a redraw
        GraficoStreaming.Invalidate()


        Statistica(Lettura)   'Chiama la sub che calcola le statistiche

        LabNCampioni.Text = NCampioni         'Aggiorna i valori delle label arrotondando i valori
        LabMedia.Text = Math.Round(Media, 7)
        LabDevStand.Text = Math.Round(DeviazioneStandard, 7)
        LabVarianza.Text = Math.Round(Varianza, 7)

    End Sub

    'calcola varianza, deviazione standard, media delle letture
    Private Sub Statistica(ByVal NuovaLettura As Double)
        Sommatoria = Sommatoria + NuovaLettura   'Aggiunge la nuova lettura alla sommatoria attuale
        SommatoriaQuadrati = SommatoriaQuadrati + NuovaLettura ^ 2
        Media = Sommatoria / NCampioni           'Calcola la media
        DeviazioneStandard = (1 / NCampioni) * Math.Sqrt(NCampioni * SommatoriaQuadrati - (Sommatoria ^ 2))   'Calcola la deviazione standard come trovato su wikipedia alla voce "deviazione standard"
        Varianza = DeviazioneStandard ^ 2
    End Sub








    'ALTRE FUNZIONI DI UTILITA' GENERALE

    'Disabilità tutti i pulsanti e affini quando l'altimetro non è connesso
    Private Sub AbilitaTutto(ByVal SioNo As Boolean)
        'Robe sul form
        ComboBoxCanale.Enabled = SioNo
        PlayStreaming.Enabled = SioNo
        PauseStreaming.Enabled = SioNo
        StopStreaming.Enabled = SioNo
        'Robe nei menù
        ResetMemoriaToolStripMenuItem.Enabled = SioNo
        'GlowDurabilityTestToolStripMenuItem.Enabled = SioNo
    End Sub

    'Cancella tutte le label della 2a tab
    Sub CancellaLabelGraficoDati()
        lbl_baro_zero.Text = ""
        lbl_acc_zero.Text = ""
    End Sub
    'Cancella tutte le label della 3a tab
    Sub CancellaLabelStreaming()
        LabNCampioni.Text = ""
        LabMedia.Text = ""
        LabDevStand.Text = ""
        LabVarianza.Text = ""
    End Sub

    'Effettua la conversione
    Private Function Da_ADC_a_Metri(ByVal Valore_ADC As Double, ByVal sensor_zero As Double) As Double
        Dim QuotaInMetri As Integer
        QuotaInMetri = 44345.9 - 44345.9 * ((389.12 + Valore_ADC) / (389.12 + sensor_zero)) ^ 0.18092998
        If Valore_ADC = 0 Then QuotaInMetri = 0 'Se il valore in ingresso è zero per qualche motivo, lo trascura ponendolo a zero metri
        Return QuotaInMetri
    End Function

    'Se c'è lo streaming in corso e si cambia tab mette in pausa lo streaming
    Private Sub TabStreaming_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabStreaming.Leave
        If Playing = True Then PausaStreaming()
    End Sub

    'Verifica che si inseriscano solo numeri nel campo ID volo
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Not IsNumeric(e.KeyChar)) And (Asc(e.KeyChar) <> 8) Then             'Controlla che siano inseriti solo numeri (rubato da internet)
            e.Handled = True
        End If
    End Sub

    'Cambia il titolo del grafico voluto
    Private Sub CambiaTitolo(ByVal zgc As ZedGraphControl)
        zgc.GraphPane.Title.Text = tb_titolo.Text     'Assegna la data, il nome del modello e il motore
        zgc.AxisChange()         'Ridisegna il grafico
        zgc.Invalidate()
    End Sub

    'Quando cambia il contenuto cambia il titolo del grafico
    Private Sub tb_titolo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CambiaTitolo(GraficoDati)
    End Sub

    'Visualizza in basso a destra la posizione sul GraficoDati. L'ho copiato paro paro dal sito di zedgraph
    Private Function ZedGraphControl1_MouseMoveEvent(ByVal sender As ZedGraph.ZedGraphControl, ByVal e As System.Windows.Forms.MouseEventArgs) As System.Boolean Handles GraficoDati.MouseMoveEvent
        ' Save the mouse location
        Dim mousePt As New PointF(e.X, e.Y)

        ' Find the Chart rect that contains the current mouse location
        Dim pane As GraphPane = sender.MasterPane.FindChartRect(mousePt)

        ' If pane is non-null, we have a valid location.  Otherwise, the mouse is not
        ' within any chart rect.
        If Not pane Is Nothing Then
            Dim x As Double, y As Double
            ' Convert the mouse location to X, Y scale values
            pane.ReverseTransform(mousePt, x, y)
            ' Format the status label text
            ToolStripStatusXY.Text = "(" + x.ToString("f2") + ", " + y.ToString("f2") + ")"
        Else
            ' If there is no valid data, then clear the status label text
            ToolStripStatusXY.Text = String.Empty
        End If

        ' Return false to indicate we have not processed the MouseMoveEvent
        ' ZedGraphControl should still go ahead and handle it
        Return False
    End Function

    'Visualizza la finestra con le info sul software
    Private Sub InformazioniToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformazioniToolStripMenuItem.Click
        About.Show()
    End Sub







    'GESTISCE IL MENU'

    'FILE

    'Apri file
    Private Sub ApriToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApriToolStripMenuItem.Click
        If DlgApri.ShowDialog = DialogResult.OK Then     'Se l'utente ha cliccato su ok chiama la funzione ApriFile, altrimenti non fa nulla
            dati_scaricati = ApriFile(DlgApri.FileName)
            DisegnaDati(dati_scaricati, True)
        End If
    End Sub

    'Salva file
    Private Sub SalvaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalvaToolStripMenuItem.Click

        'If (InfoVoloCorrente Is Nothing) Or (DatiInteger Is Nothing) Then            'Se non si è scaricato alcun volo gli array sono vuoti e non è possibile scrivere un file
        If (dati_scaricati.accelerometro Is Nothing) Then            'Se non si è scaricato alcun volo gli array, tra cui anche quello dell'accelerometro, sono vuoti e non è possibile scrivere un file
            MessageBox.Show("You need to download a fly befor saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)  'mostra una messagebox
            Exit Sub                                                                          'Abortisce il salvataggio
        End If

        If DlgSalva.ShowDialog = DialogResult.OK Then    'Se l'utente ha cliccato su ok chiama la funzione SalvaFile, altrimenti non fa nulla
            SalvaFile(DlgSalva.FileName, dati_scaricati)
        End If
    End Sub

    'Esporta dati grezzi
    Private Sub EsportaDatiGrezziToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsportaDatiGrezziToolStripMenuItem.Click
        If (dati_scaricati.accelerometro Is Nothing) Then            'Se non si è scaricato alcun volo gli array, tra cui anche quello dell'accelerometro, sono vuoti e non è possibile scrivere un file
            MessageBox.Show("You need to download a fly befor saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)  'mostra una messagebox
            Exit Sub                                                                          'Abortisce il salvataggio
        End If
        If DlgEsporta.ShowDialog = DialogResult.OK Then    'Se l'utente ha cliccato su ok chiama la funzione SalvaFile, altrimenti non fa nulla
            EsportaDati(DlgEsporta.FileName, dati_scaricati)
        End If
    End Sub

    'Esci
    Private Sub EsciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsciToolStripMenuItem.Click
        Me.Close()
    End Sub

    'STRUMENTI

    '"Reset memoria (cancella tutti i voli)" :Cancella l'intera memoria EEPROM esterna
    Private Sub ResetMemoriaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetMemoriaToolStripMenuItem.Click
        Dim Result As DialogResult
        Result = MessageBox.Show("Sei sicuro di voler cancellare tutti i voli memorizzati?" + vbCrLf + "Are you sure to delete all data?", "Clear memory", MessageBoxButtons.YesNo)         'Visualizza la messagebox di conferma con i pulsanti si e no
        If Result = System.Windows.Forms.DialogResult.Yes Then                                           'Se la risposta è SI
            BufferOut(1) = 131                                  'Comando cancellazione intero chip
            hidWriteEx(VendorID, ProductID, BufferOut(0))       'Invia
        Else
            Exit Sub                                                      'Altrimenti esce
        End If
    End Sub

    'Riceve il numero 12345,6789 per vedere se viene visualizzato correttamente
    Private Sub TryFloatingPointNumberToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TryFloatingPointNumberToolStripMenuItem.Click
        BufferOut(1) = 134                                  'Comando prova ricezione virgola mobile
        hidWriteEx(VendorID, ProductID, BufferOut(0))       'Invia
    End Sub



    'OPERAZIONI DI SALVATAGGIO E RECUPERO DEI FILE

    'Salva in un file binario la struttura dati
    'Rubato da
    'http://community.visual-basic.it/ALESSANDRO/archive/2006/11/27/18023.aspx
    Private Sub SalvaFile(ByVal NomeFile As String, ByVal volo_da_salvare As StruDatiScaricati)

        ' Ottiene un oggetto BinaryFormatter e crea un nuovo MemoryStream, a cui associa il BinaryFormatter, quindi scrive il contenuto nel file binario tramite My
        Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim MS As New System.IO.MemoryStream()

        'La serializzazione avviene dapprima in memoria, quindi riversata nel file binario
        BF.Serialize(MS, volo_da_salvare)
        My.Computer.FileSystem.WriteAllBytes(NomeFile, MS.GetBuffer(), False)

    End Sub

    'Salva in un file i dati grezzi con a lato il tempo.
    Private Sub EsportaDati(ByVal NomeFile As String, ByVal dati_da_esportare As StruDatiScaricati)
        Dim jjj As Integer                                         'Variabile di ciclo
        Dim Scrittore As New System.IO.StreamWriter(NomeFile)      'Apri il file

        'Comincia a scrivere
        Scrittore.WriteLine("### File generated with Roll control system interface v." + VersioneSoftware)   'Prima riga informativa
        Scrittore.WriteLine("### a program created by Adriano Arcadipane")
        Scrittore.WriteLine("### Flight computer: Roll control system")                                      'Seconda riga che informa sul tipo di altimetro
        Scrittore.WriteLine("### Data sample rate: 100 Hz")
        Scrittore.WriteLine("")
        Scrittore.WriteLine("  T          Acc        P          Aux     GyroX(-yaw) GyroY(pitch)  GyroZ(Roll)      Alt           Speed           Acc        Ang pos        Ang speed        Servo pos       Ang acc")
        Scrittore.WriteLine(" [s]        [ADC]      [ADC]      [ADC]       [ADC]        [ADC]        [ADC]         [m]           [m/s]         [m/s^2]       [rad]          [rad/s]           [rad]        [rad/s^2]")
        For jjj = 0 To (dati_da_esportare.tempo.GetLength(0) - 1)                 'Copia tutti i valore degli array (tranne l'ultimo elemento) con uno spazio tra l'uno e l'altro
            'Scrivo i dati in colonne formattandoli in formati numerici fissi. il ; che c'è in alcuni dati serve a trattare diversamente numeri positivi e negativi

            Scrittore.WriteLine(dati_da_esportare.tempo(jjj).ToString("00.00") + "       " + dati_da_esportare.accelerometro(jjj).ToString("0000") + "       " +
                                dati_da_esportare.barometro(jjj).ToString("0000") + "       " + dati_da_esportare.analog_aux(jjj).ToString("0000") + "       " +
                                dati_da_esportare.pitch(jjj).ToString(" 00000;-00000") + "       " + dati_da_esportare.yaw(jjj).ToString(" 00000;-00000") + "       " +
                                dati_da_esportare.roll(jjj).ToString(" 00000;-00000") + "       " + dati_da_esportare.quota(jjj).ToString(" 0000.00;-0000.00") + "       " +
                                dati_da_esportare.velocità(jjj).ToString(" 000.00;-000.00") + "       " + dati_da_esportare.accelerazione(jjj).ToString(" 000.00;-000.00") + "       " +
                                dati_da_esportare.pos_angolare(jjj).ToString(" 0000.00;-0000.00") + "       " + dati_da_esportare.vel_angolare(jjj).ToString(" 0000.000;-0000.000") + "       " +
                                dati_da_esportare.pos_servo(jjj).ToString(" 0.00000;-0.00000") + "       " + dati_da_esportare.acc_angolare(jjj).ToString(" 0000.00;-0000.00"))
        Next
        'Copia l'ultimo elemento senza poi andare a capo
        'Scrittore.Write(TempoDaSalvare(DatiGrezziDaSalvare.GetLength(0) - 1).ToString("000.00000") + "   " + DatiGrezziDaSalvare(DatiGrezziDaSalvare.GetLength(0) - 1).ToString("####"))

        Scrittore.Close()                                        'Chiude il file
        'Libera le risorse allocate con New.
        Scrittore = Nothing
    End Sub

    'Apri file.
    'Funzione rubata da
    'http://community.visual-basic.it/ALESSANDRO/archive/2006/11/27/18023.aspx
    Private Function ApriFile(ByVal NomeFile As String) As StruDatiScaricati
        Dim struttura_recuperata As StruDatiScaricati
        Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim bytes_grezzi As Byte() = My.Computer.FileSystem.ReadAllBytes(NomeFile)
        struttura_recuperata = DirectCast(BF.Deserialize(New System.IO.MemoryStream(bytes_grezzi)), StruDatiScaricati)
        LabelFileName.Text = NomeFile       'Visualizza il nome del file nella label
        Return (struttura_recuperata)
    End Function

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        RecuperaDati()
    End Sub

    'Scrivi le impostazioni sulla EEPROM interna
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Invio i dati divisi per byte. Alcuni valori vanno moltiplicati per 10000 per
        'evitare di usare la virgola mobile.


        BufferOut(1) = 130           'Comando di scrittura EEPROM
        'BufferOut(2) = HiByte(TextBox_P0.Text)
        'BufferOut(3) = LoByte(TextBox_P0.Text)
        'BufferOut(4) = HiByte(TextBox_Q0.Text)
        'BufferOut(5) = LoByte(TextBox_Q0.Text)
        'BufferOut(6) = HiByte(TextBox_R0.Text)
        'BufferOut(7) = LoByte(TextBox_R0.Text)
        BufferOut(8) = HiByte(TextBox_low_battery_value.Text)
        BufferOut(9) = LoByte(TextBox_low_battery_value.Text)
        BufferOut(10) = HiByte(TextBox_servo_zero.Text * 10000)
        BufferOut(11) = LoByte(TextBox_servo_zero.Text * 10000)
        BufferOut(12) = HiByte(TextBox_servo_min.Text * 10000)
        BufferOut(13) = LoByte(TextBox_servo_min.Text * 10000)
        BufferOut(14) = HiByte(TextBox_servo_max.Text * 10000)
        BufferOut(15) = LoByte(TextBox_servo_max.Text * 10000)
        BufferOut(16) = HiByte(TextBox_trasferenza_rad_ms.Text * 10000)
        BufferOut(17) = LoByte(TextBox_trasferenza_rad_ms.Text * 10000)
        BufferOut(18) = TextBox_programma.Text
        BufferOut(19) = HiByte(TextBox_rotazione_per_programma_1.Text * 10000)
        BufferOut(20) = LoByte(TextBox_rotazione_per_programma_1.Text * 10000)
        BufferOut(21) = HiByte(TextBox_Kp.Text * 10000)
        BufferOut(22) = LoByte(TextBox_Kp.Text * 10000)
        BufferOut(23) = HiByte(TextBox_Ki.Text * 10000)
        BufferOut(24) = LoByte(TextBox_Ki.Text * 10000)
        BufferOut(25) = HiByte(TextBox_Kd.Text * 10000)
        BufferOut(26) = LoByte(TextBox_Kd.Text * 10000)

        hidWriteEx(VendorID, ProductID, BufferOut(0))       'Invia alla USB
    End Sub

    'Restituisce il secondo byte meno significativo di un valore integer
    Function HiByte(ByVal intero As Integer) As Byte
        Dim MSB As Byte
        MSB = (intero And &HFF00) >> 8   'Elimina tutti i bit tranne dall'ottavo al sedicesimo e shifta a destra di 8 posti
        Return (MSB)
    End Function

    'Restituisce il byte meno significativo di un valore integer
    Function LoByte(ByVal intero As Integer) As Byte
        Dim LSB As Byte
        LSB = intero And &HFF    'Elimina tutti i bit tranne gli ultimi 8
        Return (LSB)
    End Function

    'Unisce 4 byte per formare un single (si utilizza per ricomporre numeri a virgola mobile)
    Function Unisci_4_byte(ByVal byte1_MSB As Byte, ByVal byte2 As Byte, ByVal byte3 As Byte, ByVal byte4_LSB As Byte) As Single
        Dim numero_ricostruito_single As Single
        Dim byte_da_unire() As Byte = {byte4_LSB, byte3, byte2, byte1_MSB}
        numero_ricostruito_single = BitConverter.ToSingle(byte_da_unire, 0)
        Return (numero_ricostruito_single)
    End Function

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'Scarica i dati di volo.
        BufferOut(1) = 133                                  'Comando scaricamento
        hidWriteEx(VendorID, ProductID, BufferOut(0))       'Invia
    End Sub

    'Ridisegna il grafico (per quando si deelezionano caselle)
    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        DisegnaDati(dati_scaricati, True)
    End Sub

    'Legge un bute all'indirizzo voluto della FRAM e lo visualizza in decimale ed esadecimale
    Private Sub Button4_Click_1(sender As System.Object, e As System.EventArgs) Handles Button_leggi_FRAM.Click
        Dim indirizzo_da_leggere As UInteger
        indirizzo_da_leggere = Convert.ToUInt32(TextBox_indirizzo_lettura.Text)   'Converti la stringa in numero
        If indirizzo_da_leggere > 131071 Then                                     'Se l'indirizzo è maggiore dalla memoria FRAM
            MessageBox.Show("Memory address must be from 0 to 131071", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)  'mostra una messagebox
            Exit Sub
        End If
        BufferOut(1) = 136
        BufferOut(2) = Convert.ToByte((indirizzo_da_leggere >> 16) And &HFF) 'Invia l'indirizzo in tre byte. bisogna fare l' AND con FF...
        BufferOut(3) = Convert.ToByte((indirizzo_da_leggere >> 8) And &HFF)  '...altrimenti va in overflow
        BufferOut(4) = Convert.ToByte((indirizzo_da_leggere) And &HFF)
        hidWriteEx(VendorID, ProductID, BufferOut(0))               'Invia
    End Sub

    'Consente l'inserimento soltanto di numeri, virgola, meno e backspace. Si occupa inoltre se viene digitato il punto viene convertito in virgola
    Private Sub Consenti_solo_numeri_virgola_e_converti_il_punto(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox_servo_zero.KeyPress,
        TextBox_servo_min.KeyPress, TextBox_servo_max.KeyPress, TextBox_trasferenza_rad_ms.KeyPress, TextBox_rotazione_per_programma_1.KeyPress, TextBox_Kd.KeyPress, TextBox_Ki.KeyPress, TextBox_Kp.KeyPress
        'Se viene premuto il punto scrive la virgola
        If e.KeyChar = Convert.ToChar(46) Then
            sender.appendtext(",")
            e.Handled = True
        End If
        'Scrive solo i caratteri numerici, la virgola e il backspace
        If (Not IsNumeric(e.KeyChar)) And (Asc(e.KeyChar) <> 8) And (Asc(e.KeyChar) <> 44) And (Asc(e.KeyChar) <> 45) Then             'Controlla che siano inseriti solo numeri (rubato da internet)
            e.Handled = True
        End If
    End Sub


    'Consente l'inserimento di soli numeri e backspace
    Private Sub Consenti_solo_numeri(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox_low_battery_value.KeyPress, TextBox_programma.KeyPress
        'Scrive solo i caratteri numerici e il backspace
        If (Not IsNumeric(e.KeyChar)) And (Asc(e.KeyChar) <> 8) Then             'Controlla che siano inseriti solo numeri (rubato da internet)
            e.Handled = True
        End If
    End Sub

    'Consente l'inserimento di soli numeri, backspace e meno
    Private Sub Consenti_solo_numeri_e_meno(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox_P0.KeyPress, TextBox_Q0.KeyPress, TextBox_R0.KeyPress
        'Scrive solo i caratteri numerici e il backspace
        If (Not IsNumeric(e.KeyChar)) And (Asc(e.KeyChar) <> 8) And (Asc(e.KeyChar) <> 45) Then             'Controlla che siano inseriti solo numeri (rubato da internet)
            e.Handled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button_attiva_servo.Click
        BufferOut(1) = 137     'invia comando gestione servo
        BufferOut(2) = 1       'invia comando attivazione
        hidWriteEx(VendorID, ProductID, BufferOut(0))               'Invia
    End Sub

    Private Sub Button_disattiva_servo_Click(sender As System.Object, e As System.EventArgs) Handles Button_disattiva_servo.Click
        BufferOut(1) = 137     'invia comando gestione servo
        BufferOut(2) = 0       'invia comando disattivazione
        hidWriteEx(VendorID, ProductID, BufferOut(0))               'Invia
    End Sub

    'Resetta l'asse Y a un valore decente
    Private Sub Button_reset_y_Click(sender As System.Object, e As System.EventArgs) Handles Button_reset_y.Click
        Dim yScale As Scale = GraficoDati.GraphPane.YAxis.Scale
        'Ridimensiona l'asse y
        yScale.Max = 5000     'a colpi di 5 secondi
        yScale.Min = -5000
        'Ridisegna il grafico
        GraficoDati.AxisChange()
        ' Force a redraw
        GraficoDati.Invalidate()
        GraficoDati.Focus()             'Restituisce il focus al grafico per potere zoomare con la rotella senza ricliccare il grafico
    End Sub

End Class