Imports DevExpress.Skins
Imports DevExpress.Skins.XtraForm
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing.Helpers
Imports DevExpress.XtraBars.Localization
Imports DevExpress.XtraEditors
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace DXSample
	Public Class XtraFormEx
		Inherits XtraForm
		Implements IToolTipControlClient

		Public Sub New()
			ToolTipController.DefaultController.AddClientControl(Me)
		End Sub
		Protected Overrides Function CreateFormBorderPainter() As FormPainter
			Return New FormPainterEx(Me, LookAndFeel)
		End Function
		Private ReadOnly Property IToolTipControlClient_ShowToolTips() As Boolean Implements IToolTipControlClient.ShowToolTips
			Get
				Return True
			End Get
		End Property
		Friend Shadows ReadOnly Property FormPainter() As FormPainterEx
			Get
				Return TryCast(MyBase.FormPainter, FormPainterEx)
			End Get
		End Property
		Private Function IToolTipControlClient_GetObjectInfo(ByVal point As Point) As ToolTipControlInfo Implements IToolTipControlClient.GetObjectInfo
			Dim pt = PointToScreen(point)
			Dim btn As FormCaptionButton = FormPainter?.GetButton(pt)
			If btn IsNot Nothing Then
				Dim info = New ToolTipControlInfo()
				info.Object = Me
				info.ToolTipType = ToolTipType.SuperTip
				info.SuperTip = CreateFormButtonSuperTip(btn.Kind)
				Return info
			End If
			Return Nothing
		End Function
		Private Function CreateFormButtonSuperTip(ByVal buttonKind As FormCaptionButtonKind) As SuperToolTip
			Dim stip As New SuperToolTip()
			Dim item As New ToolTipItem()
			Select Case buttonKind
				Case FormCaptionButtonKind.Minimize
					item.Text = BarLocalizer.Active.GetLocalizedString(BarString.MinimizeButton)
				Case FormCaptionButtonKind.Maximize
					If WindowState = FormWindowState.Maximized Then
						item.Text = BarLocalizer.Active.GetLocalizedString(BarString.RestoreButton)
					Else
						item.Text = BarLocalizer.Active.GetLocalizedString(BarString.MaximizeButton)
					End If
				Case FormCaptionButtonKind.Close
					item.Text = BarLocalizer.Active.GetLocalizedString(BarString.CloseButton)
				Case FormCaptionButtonKind.Help
					item.Text = BarLocalizer.Active.GetLocalizedString(BarString.HelpButton)
			End Select
			stip.Items.Add(item)
			Return stip
		End Function
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				ToolTipController.DefaultController.RemoveClientControl(Me)
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
	Public Class FormPainterEx
		Inherits FormPainter

		Public Sub New(ByVal owner As Control, ByVal provider As ISkinProvider)
			MyBase.New(owner, provider)
		End Sub
		Protected Overrides Sub WMNCHitTest(ByRef msg As Message)
			MyBase.WMNCHitTest(msg)
			Dim pt As Point = PointToFormBounds(msg.LParam)
			If Buttons.GetButton(pt) IsNot Nothing Then
				msg.Result = New IntPtr(NativeMethods.HT.HTCLIENT)
			End If
		End Sub
		Public Overrides Function DoWndProc(ByRef msg As Message) As Boolean
            If msg.Msg = DevExpress.Utils.Drawing.Helpers.MSG.WM_MOUSELEAVE Then
                If IsAllowButtonMessages Then
                    OnMouseLeave()
                End If
            End If
            Return MyBase.DoWndProc(msg)
		End Function
		Friend Function GetButton(ByVal point As Point) As FormCaptionButton
			Dim pt As Point = PointToFormBounds(point)
			Return Buttons.GetButton(pt)
		End Function
	End Class
End Namespace
