using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlesDinamicosVS
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        DataSet ds = new DataSet();
        int posicion = 0;

        public Form1()
        {
            cn = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=true");
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboBaseDatos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT dbid, name FROM sys.sysdatabases", cn);
            da.Fill(ds, "db");
            cboBaseDatos.DataSource = ds.Tables["db"];
            cboBaseDatos.DisplayMember = "name";
            cboBaseDatos.ValueMember = "dbid";

            SqlDataAdapter daTabla = new SqlDataAdapter("SELECT object_id, name FROM " + cboBaseDatos.Text + ".sys.tables", cn);
            daTabla.Fill(ds, "tabla");
            cboTablas.DataSource = ds.Tables["tabla"];
            cboTablas.DisplayMember = "name";
            cboTablas.ValueMember = "object_id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboTablas_MouseClick(object sender, MouseEventArgs e)
        {
            ds.Tables["tabla"].Clear();
            cboTablas.DataSource = null;
            cboTablas.Items.Clear();
            SqlDataAdapter daTabla = new SqlDataAdapter("SELECT object_id, name FROM " + cboBaseDatos.Text + ".sys.tables", cn);
            daTabla.Fill(ds, "tabla");
            cboTablas.DataSource = ds.Tables["tabla"];
            cboTablas.DisplayMember = "name";
            cboTablas.ValueMember = "object_id";
        }

        private void btnGenerador_Click(object sender, EventArgs e)
        {
            MyControls.Controls.Clear();
            SqlDataAdapter daCampos = new SqlDataAdapter("SELECT T.name as dato, C.object_id as id, C.name as columna FROM " + cboBaseDatos.Text + ".sys.all_columns C join sys.systypes T ON C.user_type_id = T.xusertype WHERE object_id = " + cboTablas.SelectedValue, cn);
            if (ds.Tables.Contains("campos"))
            {
                ds.Tables["campos"].Clear();
            }
            daCampos.Fill(ds, "campos");
            
            int j = 0;
            foreach(DataRow rowCampos in ds.Tables["campos"].Rows)
            {
                Label l = new Label();
                l.Name = "L" + rowCampos["columna"].ToString().Trim();
                l.Text = rowCampos["columna"].ToString().Trim();
                l.Location = new Point(50, j);
                MyControls.Controls.Add(l);

                switch (rowCampos["dato".ToString().Trim()])
                {
                    case "int":
                        NumericUpDown nud = new NumericUpDown();
                        nud.Name = "T" + rowCampos["columna"].ToString().Trim();
                        nud.Location = new Point(200, j);
                        MyControls.Controls.Add(nud);
                        break;
                    case "bit":
                        CheckBox cb = new CheckBox();
                        cb.Name = "T" + rowCampos["columna"].ToString().Trim();
                        cb.Location = new Point(200, j);
                        MyControls.Controls.Add(cb);
                        break;
                    case "datetime":
                        DateTimePicker dtp = new DateTimePicker();
                        dtp.Name = "T" + rowCampos["columna"].ToString().Trim();
                        dtp.Location = new Point(200, j);
                        MyControls.Controls.Add(dtp);
                        break;
                    case "date":
                        DateTimePicker dt = new DateTimePicker();
                        dt.Name = "T" + rowCampos["columna"].ToString().Trim();
                        dt.Location = new Point(200, j);
                        MyControls.Controls.Add(dt);
                        break;
                    default:
                        TextBox t = new TextBox();
                        t.Name = "T" + rowCampos["columna"].ToString().Trim();
                        t.Location = new Point(200, j);
                        MyControls.Controls.Add(t);
                        break;
                }

                j += 40;
            }

            SqlDataAdapter daRegistros = new SqlDataAdapter("SELECT * FROM " + cboBaseDatos.Text + ".dbo." + cboTablas.Text, cn);
            if (ds.Tables.Contains("registros"))
            {
                ds.Tables["registros"].Clear();
            }
            daRegistros.Fill(ds, "registros");
            posicion = 0;
            asignarValores(posicion);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            posicion = 0;
            asignarValores(posicion);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion -= 1;
                asignarValores(posicion);
            }
            else
            {
                MessageBox.Show("No hay registros anteriores");
            }
        }

        private void asignarValores(int posicion)
        {
            Control.ControlCollection coll = MyControls.Controls;
            DataRow rowRegistro = ds.Tables["registros"].Rows[posicion];
            for (int i = 0; i <= (ds.Tables["campos"].Rows.Count - 1); i++)
            {
                DataRow rowCampo = ds.Tables["campos"].Rows[i];
                switch (rowCampo["dato".ToString().Trim()])
                {
                    case "bit":
                        ((CheckBox)coll["T" + rowCampo["columna"].ToString().Trim()]).Checked = Convert.ToBoolean(rowRegistro[rowCampo["columna"].ToString().Trim()]);

                        CheckBox cbTemp = new CheckBox();
                        cbTemp.Checked = Convert.ToBoolean(rowRegistro[rowCampo["columna"].ToString().Trim()]);
                        cbTemp.Name = coll["T" + rowCampo["columna"].ToString().Trim()].Name;
                        cbTemp.Location = coll["T" + rowCampo["columna"].ToString().Trim()].Location;

                        MyControls.Controls.Remove(coll["T" + rowCampo["columna"]]);
                        MyControls.Controls.Add(cbTemp);
                        break;
                    case "int":
                        coll["T" + rowCampo["columna"].ToString().Trim()].Text = rowRegistro[rowCampo["columna"].ToString().Trim()].ToString();
                        break;
                    default:
                        coll["T" + rowCampo["columna"].ToString().Trim()].Text = rowRegistro[rowCampo["columna"].ToString().Trim()].ToString().Trim();
                        break;
                }
                    //TODO: terminar de configurar el checkbox para mostrar la informacion. posible respuesta , convertirlo
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (posicion < (ds.Tables["registros"].Rows.Count - 1))
            {
                posicion += 1;
                asignarValores(posicion);
            }
            else
            {
                MessageBox.Show("No hay registros posteriores");
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            posicion = ds.Tables["registros"].Rows.Count - 1;
            asignarValores(posicion);
        }
    }
}