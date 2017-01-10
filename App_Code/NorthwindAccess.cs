using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Class1
/// </summary>
public static class NorthwindAccess
{
    public static SqlDataSource GetSuppliersSDS(string Input)
    {

        string sQuery = "select SupplierID, CompanyName from Suppliers ";
        if(Input != null || Input != "")
        sQuery += " where CompanyName like '%" + Input + "%'";
        SqlDataSource sds = new SqlDataSource(
          ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString,
          sQuery);
        return sds;
    }

    public static List<List<string>> GetProducts(string Input)
    {
        List<List<string>> Return = new List<List<string>>();
        if (Input == null || Input == "")
            return Return;
        string sQuery = "select ProductName, QuantityPerUnit, UnitsInStock ";
        sQuery += " from Products ";
        sQuery += " where SupplierID = '" + Input + "'";
        sQuery += " Order by ProductName";
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(sQuery, conn))
            {
                List<string> Temp1 = new List<string>();
                conn.Open(); // open our connection
                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                for (int i = 0; i < reader.FieldCount; i++)
                    Temp1.Add(reader.GetName(i));
                Return.Add(Temp1);
                if (!reader.HasRows) return Return; // NO rows, explicit return
                while (reader.Read()) // true while a result row exists that is not consumed
                {
                    Temp1 = new List<string>();
                    int iCount = reader.FieldCount; // number of columns
                    string sColumnName = reader.GetName(0); // get name of column 0
                    Temp1.Add(reader["ProductName"].ToString());
                    Temp1.Add(reader["QuantityPerUnit"].ToString());
                    Temp1.Add(reader["UnitsInStock"].ToString());
                    Return.Add(Temp1);
                }
            }
        }

        return Return;
    }

    public static void FillCustomersDDL(DropDownList DLL, string Filter)
    {
        SqlDataReader reader = null;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString);
        conn.Open();
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = conn; // Set connection to 'talk' with
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetCustomers"; // SP name, not query here

            // Create and Populate parameters
            SqlParameter pFilter = new SqlParameter("@Filter", System.Data.SqlDbType.VarChar, 25);
            pFilter.Value = Filter; // set the VALUE
            pFilter.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pFilter); // ADD IT !!!!


            DLL.DataSource = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            DLL.DataTextField = "CompanyName";
            DLL.DataValueField = "CustomerID";
            DLL.Items.Clear(); // clear any existing items
            DLL.DataBind();
            DLL.Items.Insert(0, new ListItem("Please Pick a customer from [" + DLL.Items.Count + "]", ""));
            DLL.AutoPostBack = true;
        }

    }

    public static SqlDataReader CustomerCategorySummary(string ID)
    {
        SqlDataReader reader = null;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString);
        conn.Open();
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = conn; // Set connection to 'talk' with
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "CustCatSummary"; // SP name, not query here

            // Create and Populate parameters
            SqlParameter pID = new SqlParameter("@CustomerID", System.Data.SqlDbType.VarChar, 25);
            pID.Value = ID; // set the VALUE
            pID.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pID); // ADD IT !!!!


            reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        return reader;
    }

    public static string DeleteOrderDetails(int OID, int PID)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString);
        conn.Open();
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = conn; // Set connection to 'talk' with
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "DeleteOrderDetails"; // SP name, not query here

            // Create and Populate parameters
            SqlParameter pOID = new SqlParameter("@OrderID", System.Data.SqlDbType.VarChar, 25);
            pOID.Value = OID; // set the VALUE
            pOID.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pOID); // ADD IT !!!!

            SqlParameter pPID = new SqlParameter("@ProductID", System.Data.SqlDbType.VarChar, 25);
            pPID.Value = PID; // set the VALUE
            pPID.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pPID); // ADD IT !!!!

            SqlParameter pReturn = new SqlParameter("@status", System.Data.SqlDbType.VarChar, 25);
            pReturn.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add(pReturn); // ADD IT !!!!
            command.ExecuteNonQuery();
            return pReturn.Value.ToString();
        }
    }

    public static string InsertOrderDetails(int OrderID, int ProductID, short Quantity)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AngeloSanches_NorthwindConnectionString"].ConnectionString);
        conn.Open();
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = conn; // Set connection to 'talk' with
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertOrderDetails"; // SP name, not query here

            // Create and Populate parameters
            SqlParameter pOID = new SqlParameter("@OrderID", System.Data.SqlDbType.VarChar, 25);
            pOID.Value = OrderID; // set the VALUE
            pOID.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pOID); // ADD IT !!!!

            SqlParameter pPID = new SqlParameter("@ProductID", System.Data.SqlDbType.VarChar, 25);
            pPID.Value = ProductID; // set the VALUE
            pPID.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pPID); // ADD IT !!!!

            SqlParameter pQu = new SqlParameter("@Quantity", System.Data.SqlDbType.VarChar, 25);
            pQu.Value = Quantity;
            pQu.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(pQu); // ADD IT !!!!

            SqlParameter pR = new SqlParameter("@status", System.Data.SqlDbType.VarChar, 25);
            pR.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add(pR); // ADD IT !!!!

            command.ExecuteNonQuery();
            if (!(pR.Value is string))
                return "Inserted : return_value rows " + 0;
            return "Inserted : return_value rows " + pR.Value;
        }
    }


}