using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace TpProg
{
    public partial class Form1 : Form
    {
        AccesoDatos ad = new AccesoDatos("Data Source=MARCOS;Initial Catalog=bd_utn_programacion;Integrated Security=True");
        List<Docente> ld = new List<Docente>();
        // List<Personas> pe = new List<Personas>();
        int accion = 1;
        bool nuevo = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarCombo(cmbTipo, "tipos_documentos");
            cargarCombo(cmbEstcivil, "estados_civiles");
            cargarCombo(cmbBarrios, "barrios");
            cargarLista();

            habilitarBotones(true);
           





        }

        private void cargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable dt = new DataTable();
            dt = ad.consultarTabla(nombreTabla);
            combo.DataSource = dt;
            combo.ValueMember = dt.Columns[0].ColumnName;
            combo.DisplayMember = dt.Columns[1].ColumnName;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cargarLista()
        {
            ld.Clear();
            ad.leerTablaJoin("SELECT D.legajo,P.nombre,P.apellido,P.id_tipo_doc,P.num_documento,P.fecha_nacimiento,P.email,P.telefono,P.calle,P.num_calle,P.id_barrio,P.id_estado_civil,P.id_genero,D.fecha_alta,P.id_persona FROM PERSONAS P JOIN DOCENTES D ON P.id_persona = D.id_persona");

            while (ad.pDr.Read())
            {



                Docente d = new Docente();


                if (!ad.pDr.IsDBNull(0))
                    d.pLegajo = ad.pDr.GetInt32(0);
                if (!ad.pDr.IsDBNull(1))
                    d.pnombre = ad.pDr.GetString(1);
                if (!ad.pDr.IsDBNull(2))
                    d.papellido = ad.pDr.GetString(2);
                if (!ad.pDr.IsDBNull(3))
                    d.pid_tipo_doc = ad.pDr.GetInt32(3);
                if (!ad.pDr.IsDBNull(4))
                    d.pnum_documento = ad.pDr.GetString(4);
                if (!ad.pDr.IsDBNull(5))
                    d.pfecha_nacimiento = ad.pDr.GetDateTime(5);
                if (!ad.pDr.IsDBNull(6))
                    d.pemail = ad.pDr.GetString(6);
                if (!ad.pDr.IsDBNull(7))
                    d.ptelefono = ad.pDr.GetString(7);
                if (!ad.pDr.IsDBNull(8))
                    d.pcalle = ad.pDr.GetString(8);
                if (!ad.pDr.IsDBNull(9))
                    d.pnum_calle = ad.pDr.GetString(9);
                if (!ad.pDr.IsDBNull(10))
                    d.pid_barrio = ad.pDr.GetInt32(10);
                if (!ad.pDr.IsDBNull(11))
                    d.pid_estado_civil = ad.pDr.GetInt32(11);
                if (!ad.pDr.IsDBNull(12))
                    d.pid_genero = ad.pDr.GetInt32(12);
                if (!ad.pDr.IsDBNull(13))
                    d.pfecha_alta = ad.pDr.GetDateTime(13);
                if (!ad.pDr.IsDBNull(14))
                    d.pid_persona = ad.pDr.GetInt32(14);

                ld.Add(d);
            }

            ad.pDr.Close();
            ad.desconectar();

            lstdocente.Items.Clear();

            lstdocente.Items.AddRange(ld.ToArray());

        }

        private void cargarCampo(int posicion)
        {
            Docente selected = ld[posicion];

            txtLegajo.Text = selected.pLegajo.ToString();

            dtpfechaalta.Value = selected.pfecha_alta;

            txtNombre.Text = selected.pnombre;
            txtApellido.Text = selected.papellido;
            cmbTipo.SelectedValue = selected.pid_tipo_doc;
            txtNumdoc.Text = selected.pnum_documento.ToString();
            dtpfechanac.Value = selected.pfecha_nacimiento;
            txtEmail.Text = selected.pemail.ToString();
            txtTelefono.Text = selected.ptelefono.ToString();
            txtCalle.Text = selected.pcalle.ToString();
            txtNumero.Text = selected.pnum_calle.ToString();

            cmbBarrios.SelectedValue = selected.pid_barrio;

            cmbEstcivil.SelectedValue = selected.pid_estado_civil;

            if (selected.pid_genero == 1)
                rbtMasculino.Checked = true;
            else
                rbtFemenino.Checked = true;

        }

        bool existe(string pk)
        {
            for (int i = 0; i < lstdocente.Items.Count; i++)
                if (ld[i].pnum_documento == pk)
                    return true;
            return false;
        }

        private void limpiarCampos()
        {

            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            cmbTipo.SelectedIndex = -1;
            txtNumdoc.Text = "";
            dtpfechanac.Value = DateTime.Today;
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCalle.Text = "";
            txtNumero.Text = "";
            cmbBarrios.SelectedIndex = -1;
            cmbEstcivil.SelectedIndex = -1;
            rbtMasculino.Checked = false;
            rbtFemenino.Checked = false;
            dtpfechaalta.Value = DateTime.Today;

        }

        private void Lstdocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCampo(lstdocente.SelectedIndex);
            btnEditar.Enabled= true;
            btnEliminar.Enabled = true;
        }

        private void habilitarBotones(bool value)
        {

            btnCancelar.Enabled = !value;
            btnCargar.Enabled = !value;
            btnEditar.Enabled = value;
            btnEliminar.Enabled = value;
            btnNuevo.Enabled = value;
            btnSalir.Enabled = value;

        }



        private void BtnCargar_Click(object sender, EventArgs e)
        {
            string sql = null;
            Docente d = new Docente();

            d.pnombre = txtNombre.Text;
            d.papellido = txtApellido.Text;
            d.pid_tipo_doc = Convert.ToInt32(cmbTipo.SelectedValue);
            d.pnum_documento = txtNumdoc.Text;
            d.pfecha_nacimiento = dtpfechanac.Value;
            d.pemail = txtEmail.Text;
            d.ptelefono = txtTelefono.Text;
            d.pcalle = txtCalle.Text;
            d.pnum_calle = txtNumero.Text;
            d.pid_barrio = Convert.ToInt32(cmbBarrios.SelectedValue);
            d.pid_estado_civil = Convert.ToInt32(cmbEstcivil.SelectedValue);

            if (rbtMasculino.Checked)
                d.pid_genero = 1;
            else
                d.pid_genero = 2;
            d.pfecha_alta = dtpfechaalta.Value;

            if (validar())
            {


                if (nuevo)
                    if (!existe(d.pnum_documento))
                    {
                        sql = "insert into personas (nombre,apellido,id_tipo_doc,num_documento,fecha_nacimiento,email,telefono,calle,num_calle,id_barrio,id_estado_civil,id_genero) values ('"
                            + d.pnombre + "','"
                            + d.papellido + "',"
                            + d.pid_tipo_doc + ",'"
                            + d.pnum_documento + "','"
                            + d.pfecha_nacimiento + "','"
                            + d.pemail + "','"
                            + d.ptelefono + "','"
                            + d.pcalle + "','"
                            + d.pnum_calle + "',"
                            + d.pid_barrio + ","
                            + d.pid_estado_civil + ","
                            + d.pid_genero + ")  insert into docentes values( (select MAX(id_persona) from personas),'" + d.pfecha_alta + "')";

                        ad.actualizarTabla(sql);

                        cargarLista();
                    }
                    else
                        MessageBox.Show("El docente ya se encuentra registrado.");
                else
                {

                    if (MessageBox.Show("Seguro que desea actualizar el docente", "aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = lstdocente.SelectedIndex;



                        //VP[i].pApellido = txtApellido.Text;
                        //VP[i].pNombres = txtNombres.Text;
                        //VP[i].pEstadoCivil = (int)cboEstadoCivil.SelectedValue;
                        //if (rbtFemenino.Checked)
                        //    VP[i].pSexo = 1;
                        //else
                        //    VP[i].pSexo = 2;
                        //VP[i].pFallecio = chkFallecio.Checked;
                        Docente selected = ld[lstdocente.SelectedIndex];
                        sql = "Update personas set nombre='" + d.pnombre + "',"
                                                       + "apellido='" + d.papellido + "',"
                                                       + "id_tipo_doc=" + d.pid_tipo_doc + ","
                                                       + "num_documento='" + d.pnum_documento + "',"
                                                       + "fecha_nacimiento='" + d.pfecha_nacimiento + "',"
                                                       + "email='" + d.pemail + "',"
                                                       + "telefono='" + d.ptelefono + "',"
                                                       + "calle='" + d.pcalle + "',"
                                                       + "num_calle='" + d.pnum_calle + "',"
                                                       + "id_barrio=" + d.pid_barrio + ","
                                                       + "id_estado_civil=" + d.pid_estado_civil + ","
                                                       + "id_genero=" + d.pid_genero
                                                       + "Where id_persona=" + selected.pid_persona + " Update docentes set fecha_alta='" + d.pfecha_alta + "' WHERE id_persona= " + selected.pid_persona;
                    }
                    //sql=sql = "Update personas set nombre='" + d.pnombre + "',"
                    //                        + "apellido='" + d.papellido + "',"
                    //                        + "id_tipo_doc=" + d.pid_tipo_doc + ","
                    //                        + "num_documento='" + d.pnum_documento + "',"
                    //                        + "fecha_nacimiento='" + d.pfecha_nacimiento + "',"
                    //                        + "email='" + d.pemail + "',"
                    //                        + "telefono='" + d.ptelefono + "',"
                    //                        + "calle='" + d.pcalle + "',"
                    //                        + "num_calle='" + d.pnum_calle + "',"
                    //                        + "id_barrio=" + d.pid_barrio + ","
                    //                        + "id_estado_civil=" + d.pid_estado_civil + ","
                    //                        + "id_genero=" + d.pid_genero + " "
                    //                        + "where id_persona=" + d.pid_persona + " Update docentes set fecha_alta='" + d.pfecha_alta + "' WHERE id_persona= " + d.pid_persona;

                    ad.actualizarTabla(sql);
                    cargarLista();
                    nuevo = false;
                    limpiarCampos();
                    

                }
            }
            habilitar(false);
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = true;
            btnCargar.Enabled = false;
            



        }


        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void BtnDocentes_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage;
            habilitar(false);
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void BtnAlumnos_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            Reporte report = new Reporte();
            report.Show();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            nuevo = true;
            habilitarBotones(false);
            habilitar(true);
            lstdocente.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Docente selected = ld[lstdocente.SelectedIndex];
            if (MessageBox.Show("Seguro que desea eliminar el alumno?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string q = "DELETE from docentes where id_persona = " + selected.pid_persona.ToString() + "DELETE from personas where id_persona = " + selected.pid_persona.ToString();
                ad.actualizarTabla(q);

                cargarLista();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            nuevo = false;
            habilitarBotones(false);
            habilitar(true);
            lstdocente.Enabled = true;
        }

        private void tabPage_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cancelar la carga?", "Cancelar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                limpiarCampos();
                habilitarBotones(true);
                habilitar(false);
                lstdocente.Enabled = true;
                btnEliminar.Enabled = false;
                btnEditar.Enabled = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea Salir?", "Salir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void panelSuperior_Paint(object sender, PaintEventArgs e)
        {

        }
        private void habilitar(bool x)
        {

            txtNombre.Enabled = x;
            txtApellido.Enabled = x;
            cmbTipo.Enabled = x;
            txtNumdoc.Enabled = x;
            dtpfechanac.Enabled = x;
            txtEmail.Enabled = x;
            txtTelefono.Enabled = x;
            txtCalle.Enabled = x;
            txtNumero.Enabled = x;
            cmbBarrios.Enabled = x;
            cmbEstcivil.Enabled = x;
            rbtMasculino.Enabled = x;
            rbtFemenino.Enabled = x;
            dtpfechaalta.Enabled = x;

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);



        private bool validar()
        {

            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe completar el Nombre...");
                txtNombre.Focus();
                return false;
            }

            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe completar el Apellido...");
                txtApellido.Focus();
                return false;
            }

            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de documente...");
                cmbTipo.Focus();
                return false;
            }

            if (txtNumdoc.Text == "")
            {
                MessageBox.Show("Debe completar el numero de documento...");
                txtNumdoc.Focus();
                return false;
            }

            if (dtpfechanac.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser posterior a hoy...");
                dtpfechanac.Focus();
                return false;
            }

            if (txtEmail.Text == "")
            {
                MessageBox.Show("Debe completar el mail...");
                txtEmail.Focus();
                return false;
            }

            if (txtTelefono.Text == "")
            {
                MessageBox.Show("Debe completar el numero de telefono...");
                txtTelefono.Focus();
                return false;
            }

           

            if (txtCalle.Text == "")
            {
                MessageBox.Show("Debe completar el nombre de la calle...");
                txtCalle.Focus();
                return false;
            }
            if (txtNumero.Text == "")
            {
                MessageBox.Show("Debe completar el numero de la calle...");
                txtNumero.Focus();
                return false;
            }
            

            if (cmbBarrios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un barrio...");
                cmbBarrios.Focus();
                return false;
            }
            if (cmbEstcivil.Text == "")
            {
                MessageBox.Show("Debe seleccionar el estado civil...");
                cmbEstcivil.Focus();
                return false;
            }


            if (!rbtMasculino.Checked && !rbtFemenino.Checked)
            {
                MessageBox.Show("Debe seleccionar un genero...");
                rbtMasculino.Focus();
                return false;
            }



            if (dtpfechaalta.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha de alta no puede ser posterior a hoy...");
                dtpfechaalta.Focus();
                return false;
            }
            return true;

        }

        private void panelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }

    
}
