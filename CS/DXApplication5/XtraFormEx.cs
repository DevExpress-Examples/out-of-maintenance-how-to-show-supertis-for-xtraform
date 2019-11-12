using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DXSample {
    public class XtraFormEx : XtraForm, IToolTipControlClient {
        public XtraFormEx() {
            ToolTipController.DefaultController.AddClientControl(this);
        }
        protected override FormPainter CreateFormBorderPainter() {
            return new FormPainterEx(this, LookAndFeel);
        }
        bool IToolTipControlClient.ShowToolTips {
            get { return true; }
        }
        internal new FormPainterEx FormPainter {
            get { return base.FormPainter as FormPainterEx; }
        }
        ToolTipControlInfo IToolTipControlClient.GetObjectInfo(Point point) {
            var pt = PointToScreen(point);
            FormCaptionButton btn = FormPainter?.GetButton(pt);
            if(btn != null) {
                var info = new ToolTipControlInfo();
                info.Object = this;
                info.ToolTipType = ToolTipType.SuperTip; 
                info.SuperTip = CreateFormButtonSuperTip(btn.Kind);
                return info;
            }
            return null;
        }
        SuperToolTip CreateFormButtonSuperTip(FormCaptionButtonKind buttonKind) {
            SuperToolTip stip = new SuperToolTip();
            ToolTipItem item = new ToolTipItem();
            switch(buttonKind) {
                case FormCaptionButtonKind.Minimize:
                    item.Text = BarLocalizer.Active.GetLocalizedString(BarString.MinimizeButton);
                    break;
                case FormCaptionButtonKind.Maximize:
                    if(WindowState == FormWindowState.Maximized)
                        item.Text = BarLocalizer.Active.GetLocalizedString(BarString.RestoreButton);
                    else
                        item.Text = BarLocalizer.Active.GetLocalizedString(BarString.MaximizeButton);
                    break;
                case FormCaptionButtonKind.Close:
                    item.Text = BarLocalizer.Active.GetLocalizedString(BarString.CloseButton);
                    break;
                case FormCaptionButtonKind.Help:
                    item.Text = BarLocalizer.Active.GetLocalizedString(BarString.HelpButton);
                    break;
            }
            stip.Items.Add(item);
            return stip;
        }
        protected override void Dispose(bool disposing) {
            if(disposing)
                ToolTipController.DefaultController.RemoveClientControl(this);
            base.Dispose(disposing);
        }
    }
    public class FormPainterEx : FormPainter {
        public FormPainterEx(Control owner, ISkinProvider provider) : base(owner, provider) { }
        protected override void WMNCHitTest(ref Message msg) {
            base.WMNCHitTest(ref msg);
            Point pt = PointToFormBounds(msg.LParam);
            if(Buttons.GetButton(pt) != null)
                msg.Result = new IntPtr(NativeMethods.HT.HTCLIENT);
        }
        public override bool DoWndProc(ref Message msg) {
            if(msg.Msg == MSG.WM_MOUSELEAVE) {
                if(IsAllowButtonMessages)
                    OnMouseLeave();
            }
            return base.DoWndProc(ref msg);
        }
        internal FormCaptionButton GetButton(Point point) {
            Point pt = PointToFormBounds(point);
            return Buttons.GetButton(pt);
        }
    }
}
