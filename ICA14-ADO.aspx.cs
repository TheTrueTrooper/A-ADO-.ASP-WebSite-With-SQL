using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICA14_ADO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void _Bu_Delete_Click(object sender, EventArgs e)
    {
        if (_GW_.SelectedIndex == -1)
        {
            _La_Status.Text = "Status : Failed No index was selected";
            return;
        }
        int OID = int.Parse(_GW_.SelectedDataKey.Values["OrderID"].ToString());
        int PID = int.Parse(_GW_.SelectedDataKey.Values["ProductID"].ToString());


        _La_Status.Text = "Status : " + NorthwindAccess.DeleteOrderDetails(OID, PID);
        _GW_.DataBind();
        _GW_.SelectedIndex = -1;
    }

    protected void _Bu_Insert_Click(object sender, EventArgs e)
    {
        int OID;
        short Qty;
        if (!(int.TryParse(_TB_OrderID.Text, out OID) && short.TryParse(_TB_Qty.Text, out Qty)))
            return;
        _La_Status2.Text = NorthwindAccess.InsertOrderDetails(OID, int.Parse(_DDL_Pro.SelectedValue), Qty);
        _GW_.DataBind();
    }
}