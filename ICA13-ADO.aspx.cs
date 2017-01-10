using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICA13_ADO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            NorthwindAccess.FillCustomersDDL(_DDL_Comapnies, "");

        
    }

    protected void _Bu_Filter_Click(object sender, EventArgs e)
    {
        NorthwindAccess.FillCustomersDDL(_DDL_Comapnies, _TB_Filter.Text);
    }
       
    protected void _DDL_Comapnies_Index(object sender, EventArgs e)
    {
        if (_DDL_Comapnies.SelectedIndex < 1)
            return;
        _GW_.DataSource = NorthwindAccess.CustomerCategorySummary(_DDL_Comapnies.SelectedValue);
        _GW_.DataBind();
        _GW_.HeaderStyle.BackColor = System.Drawing.Color.Black;
        _GW_.HeaderStyle.ForeColor = System.Drawing.Color.White;
        _GW_.HeaderStyle.Font.Size = 50;
    }

    protected void _GW__RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Fire for every row generated for view
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return; // can't get to data, bail out, nothing to do here
        DataRowView drv = e.Row.DataItem as DataRowView;
        
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

    }
}