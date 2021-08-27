<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/221236757/19.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T832075)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [XtraFormEx.cs](./CS/DXApplication5/XtraFormEx.cs) (VB: [XtraFormEx.vb](./VB/DXApplication5/XtraFormEx.vb))
<!-- default file list end -->
# How to show a SuperToolTip for XtraForm's buttons 

By default, **XtraForm** being a standard Form descendant shows standard tooltips. To show DevExpress [SuperToolTips](https://docs.devexpress.com/WindowsForms/DevExpress.Utils.SuperToolTip), implement the **IToolTipControlClient** interface for a form and create a custom **FormPainter** to override its **WMNCHitTest** and **DoWndProc** methods. 
