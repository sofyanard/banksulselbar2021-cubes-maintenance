using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for SelectField.
	/// </summary>
	public partial class SelectField : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_TABLE.Items.Add(new ListItem("--Select--",""));
				DDL_FIELD.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select ID, UID, Name from sysobjects where sysobjects.xtype= 'U' order by Name";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_TABLE.Items.Add(new ListItem(conncc.GetFieldValue(i,2),conncc.GetFieldValue(i,0)));
				}
			}
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void DDL_TABLE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string table = DDL_TABLE.SelectedValue;
			DDL_FIELD.Items.Clear();
			DDL_FIELD.Items.Add(new ListItem("-- select --", ""));
			conncc.QueryString = "select syscolumns.ID, syscolumns.ColID, syscolumns.NAME "+
				"from sysobjects join syscolumns on sysobjects.id=syscolumns.id "+
				"where sysobjects.ID ='"+ table +"'";
			conncc.ExecuteQuery();
			for (int i=0; i < conncc.GetRowCount() ; i++)
			{	
				DDL_FIELD.Items.Add(new ListItem(conncc.GetFieldValue(i,2),conncc.GetFieldValue(i,1)));
			}
		}

		protected void BTN_OK_Click(object sender, System.EventArgs e)
		{
			string tmpTable = DDL_TABLE.SelectedValue;
			conncc.QueryString="select Name from sysobjects where sysobjects.xtype= 'U' and ID='"+tmpTable+"'";
			conncc.ExecuteQuery();
			string tablenya = conncc.GetFieldValue("Name").ToString();

			string tmpField = DDL_FIELD.SelectedValue;
			conncc.QueryString = "select syscolumns.NAME "+
				"from sysobjects join syscolumns on sysobjects.id=syscolumns.id "+
				"where sysobjects.ID ='"+ tmpTable +"' and syscolumns.ColID='"+tmpField+"'";
			conncc.ExecuteQuery();
			string fieldnya = conncc.GetFieldValue("NAME").ToString();
			string fieldtable = tablenya+"."+fieldnya;
			//string koma=",";

			Response.Write("<script>opener.document.Form1.TXT_CALC_FORMULA.value=opener.document.Form1.TXT_CALC_FORMULA.value+'"+fieldtable+"'</script>");
			Response.Write("<script>opener.document.Form1.TXT_CALC_TABLE.value=opener.document.Form1.TXT_CALC_TABLE.value+'"+tablenya+"'</script>");
			Response.Write("<script language='javascript'>window.close()</script>");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close()</script>");
		}
	}
}
