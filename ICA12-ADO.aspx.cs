using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICA12_ADO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            FillDDL(_DDL_Comapnies, "");
        }
    }

    protected void FillDDL(DropDownList ddl, string sFilter)
    {
        ddl.AppendDataBoundItems = true;
        ddl.DataSource = NorthwindAccess.GetSuppliersSDS(sFilter); // binds the SDS
        ddl.DataTextField = "CompanyName";
        ddl.DataValueField = "SupplierID";
        ddl.Items.Clear(); // clear any existing items
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Now Pick a Company form [" + ddl.Items.Count + "]", ""));
        ddl.AutoPostBack = true;
    }


    protected void _DDL_Comapnies_Index(object sender, EventArgs e)
    {
        if (_DDL_Comapnies.SelectedIndex < 1)
            return;
        Table1.Rows.Clear();
        List<List<string>> Data = NorthwindAccess.GetProducts(_DDL_Comapnies.SelectedValue);
        TableHeaderRow THR = new TableHeaderRow();
        foreach(string s in Data[0])
        {
            TableHeaderCell THC = new TableHeaderCell();
            THC.Text = s;
            THC.Font.Size = 50;
            THR.BorderStyle = BorderStyle.Solid;
            THR.Cells.Add(THC);
        }
        THR.Height = 50;
        THR.BackColor = System.Drawing.Color.Lavender;
        Table1.Rows.Add(THR);
        for (int i = 1; i < Data.Count; i++)
        {
            TableRow TR = new TableRow();
            foreach(string s in Data[i])
            {
                TableCell TC = new TableCell();
                TC.Text = s;
                TC.BorderStyle = BorderStyle.Solid;
                TC.HorizontalAlign = HorizontalAlign.Center;
                TR.Cells.Add(TC);
            }
            Table1.Rows.Add(TR);
        }
    }

    protected void _Bu_Filter_Click(object sender, EventArgs e)
    {
        FillDDL(_DDL_Comapnies, _TB_Filter.Text);
    }
}