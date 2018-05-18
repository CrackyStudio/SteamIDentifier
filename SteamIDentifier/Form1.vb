'_________                       __
'\_   ___ \____________    ____ |  | _____.__.
'/    \  \/\_  __ \__  \ _/ ___\|  |/ <   |  |
'\     \____|  | \// __ \\  \___|    < \___  |
' \______  /|__|  (____  /\___  >__|_ \/ ____|
'        \/            \/     \/     \/\/  
'This freeware was developed by Cracky
'http://steamcommunity.com/id/officialcracky/
'This code is free to use - If you use it, give credits
Imports System.Runtime.InteropServices
Public Class Form1
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendMessage(Me.TextBox1.Handle, &H1501, 0, "Put URL here")
        If My.Computer.Network.IsAvailable Then
        Else
            MessageBox.Show("Internet connection is not detected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Label1.Text = ""
        Me.Label2.Text = ""
        If TextBox1.Text.Length > 26 Then
            Dim sourceString As String = New System.Net.WebClient().DownloadString(TextBox1.Text)
            Dim filePath As String
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            My.Computer.FileSystem.WriteAllText(filePath & "SteamID.txt", sourceString, False)
            For Each k As String In IO.File.ReadLines(filePath & "SteamID.txt")
                If k.Contains("g_rgProfileData =") Then
                    Dim mystr As String = IO.File.ReadAllLines(filePath & "SteamID.txt").FirstOrDefault(Function(x) x.Contains("g_rgProfileData"))
                    Dim cut_at As String = """steamid"":"""
                    Dim cut_at2 As String = """,""personname"""
                    Dim xev As Integer = InStr(mystr, cut_at)
                    Dim string_before As String = mystr.Substring(0, xev - 2)
                    Dim string_after As String = mystr.Substring(xev + cut_at2.Length - 4)
                    Label1.Text = string_after.Substring(0, 17)
                End If
            Next
            Dim Valeur As Int64 = 0
            Dim Resultat As Double = 0
            If Not (String.IsNullOrEmpty(Me.Label1.Text)) Then
                If (Int64.TryParse(Me.Label1.Text, Valeur)) Then
                    Resultat = ((Valeur + 0) - 76561197960265728) / 2
                    If Valeur Mod 2 = 0 Then
                        Me.Label2.Text = "STEAM_0:0:" & Resultat.ToString
                    Else
                        Me.Label2.Text = "STEAM_0:1:" & Resultat.ToString(0) - 1
                    End If
                End If
            End If
            For Each l As String In IO.File.ReadLines(filePath & "SteamID.txt")
                If l.Contains("<div class=""playerAvatarAutoSizeInner""><img src=""") Then
                    Dim mystr As String = IO.File.ReadAllLines(filePath & "SteamID.txt").FirstOrDefault(Function(x) x.Contains("<div class=""playerAvatarAutoSizeInner""><img src="""))
                    Dim cut_at As String = "src="""
                    Dim cut_at2 As String = """,""personname"""
                    Dim xev As Integer = InStr(mystr, cut_at)
                    Dim string_after As String = mystr.Substring(xev + cut_at2.Length - 10)
                    PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(string_after.Substring(0, string_after.Length - 8))))
                End If
            Next
            For Each m As String In IO.File.ReadLines(filePath & "SteamID.txt")
                If m.Contains("<div class=""playerAvatar profile_header_size online") Then
                    Label6.BackColor = Color.LightSkyBlue
                ElseIf m.Contains("<div class=""playerAvatar profile_header_size in-game") Then
                    Label6.BackColor = Color.LimeGreen
                ElseIf m.Contains("<div class=""playerAvatar profile_header_size offline") Then
                    Label6.BackColor = Color.DarkGray
                End If
            Next
            For Each n As String In IO.File.ReadLines(filePath & "SteamID.txt")
                If n.Contains("<span class=""actual_persona_name"">") Then
                    Dim mystr As String = IO.File.ReadAllLines(filePath & "SteamID.txt").FirstOrDefault(Function(x) x.Contains("<span class=""actual_persona_name"">"))
                    Dim cut_at As String = "name"">"
                    Dim cut_at2 As String = """,""personname"""
                    Dim xev As Integer = InStr(mystr, cut_at)
                    Dim string_after As String = mystr.Substring(xev + cut_at2.Length - 10)
                    Label5.Text = string_after.Substring(1, string_after.Length - 8)
                End If
            Next
        Else
            Me.Label1.Text = ""
            Me.Label2.Text = ""
            MessageBox.Show("Invalid link!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim webAddress As String = "http://store.steampowered.com/"
        Process.Start(webAddress)
    End Sub
    Private Sub LabelMouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.MouseEnter
        Dim newFont As New Font(Label3.Font.Name, Label3.Font.Size, FontStyle.Underline)
        Label3.Font = newFont
    End Sub
    Private Sub LabelMouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.MouseLeave, Label2.MouseLeave, Label3.MouseLeave
        Dim newFont2 As New Font(Label3.Font.Name, Label3.Font.Size, FontStyle.Regular)
        Label3.Font = newFont2
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If Label1.Text <> "" Then
            Clipboard.SetText(Label1.Text)
        End If
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        If Label2.Text <> "" Then
            Clipboard.SetText(Label2.Text)
        End If
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        If Label5.Text <> "" Then
            Clipboard.SetText(Label5.Text)
        End If
    End Sub
    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        Me.TextBox1.Text = ""
    End Sub
    Private Sub Form1_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Donate link: paypal.me/officialcracky
    End Sub
End Class
'  _________ __
' /   _____//  |_  ____ _____    _____  
' \_____  \\   __\/ __ \\__  \  /     \ 
' /        \|  | \  ___/ / __ \|  Y Y  \
'/_______  /|__|  \___  >____  /__|_|  /
'        \/           \/     \/      \/      
'http://store.steampowered.com/
'Powered by Steam
'SteamIDentifier and Cracky are not affiliated with Valve. All trademarks and registered trademarks are the property of their respective owners. 