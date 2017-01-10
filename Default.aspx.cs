using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> WebSites = Directory.GetFiles(MapPath("")).Where(x => x.EndsWith(".aspx")).ToList();
        Table T = new Table();
        T.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
        T.Style.Add(HtmlTextWriterStyle.Width, "100%");
        foreach (string s in WebSites)
        {
            TableRow TR = new TableRow();
            TableCell TC = new TableCell();
            string x = s.Replace(MapPath("") + "\\", "");
            HyperLink L = new HyperLink();
            L.NavigateUrl = x;
            L.Text = x;
            TC.Controls.Add(L);
            TR.Cells.Add(TC);
            T.Rows.Add(TR);
        }
        _Links.Controls.Add(T);
    }
}