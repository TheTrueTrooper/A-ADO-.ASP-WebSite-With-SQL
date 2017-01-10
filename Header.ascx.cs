using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[DefaultProperty("Text")]
[ToolboxData("<{0}:ServerControl1 runat=server/>")]
public partial class WebUserControl : System.Web.UI.UserControl
{


    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
        get
        {
           return (string)ViewState["Text"];
        }

        set
        {
            ViewState["Text"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        _Header.InnerText = Text;
    //    protected void Page_Load(object sender, EventArgs e)
    //{
    //    _FooterText.InnerText = "\u00a9 Copyright 2016 by Angelo - " + File.GetLastWriteTime(MapPath("") + this.Request.FilePath);
    //}
    }
}