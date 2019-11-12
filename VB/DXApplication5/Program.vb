Imports DevExpress.UserSkins
Imports System
Imports System.Linq
Imports System.Windows.Forms

Namespace DXSample
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
			BonusSkins.Register()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Main())
		End Sub
	End Module
End Namespace