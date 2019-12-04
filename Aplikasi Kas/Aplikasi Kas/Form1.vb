Imports System.Data.Odbc

Public Class Form1
    Sub TampilGrid()
        bukakoneksi()

        DA = New OdbcDataAdapter("SELECT tgl As Tanggal, jenis As Jenis, jml_saldo As Jumlah, saldo As 'Saldo Sekarang', Keterangan  FROM kas ", CONN)
        DS = New DataSet
        DA.Fill(DS, "kas")
        DataGridView1.DataSource = DS.Tables("kas")
        TampilLabelSaldo()

        tutupkoneksi()
    End Sub

    Sub TampilLabelSaldo()
        CMD = New OdbcCommand("SELECT * FROM `kas` ORDER BY `id_kas` DESC", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            lblSaldo.Text = "Rp. 0"
        Else
            lblSaldo.Text = RD.Item("saldo")
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        TampilGrid()
        Munculcombo()
    End Sub

    Sub KosongkanData()
        dtpTanggal.Text = ""
        cmbJenis.Text = ""
        txtJumlah.Text = ""
        txtKet.Text = ""
    End Sub

    Sub Munculcombo()
        cmbJenis.Items.Add("Masuk")
        cmbJenis.Items.Add("Keluar")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If dtpTanggal.Text = "" Or cmbJenis.Text = "" Or txtJumlah.Text = "" Or txtKet.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            bukakoneksi()
            Dim jumlah As Integer
            Dim saldo As Integer
            CMD = New OdbcCommand("SELECT * FROM `kas` ORDER BY `id_kas` DESC", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                saldo = 0
                jumlah = Integer.Parse(txtJumlah.Text)
                If cmbJenis.Text = "Masuk" Then
                    saldo = saldo + jumlah
                ElseIf cmbJenis.Text = "Keluar"
                    saldo = saldo - jumlah
                End If

                Dim simpan As String = "INSERT INTO `kas` (`id_kas`, `tgl`, `jenis`, `jml_saldo`, `saldo`, `Keterangan`) VALUES (NULL, '" & dtpTanggal.Text & "', '" & cmbJenis.Text & "', '" & txtJumlah.Text & "', '" & saldo & "', '" & txtKet.Text & "')"

                CMD = New OdbcCommand(simpan, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Input data berhasil")
                TampilGrid()
                KosongkanData()

                tutupkoneksi()
            Else
                saldo = RD.Item("saldo")
                jumlah = Integer.Parse(txtJumlah.Text)
                If cmbJenis.Text = "Masuk" Then
                    saldo = saldo + jumlah
                ElseIf cmbJenis.Text = "Keluar"
                    saldo = saldo - jumlah
                End If

                Dim simpan As String = "INSERT INTO `kas` (`id_kas`, `tgl`, `jenis`, `jml_saldo`, `saldo`, `Keterangan`) VALUES (NULL, '" & dtpTanggal.Text & "', '" & cmbJenis.Text & "', '" & txtJumlah.Text & "', '" & saldo & "', '" & txtKet.Text & "')"

                CMD = New OdbcCommand(simpan, CONN)
                CMD.ExecuteNonQuery()
                MsgBox("Input data berhasil")
                TampilGrid()
                KosongkanData()

                tutupkoneksi()
            End If
        End If
    End Sub
End Class