<!-- default file list -->
*Files to look at*:

* [XtraFormEx.cs](./CS/DXApplication5/XtraFormEx.cs) (VB: [XtraFormEx.vb](./VB/DXApplication5/XtraFormEx.vb))
<!-- default file list end -->
# How to show a SuperToolTip for XtraForm's buttons 

By default, **XtraForm** being a standard Form descendant shows standard tooltips. To show DevExpress [SuperToolTips](https://docs.devexpress.com/WindowsForms/DevExpress.Utils.SuperToolTip), implement the **IToolTipControlClient** interface for a form and create a custom **FormPainter** to override its **WMNCHitTest** and **DoWndProc** methods. 
